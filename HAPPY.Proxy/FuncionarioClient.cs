using HAPPYWCF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;

namespace HAPPY.Proxy
{
    public class FuncionarioClient : DuplexClientBase<IFuncionario>, IFuncionario
    {
        public FuncionarioClient(InstanceContext instanceContext)
            : base(instanceContext)
        {

        }

        public FuncionarioClient(InstanceContext instanceContext, string endpointName)
              : base(instanceContext, endpointName)
        {

        }

        public FuncionarioClient(InstanceContext instanceContext, Binding binding, EndpointAddress address)
             : base(instanceContext, binding, address)
        {

        }

        public void AddMensagens(Mensagem msn)
        {
            Channel.AddMensagens(msn);
        }

        public void GetMensagens()
        {
            Channel.GetMensagens();
        }

    }
}
