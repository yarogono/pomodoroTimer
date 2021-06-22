using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomodoroTimer.Common.Lib
{
    public class BasePropertyChanged : INotifyPropertyChanged, IDisposable
    {
        #region INotifyPropertyChanged 

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string Name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Name));
        }

        #endregion

        #region IDisposable 

        public virtual void Dispose()
        {
        }

        #endregion
    }
}
