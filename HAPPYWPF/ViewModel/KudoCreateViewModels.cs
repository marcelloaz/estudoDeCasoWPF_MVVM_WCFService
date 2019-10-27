using HAPPY.Model;
using HAPPYWCF;
using HAPPYWPF.Command;
using HAPPYWPF.Events;
using HAPPYWPF.View.Services;
using HAPPYWPF.Wrapper;
using Microsoft.Practices.Prism.PubSubEvents;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using System;
using Autofac;
using System.Windows;

namespace HAPPYWPF
{
    public interface IKudoCreateViewModels
    {
        void Load(int? Id = null);
    }

    public class Funcionario
    {
        public string FuncionarioName { get; set; }
    }

    public class FuncionarioWrapper
    {
        public Funcionario Funcionario { get; set; }
    }

    public class KudoCreateViewModels : Observable, IKudoCreateViewModels
    {
        public ICommand AddKudoCommand { get; private set; }
        private readonly IMessageDialogService _messageDialogService;
        public INavigationMenuKudoViewModel LoadMenuFuncionariosViewModel { get; private set; }
        public IMainViewModel MainViewModel { get; private set; }
        private readonly IEventAggregator _eventAggregator;
        public KudoCreateViewModels(IEventAggregator eventAggregator,
             INavigationMenuKudoViewModel mnFuncionariosViewModel,
             IMainViewModel mainViewModel,
            IMessageDialogService messageDialogService)
        {
            _eventAggregator = eventAggregator;
            LoadMenuFuncionariosViewModel = mnFuncionariosViewModel;
            MainViewModel = mainViewModel;
            _messageDialogService = messageDialogService;
            AddKudoCommand = new DelegateCommand(OnAddMensagemExecute, OnAddKudoCanExecute);
        }


        private bool OnAddKudoCanExecute(object arg)
        {
            return MensagemChange.IsChanged;
        }

        private void OnAddMensagemExecute(object obj)
        {
            var kudo = MensagemChange.Model;
            if (kudo != null)
            {
                var result = _messageDialogService.ShowYesNoDialog(
                      "Você gostaria de enviar esse KUDO para toda a galera ?",
                      string.Format("Você gostaria de enviar esse KUDO para toda a galera?"),
                      MessageDialogResult.Não);
                if (result == MessageDialogResult.Sim)
                {
                    LoadMenuFuncionariosViewModel.InserItem(new Wrapper.MensagemData()
                    {
                        Id = kudo.Id,
                        Titulo = kudo.Titulo,
                        Azul = kudo.Azul,
                        Rosa = kudo.Rosa,
                        Vermelho = kudo.Vermelho
                    }, "asdasd");

                    MensagemChange.AcceptChanges();
                    _eventAggregator.GetEvent<MensagemSavedEvent>().Publish(MensagemChange.Model);
                    InvalidateCommands();

                    //LoadMenuFuncionariosViewModel.Load();
                }
            }

        }

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

        public void Load(int? Id = default(int?))
        {
            MensagemChange = new MensagemWrapper(new MensagemData());
            MensagemChange.PropertyChanged += (s, d) =>
            {
                if (d.PropertyName == nameof(MensagemChange.Titulo))
                {
                    InvalidateCommands();
                }

                if (d.PropertyName == nameof(MensagemChange.IsChanged))
                {
                    InvalidateCommands();
                }
            };

            InvalidateCommands();
        }

        private void InvalidateCommands()
        {
            ((DelegateCommand)AddKudoCommand).RaiseCanExecuteChanged();
        }

        private IEnumerable<FuncionarioWrapper> NomesFuncionarios()
        {
            yield return new FuncionarioWrapper { Funcionario = new Funcionario { FuncionarioName = "MARCELLO DE SOUZA AZEVEDO" } };
            yield return new FuncionarioWrapper { Funcionario = new Funcionario { FuncionarioName = "MAntonio Emanuel Fernando Rodrigues" } };
            yield return new FuncionarioWrapper { Funcionario = new Funcionario { FuncionarioName = "Nathan Gustavo Dias" } };
            yield return new FuncionarioWrapper { Funcionario = new Funcionario { FuncionarioName = "Thiago Hugo Renato Oliveira" } };
            yield return new FuncionarioWrapper { Funcionario = new Funcionario { FuncionarioName = "Vitor Renan Henry de Paula" } };
            yield return new FuncionarioWrapper { Funcionario = new Funcionario { FuncionarioName = "Luiz Thales Almeida" } };
            yield return new FuncionarioWrapper { Funcionario = new Funcionario { FuncionarioName = "Lucas Otávio Campos" } };
            yield return new FuncionarioWrapper { Funcionario = new Funcionario { FuncionarioName = "Rafael Danilo Gomes" } };
            yield return new FuncionarioWrapper { Funcionario = new Funcionario { FuncionarioName = "Calebe Daniel Lucas Campos" } };
            yield return new FuncionarioWrapper { Funcionario = new Funcionario { FuncionarioName = "Murilo Pietro Fernandes" } };
            yield return new FuncionarioWrapper { Funcionario = new Funcionario { FuncionarioName = "Nicolas Cauã Renato Araújo" } };
            yield return new FuncionarioWrapper { Funcionario = new Funcionario { FuncionarioName = "Francisco Bruno Dias" } };
        }
    }

    public class MenuItemFuncionariosViewModelSavedEvent : PubSubEvent<MensagemData>
    {
    }
}
