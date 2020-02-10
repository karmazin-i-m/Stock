using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.ClientWPF.Interfaces
{
    /// <summary>
    /// Интерфейс для коллеции моделей страниц
    /// </summary>
    public interface IViewModelsResolver
    {
        INotifyPropertyChanged GetViewModelInstance(string alias);
    }
}
