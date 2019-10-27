using HAPPY.Model;
using HAPPYWCF;
using HAPPYWPF.Command;
using HAPPYWPF.DataProvider;
using HAPPYWPF.DataProvider.Lookups;
using HAPPYWPF.Events;
using HAPPYWPF.View.Services;
using HAPPYWPF.Wrapper;
using System;
using System.Collections.Generic;
using Microsoft.Practices.Prism.PubSubEvents;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HAPPYWPF
{
    public class MainViewModel : Observable, IMainViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IMessageDialogService _messageDialogService;
        public readonly IKudoDialogViewModele _cudoMensage;
        public INavigationMenuKudoViewModel _navigationMenuKudoViewModel { get; private set; }

        public ICommand AddMensagemHostCommand { get; private set; }
        public ICommand AddMensagemCommand { get; private set; }
        public ICommand RemoverFuncionarioCommand { get; set; }
        public ICommand LimparMensagensCommand { get; set; }
        public ICommand AddTabKudoCommand { get; set; }
        public ICommand NewTabCommand { get; set; }
        public ICommand CloseMensagemTabCommand { get; private set; }
        public List<Mensagens> FuncionarioNovaMensagem;
        public Funcionario FuncionarioData;
        private IKudoCreateViewModels _kudoCreateViewModels;
        private IKudoCreateViewModels _selecaoKudomDetalhesViewModels;
        public IKudoCreateViewModels SelecaoKudoDetalhesViewModels
        {
            get { return _selecaoKudomDetalhesViewModels; }
            set
            {
                _selecaoKudomDetalhesViewModels = value;
                OnPropertyChanged();
            }
        }
        private Func<IKudoCreateViewModels> _kudoCreateViewModelsCreator;
        private MensagemWrapper _mensagemChange;
        public MensagemWrapper MensagemChange
        {
            get { return _mensagemChange; }
            private set
            {
                _mensagemChange = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<IKudoCreateViewModels> KudoCollectionDetalhesViewModels { get; private set; }


        public MainViewModel(IEventAggregator eventAggregator,
             IMessageDialogService messageDialogService,
             IKudoDialogViewModele CudoMensage,
             Func<IKudoCreateViewModels> kudoCreateViewModels,
                INavigationMenuKudoViewModel navigationMenuKudoViewModel)
        {
            _eventAggregator = eventAggregator;
            _navigationMenuKudoViewModel = navigationMenuKudoViewModel;

            _cudoMensage = CudoMensage;
            _messageDialogService = messageDialogService;
            _kudoCreateViewModelsCreator = kudoCreateViewModels;

            KudoCollectionDetalhesViewModels = new ObservableCollection<IKudoCreateViewModels>();
            AddTabKudoCommand = new DelegateCommand(OnAddTabKudoCommandExecute);
            CloseMensagemTabCommand = new DelegateCommand(OnCloseTabKudoTabExecute);

            _navigationMenuKudoViewModel.Load();
        }


        public IKudoCreateViewModels SelectedKudoCreateViewModels
        {
            get { return _kudoCreateViewModels; }
            set
            {
                _kudoCreateViewModels = value;
                OnPropertyChanged();
            }
        }

        private void OnAddTabKudoCommandExecute(object obj)
        {
            IKudoCreateViewModels kudoNewVm = _kudoCreateViewModelsCreator();
            kudoNewVm.Load();
            KudoCollectionDetalhesViewModels.Add(kudoNewVm);
            SelectedKudoCreateViewModels = kudoNewVm;

            kudoNewVm.Load();
        }

        private void OnCloseTabKudoTabExecute(object obj)
        {
            var kudoVmToClose = obj as IKudoCreateViewModels;

            if (kudoVmToClose != null)
            {
                KudoCollectionDetalhesViewModels.Remove(kudoVmToClose);
            }
        }

        private void InvalidateCommands()
        {
            ((DelegateCommand)AddTabKudoCommand).RaiseCanExecuteChanged();
        }

        public void Load()
        {
            InvalidateCommands();
        }
      
    }
}
