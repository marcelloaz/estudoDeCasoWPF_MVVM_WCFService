using Autofac;
using HAPPYWPF.View;
using Prism;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace HAPPYWPF
{
   
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private MainViewModel _mainViewModel;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var bootstrapper = new Bootstrapper();
            IContainer container = bootstrapper.Bootstrap();

            _mainViewModel = container.Resolve<MainViewModel>();
            _mainViewModel.Load();
            MainWindow = new MainWindow(_mainViewModel);
           
            MainWindow.WindowState = WindowState.Normal;
            


            MainWindow.Show();
        }
    }
}
