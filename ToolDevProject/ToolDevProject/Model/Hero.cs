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
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "localized_name")]
        public string Name { get; set; }

        private string _imageUrl;
        [JsonProperty(PropertyName = "img")]
        public string ImageUrl 
        {
            get => "http://cdn.dota2.com" + _imageUrl;
            set => _imageUrl = value;
        }

        private string _iconUrl;
        [JsonProperty(PropertyName = "icon")]
        public string IconUrl 
        {
            get => "http://cdn.dota2.com" + _iconUrl;
            set => _iconUrl = value;
        }


        //other info
        [JsonProperty(PropertyName = "roles")]
        public List<string> Roles { get; set; }

        
        [JsonProperty(PropertyName = "primary_attr")]
        public string PrimaryAttribute { get; set; }


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
        public int Health
        {
            get
            {
                return (int)(BaseHealth +
                    OverviewPageVM.AttributesRepository.StrengthHealthGain * (BaseStrength + Level * StrengthGain));
            }
        }

        public float HealthRegen
        {
            get
            {
                return BaseHealthRegen +
                    OverviewPageVM.AttributesRepository.StrengthHealthRegenGain * (BaseStrength + Level * StrengthGain);
            }
        }

        //agility
        public int Armor
        {
            get
            {
                return (int)(BaseArmor +
                    OverviewPageVM.AttributesRepository.AgilityArmorGain * (BaseAgility + Level * AgilityGain));
            }
        }

        public int AttackSpeed
        {
            get
            {
                return (int)(BaseAttackSpeed +
                    OverviewPageVM.AttributesRepository.AgilityAttackSpeedGain * (BaseAgility + Level * AgilityGain));
            }
        }

        //intelligence
        public int Mana
        {
            get
            {
                return (int)(BaseMana +
                    OverviewPageVM.AttributesRepository.IntelligenceManaGain * (BaseIntelligence + Level * IntelligenceGain));
            }
        }

        public float ManaRegen
        {
            get
            {
                return BaseManaRegen +
                    OverviewPageVM.AttributesRepository.IntelligenceManaRegenGain * (BaseIntelligence + Level * IntelligenceGain);
            }
        }

        public int MagicResistance
        {
            get
            {
                return (int)(BaseMagicResistance +
                    OverviewPageVM.AttributesRepository.IntelligenceMagicResistanceGain * (BaseIntelligence + Level * IntelligenceGain));
            }
        }

        //damage
        public int Damage
        {
            get
            {
                int damage = (BaseMinDamage + BaseMaxDamage) / 2;

                if (PrimaryAttribute == "str")
                {
                    damage += (int)(BaseStrength + Level * StrengthGain);
                }
                else if (PrimaryAttribute == "agi")
                {
                    damage += (int)(BaseAgility + Level * AgilityGain);
                }
                else if (PrimaryAttribute == "int")
                {
                    damage += (int)(BaseIntelligence + Level * IntelligenceGain);
                }
                else
                {
                    damage += (int)(((BaseStrength + Level * StrengthGain) + (BaseAgility + Level * AgilityGain) + (BaseIntelligence + Level * IntelligenceGain)) * OverviewPageVM.AttributesRepository.UniversalDamageGain);
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
