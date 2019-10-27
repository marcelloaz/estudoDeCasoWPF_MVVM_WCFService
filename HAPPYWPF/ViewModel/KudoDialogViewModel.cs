using HAPPYWPF.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAPPYWPF
{
    public class KudoDialogViewModel : IKudoDialogViewModele
    {
        public void ShowDialog(string title, string text)
        {
            var dlg = new CudoMensage(title, text);
            dlg.Owner = System.Windows.Application.Current.MainWindow;
            dlg.ShowDialog();
        }
    }
}
