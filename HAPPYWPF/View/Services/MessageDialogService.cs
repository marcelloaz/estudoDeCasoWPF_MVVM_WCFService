using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace HAPPYWPF.View.Services
{
  public class MessageDialogService : IMessageDialogService
  {
    public MessageDialogResult ShowYesNoDialog(string title, string text,
      MessageDialogResult defaultResult = MessageDialogResult.Sim)
    {
      var dlg = new MessageDialog(title, text, defaultResult, MessageDialogResult.Sim, MessageDialogResult.Não);
      dlg.Owner = Application.Current.MainWindow;
      return dlg.ShowDialog();
    }
  }
}
