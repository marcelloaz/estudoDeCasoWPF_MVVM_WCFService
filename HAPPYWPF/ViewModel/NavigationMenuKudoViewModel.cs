using Microsoft.Practices.Prism.PubSubEvents;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using HAPPYWPF.View.Services;
using HAPPYWPF.DataProvider.Lookups;
using HAPPYWPF.Events;
using HAPPY.Model;
using HAPPYWCF;
using HAPPYWPF.Command;
using HAPPYWPF.Wrapper;
using System;
using System.Threading.Tasks;

namespace HAPPYWPF
{
    public class NavigationMenuKudoViewModel : Observable, INavigationMenuKudoViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IMessageDialogService _messageDialogService;
        public readonly IKudoDialogViewModele _cudoMensage;
        private readonly ILookupProvider<Funcionario> _funcionarioLookupProvider;

        private const int IntervaloContador = 1000;
        private System.Timers.Timer _timer = null;
        
        public NavigationMenuKudoViewModel(IEventAggregator eventAggregator,
             ILookupProvider<Funcionario> funcionarioLookupProvider,
             IKudoDialogViewModele CudoMensage,
        IMessageDialogService messageDialogService)
        {
            _eventAggregator = eventAggregator;
            _cudoMensage = CudoMensage;
            _eventAggregator.GetEvent<MensagemSavedEvent>().Subscribe(OnKudoSaved);
            _funcionarioLookupProvider = funcionarioLookupProvider;
            _messageDialogService = messageDialogService;
            NavigationItems = new ObservableCollection<MenuItemFuncionariosViewModel>();
        }

        private void OnKudoSaved(MensagemData obj)
        {
            _funcionarioLookupProvider.AddLookupItem(new LookupItem()
            {
                Id = obj.Id,
                DisplayValue = obj.Titulo,
                Azul = true,
                Rosa = false,
                Vermelho = false
            });

            NavigationItems.Add(
                 new MenuItemFuncionariosViewModel(
                   99,
                   obj.Titulo,
                   true,
                   false,
                   false,
                   _eventAggregator));
        

            //Load();
        //var navigationItem =
        //         NavigationItems.SingleOrDefault(item => item.Id == obj.Id);
        //if (navigationItem != null)
        //{
        //    navigationItem.DisplayValue = string.Format("{0}", obj.Titulo);
        //}
        //else
        //{
        //    Load();
        //}
    }

        private void OnMensagemSaved(HAPPYWPF.Wrapper.MensagemData obj)
        {
            _eventAggregator.GetEvent<MensagemSavedEvent>().Publish(obj);
        }

        public void Load()
        {
            //_timer = new System.Timers.Timer(IntervaloContador);
            //_timer.Elapsed += _timer_Elapsed1;
            //_timer.Enabled = true;

            NavigationItems.Clear();

            foreach (var menuFuncionariosItem in _funcionarioLookupProvider.GetLookup())
            {
                NavigationItems.Add(
                  new MenuItemFuncionariosViewModel(
                    menuFuncionariosItem.Id,
                    menuFuncionariosItem.DisplayValue,
                    menuFuncionariosItem.Rosa,
                    menuFuncionariosItem.Vermelho,
                    menuFuncionariosItem.Azul,
                    _eventAggregator));
            }
        }

        private void _timer_Elapsed1(object sender, System.Timers.ElapsedEventArgs e)
        {
          
        }

        public void InserItem(HAPPYWPF.Wrapper.MensagemData item, string nomeFuncionario)
        {
            HAPPYWCF.Mensagem MSNDATA = new Mensagem();
            MSNDATA.Id = item.Id;
            MSNDATA.Texto = item.Titulo;
            MSNDATA.Rosa = item.Rosa;
            MSNDATA.Vermelho = item.Vermelho;
            MSNDATA.Azul = item.Azul;

            _funcionarioLookupProvider.AddMensagens(MSNDATA);

            LookupItem lk = new LookupItem();
            lk.DisplayValue = item.Titulo;
            lk.Id = item.Id;
            lk.Rosa = item.Rosa;
            lk.Vermelho = item.Vermelho;
            lk.Azul = item.Azul;


            NavigationItems.Add(
                  new MenuItemFuncionariosViewModel(
                    lk.Id,
                    lk.DisplayValue,
                    lk.Rosa,
                    lk.Vermelho,
                    lk.Azul,
                    _eventAggregator));   
        }

        public void RemoverItem()
        {
            NavigationItems.Remove(this.NavigationItems[this.NavigationItems.Count() - 1]);
        }

        public ICommand SaveCommand { get; private set; }
        public ObservableCollection<MenuItemFuncionariosViewModel> NavigationItems { get; set; }

        #region TIMES

        private void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
           

        }
        #endregion
    }

    public class MenuItemFuncionariosViewModel : Observable
    {
        public ICommand OpenselecaoMensagemDetalhesViewCommand { get; set; }
        private readonly IEventAggregator _eventAggregator;

        public ICommand OpenFuncionarioEditViewCommand { get; set; }
        
        public int Id { get; private set; }
        private string _displayValue;
        public string DisplayValue
        {
            get { return _displayValue; }
            set
            {
                _displayValue = value;
                OnPropertyChanged();
            }
        }

        private bool _rosa;
        public bool Rosa
        {
            get { return _rosa; }
            set
            {
                _rosa = value;
                OnPropertyChanged();
            }
        }

        private bool _azul;
        public bool Azul
        {
            get { return _azul; }
            set
            {
                _azul = value;
                OnPropertyChanged();
            }
        }

        private bool _vermelho;
        public bool Vermelho
        {
            get { return _vermelho; }
            set
            {
                _vermelho = value;
                OnPropertyChanged();
            }
        }

        public MenuItemFuncionariosViewModel(int id,
          string displayValue, bool rosa, bool vermelho, bool azul, IEventAggregator eventAggregator)
        {
            Id = id;
            Rosa = rosa;
            Vermelho = vermelho;
            Azul = azul;
            DisplayValue = displayValue;
            _eventAggregator = eventAggregator;
            OpenselecaoMensagemDetalhesViewCommand = new DelegateCommand(OpenselecaoMensagemDetalhesViewCommandExecute);
        }

        private void OpenselecaoMensagemDetalhesViewCommandExecute(object obj)
        {
            _eventAggregator.GetEvent<OpenMensagemEditViewEvent>().Publish(Id);
        }
        
    }

    #region eventos
    public class OpenFuncionarioEditViewEvent : PubSubEvent<int>
    {

    }
    #endregion

}
