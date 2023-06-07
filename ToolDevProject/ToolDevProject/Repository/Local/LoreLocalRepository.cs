using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace ToolDevProject.WPF.Repository
{
    internal class LoreLocalRepository : ILoreRepository
    {
        private Dictionary<string, string> _heroLores;

        public async Task<string> GetLore(string heroName)
        {
            if (_heroLores == null)
            {
                await LoadLore();
            }
            return _heroLores[heroName];
        }

        public async Task LoadLore()
        {
            if (_heroLores != null) return; //already loaded before

            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "ToolDevProject.WPF.Resources.DataFiles.Lore.json";

            Stream stream = assembly.GetManifestResourceStream(resourceName);
            var reader = new StreamReader(stream);
            string json = reader.ReadToEnd();
            JObject obj = JObject.Parse(json);
            _heroLores = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

            await Task.Delay(1000); //simulate api request delay
        }
    }
}
