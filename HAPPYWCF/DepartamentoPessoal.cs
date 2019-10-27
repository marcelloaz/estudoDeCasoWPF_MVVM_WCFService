using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Timers;

namespace HAPPYWCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant)]
    public class DepartamentoPessoal : IFuncionario
    {
      
        public static List<Mensagem> listMensagem = new List<Mensagem>();

        public void AddMensagens(Mensagem msn)
        {
            //try
            //{
            //    IIFuncionarioCallback callback =
            //     OperationContext.Current.GetCallbackChannel<IIFuncionarioCallback>();

            //    listMensagem.Add(msn);

            //    if (callback != null)
            //    {
            //        callback.AddMensagensCallBack(msn);
            //    }

            //}
            //catch (Exception ex)
            //{
            //    throw new Exception(ex.ToString());
            //}

            //lock (Subscribers)
            //{
            //    IPubSubCallback callbackChannel = OperationContext.Current.GetCallbackChannel<IPubSubCallback>();
            //    if (callbackChannel != null)
            //    {
            //        List<IPubSubCallback> stateSubscribers;
            //        if (Subscribers.ContainsKey(state.ToUpper()))
            //            stateSubscribers = Subscribers[state.ToUpper()];
            //        else
            //        {
            //            stateSubscribers = new List<IPubSubCallback>();
            //            Subscribers.Add(state.ToUpper(), stateSubscribers);
            //        }

            //        if (!stateSubscribers.Contains(callbackChannel))
            //            stateSubscribers.Add(callbackChannel);

            //        UpdateClientSubscriptionCount(state);

            //        return stateSubscribers.Count;
            //    }
            //    else
            //        return 0;
            //}
        }

        private const int IntervaloContador = 5000;
        private System.Timers.Timer _timer = null;


        [OperationBehavior()]
        public void GetMensagens()
        {
            try
            {
                IIFuncionarioCallback callback =
                OperationContext.Current.GetCallbackChannel<IIFuncionarioCallback>();


                if (callback != null)
                {
                    foreach (var item in listMensagem.AsEnumerable())
                    {
                        var mensage = (Mensagem)item;
                        callback.GetMensagensCallBack(mensage);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        private List<Funcionario> AssociaMsnFuncionarios()
        {
            List<string> nomesFuncionarios = NomesFuncionarios();
            var funcionarios = Funcionarios(nomesFuncionarios);

            for (int i = 0; i < funcionarios.Count(); i++)
            {
                Mensagem minhaMensagem = new Mensagem();
                minhaMensagem.Texto = GetFrasesRandom();
                minhaMensagem.Id = i + 1;

                listMensagem.Add(minhaMensagem);

                funcionarios[i].Mensagems = listMensagem;
            }

            return funcionarios;
        }

        private List<Funcionario> Funcionarios(List<string> nomesFuncionarios)
        {
            try
            {
                List<Funcionario> funcCreator = new List<Funcionario>();
                foreach (var item in nomesFuncionarios)
                {
                    Funcionario fn = new Funcionario();
                    fn.Nome = item;
                    funcCreator.Add(fn);
                }
                return funcCreator;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

        }
        static Random rnd = new Random();
        private static string GetFrasesRandom()
        {
            try
            {
                List<string> frases = new List<string>();
                frases.Add("Talvez eu não tenha muitos amigos. Mas os que eu tenho são os melhores que alguém poderia ter!");
                frases.Add("Só existe uma coisa melhor do que fazer novos amigos: conservar os velhos.");
                frases.Add("Quer saber quantos amigos você tem? Dê uma festa. Quer saber a qualidade deles ? Fique doente.");
                frases.Add("Não preciso de modelos. Não preciso de heróis. Eu tenho meus amigos.");
                frases.Add("Quando você acha um amigo de verdade, acha um tesouro.");
                frases.Add("Passar o dia sem irritar alguns amigos dá a sensação que o dia não está completo.");
                frases.Add("E quem disse que um homem não pode ser melhor amigo de uma mulher?");
                frases.Add("Sem sua amizade e sem meu café, não sou nada!");
                frases.Add("Seus amigos de verdade amam você de qualquer jeito.");
                frases.Add("Te chama de amigo mas te marca em foto que você tá parecendo o demônio.");
                frases.Add("Aquele momento que você percebe que até seu amigo feio tá namorando e você não.");
                frases.Add("Aquele momento que você percebe que até seu amigo feio tá namorando e você não.");
                frases.Add("Aquele momento que você percebe que até seu amigo feio tá namorando e você não.");
                int r = rnd.Next(frases.Count);
                return frases[r];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

        }
        private static List<string> NomesFuncionarios()
        {
            try
            {
                List<string> nomesFuncionarios = new List<string>();
                nomesFuncionarios.Add("karol");
                nomesFuncionarios.Add("Rosângela");
                nomesFuncionarios.Add("Ivan");
                //nomesFuncionarios.Add("Braun");
                //nomesFuncionarios.Add("Gabriel");
                //nomesFuncionarios.Add("Marcello");
                //nomesFuncionarios.Add("Wilson");
                //nomesFuncionarios.Add("Gerson");
                return nomesFuncionarios;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }


    }
}
