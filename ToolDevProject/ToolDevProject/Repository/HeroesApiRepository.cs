using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ToolDevProject.WPF.Model;

namespace ToolDevProject.WPF.Repository
{
    internal class HeroesApiRepository : IHeroesRepository
    {
        private List<BaseHero> _heroes;

        public async Task<List<BaseHero>> GetHeroes()
        {
            if(_heroes == null)
            {
                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        var response = await client.GetAsync("https://api.opendota.com/api/constants/heroes");

                        if (!response.IsSuccessStatusCode)
                        {
                            throw new HttpRequestException(response.ReasonPhrase);
                        }

                        string json = await response.Content.ReadAsStringAsync();
                        JObject obj = JObject.Parse(json); //it's actually an array, but doesn't have [], so I have to parse it as an object
                        _heroes = new List<BaseHero>();
                        foreach (JToken token in obj.Children())
                        {
                            string attackType = token.First().SelectToken("attack_type").ToObject<string>();
                            Type heroType = GetHeroType(attackType);
                            _heroes.Add(token.First().ToObject(heroType) as BaseHero);
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

                //sort alphabetically
                _heroes.Sort((hero1, hero2) => hero1.Name.CompareTo(hero2.Name));
            }

            return _heroes;
        }

        public async Task<List<BaseHero>> GetHeroes(string attribute, string role, string nameContains)
        {
            if (_heroes == null)
            {
                await GetHeroes();
            }

            List<BaseHero> filteredHeroes = new List<BaseHero>();

            foreach (BaseHero hero in _heroes)
            {
                if ((attribute == "" || attribute == "all" || hero.PrimaryAttribute == attribute) &&
                    (role == "" || role == "all" || hero.Roles.Contains(role)) &&
                    (nameContains == "" || hero.Name.Contains(nameContains)))
                {
                    filteredHeroes.Add(hero);
                }
            }

            return filteredHeroes;
        }

        public async Task<List<string>> GetAttributes()
        {
            if (_heroes == null)
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
                foreach (string role in hero.Roles)
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
            if (attackType == "Melee")
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
