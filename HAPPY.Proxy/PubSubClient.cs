using HAPPYWCF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace HAPPY.Proxy
{
    public class PubSubClient : DuplexClientBase<IPubSubService>, IPubSubService
    {
        public PubSubClient(InstanceContext callbackInstance)
           : base(callbackInstance)
        {
        }

        public int Subscribe(string state)
        {
            return Channel.Subscribe(state);
        }

        public void Unsubscribe(string state)
        {
            Channel.Unsubscribe(state);
        }
    }
}
