using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Stock.ClientWPF.Interfaces
{
    public interface IPageResolver
    {
        Page GetPageInstance(string alias);
    }
}
