using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ToolDevProject.WPF.Model;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace ToolDevProject.WPF.Repository.Api
{
    internal class ItemPopularitiesApiRepository : IItemPopularitiesRepository
    {
        private Dictionary<int, ItemPopularities> _itemPopularities;

        public async Task<ItemPopularities> GetItemPopularities(int heroId)
        {
            if (_itemPopularities == null)
            {
                _itemPopularities = new Dictionary<int, ItemPopularities>();
            }

            if (!_itemPopularities.ContainsKey(heroId))
            {
                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        var response = await client.GetAsync("https://api.opendota.com/api/constants/items");

                        if (!response.IsSuccessStatusCode)
                        {
                            throw new HttpRequestException(response.ReasonPhrase);
                        }

                        string json = await response.Content.ReadAsStringAsync();

                        JObject obj = JObject.Parse(json);

                        List<Tuple<int, int>> list = new List<Tuple<int, int>>();
                        foreach (JProperty property in obj.Properties())
                        {
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
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        return new ItemPopularities(new List<Tuple<int, int>>());
                    }
                }
            }

            return _itemPopularities[heroId];
        }
    }
}
