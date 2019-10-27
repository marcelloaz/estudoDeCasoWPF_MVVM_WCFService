using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HAPPYWPF.View
{
    /// <summary>
    /// Interaction logic for NavigationMenuFuncionariosView.xaml
    /// </summary>
    public partial class NavigationMenuKudoView : UserControl
    {
        public NavigationMenuKudoView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)e.OriginalSource;
            CudoMensage KUDO = new CudoMensage(btn.Content.ToString(), btn.Content.ToString());
            KUDO.WindowState = WindowState.Normal;
            
            KUDO.Show();
        }
    }
}
