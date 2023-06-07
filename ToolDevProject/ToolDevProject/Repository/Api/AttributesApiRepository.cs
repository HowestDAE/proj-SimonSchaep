using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ToolDevProject.WPF.Repository
{
    //this data is currently not available in the api
    //but if they ever add it, it should be easy to just load that data here
    internal class AttributesApiRepository : IAttributesRepository
    {
        public int StrengthHealthGain { get; set; }
        public float StrengthHealthRegenGain { get; set; }

        public float AgilityArmorGain { get; set; }
        public float AgilityAttackSpeedGain { get; set; }

        public int IntelligenceManaGain { get; set; }
        public float IntelligenceManaRegenGain { get; set; }
        public float IntelligenceMagicResistanceGain { get; set; }
        public float UniversalDamageGain { get; set; }

        public async Task LoadAttributes()
        {
            await Task.Delay(0); //to get rid of warning, remove when this data becomes available on the api and this repository can be implemented
            throw new NotImplementedException();
        }
    }
}
