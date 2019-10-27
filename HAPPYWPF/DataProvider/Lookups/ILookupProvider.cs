using HAPPY.Model;
using HAPPYWCF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAPPYWPF.DataProvider.Lookups
{
    public interface ILookupProvider<T>
    {
        IEnumerable<LookupItem> GetLookup();
        void AddMensagens(Mensagem msn);
        void GetMensageServer();
        void GetMensagens();
        void AddLookupItem(LookupItem item);

    }
}
