using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace HAPPYWCF
{
    [ServiceContract(CallbackContract = typeof(IIFuncionarioCallback))]
    public interface IFuncionario
    {
        [OperationContract]
        void GetMensagens();
        //[TransactionFlow(TransactionFlowOption.Allowed)]

        [OperationContract]
        void AddMensagens(Mensagem msn);
        //[TransactionFlow(TransactionFlowOption.Allowed)]

    }

    public interface IIFuncionarioCallback
    {
        [OperationContract(IsOneWay = true)]
        void GetMensagensCallBack(Mensagem mensagem);

        [OperationContract(IsOneWay = true)]
        void AddMensagensCallBack(Mensagem mensagem);
    }

    [DataContract]
    public class Funcionario
    {
        [DataMember]
        public int FuncionarioId { get; set; }

        [DataMember]
        public string Nome { get; set; }

        [DataMember]
        public string SobreNome { get; set; }

        [DataMember]
        public bool Ativo { get; set; }

        [DataMember]
        public ICollection<Mensagem> Mensagems { get; set; }
        
    }

    [DataContract]
    public class Mensagem
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Texto { get; set; }

        [DataMember]
        public Funcionario Funcionario { get; set; }

        [DataMember]
        public bool Vermelho { get; set; }

        [DataMember]
        public bool Azul { get; set; }

        [DataMember]
        public bool Rosa { get; set; }
    }
}
