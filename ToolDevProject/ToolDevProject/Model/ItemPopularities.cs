using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolDevProject.WPF.Model
{
    internal class ItemPopularities
    {
        private List<Tuple<DotaItem, int>> _items;
        public List<Tuple<DotaItem, int>> Items
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
