using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolDevProject.WPF.Model
{
    public abstract class BaseHero
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "localized_name")]
        public string Name { get; set; }
    }

    public class MeleeHero : BaseHero
    {
        
    }

    public class RangedHero : BaseHero
    {
        [JsonProperty(PropertyName = "projectile_speed")]
        public int ProjectileSpeed { get; set; }
    }
}
