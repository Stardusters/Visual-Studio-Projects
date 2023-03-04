using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Input;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

using MvvmHelpers;
using Xamarin.Forms;

using XamarinForm.Models;
using MvvmHelpers.Commands;
using XamarinForm.Views;

namespace XamarinForm.ViewModels
{
    public class NameListViewModel: ViewModelBase
    {
        string initialString = string.Empty;

        public ObservableRangeCollection<ObjNames> CollectionNames { get; set; }
        public ICommand SubmitButtonClick { get; }
        public ICommand SortButtonClick { get; }
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand<ObjNames> DeleteCommand { get; }

        private ObjNames selectedName;
        private ObjNames previouslySelected;

        public NameListViewModel()
        {
            Title = "Name List";

            CollectionNames = new ObservableRangeCollection<ObjNames>();
#if DEBUG
            CollectionNames.Add(new ObjNames { SurName = "Crthur", MidName = "L", FamName = "Yu" });
            CollectionNames.Add(new ObjNames { SurName = "Arthur", MidName = "J", FamName = "Wu" });
            CollectionNames.Add(new ObjNames { SurName = "Drthur", MidName = "M", FamName = "Zu" });
            CollectionNames.Add(new ObjNames { SurName = "Brthur", MidName = "K", FamName = "Xu" });
#endif
            SubmitButtonClick = new Xamarin.Forms.Command(onSubmitButtonClick);
            SortButtonClick = new Xamarin.Forms.Command(onSortButtonClick);
            RefreshCommand = new AsyncCommand(Refresh);
            DeleteCommand = new AsyncCommand<ObjNames>(DeleteItem);
        }

        async Task Refresh()
        {
            IsBusy = true;

            await Task.Delay(2000);

            IsBusy = false;
        }

        async Task DeleteItem(ObjNames _obj)
        {
            if (_obj == null)
                return;

            await Application.Current.MainPage.DisplayAlert("Delete", _obj.FulName, "OK");
        }

        public string CurrentString
        {
            get => initialString;
            set
            {
                if (value == initialString)
                    return;

                initialString = value;
                OnPropertyChanged(nameof(CurrentString));
            }
        }

        public ObjNames SelectedName
        {
            get => selectedName;
            set
            {
                if(value != null)
                {
                    Application.Current.MainPage.DisplayAlert("Selected", value.FulName, "OK");
                    previouslySelected = value;
                    value = null;
                }

                selectedName = value;
                OnPropertyChanged();
            }
        }

        void onSubmitButtonClick()
        {
            ObjNames _inputNameObj = new ObjNames();
            string _inputedString = string.Empty;
            
            if (string.IsNullOrWhiteSpace(CurrentString))
                return;

            _inputedString = Helpers.stringProcess_RemoveSpace(CurrentString);
            if (!_inputedString.Contains(" "))
            {
                CollectionNames.Add(new ObjNames { SurName = _inputedString});
                //LstNames.Add(new ObjNames { SurName = inputedString });
                return;
            }

            _inputNameObj.SurName = _inputedString.Substring(0, _inputedString.IndexOf(" "));
            _inputedString = Helpers.stringProcess_RemoveSpace
                (_inputedString.Substring(_inputedString.IndexOf(" ") + 1));

            if(!_inputedString.Contains(" "))
            {
                _inputNameObj.MidName = string.Empty;
                _inputNameObj.FamName = _inputedString;
            }
            else
            {
                _inputNameObj.MidName = _inputedString.Substring(0, _inputedString.LastIndexOf(" "));
                _inputNameObj.FamName = _inputedString.Substring(_inputedString.LastIndexOf(" ") + 1);
            }

            CollectionNames.Add(_inputNameObj);
            //LstNames.Add(_inputNameObj);
            CurrentString = string.Empty;
        }

        void onSortButtonClick()
        {
            List<ObjNames> _lstNames = new List<ObjNames>();
            foreach (var _item in CollectionNames)
                _lstNames.Add(_item);

            CollectionNames.Clear();
            _lstNames = _lstNames.OrderBy(x => x.SurName).ThenBy(z => z.FamName).ToList();

            foreach (var _obj in _lstNames)
                CollectionNames.Add(_obj);
        }
    }
}
