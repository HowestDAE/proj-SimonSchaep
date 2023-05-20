using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolDevProject.WPF.Model;

namespace ToolDevProject.WPF.ViewModel
{
    internal class DetailPageVM : ObservableObject
    {
        public int CurrentHeroLevel
        {
            get { return CurrentHero.Level; }
            set 
            {
                CurrentHero.Level = value;

                OnPropertyChanged(nameof(CurrentHeroLevel));
                OnPropertyChanged(nameof(CurrentHero));
                OnPropertyChanged(nameof(CurrentMeleeHero));
                OnPropertyChanged(nameof(CurrentRangedHero));
            }
        }


        private BaseHero _currentHero;
        public BaseHero CurrentHero
        {
            get { return _currentHero; }
            set
            {
                _currentHero = value;
                OnPropertyChanged(nameof(CurrentHero));
                OnPropertyChanged(nameof(CurrentMeleeHero));
                OnPropertyChanged(nameof(CurrentRangedHero));
            }
        }

        public MeleeHero CurrentMeleeHero
        {
            get { return _currentHero as MeleeHero; }
        }

        public RangedHero CurrentRangedHero
        {
            get { return _currentHero as RangedHero; }
        }

        public DetailPageVM()
        {
            CurrentHero = new MeleeHero();
        }
    }
}
