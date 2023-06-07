using Newtonsoft.Json;
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

namespace ToolDevProject.WPF.Repository.Api
{
    internal class ItemsApiRepository : IItemsRepository
    {
        private Dictionary<int, DotaItem> _items;

        public async Task<DotaItem> GetItem(int id)
        {
            if (_items == null)
            {
                await LoadItems();
            }
            return _items[id];
        }

        public async Task LoadItems()
        {
            if (_items != null) return; //already loaded before

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    _items = new Dictionary<int, DotaItem>();

                    var response = await client.GetAsync("https://api.opendota.com/api/constants/items");

                    if (!response.IsSuccessStatusCode)
                    {
                        throw new HttpRequestException(response.ReasonPhrase);
                    }

                    string json = await response.Content.ReadAsStringAsync();
                    
                    JObject obj = JObject.Parse(json);
                    foreach (JToken token in obj.Children())
                    {
                        int id = token.First().SelectToken("id").ToObject<int>();
                        _items.Add(id, token.First().ToObject<DotaItem>());
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
