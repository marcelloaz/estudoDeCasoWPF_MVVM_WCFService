using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using HAPPY.Model;
using System.Configuration;
using HAPPY.Proxy;
using HAPPYWCF;
using System.Threading;

namespace HAPPYWPF.DataProvider.Lookups
{
    [CallbackBehavior(UseSynchronizationContext = false)]
    public class DPLookupProvider : ILookupProvider<Funcionario>, IIFuncionarioCallback, IPubSubCallback
    {

        public DPLookupProvider()
        {

            _SyncContext = SynchronizationContext.Current;
            //GetLookupList.Add(new LookupItem() { DisplayValue = "asdasd", Azul = true, Id = 1, Rosa = false, Vermelho = false });
        }

        public static string serverEndPont = ConfigurationSettings.AppSettings["DepartamentoPessoal"];
        public static string serverPubSubManagerEndPont = ConfigurationSettings.AppSettings["PubSubManager"];
        public List<LookupItem> GetLookupList = new List<LookupItem>();

        public IEnumerable<LookupItem> GetLookup()
        {
            this.GetMensagens();


            return GetLookupList;
        }
        SynchronizationContext _SyncContext = null;
        public void GetMensagens()
        {
            try
            {
                TesteCon();
                var itemsFunc = new List<Mensagem>();
                EndpointAddress address = new EndpointAddress(serverEndPont);
                Binding binding = new NetTcpBinding();

                FuncionarioClient proxy = new FuncionarioClient(new InstanceContext(this), "tcpPentagoService");
                proxy.Open();

                proxy.GetMensagens();

                proxy.Close();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

        }

        private void TesteCon()
        {

            var itemsFunc = new List<Mensagem>();
            EndpointAddress address = new EndpointAddress(serverPubSubManagerEndPont);
            Binding binding = new NetTcpBinding();

            PubSubClient proxy = new PubSubClient(new InstanceContext(this));

            proxy.Open();

            proxy.Subscribe("asdasd");

            proxy.Close();
        }

        public void GetMensagensCallBack(Mensagem mensagem)
        {
            try{
                Task.Run(() =>
                {
                    _SyncContext.Send(new SendOrPostCallback(arg =>
                    {
                        LookupItem it = new LookupItem();
                        it.Id = mensagem.Id;
                        it.DisplayValue = mensagem.Texto;
                        GetLookupList.Add(it);

                    }), null);
                });
            }
            catch (Exception ex)
            {

                throw new Exception(ex.ToString());
            }

        }

        public void AddMensagens(Mensagem msn)
        {
            try
            {
                EndpointAddress address = new EndpointAddress(serverEndPont);
                Binding binding = new NetTcpBinding();

                FuncionarioClient proxy = new FuncionarioClient(new InstanceContext(this), "tcpPentagoService");
                proxy.Open();
                proxy.AddMensagens(msn);
                proxy.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public void AddMensagensCallBack(Mensagem mensagem)
        {
            try
            {
                Task.Run(() =>
                {
                    _SyncContext.Send(new SendOrPostCallback(arg =>
                    {
                        LookupItem it = new LookupItem();
                        it.Id = mensagem.Id;
                        it.DisplayValue = mensagem.Texto;
                        GetLookupList.Add(it);

                    }), null);
                });
            }
            catch (Exception ex)
            {

                throw new Exception(ex.ToString());
            }
        }

        public void GetMensageServer()
        {
            try
            {
                GetMensagens();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.ToString());
            }
        }

        public void AddLookupItem(LookupItem item)
        {
            GetLookupList.Add(new LookupItem() { DisplayValue = "asdasdwwww", Azul = true, Id = 1, Rosa = false, Vermelho = false });
        }

        public void UpdateSubscriptions(string state, int subscribers)
        {
            LookupItem it = new LookupItem();
            it.Id = subscribers;
            it.DisplayValue = state;

            GetLookupList.Add(it);
        }

        public void StateQueried(string state)
        {
            throw new NotImplementedException();
        }
    }
}
