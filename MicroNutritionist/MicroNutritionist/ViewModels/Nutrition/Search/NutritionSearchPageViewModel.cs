using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MicroNutritionist.Common.Extensions;
using MicroNutritionist.Common.Formatting;
using MicroNutritionist.PubSubMessages;
using MicroNutritionist.ViewModels.Nutrition.Models;
using System.Collections.ObjectModel;

namespace MicroNutritionist.ViewModels.Nutrition.Search
{
    [QueryProperty(nameof(CurrentNutrients), nameof(NutritionSearchPageViewModel))]
    public partial class NutritionSearchPageViewModel : ObservableObject
    {

        private string _searchQuery = "";
        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                SetProperty(ref _searchQuery, value);
                PerformSearch();
            }
        }
        public bool IsUniqueValue => !string.IsNullOrEmpty(_searchQuery) && Nutrients.Count == 0;

        private HashSet<int> _currentNutrients = new HashSet<int>();
        public HashSet<int> CurrentNutrients
        {
            get => _currentNutrients;
            set
            {
                SetProperty(ref _currentNutrients, value);
                Initialize();
            }
        }

        [ObservableProperty]
        private ObservableCollection<NutritionViewModel> _nutrients = new ObservableCollection<NutritionViewModel>();
        private List<NutritionViewModel> _allNutrients = new List<NutritionViewModel>();

        private void Initialize()
        {
            var currNutrients = CurrentNutrients;

            var nutrients = MauiProgram.Database.GetAllNutrition().WaitForResult();

            _allNutrients.Clear();
            Nutrients.Clear();
            foreach (var item in nutrients)
            {
                _allNutrients.Add(new NutritionViewModel(item));
            }

            PerformSearch();
        }

        private void PerformSearch()
        {
            if (string.IsNullOrEmpty(_searchQuery))
            {
                Nutrients.Clear();
                foreach (var item in _allNutrients.Where(e => !_currentNutrients.Contains(e.InnerNutrition.Id)))
                {
                    Nutrients.Add(item);
                }
            }
            else
            {
                Nutrients.Clear();
                foreach (var item in _allNutrients.Where(e => !_currentNutrients.Contains(e.InnerNutrition.Id) && e.Name.StartsWith(NameFormatting.FormatName(_searchQuery))))
                {
                    Nutrients.Add(item);
                }
            }

            OnPropertyChanged(nameof(IsUniqueValue));
        }

        [RelayCommand]
        private async Task ItemSelected(NutritionViewModel item)
        {
            WeakReferenceMessenger.Default.Send<NutritionSelectedMessage>(new NutritionSelectedMessage(item.InnerNutrition));
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        private async void AddNutrient()
        {
            var newItem = new MicroNutritionist.Common.Database.Models.Nutrition() { Name = NameFormatting.FormatName(SearchQuery) };

            await MauiProgram.Database.InsertNutrition(newItem);
            var dbItem = await MauiProgram.Database.GetNutrition(newItem.Name);

            WeakReferenceMessenger.Default.Send<NutritionSelectedMessage>(new NutritionSelectedMessage(dbItem));
            await Shell.Current.GoToAsync("..");

        }
    }
}
