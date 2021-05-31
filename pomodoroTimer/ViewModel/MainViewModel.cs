using DataManager.Common.Lib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;

namespace pomodoroTimer.ViewModel
{
    public class MainViewModel : BasePropertyChanged
    {

        #region Field

        private DispatcherTimer _timer;
        private int _sessionTime = 25;
        private TimeSpan _time;

        private string _timeTextBlock = TimeSpan.FromMinutes(25).ToString("c");

        private string _timerStartButton = "▶";
        private bool _isActive = false;

        private int _breakTime = 05;

        private string _selectedSound = "magicRing";
        private ObservableCollection<string> _collectionSound;
        #endregion

        #region Property

        public string TimeTextBlock
        {
            get { return _timeTextBlock; }
            set { _timeTextBlock = value; OnPropertyChanged(nameof(TimeTextBlock)); }
        }


        public string TimerStartButton
        {
            get { return _timerStartButton; }
            set { _timerStartButton = value; OnPropertyChanged(nameof(TimerStartButton)); }
        }


        public int BreakTime
        {
            get { return _breakTime; }
            set { _breakTime = value; OnPropertyChanged(nameof(BreakTime)); }
        }



        public int SessionTime
        {
            get { return _sessionTime; }
            set { _sessionTime = value; OnPropertyChanged(nameof(SessionTime)); }
        }




        public ObservableCollection<string> CollectionSound
        {
            get { return _collectionSound; }
            set { _collectionSound = value; OnPropertyChanged(nameof(CollectionSound)); }
        }



        public string SelectedSound
        {
            get { return _selectedSound; }
            set { _selectedSound = value; OnPropertyChanged(nameof(SelectedSound)); }
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



        public ICommand BreakTimeMinusCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    BreakTiimeMinus();
                }, delegate () { return true; });
            }
        }


        public ICommand BreakTimePlusCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    BreakTimePlus();
                }, delegate () { return true; });
            }
        }



        public ICommand SessionTimeMinusCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    SessionTimeMinus();
                }, delegate () { return true; });
            }
        }

        public ICommand SessionTimePlusCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    SessionTimePlus();
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
            CollectionSound = new ObservableCollection<string>();
            CollectionSound.Add("magicRing");
            CollectionSound.Add("ringSound");
            CollectionSound.Add("toyTelephone");

        }

        #endregion

        #region User Metod



        #endregion

        #region Command Method
                                                                                                                
        private void StartTimer()
        {
            if (_isActive == false)
            {
                _isActive = true;
                TimerStartButton = "⏸";
                _time = TimeSpan.FromMinutes(SessionTime);

                _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), 
                    DispatcherPriority.Normal, delegate
                {
                    TimeTextBlock = _time.ToString("c");
                    if (_time == TimeSpan.Zero)
                    {
                        _timer.Stop();

                        string magicRing 
                        = @"D:\coding\c#\toyProject\pomodoroTimer\pomodoroTimer\RingSound\magicRing.wav";

                        string ringSound 
                        = @"D:\coding\c#\toyProject\pomodoroTimer\pomodoroTimer\RingSound\ringSound.wav";

                        string toyTelephone
                        = @"D:\coding\c#\toyProject\pomodoroTimer\pomodoroTimer\RingSound\toyTelephone.wav";


                        string plyerFilePath = "";
                        switch (SelectedSound)
                        {
                            case "magicRing":
                                plyerFilePath = magicRing;
                                break;
                            case "ringSound":
                                plyerFilePath = ringSound;
                                break;
                            case "toyTelephone":
                                plyerFilePath = toyTelephone;
                                break;
                        }
                        var mediaPlayer = new System.Windows.Media.MediaPlayer();
                        mediaPlayer.Open(new System.Uri(plyerFilePath));
                        mediaPlayer.Play();
                    }
                    _time = _time.Add(TimeSpan.FromSeconds(-1));
                }, Application.Current.Dispatcher);

                _timer.Start();
            }
            else
            {
                _isActive = false;
                _timer.Stop();
                TimerStartButton = "▶";
            }
        }


        private void ResetTimer()
        {
            if (_isActive == true)
            {
                _isActive = false;
                _timer.Stop();
                TimerStartButton = "▶";
                _time = TimeSpan.FromMinutes(25);
                TimeTextBlock = _time.ToString("c");
            }
            else
            {
                TimerStartButton = "▶";
                _time = TimeSpan.FromMinutes(25);
                TimeTextBlock = _time.ToString("c");
            }
        }
        private void BreakTiimeMinus()
        {
            if (BreakTime > 0)
            {
                 BreakTime--;
            }
        }
        private void BreakTimePlus()
        {
            if (BreakTime < 10)
            {
                BreakTime++;
            }
        }

        private void SessionTimeMinus()
        {
            if (SessionTime > 1 && _isActive == false)
            {
                SessionTime--;

                _time = TimeSpan.FromMinutes(SessionTime);
                TimeTextBlock = _time.ToString("c");
            }
        }

        private void SessionTimePlus()
        {
            if (_isActive == false)
            {
                SessionTime++;

                _time = TimeSpan.FromMinutes(SessionTime);
                TimeTextBlock = _time.ToString("c");
            }
        }

        #endregion

        #region Event Method

        #endregion
    }
}
