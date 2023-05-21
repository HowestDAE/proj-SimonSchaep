using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ToolDevProject.WPF.Model;

namespace ToolDevProject.WPF.Repository
{
    internal class HeroesLocalRepository : IHeroesRepository
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

                await Task.Delay(1000);
            }

            return _heroes;
        }

        public List<BaseHero> GetHeroes(string attribute, string role, string nameContains)
        {
            List<BaseHero> filteredHeroes = new List<BaseHero>();

            foreach (BaseHero hero in _heroes)
            {
                if ((attribute == "" || attribute == "all" || hero.PrimaryAttribute == attribute) &&
                    (role == "" || role == "all" || hero.Roles.Contains(role)) &&
                    (nameContains == "" || hero.Name.IndexOf(nameContains, StringComparison.OrdinalIgnoreCase) >= 0)) //case insensitive contains: https://stackoverflow.com/questions/444798/case-insensitive-containsstring/444818#444818
                {
                    filteredHeroes.Add(hero);
                }
            }

            return filteredHeroes;
        }

        public async Task<List<string>> GetAttributes()
        {
            if(_heroes == null)
            {
                await GetHeroes();
            }

            List<string> attributes = new List<string>();

            foreach (BaseHero hero in _heroes)
            {
                if (!attributes.Contains(hero.PrimaryAttribute))
                {
                    attributes.Add(hero.PrimaryAttribute);
                }
            }

            attributes.Add("all");
            return attributes;
        }

        public async Task<List<string>> GetRoles()
        {
            if (_heroes == null)
            {
                await GetHeroes();
            }

            List<string> roles = new List<string>();

            foreach (BaseHero hero in _heroes)
            {
                foreach(string role in hero.Roles)
                {
                    if (!roles.Contains(role))
                    {
                        roles.Add(role);
                    }
                }
            }

            roles.Add("all");
            return roles;
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
