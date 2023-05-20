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
    internal class LocalRepository : IRepository
    {
        private List<BaseHero> _heroes;

        public async Task<List<BaseHero>> GetHeroes()
        {
            if (_heroes == null)
            {
                _heroes = new List<BaseHero>();

                var assembly = Assembly.GetExecutingAssembly();
                var resourceName = "ToolDevProject.WPF.Resources.DataFiles.Heroes.json";

                Stream stream = assembly.GetManifestResourceStream(resourceName);
                var reader = new StreamReader(stream);
                string json = reader.ReadToEnd();
                JObject obj = JObject.Parse(json); //it's actually an array, but doesn't have [], so I have to parse it as an object
                foreach (JToken token in obj.Children())
                {
                    string attackType = token.First().SelectToken("attack_type").ToObject<string>();
                    Type heroType = GetHeroType(attackType);
                    _heroes.Add(token.First().ToObject(heroType) as BaseHero);
                }                    
            }

            await Task.Delay(1000);

            return _heroes;
        }

        private Type GetHeroType(string attackType)
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
