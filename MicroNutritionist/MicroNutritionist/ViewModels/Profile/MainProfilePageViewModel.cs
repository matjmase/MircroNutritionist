using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MicroNutritionist.Common.Database.Models;
using MicroNutritionist.Common.Extensions;
using MicroNutritionist.Common.Services;
using MicroNutritionist.PubSubMessages.Search;
using MicroNutritionist.ViewModels.Profile.Crud;
using MicroNutritionist.ViewModels.Search;
using MicroNutritionist.ViewModels.Search.Model;
using MicroNutritionist.Views.Profile;
using MicroNutritionist.Views.Profile.Crud;
using MicroNutritionist.Views.Search;

namespace MicroNutritionist.ViewModels.Profile
{
    public partial class MainProfilePageViewModel : ObservableObject
    {
        private IAlertService _alertService;

        public MainProfilePageViewModel(IAlertService alertService)
        {
            _alertService = alertService;

            
        }

        [RelayCommand]
        private async Task Loaded()
        {
            WeakReferenceMessenger.Default.Register<MainProfilePageViewModel, EditObjectMessage>(this, async (receiver, editProfileMessage) =>
            {
                await Shell.Current.GoToAsync(nameof(AddOrEditProfilePage), true, new Dictionary<string, object> { { nameof(AddOrEditProfilePageViewModel), (MicroNutritionist.Common.Database.Models.Profile)editProfileMessage.Payload } });
            });

            WeakReferenceMessenger.Default.Register<MainProfilePageViewModel, DeleteObjectMessage>(this, async (receiver, deleteProfileMessage) =>
            {
                var answer = await _alertService.ShowConfirmationAsync("Confirmation", "Are you sure you want to delete this profile?");

                if (answer)
                {
                    await MauiProgram.Database.DeleteProfileCascade((MicroNutritionist.Common.Database.Models.Profile)deleteProfileMessage.Payload);
                }
            });
        }

        [RelayCommand]
        private async Task UnLoaded()
        {
            WeakReferenceMessenger.Default.UnregisterAll(this);
        }

        [RelayCommand]
        public async Task AddNewProfile()
        {
            await Shell.Current.GoToAsync(nameof(AddOrEditProfilePage), true, new Dictionary<string, object> { { nameof(AddOrEditProfilePageViewModel), new MicroNutritionist.Common.Database.Models.Profile() } });
        }

        [RelayCommand]
        public async Task EditProfile()
        {
            await Shell.Current.GoToAsync(nameof(StringSearchPage), true, new Dictionary<string, object> { { nameof(StringSearchPageViewModel), new StringSearchPageModel(MauiProgram.Database.GetAllProfiles().WaitForResult().ToArray(), (obj) => ((MicroNutritionist.Common.Database.Models.Profile)obj).Name, StringSearchPageModelType.Edit)} });
        }

        [RelayCommand]
        public async Task DeleteProfile()
        {
            await Shell.Current.GoToAsync(nameof(StringSearchPage), true, new Dictionary<string, object> { { nameof(StringSearchPageViewModel), new StringSearchPageModel(MauiProgram.Database.GetAllProfiles().WaitForResult().ToArray(), (obj) => ((MicroNutritionist.Common.Database.Models.Profile)obj).Name, StringSearchPageModelType.Delete) } });
        }
    }
}
