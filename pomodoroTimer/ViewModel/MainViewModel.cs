using DataManager.Common.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace pomodoroTimer.ViewModel
{
    public class MainViewModel : BasePropertyChanged
    {

        #region Field

        private int _timeSec = 0;
        private int _timeMin = 0;
        private int _timeHour = 0;
        private bool _isActive = true;

        #endregion

        #region Property

        public int TimeSec
        {
            get
            {
                return _timeSec; 
            }

            set
            {
                _timeSec = value;
                OnPropertyChanged(nameof(TimeSec));
            }
        }

        public int TimeMin
        {
            get
            {
                return _timeMin;
            }

            set
            {
                _timeMin = value;
                OnPropertyChanged(nameof(TimeMin));
            }
        }

        public int TimeHour
        {
            get
            {
                return _timeHour;
            }

            set
            {
                _timeHour = value;
                OnPropertyChanged(nameof(TimeHour));
            }
        }


        #endregion

        #region Command


        public ICommand StartTimerCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    startTimer();
                }, delegate () { return true; });
            }
        }


        public ICommand ResetTimerCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    resetTimer();
                }, delegate () { return true; });
            }
        }





        #endregion

        #region Event Handler

        #endregion

        #region Constructor

        public MainViewModel()
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
        private void DrawTime(int sec, int min, int hour)
        {
            //_timeSec = string.Format("{0:00}", sec);
            //_timeMin = string.Format("{0:00}", min);
            //_timeHour = string.Format("{0:00}", hour);
        }

        #endregion

        #region Command Method
                                                                                                                
        private void startTimer()
        {

            while(_isActive == true)
            {
                _timeSec++;

                if (_timeSec >= 60)
                { 
                    _timeMin++;
                    _timeSec = 0;

                    if (_timeMin >= 60)
                    {
                        _timeHour++;
                        _timeMin = 0;
                    }
                }
            }
        }


        private void resetTimer()
        {
            _isActive = false;
            _timeSec = 0;
            _timeMin = 0;
            _timeHour = 0;
        }

        #endregion

        #region Event Method

        #endregion
    }
}
