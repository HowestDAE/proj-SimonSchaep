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
            }

            return _heroes;
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
