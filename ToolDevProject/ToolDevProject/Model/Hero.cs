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
        public enum AttributeType
        {
            strength,
            agility,
            intelligence,
            universal,
        }

        //info
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "localized_name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "img")]
        public string ImageUrl { get; set; }

        [JsonProperty(PropertyName = "icon")]
        public string IconUrl { get; set; }


        //other info
        [JsonProperty(PropertyName = "roles")]
        public List<string> Roles { get; set; }

        [JsonProperty(PropertyName = "primary_attr")]
        public AttributeType PrimaryAttribute { get; set; }


        //static stats
        [JsonProperty(PropertyName = "attack_range")]
        public int AttackRange { get; set; }

        [JsonProperty(PropertyName = "move_speed")]
        public int MoveSpeed { get; set; }


        //private stats
        [JsonProperty(PropertyName = "base_health")]
        private int _baseHealth;

        [JsonProperty(PropertyName = "base_health_regen")]
        private float _baseHealthRegen;

        [JsonProperty(PropertyName = "base_mana")]
        private int _baseMana;

        [JsonProperty(PropertyName = "base_mana_regen")]
        private float _baseManaRegen;

        [JsonProperty(PropertyName = "base_attack_min")]
        private int _baseMinDamage;

        [JsonProperty(PropertyName = "base_attack_max")]
        private int _baseMaxDamage;

        [JsonProperty(PropertyName = "base_attack_time")]
        private int _baseAttackSpeed;

        [JsonProperty(PropertyName = "base_armor")]
        private int _baseArmor;

        [JsonProperty(PropertyName = "base_mr")]
        private int _baseMagicResistance;


        //attributes
        [JsonProperty(PropertyName = "base_str")]
        private int _baseStrength;

        [JsonProperty(PropertyName = "base_agi")]
        private int _baseAgility;

        [JsonProperty(PropertyName = "base_int")]
        private int _baseIntelligence;

        [JsonProperty(PropertyName = "str_gain")]
        private float _strengthGain;

        [JsonProperty(PropertyName = "agi_gain")]
        private float _agilityGain;

        [JsonProperty(PropertyName = "int_gain")]
        private float _intelligenceGain;


        //public stats
        public int Level { get; set; }

        //strength
        public int Health
        {
            get
            {
                return (int)(_baseHealth +
                    OverviewPageVM.AttributesRepository.StrengthHealthGain * (_baseStrength + Level * _strengthGain));
            }
        }

        public float HealthRegen
        {
            get
            {
                return _baseHealthRegen +
                    OverviewPageVM.AttributesRepository.StrengthHealthRegenGain * (_baseStrength + Level * _strengthGain);
            }
        }

        //agility
        public int Armor
        {
            get
            {
                return (int)(_baseArmor +
                    OverviewPageVM.AttributesRepository.AgilityArmorGain * (_baseAgility + Level * _agilityGain));
            }
        }

        public int AttackSpeed
        {
            get
            {
                return (int)(_baseAttackSpeed +
                    OverviewPageVM.AttributesRepository.AgilityAttackSpeedGain * (_baseAgility + Level * _agilityGain));
            }
        }

        //intelligence
        public int Mana
        {
            get
            {
                return (int)(_baseMana +
                    OverviewPageVM.AttributesRepository.IntelligenceManaGain * (_baseIntelligence + Level * _intelligenceGain));
            }
        }

        public float ManaRegen
        {
            get
            {
                return _baseManaRegen +
                    OverviewPageVM.AttributesRepository.IntelligenceManaRegenGain * (_baseIntelligence + Level * _intelligenceGain);
            }
        }

        public int MagicResistance
        {
            get
            {
                return (int)(_baseMagicResistance +
                    OverviewPageVM.AttributesRepository.IntelligenceMagicResistanceGain * (_baseIntelligence + Level * _intelligenceGain));
            }
        }

        //damage
        public int Damage
        {
            get
            {
                int damage = (_baseMinDamage + _baseMaxDamage) / 2;

                switch (PrimaryAttribute)
                {
                    case AttributeType.strength:
                        damage += (int)(_baseStrength + Level * _strengthGain);
                        break;
                    case AttributeType.agility:
                        damage += (int)(_baseAgility + Level * _agilityGain);
                        break;
                    case AttributeType.intelligence:
                        damage += (int)(_baseIntelligence + Level * _intelligenceGain);
                        break;
                    case AttributeType.universal:
                        damage += (int)(((_baseStrength + Level * _strengthGain) + (_baseAgility + Level * _agilityGain) + (_baseIntelligence + Level * _intelligenceGain)) * OverviewPageVM.AttributesRepository.UniversalDamageGain);
                        break;
                }

                return damage;
            }
        }

    }

    public class MeleeHero : BaseHero
    {
        //empty until we need some melee specific stat (could be damage block, or different interactions with items)
    }

    public class RangedHero : BaseHero
    {
        [JsonProperty(PropertyName = "projectile_speed")]
        public int ProjectileSpeed { get; set; }
    }
}
