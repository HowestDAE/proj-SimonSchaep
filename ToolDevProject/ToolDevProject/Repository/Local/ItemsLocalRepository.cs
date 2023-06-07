using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolDevProject.WPF.Model;

namespace ToolDevProject.WPF.Repository.Local
{
    internal class ItemsLocalRepository : IItemsRepository
    {
        private List<DotaItem> _items;

        public DotaItem GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public Task LoadItems()
        {
            throw new NotImplementedException();
        }
    }
}
