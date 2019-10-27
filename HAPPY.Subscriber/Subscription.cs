using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace HAPPY.Subscriber
{
    public class Subscription : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        string _State;
        int _Subscribers;
        int _Queried;

        public string State
        {
            get { return _State; }
            set
            {
                if (_State != value)
                {
                    _State = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Subscribers
        {
            get { return _Subscribers; }
            set
            {
                if (_Subscribers != value)
                {
                    _Subscribers = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Queried
        {
            get { return _Queried; }
            set
            {
                if (_Queried != value)
                {
                    _Queried = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
