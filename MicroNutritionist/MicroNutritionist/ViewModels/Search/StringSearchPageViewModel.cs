using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MicroNutritionist.PubSubMessages.Search;
using MicroNutritionist.ViewModels.Search.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroNutritionist.ViewModels.Search
{
    [QueryProperty(nameof(InjectedModel), nameof(StringSearchPageViewModel))]
    public partial class StringSearchPageViewModel : ObservableObject
    {
        public StringSearchPageModel InjectedModel
        {
            get => _injectedModel;
            set
            {
                SetProperty(ref _injectedModel, value);
                FilterProfileWithSearch();
            }
        }
        private StringSearchPageModel _injectedModel;

        [ObservableProperty]
        private ObservableCollection<object> _filteredCollection = new ObservableCollection<object>();

        public string SearchQuery
        {
            get
            {
                return _searchQuery;
            }
            set
            {
                if (_searchQuery != value)
                {
                    SetProperty(ref _searchQuery, value);
                    FilterProfileWithSearch();
                }
            }
        }

        private string _searchQuery = "";

        [RelayCommand]
        private async Task ItemSelected(StringSearchConvertWrapper wrapper)
        {
            await Shell.Current.GoToAsync("..");
            switch (InjectedModel.ModelType)
            {
                case StringSearchPageModelType.Select:
                    WeakReferenceMessenger.Default.Send<SelectObjectMessage>(new SelectObjectMessage(wrapper.Inner));
                    break;
                case StringSearchPageModelType.Edit:
                    WeakReferenceMessenger.Default.Send<EditObjectMessage>(new EditObjectMessage(wrapper.Inner));
                    break;
                case StringSearchPageModelType.Delete:
                    WeakReferenceMessenger.Default.Send<DeleteObjectMessage>(new DeleteObjectMessage(wrapper.Inner));
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        public void FilterProfileWithSearch()
        {
            FilteredCollection.Clear();

            var filtered = InjectedModel.Collection.Where(e => InjectedModel.StringDisplay.Invoke(e).ToLower().StartsWith(SearchQuery.ToLower()));

            foreach (var profile in filtered)
            {
                FilteredCollection.Add(new StringSearchConvertWrapper(profile, InjectedModel.StringDisplay));
            }
        }
    }

    public class StringSearchConvertWrapper
    { 
        public object Inner { get; private set; }
        private Func<object, string> _converter;

        public StringSearchConvertWrapper(object inner, Func<object, string> converter)
        { 
            Inner = inner;
            _converter = converter;
        }

        public override string ToString()
        {
            return _converter(Inner);
        }
    }
}
