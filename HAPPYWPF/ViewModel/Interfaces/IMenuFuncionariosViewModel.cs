using HAPPY.Model;
using HAPPYWCF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAPPYWPF
{
   public interface INavigationMenuKudoViewModel
    {
        void Load();
        void InserItem(HAPPYWPF.Wrapper.MensagemData intem, string nomeFuncionario);
        void RemoverItem();
    }
}
