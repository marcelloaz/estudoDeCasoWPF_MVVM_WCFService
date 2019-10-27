
using HAPPYWCF;
using HAPPYWPF.Wrapper;
using Microsoft.Practices.Prism.PubSubEvents;

namespace HAPPYWPF.Events
{
    public class FuncionarioSavedEvent : PubSubEvent<Funcionario>
    {
 
    }

    public class OpenMensagemEditViewEvent : PubSubEvent<int>
    {
    }

    public class MensagemSavedEvent : PubSubEvent<HAPPYWPF.Wrapper.MensagemData>
    {

    }
}
