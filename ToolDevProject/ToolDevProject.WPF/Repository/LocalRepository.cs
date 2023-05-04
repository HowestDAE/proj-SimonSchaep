using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ToolDevProject.WPF.Model;

namespace ToolDevProject.WPF.Repository
{
    internal class LocalRepository
    {
        private static List<Hero> _heroStats;

        public static List<Hero> GetHeroStats()
        {
            if (_heroStats == null)
            {
                var assembly = Assembly.GetExecutingAssembly();
                var resourceName = "ToolDevProject.WPF.Resources.DataFiles.HeroStatsLite.json";

                Stream stream = assembly.GetManifestResourceStream(resourceName);
                var reader = new StreamReader(stream);
                string json = reader.ReadToEnd();

                _heroStats = new List<Hero>(JsonConvert.DeserializeObject<Hero[]>(json));
            }

            return _heroStats;
        }
    }
}
