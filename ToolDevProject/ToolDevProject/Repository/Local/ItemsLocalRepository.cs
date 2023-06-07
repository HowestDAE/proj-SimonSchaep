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

namespace ToolDevProject.WPF.Repository.Local
{
    internal class ItemsLocalRepository : IItemsRepository
    {
        private Dictionary<int, DotaItem> _items;

        public async Task<DotaItem> GetItem(int id)
        {
            if (_items == null)
            {
                await LoadItems();
            }
            if (!_items.ContainsKey(id))
            {
                return null;
            }
            else
            {
                return _items[id];
            }
        }

        public async Task LoadItems()
        {
            if (_items != null) return; //already loaded before

            _items = new Dictionary<int, DotaItem>();

            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "ToolDevProject.WPF.Resources.DataFiles.Items.json";

            Stream stream = assembly.GetManifestResourceStream(resourceName);
            var reader = new StreamReader(stream);
            string json = reader.ReadToEnd();
            JObject obj = JObject.Parse(json);
            foreach (JToken token in obj.Children())
            {
                int id = token.First().SelectToken("id").ToObject<int>();
                _items.Add(id,token.First().ToObject<DotaItem>());
            }

            await Task.Delay(1000); //simulate api request delay
        }
    }
}
