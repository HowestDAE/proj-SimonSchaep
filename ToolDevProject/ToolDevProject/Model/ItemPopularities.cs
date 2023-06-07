using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolDevProject.WPF.ViewModel;

namespace ToolDevProject.WPF.Model
{
    internal class ItemPopularities 
    {
        private List<int> _itemIds;
        public List<int> ItemIds 
        {
            get
            {
                int maxCount = Math.Min(_itemIds.Count,5);
                return _itemIds.GetRange(0, maxCount);
            }
            set
            {
                _itemIds = value;
            }
        }

        public ItemPopularities(List<Tuple<int, int>> items)
        {
            //sort based on popularity
            items.Sort((itemTuple1, itemTuple2) => itemTuple1.Item2.CompareTo(itemTuple2.Item2));

            ItemIds = new List<int>();
            foreach (var item in items)
            {
                _itemIds.Add(item.Item1);
            }
            
        }
    }
}
