using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace ToolDevProject.WPF.Repository
{
    internal interface IItemsRepository
    {
        Task LoadItems();

        Item GetItem(int id);
    }
}
