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
                        var response = await client.GetAsync($"https://api.opendota.com/api/heroes/{heroId}/itemPopularity");

                        if (!response.IsSuccessStatusCode)
                        {
                            throw new HttpRequestException(response.ReasonPhrase);
                        }

                        string json = await response.Content.ReadAsStringAsync();

                        JObject obj = JObject.Parse(json);

                        var itemPopularities = new ItemPopularities();
                        List<Tuple<int, int>> itemsList = new List<Tuple<int, int>>();
                        foreach (JProperty property in obj.Properties())
                        {
                            string key = property.Name;
                            JObject itemObject = (JObject)property.Value;
                            foreach (JProperty childProperty in itemObject.Properties())
                            {
                                int itemId = int.Parse(childProperty.Name);
                                int popularity = childProperty.Value.ToObject<int>();
                                itemsList.Add(new Tuple<int, int>(itemId, popularity));
                            }
                            itemPopularities.SetItems(key, itemsList);
                            itemsList.Clear();
                        }

                        _itemPopularities.Add(heroId, itemPopularities);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        return new ItemPopularities();
                    }
                }
            }

            return _itemPopularities[heroId];
        }
    }
}
