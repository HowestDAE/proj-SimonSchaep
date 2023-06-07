using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolDevProject.WPF.ViewModel;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace ToolDevProject.WPF.Model
{
    internal class ItemPopularities 
    {
        private const int _maxItems = 3;
        private Dictionary<string, List<int>> _itemIds;

        public List<int> GetItems(string key)
        {
            if(!_itemIds.ContainsKey(key)) return new List<int>();

            int maxCount = Math.Min(_itemIds[key].Count, _maxItems);
            return _itemIds[key].GetRange(0, maxCount);
        }

        public void SetItems(string key, List<Tuple<int, int>> items)
        {
            //sort based on popularity
            items.Sort((itemTuple1, itemTuple2) => itemTuple2.Item2.CompareTo(itemTuple1.Item2));

            if (!_itemIds.ContainsKey(key))
            {
                _itemIds.Add(key, new List<int>());
            }
            else
            {
                _itemIds[key] = new List<int>();
            }
            foreach (var item in items)
            {
                _itemIds[key].Add(item.Item1);
            }
        }

        public ItemPopularities()
        {
            _itemIds = new Dictionary<string, List<int>>();
        }
    }
}
