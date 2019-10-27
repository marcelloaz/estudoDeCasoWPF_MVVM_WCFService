using Autofac;

using HAPPYWPF.View.Services;
using HAPPYWPF.DataProvider;
using HAPPYWPF.DataProvider.Lookups;
using HAPPYWPF.View;
using HAPPY.Model;
using System.Collections.Generic;
using HAPPYWCF;
using Microsoft.Practices.Prism.PubSubEvents;

namespace HAPPYWPF
{
    public class Bootstrapper
    {
        public IContainer Bootstrap()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();
            builder.RegisterType<DPLookupProvider>().As<ILookupProvider<Funcionario>>();
            builder.RegisterType<DPLookupProvider>().As<IIFuncionarioCallback>();

            builder.RegisterType<KudoCreateViewModels>().AsSelf();
            builder.RegisterType<KudoDialogViewModel>().AsSelf();
            builder.RegisterType<MessageDialogService>().AsSelf();
            builder.RegisterType<NavigationMenuKudoViewModel>().AsSelf();
            builder.RegisterType<MainViewModel>().AsSelf();
            builder.RegisterType<MensagemViewModel>().AsSelf();

            builder.RegisterType<KudoCreateViewModels>().As<IKudoCreateViewModels>();
            builder.RegisterType<KudoDialogViewModel>().As<IKudoDialogViewModele>();
            builder.RegisterType<MessageDialogService>().As<IMessageDialogService>();
            builder.RegisterType<MainViewModel>().As<IMainViewModel>();
            builder.RegisterType<NavigationMenuKudoViewModel>().As<INavigationMenuKudoViewModel>();
            
            return builder.Build();
        }
    }
}
