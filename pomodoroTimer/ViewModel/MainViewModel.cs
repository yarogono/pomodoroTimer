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
        private TimeSpan _time = TimeSpan.FromMinutes(25);

        private string _textBlock = "00:25:00";

        private string _timerStartButton = "▶";
        private bool _isActive = false;

        private string _selectedSound = "magicRing";
        private ObservableCollection<string> _collectionSound;
        #endregion

        #region Property

        public string TextBlock
        {
            get { return _textBlock; }
            set { _textBlock = value; OnPropertyChanged(nameof(TextBlock)); }
        }


        public string TimerStartButton
        {
            get { return _timerStartButton; }
            set { _timerStartButton = value; OnPropertyChanged(nameof(TimerStartButton)); }
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

                _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), 
                    DispatcherPriority.Normal, delegate
                {
                    TextBlock = _time.ToString("c");
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
            _isActive = false;
            _timer.Stop();
            TimerStartButton = "▶";
            TextBlock = "00:25:00";
            _time = TimeSpan.FromMinutes(25);
        }

        #endregion

        #region Event Method

        #endregion
    }
}
