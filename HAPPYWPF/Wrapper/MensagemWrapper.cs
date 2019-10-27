using HAPPYWCF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAPPYWPF.Wrapper
{
    public class MensagemData
    {
        public int Id { get; set; }

        public int FuncionarioId { get; set; }

        public string Titulo { get; set; }

        public bool Vermelho { get; set; }

        public bool Azul { get; set; }

        public bool Rosa { get; set; }
    }

    public class MensagemWrapper : ModelWrapper<HAPPYWPF.Wrapper.MensagemData>
    {
        public MensagemWrapper(HAPPYWPF.Wrapper.MensagemData model) : base(model)
        {
        }

        protected override void InitializeCollectionProperties(HAPPYWPF.Wrapper.MensagemData model)
        {
            base.InitializeCollectionProperties(model);
        }

        public int Id
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public int FuncionarioId
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public string Titulo
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string Mensagem
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public bool Vermelho
        {
            get { return GetValue<bool>(); }
            set { SetValue(value); }
        }

        public bool Azul
        {
            get { return GetValue<bool>(); }
            set { SetValue(value); }
        }

        public bool Rosa
        {
            get { return GetValue<bool>(); }
            set { SetValue(value); }
        }

    }
}
