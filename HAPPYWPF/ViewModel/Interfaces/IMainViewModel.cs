using HAPPYWPF.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAPPYWPF
{
    public interface IMainViewModel
    {
        MensagemWrapper MensagemChange { get; }
        void Load();
    }
}
