using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ToolDevProject.WPF.Model;

namespace ToolDevProject.WPF.Repository
{
    //this data is currently not available in the api
    internal class AttributesLocalRepository : IAttributesRepository
    {
        public int StrengthHealthGain { get; set; }
        public float StrengthHealthRegenGain { get; set; }

        public float AgilityArmorGain { get; set; }
        public float AgilityAttackSpeedGain { get; set; }

        public int IntelligenceManaGain { get; set; }
        public float IntelligenceManaRegenGain { get; set; }
        public float IntelligenceMagicResistanceGain { get; set; }

        public float UniversalDamageGain { get; set; }

        public async void LoadAttributes()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "ToolDevProject.WPF.Resources.DataFiles.Attributes.json";

            Stream stream = assembly.GetManifestResourceStream(resourceName);
            var reader = new StreamReader(stream);
            string json = reader.ReadToEnd();
            JObject obj = JObject.Parse(json);
            JToken strength = obj.SelectToken("strength");
            JToken agility = obj.SelectToken("agility");
            JToken intelligence = obj.SelectToken("intelligence");
            JToken universal = obj.SelectToken("universal");

            StrengthHealthGain = strength.SelectToken("health").ToObject<int>();
            StrengthHealthRegenGain = strength.SelectToken("health_regen").ToObject<float>();
            AgilityArmorGain = agility.SelectToken("armor").ToObject<float>();
            AgilityAttackSpeedGain = agility.SelectToken("attack_speed").ToObject<float>();
            IntelligenceManaGain = intelligence.SelectToken("mana").ToObject<int>();
            IntelligenceManaRegenGain = intelligence.SelectToken("mana_regen").ToObject<float>();
            IntelligenceMagicResistanceGain = intelligence.SelectToken("magic_resist").ToObject<float>();
            UniversalDamageGain = universal.SelectToken("damage").ToObject<float>();

            await Task.Delay(1000); //simulate api request delay
        }
    }
}
