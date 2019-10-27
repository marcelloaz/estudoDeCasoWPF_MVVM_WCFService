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
using System.Windows.Shapes;

namespace HAPPYWPF.View
{
    /// <summary>
    /// Interaction logic for CudoMensage.xaml
    /// </summary>
    public partial class CudoMensage : Window
    {
        public CudoMensage(string title, string text)
        {
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            Title = title;
            txtKudo.Text = text;
        }

    
    }
}
