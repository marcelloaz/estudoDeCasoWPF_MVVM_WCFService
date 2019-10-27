using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Documents;
using HAPPY.Proxy;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;
using HAPPYWCF;
using System.Configuration;
using System.ServiceModel.Channels;

namespace HAPPY.Subscriber
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>

    [CallbackBehavior(UseSynchronizationContext = false)]
    public partial class MainWindow : Window, IPubSubCallback
    {
        public MainWindow()
        {
            InitializeComponent();

            _SyncContext = SynchronizationContext.Current;

            _Proxy = new PubSubClient(new InstanceContext(this));

            lstStates.ItemsSource = _Subscriptions;
        }

      
        PubSubClient _Proxy;

        ObservableCollection<Subscription> _Subscriptions = new ObservableCollection<Subscription>();

        SynchronizationContext _SyncContext = null;
        private void btnSubscribe_Click(object sender, RoutedEventArgs e)
        {
            string state = txtState.Text.ToUpper();

            if (state != string.Empty && state.Length == 2)
            {
                Subscription subscription = _Subscriptions.FirstOrDefault(item => item.State.ToUpper() == state);
                if (subscription == null)
                {
                    subscription = new Subscription()
                    {
                        State = state
                    };

                    _Subscriptions.Add(subscription);
                 
                    Task.Run(() =>
                    {
                        int subs = _Proxy.Subscribe(state);

                        _SyncContext.Send(new SendOrPostCallback(arg =>
                        {
                            subscription.Subscribers = subs;
                            txtState.Text = "";
                        }), null);
                    });
                }
            }
        }

        private void lnkUnsubscribe_Click(object sender, RoutedEventArgs e)
        {
            Hyperlink link = e.Source as Hyperlink;
            string state = link.Tag.ToString().ToUpper();

            Task.Run(() =>
            {
                _Proxy.Unsubscribe(state);

                _SyncContext.Send(new SendOrPostCallback(arg =>
                {
                    Subscription subscription = _Subscriptions.FirstOrDefault(item => item.State == state);
                    _Subscriptions.Remove(subscription);
                }), null);
            });
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            foreach (Subscription subscription in _Subscriptions)
            {
                _Proxy.Unsubscribe(subscription.State);
            }

            _Proxy.Close();
        }

        public void UpdateSubscriptions(string state, int subscribers)
        {
            _SyncContext.Send(new SendOrPostCallback(arg =>
            {
                Subscription subscription = _Subscriptions.FirstOrDefault(item => item.State.ToUpper() == state.ToUpper());
                if (subscription != null)
                {
                    subscription.Subscribers = subscribers;
                }
            }), null);
        }

        public void StateQueried(string state)
        {
            _SyncContext.Send(new SendOrPostCallback(arg =>
            {
                Subscription subscription = _Subscriptions.FirstOrDefault(item => item.State.ToUpper() == state.ToUpper());
                if (subscription != null)
                {
                    subscription.Queried++;
                }
            }), null);
        }
    }
}
