using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ToolDevProject.WPF.Model;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace ToolDevProject.WPF.Repository.Local
{
    internal class ItemPopularitiesLocalRepository : IItemPopularitiesRepository
    {
        private Dictionary<int, ItemPopularities> _itemPopularities;

        public async Task<ItemPopularities> GetItemPopularities(int heroId)
        {
            if(_itemPopularities == null)
            {
                _itemPopularities = new Dictionary<int, ItemPopularities>();
            }

            if (!_itemPopularities.ContainsKey(heroId))
            {
                var assembly = Assembly.GetExecutingAssembly();
                var resourceName = "ToolDevProject.WPF.Resources.DataFiles.ItemPopularity.json";

                Stream stream = assembly.GetManifestResourceStream(resourceName);
                var reader = new StreamReader(stream);
                string json = reader.ReadToEnd();
                JObject obj = JObject.Parse(json);

                List<Tuple<int, int>> list = new List<Tuple<int, int>>();
                foreach (JProperty property in obj.Properties())
                {
                    if (property.Name == "start_game_items") continue; //skip starting items cause those are boring
                    JObject itemObject = (JObject)property.Value;
                    foreach (JProperty childProperty in itemObject.Properties())
                    {
                        int itemId = int.Parse(childProperty.Name);
                        int popularity = childProperty.Value.ToObject<int>();
                        list.Add(new Tuple<int, int>(itemId, popularity));
                    }
                }

                var itemPopularities = new ItemPopularities(list);
                _itemPopularities.Add(heroId, itemPopularities);

                await Task.Delay(1000); //simulate api request delay
            }

            return _itemPopularities[heroId];
        }
    }
}
