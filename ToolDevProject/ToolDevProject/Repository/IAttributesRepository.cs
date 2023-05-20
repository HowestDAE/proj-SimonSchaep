using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolDevProject.WPF.Repository
{
    internal interface IAttributesRepository
    {
        int StrengthHealthGain { get; set; }
        float StrengthHealthRegenGain { get; set; }

        float AgilityArmorGain { get; set; }
        float AgilityAttackSpeedGain { get; set; }

        int IntelligenceManaGain { get; set; }
        float IntelligenceManaRegenGain { get; set; }
        float IntelligenceMagicResistanceGain { get; set; }

        float UniversalDamageGain { get; set; }

        void LoadAttributes();
    }
}
