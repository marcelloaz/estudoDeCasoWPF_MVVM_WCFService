using HAPPYWPF.Command;
using Microsoft.Practices.Prism.PubSubEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HAPPYWPF
{
    public class MensagemViewModel : Observable
    {
        public INavigationMenuKudoViewModel LoadMenuFuncionariosViewModel { get; private set; }
        private readonly IEventAggregator _eventAggregator;
        public ICommand AddMensagemCommand { get; set; }

        public MensagemViewModel(IEventAggregator eventAggregator,
             INavigationMenuKudoViewModel mnFuncionariosViewModel)
        {
            LoadMenuFuncionariosViewModel = mnFuncionariosViewModel;
            _eventAggregator = eventAggregator;

            AddMensagemCommand = new DelegateCommand(OnAddMensagemExecute);
        }

        private void OnAddMensagemExecute(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
