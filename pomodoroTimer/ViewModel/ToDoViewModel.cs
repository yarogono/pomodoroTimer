using PomodoroTimer.Common.Lib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace pomodoroTimer.ViewModel
{
    public class ToDoViewModel : MainViewModel
    {

        #region Field


        #endregion

        #region Property


        public string ToDoText
        {
            get 
            { 
                return _toDoText; 
            }
            set 
            { 
                _toDoText = value;
                OnPropertyChanged(nameof(ToDoText));
            }
        }

        private string _toDoText;



        public ObservableCollection<string> ListBoxItem
        {
            get 
            { 
                return _listBoxItem; 
            }
            set 
            { 
                _listBoxItem = value;
                OnPropertyChanged(nameof(ListBoxItem));
            }
        }

        private ObservableCollection<string> _listBoxItem;

        #endregion

        #region Command


        public ICommand TextAddCommand
        {
            get
            {   
                return new DelegateCommand(() =>
                {
                    _textAdd();
                }, delegate () { return true; });
            }
        }

        #endregion

        #region Event Handler

        #endregion

        #region Constructor

        public ToDoViewModel()
        {
            _initialize();
        }

        #endregion

        #region IDispose Method

        public override void Dispose()
        {
            base.Dispose();
        }

        #endregion

        #region Initialize Method

        private void _initialize()
        {

        }

        #endregion

        #region User Method

        #endregion

        #region Command Method

        private void _textAdd()
        {
            string test = "test";
            ListBoxItem = new ObservableCollection<string>();
            ListBoxItem.Add(test);
        }

        #endregion

        #region Event Method

        #endregion



    }
}
