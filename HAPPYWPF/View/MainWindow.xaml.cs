using HAPPY.Proxy;
using HAPPYWCF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HAPPYWPF.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    [CallbackBehavior(UseSynchronizationContext = false)]
    public partial class MainWindow : Window, IPubSubCallback
    {
        private MainViewModel _viewModel;
        public MainWindow(MainViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;

            _SyncContext = SynchronizationContext.Current;

            _Proxy = new PubSubClient(new InstanceContext(this));

           // lstStates.ItemsSource = _Subscriptions;
        }

        SynchronizationContext _SyncContext = null;

        PubSubClient _Proxy;

        ObservableCollection<Subscription> _Subscriptions = new ObservableCollection<Subscription>();

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

        
        private void btnSubscribe_Click_1(object sender, RoutedEventArgs e)
        {
            //string state = txtState.Text.ToUpper();

            //if (state != string.Empty && state.Length == 2)
            //{
            //    Subscription subscription = _Subscriptions.FirstOrDefault(item => item.State.ToUpper() == state);
            //    if (subscription == null)
            //    {
            //        subscription = new Subscription()
            //        {
            //            State = state
            //        };

            //        _Subscriptions.Add(subscription);

            //        Task.Run(() =>
            //        {
            //            int subs = _Proxy.Subscribe(state);

            //            _SyncContext.Send(new SendOrPostCallback(arg =>
            //            {
            //                subscription.Subscribers = subs;
            //                txtState.Text = "";
            //            }), null);
            //        });
            //    }
            //}
        }
    }
}
