using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolDevProject.WPF.Model;
using ToolDevProject.WPF.Repository;
using ToolDevProject.WPF.Repository.Api;
using ToolDevProject.WPF.Repository.Local;

namespace ToolDevProject.WPF.ViewModel
{
    internal class DetailPageVM : ObservableObject
    {
        //repos
        public static ILoreRepository LoreRepository { get; set; }
        public static IItemsRepository ItemsRepository { get; set; }
        public static IItemPopularitiesRepository ItemPopularitiesRepository { get; set; }

        private ILoreRepository _localLoreRepository { get; set; }
        private ILoreRepository _apiLoreRepository { get; set; }

        private IItemsRepository _localItemsRepository { get; set; }
        private IItemsRepository _apiItemsRepository { get; set; }

        private IItemPopularitiesRepository _localItemPopularitiesRepository { get; set; }
        private IItemPopularitiesRepository _apiItemPopularitiesRepository { get; set; }

        //refs to other VMs
        public MainViewModel MainVM { get; set; }

        //data
        public string CurrentHeroLore
        {
            get 
            {
                if (CurrentHero.Name == null) return "Name not loaded";
                return LoreRepository.GetLore(CurrentHero.ActualName); 
            }
        }

        public int CurrentHeroLevel
        {
            get { return CurrentHero.Level; }
            set 
            {
                CurrentHero.Level = value;

                OnPropertyChanged(nameof(CurrentHeroLore));
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
                OnPropertyChanged(nameof(CurrentHeroLore));
                OnPropertyChanged(nameof(CurrentHeroLevel));               
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


        //back command
        public RelayCommand BackCommand { get; set; }

        public void Back()
        {
            MainVM.SwitchPage();
        }

        //constructor
        public DetailPageVM()
        {
            CurrentHero = new MeleeHero();
            BackCommand = new RelayCommand(Back);

            _localLoreRepository = new LoreLocalRepository();
            _apiLoreRepository = new LoreApiRepository();

            _localItemsRepository = new ItemsLocalRepository();
            _apiItemsRepository = new ItemsApiRepository();

            _localItemPopularitiesRepository = new ItemPopularitiesLocalRepository();
            _apiItemPopularitiesRepository = new ItemPopularitiesApiRepository();

            LoreRepository = _localLoreRepository;
            ItemsRepository = _localItemsRepository;
            ItemPopularitiesRepository = _localItemPopularitiesRepository;

            LoadLore();
            LoadItems();
        }

        private async void LoadLore()
        {
            await LoreRepository.LoadLore();
        }

        private async void LoadItems()
        {
            await ItemsRepository.LoadItems();
        }
    }
}
