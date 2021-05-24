using DataManager.Common.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace pomodoroTimer.ViewModel
{
    public class MainViewModel : BasePropertyChanged
    {

        #region Field

        private DispatcherTimer _timer;
        private TimeSpan _time;

        private string _textBlock = "00:25:00";

        private string _startButton = "▶";
        private bool _isActive = false;

        #endregion

        #region Property

        public string TextBlock
        {
            get { return _textBlock; }
            set { _textBlock = value; OnPropertyChanged(nameof(TextBlock)); }
        }


        public string StartButton
        {
            get { return _startButton; }
            set { _startButton = value; OnPropertyChanged(nameof(StartButton)); }
        }



        #endregion

        #region Command


        public ICommand StartTimerCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    StartTimer();
                }, delegate () { return true; });
            }
        }


        public ICommand ResetTimerCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    ResetTimer();
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

        #region User Met



        #endregion

        #region Command Method
                                                                                                                
        private void StartTimer()
        {
            if (_isActive == false)
            {
                _isActive = true;
                StartButton = "⏸";
                _time = TimeSpan.FromMinutes(25);

                _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
                {
                    TextBlock = _time.ToString("c");
                    if (_time == TimeSpan.Zero) _timer.Stop();
                    _time = _time.Add(TimeSpan.FromSeconds(-1));
                }, Application.Current.Dispatcher);

                _timer.Start();
            }
            else
            {
                _isActive = false;
                _timer.Stop();
                TextBlock = "00:25:00";
                StartButton = "▶";
            }
        }


        private void ResetTimer()
        {
            _isActive = false;
            _timer.Stop();
            StartButton = "▶";
            TextBlock = "00:25:00";
        }

        #endregion

        #region Event Method

        #endregion
    }
}
