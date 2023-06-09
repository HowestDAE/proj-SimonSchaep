﻿using Newtonsoft.Json.Linq;
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
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace ToolDevProject.WPF.Repository
{
    internal class LoreApiRepository : ILoreRepository
    {
        private Dictionary<string, string> _heroLores;

        public async Task<string> GetLore(string heroName)
        {
            if (_heroLores == null)
            {
                await LoadLore();
            }
            if (!_heroLores.ContainsKey(heroName))
            {
                return null;
            }
            else
            {
                return _heroLores[heroName];
            }
        }

        public async Task LoadLore() //todo: make this use api
        {
            if (_heroLores != null) return; //already loaded before

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync("https://api.opendota.com/api/constants/hero_lore");

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
