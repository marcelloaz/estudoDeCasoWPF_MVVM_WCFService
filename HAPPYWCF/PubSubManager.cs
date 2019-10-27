using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace HAPPYWCF
{

    [ServiceContract(CallbackContract = typeof(IPubSubCallback))]
    public interface IPubSubService
    {
        [OperationContract]
        int Subscribe(string state);

        [OperationContract]
        void Unsubscribe(string state);
    }

    [ServiceContract]
    public interface IPubSubCallback
    {
        //        [OperationContract]
        //        void Dummy();

        [OperationContract]
        void UpdateSubscriptions(string state, int subscribers);

        [OperationContract]
        void StateQueried(string state);
    }

    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant)]
    public class PubSubManager : IPubSubService
    {
        static PubSubManager()
        {
            Subscribers = new Dictionary<string, List<IPubSubCallback>>();
        }

        public static Dictionary<string, List<IPubSubCallback>> Subscribers { get; set; }

        public int Subscribe(string state)
        {
            lock (Subscribers)
            {
                IPubSubCallback callbackChannel = OperationContext.Current.GetCallbackChannel<IPubSubCallback>();
                if (callbackChannel != null)
                {
                    List<IPubSubCallback> stateSubscribers;
                    if (Subscribers.ContainsKey(state.ToUpper()))
                        stateSubscribers = Subscribers[state.ToUpper()];
                    else
                    {
                        stateSubscribers = new List<IPubSubCallback>();
                        Subscribers.Add(state.ToUpper(), stateSubscribers);
                    }

                    if (!stateSubscribers.Contains(callbackChannel))
                        stateSubscribers.Add(callbackChannel);

                    UpdateClientSubscriptionCount(state);

                    return stateSubscribers.Count;
                }
                else
                    return 0;
            }
        }

        public void Unsubscribe(string state)
        {
            lock (Subscribers)
            {
                IPubSubCallback callbackChannel = OperationContext.Current.GetCallbackChannel<IPubSubCallback>();
                if (callbackChannel != null)
                {
                    if (Subscribers.ContainsKey(state.ToUpper()))
                    {
                        List<IPubSubCallback> stateSubscribers = Subscribers[state.ToUpper()];
                        if (stateSubscribers.Contains(callbackChannel))
                            stateSubscribers.Remove(callbackChannel);
                    }

                    UpdateClientSubscriptionCount(state);
                }
            }
        }

        void UpdateClientSubscriptionCount(string state)
        {
            List<IPubSubCallback> badSubscribers = new List<IPubSubCallback>();

            if (Subscribers.ContainsKey(state.ToUpper()))
            {
                List<IPubSubCallback> subscribers = Subscribers[state.ToUpper()];
                foreach (IPubSubCallback subscriber in subscribers)
                {
                    try
                    {
                        subscriber.UpdateSubscriptions(state.ToUpper(), subscribers.Count);
                    }
                    catch (Exception)
                    {
                        badSubscribers.Add(subscriber);
                    }
                }
            }

            RemoveBadSubscribers(badSubscribers);
        }

        static void RemoveBadSubscribers(IEnumerable<IPubSubCallback> badSubscribers)
        {
            if (badSubscribers.Count() > 0)
            {
                foreach (KeyValuePair<string, List<IPubSubCallback>> kvpState in Subscribers)
                {
                    List<IPubSubCallback> stateSubscriberList = kvpState.Value;

                    foreach (IPubSubCallback badSubscriber in badSubscribers)
                    {
                        if (stateSubscriberList.Contains(badSubscriber))
                            stateSubscriberList.Remove(badSubscriber);
                    }
                }
            }
        }

        public static void CallClientsForState(string state)
        {
            List<IPubSubCallback> badSubscribers = new List<IPubSubCallback>();

            lock (Subscribers)
            {
                if (Subscribers.ContainsKey(state.ToUpper()))
                {
                    List<IPubSubCallback> stateSubscriberList = Subscribers[state.ToUpper()];
                    foreach (IPubSubCallback subscriber in stateSubscriberList)
                    {
                        try
                        {
                            subscriber.StateQueried(state);
                        }
                        catch (Exception)
                        {
                            badSubscribers.Add(subscriber);
                        }
                    }

                    RemoveBadSubscribers(badSubscribers);
                }
            }
        }
    }
}
