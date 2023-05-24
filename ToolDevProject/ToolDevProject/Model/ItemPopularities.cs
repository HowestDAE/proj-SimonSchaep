using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolDevProject.WPF.Model
{
    internal class ItemPopularities
    {
        private List<Tuple<Item, int>> _items;
        public List<Tuple<Item, int>> Items
        {
            get
            {
                const int maxShown = 5;                
                return _items.GetRange(0, maxShown);
            }
            set
            {
                _items = value;
                //sort based on popularity
                _items.Sort((itemTuple1, itemTuple2) => itemTuple1.Item2.CompareTo(itemTuple2.Item2));
            }
        }
    }
}
