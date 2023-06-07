using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using ToolDevProject.WPF.Model;

namespace ToolDevProject.WPF.Repository
{
    internal class LoreApiRepository : ILoreRepository
    {
        private Dictionary<string, string> _heroLores;

        public string GetLore(string heroName)
        {
            return _heroLores[heroName];
        }

        public async Task LoadLore() //todo: make this use api
        {
            if (_heroLores != null) return; //already loaded before

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync("https://api.opendota.com/api/constants/lore");

                    if (!response.IsSuccessStatusCode)
                    {
                        throw new HttpRequestException(response.ReasonPhrase);
                    }

                    string json = await response.Content.ReadAsStringAsync();
                    JObject obj = JObject.Parse(json);
                    _heroLores = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
