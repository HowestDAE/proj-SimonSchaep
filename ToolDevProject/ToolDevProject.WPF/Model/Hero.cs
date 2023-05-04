using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolDevProject.WPF.Model
{
    public class Hero
    {
        public int Id { get; set; }

        [JsonProperty(PropertyName = "localized_name")]
        public string Name { get; set; }
    }
}
