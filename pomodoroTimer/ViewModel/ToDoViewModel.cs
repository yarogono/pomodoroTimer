using PomodoroTimer.Common.Lib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;



// Listbox를 ListView로 해보기

namespace pomodoroTimer.ViewModel
{
    public class ToDoViewModel : MainViewModel
    {

        #region Field

        public class ToDoList
        {
            public string ToDo { get; set; }

            public string Priority { get; set; }

            public bool Mail { get; set; }
        }



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



        public ObservableCollection<ToDoList> ListBoxItem
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

        private ObservableCollection<ToDoList> _listBoxItem;

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
            ListBoxItem = new ObservableCollection<ToDoList>();
        }

        #endregion

        #region User Method

        #endregion

        #region Command Method

        private void _textAdd()
        {

            if (ToDoText == null)
            {
                MessageBox.Show("할일을 적어주세요.");
                return;
            }

            ListBoxItem.Add(new ToDoList() { ToDo = ToDoText});

            ToDoText = string.Empty;
        }

        #endregion

        #region Event Method




        #endregion



    }
}
