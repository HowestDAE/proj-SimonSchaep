using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolDevProject.WPF.ViewModel;

namespace ToolDevProject.WPF.Model
{
    public abstract class BaseHero
    {
        //info
        [JsonProperty(PropertyName = "localized_name")]
        public string Name { get; set; }

        private string _imageUrl;
        [JsonProperty(PropertyName = "img")]
        public string ImageUrl 
        {
            get => "http://cdn.dota2.com" + _imageUrl;
            set => _imageUrl = value;
        }

        public string BackgroundImage
        {
            get
            {
                if (PrimaryAttribute == "strength")
                {
                    return "../resources/images/strengthbackground.png";
                }
                if (PrimaryAttribute == "agility")
                {
                    return "../resources/images/agilitybackground.png";
                }
                if (PrimaryAttribute == "intelligence")
                {
                    return "../resources/images/intelligencebackground.png";
                }
                if (PrimaryAttribute == "universal")
                {
                    return "../resources/images/universalbackground.png";
                }
                return "";
            }
        }

        //other info
        [JsonProperty(PropertyName = "roles")]
        public List<string> Roles { get; set; }


        private string _primaryAttribute;
        [JsonProperty(PropertyName = "primary_attr")]
        public string PrimaryAttribute 
        {
            get
            {
                if(_primaryAttribute == "str")
                {
                    return "strength";
                }
                if (_primaryAttribute == "agi")
                {
                    return "agility";
                }
                if (_primaryAttribute == "int")
                {
                    return "intelligence";
                }
                if (_primaryAttribute == "all")
                {
                    return "universal";
                }
                return "attribute not recognized";
            }
            set
            {
                _primaryAttribute = value;
            }
        }


        //static stats
        [JsonProperty(PropertyName = "attack_range")]
        public int AttackRange { get; set; }

        [JsonProperty(PropertyName = "move_speed")]
        public int MoveSpeed { get; set; }


        //private stats
        [JsonProperty(PropertyName = "base_health")]
        public int BaseHealth { get; set; }

        [JsonProperty(PropertyName = "base_health_regen")]
        public float BaseHealthRegen { get; set; }

        [JsonProperty(PropertyName = "base_mana")]
        public int BaseMana { get; set; }

        [JsonProperty(PropertyName = "base_mana_regen")]
        public float BaseManaRegen { get; set; }

        [JsonProperty(PropertyName = "base_attack_min")]
        public int BaseMinDamage { get; set; }

        [JsonProperty(PropertyName = "base_attack_max")]
        public int BaseMaxDamage { get; set; }

        [JsonProperty(PropertyName = "base_attack_time")]
        public int BaseAttackSpeed { get; set; }

        [JsonProperty(PropertyName = "base_armor")]
        public int BaseArmor { get; set; }

        [JsonProperty(PropertyName = "base_mr")]
        public int BaseMagicResistance { get; set; }


        //attributes
        [JsonProperty(PropertyName = "base_str")]
        public int BaseStrength;

        [JsonProperty(PropertyName = "base_agi")]
        public int BaseAgility;

        [JsonProperty(PropertyName = "base_int")]
        public int BaseIntelligence;

        [JsonProperty(PropertyName = "str_gain")]
        public float StrengthGain;

        [JsonProperty(PropertyName = "agi_gain")]
        public float AgilityGain;

        [JsonProperty(PropertyName = "int_gain")]
        public float IntelligenceGain;


        //public stats
        public int Level { get; set; } = 1;

        //strength
        public int Strength
        {
            get
            {
                return (int)(BaseStrength + (Level - 1) * StrengthGain);
            }
        }

        public int Health
        {
            get
            {
                return (int)(BaseHealth + OverviewPageVM.AttributesRepository.StrengthHealthGain * Strength);
            }
        }

        public float HealthRegen
        {
            get
            {
                return (float)(Math.Round(BaseHealthRegen + OverviewPageVM.AttributesRepository.StrengthHealthRegenGain * Strength, 1));
            }
        }

        //agility
        public int Agility
        {
            get
            {
                return (int)(BaseAgility + (Level - 1) * AgilityGain);
            }
        }

        public int Armor
        {
            get
            {
                return (int)(BaseArmor + OverviewPageVM.AttributesRepository.AgilityArmorGain * Agility);
            }
        }

        public int AttackSpeed
        {
            get
            {
                return (int)(BaseAttackSpeed + OverviewPageVM.AttributesRepository.AgilityAttackSpeedGain * Agility);
            }
        }

        //intelligence
        public int Intelligence
        {
            get
            {
                return (int)(BaseIntelligence + (Level - 1) * IntelligenceGain);
            }
        }

        public int Mana
        {
            get
            {
                return (int)(BaseMana + OverviewPageVM.AttributesRepository.IntelligenceManaGain * Intelligence);
            }
        }

        public float ManaRegen
        {
            get
            {
                return (float)(Math.Round(BaseManaRegen + OverviewPageVM.AttributesRepository.IntelligenceManaRegenGain * Intelligence, 1));
            }
        }

        public int MagicResistance
        {
            get
            {
                return (int)(BaseMagicResistance + OverviewPageVM.AttributesRepository.IntelligenceMagicResistanceGain * Intelligence);
            }
        }

        //damage
        public int Damage
        {
            get
            {
                int damage = (BaseMinDamage + BaseMaxDamage) / 2;

                if (PrimaryAttribute == "strength")
                {
                    damage += Strength;
                }
                else if (PrimaryAttribute == "agility")
                {
                    damage += Agility;
                }
                else if (PrimaryAttribute == "intelligence")
                {
                    damage += Intelligence;
                }
                else if (PrimaryAttribute == "universal")
                {
                    damage += (int)((Strength + Agility + Intelligence) * OverviewPageVM.AttributesRepository.UniversalDamageGain);
                }

                return damage;
            }
        }

    }

    public class MeleeHero : BaseHero
    {
        //empty until we would use some melee specific stat (could be damage block, or different interactions with items)
    }

    public class RangedHero : BaseHero
    {
        [JsonProperty(PropertyName = "projectile_speed")]
        public int ProjectileSpeed { get; set; }
    }
}
