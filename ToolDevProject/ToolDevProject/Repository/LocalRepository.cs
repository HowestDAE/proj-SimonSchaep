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
        private static List<BaseHero> _heroStats;

        public static List<BaseHero> GetHeroStats()
        {
            if (_heroStats == null)
            {
                _heroStats = new List<BaseHero>();

                var assembly = Assembly.GetExecutingAssembly();
                var resourceName = "ToolDevProject.WPF.Resources.DataFiles.HeroStatsLite.json";

                Stream stream = assembly.GetManifestResourceStream(resourceName);
                var reader = new StreamReader(stream);
                string json = reader.ReadToEnd();
                JArray array = JArray.Parse(json);
                foreach (JToken token in array)
                {
                    string attackType = token.SelectToken("attack_type").ToObject<string>();
                    Type heroType = GetHeroType(attackType);
                    _heroStats.Add(token.ToObject(heroType) as BaseHero);
                }                    
            }

            return _heroStats;
        }

        private static Type GetHeroType(string attackType)
        {
            if(attackType == "Melee")
            {
                return typeof(MeleeHero);
            }
            else
            {
                return typeof(RangedHero);
            }
        }
    }
}
