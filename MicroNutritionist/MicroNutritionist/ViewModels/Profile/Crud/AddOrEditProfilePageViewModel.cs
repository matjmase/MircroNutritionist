using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MicroNutritionist.Common.Extensions;
using MicroNutritionist.PubSubMessages;
using MicroNutritionist.ViewModels.Nutrition.Crud;
using MicroNutritionist.ViewModels.Nutrition.Models;
using MicroNutritionist.ViewModels.Nutrition.Search;
using MicroNutritionist.ViewModels.Profile.Models;
using MicroNutritionist.Views.Nutrition.Crud;
using MicroNutritionist.Views.Nutrition.Search;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroNutritionist.ViewModels.Profile.Crud
{
    [QueryProperty(nameof(InjectedProfile), nameof(AddOrEditProfilePageViewModel))]
    public partial class AddOrEditProfilePageViewModel : ObservableObject
    {
        private MicroNutritionist.Common.Database.Models.Profile _injectedProfile;

        public MicroNutritionist.Common.Database.Models.Profile InjectedProfile
        {
            get { return _injectedProfile; }
            set
            {
                _injectedProfile = value;
                MainProfile = new ProfileViewModel(value);

                OriginalNutrients.Clear();
                var blackList = _originalToRemove.Select(e => e.InnerAmount.Id).ToHashSet();
                var nutrientAmounts = MauiProgram.Database.GetAllProfileNutritionAmountByProfile(MainProfile.Inner.Id).WaitForResult().Where(e => !blackList.Contains(e.Id));
                foreach (var nutrientAmount in nutrientAmounts)
                {
                    var nutrient = MauiProgram.Database.GetNutrition(nutrientAmount.NutritionId).WaitForResult();
                    OriginalNutrients.Add(new ProfileNutritionAmountViewModel(nutrient, nutrientAmount));
                }

                //reload new
                var newNutriCol = NewNutrients.ToList();
                NewNutrients.Clear();

                foreach (var newNutri in newNutriCol)
                {
                    var nutrient = MauiProgram.Database.GetNutrition(newNutri.InnerNutrition.Id).WaitForResult();
                    NewNutrients.Add(new ProfileNutritionAmountViewModel(nutrient, newNutri.InnerAmount));
                }
            }
        }

        [ObservableProperty]
        private ProfileViewModel mainProfile;

        [ObservableProperty]
        private ObservableCollection<ProfileNutritionAmountViewModel> newNutrients = new ObservableCollection<ProfileNutritionAmountViewModel>();

        [ObservableProperty]
        private ObservableCollection<ProfileNutritionAmountViewModel> originalNutrients = new ObservableCollection<ProfileNutritionAmountViewModel>();
        private List<ProfileNutritionAmountViewModel> _originalToRemove = new List<ProfileNutritionAmountViewModel>();

        public AddOrEditProfilePageViewModel()
        {
            WeakReferenceMessenger.Default.Register<AddOrEditProfilePageViewModel, NutritionSelectedMessage>(this, (recipient, nutrientMessage) =>
            {
                NewNutrients.Add(new ProfileNutritionAmountViewModel(nutrientMessage.Nutrition, new Common.Database.Models.ProfileNutritionAmount() { NutritionId = nutrientMessage.Nutrition.Id }));
            });
        }

        [RelayCommand]
        private async Task NutrientDetails(ProfileNutritionAmountViewModel nutrition)
        {
            await Shell.Current.GoToAsync(nameof(AddOrEditNutritionPage), true, new Dictionary<string, object> { { nameof(AddOrEditNutritionPageViewModel), nutrition.InnerNutrition } });
        }

        [RelayCommand]
        private void NutrientOriginalRemove(ProfileNutritionAmountViewModel nutrition)
        {

            OriginalNutrients.Remove(nutrition);
            _originalToRemove.Add(nutrition);
        }

        [RelayCommand]
        private async Task NutrientNewlRemove(ProfileNutritionAmountViewModel nutrition)
        {
            NewNutrients.Remove(nutrition);
        }

        [RelayCommand]
        private async Task AddNutrient()
        {
            await Shell.Current.GoToAsync(nameof(NutritionSearchPage), true, new Dictionary<string, object> { { nameof(NutritionSearchPageViewModel), OriginalNutrients.Union(NewNutrients).Select(e => e.InnerNutrition.Id).ToHashSet() } });
        }

        [RelayCommand]
        private async Task SaveChanges()
        {
            if (MainProfile.Inner.Id == 0)
                await MauiProgram.Database.InsertProfile(MainProfile.Inner);
            else
                await MauiProgram.Database.UpdateProfile(MainProfile.Inner);

            MainProfile = new ProfileViewModel(await MauiProgram.Database.GetProfileByName(MainProfile.Name));

            foreach (var item in _originalToRemove)
            {
                await MauiProgram.Database.DeleteProfileNutritionAmount(item.InnerAmount);
            }

            foreach (var item in OriginalNutrients)
            {
                await MauiProgram.Database.UpdateProfileNutritionAmount(item.InnerAmount);
            }

            foreach (var item in NewNutrients.Select(e => e.InnerAmount))
            {
                item.ProfileId = MainProfile.Inner.Id;
                await MauiProgram.Database.InsertProfileNutritionAmount(item);
            }
            await Shell.Current.GoToAsync("..");
        }
    }
}
