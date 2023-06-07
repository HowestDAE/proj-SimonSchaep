using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
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
        private string _currentHeroLore;
        public string CurrentHeroLore
        {
            get
            {
                return _currentHeroLore;
            }
            set
            {
                SetCurrentHeroLore();
            }
        }

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

                SetCurrentHeroLore();

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

        //Switch repo
        public void SwitchRepositories(bool isApi)
        {
            if (isApi)
            {
                LoreRepository = _apiLoreRepository;
                ItemsRepository = _apiItemsRepository;
                ItemPopularitiesRepository = _apiItemPopularitiesRepository;
            }
            else
            {
                LoreRepository = _localLoreRepository;
                ItemsRepository = _localItemsRepository;
                ItemPopularitiesRepository = _localItemPopularitiesRepository;
            }

            LoadLore();
            LoadItems();
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

            OnPropertyChanged(nameof(CurrentHeroLore));
        }

        private async void LoadItems()
        {
            await ItemsRepository.LoadItems();
        }

        private async void SetCurrentHeroLore()
        {
            if (CurrentHero.ActualName == null)
            {
                _currentHeroLore = "Name not loaded";
            }
            else
            {
                _currentHeroLore = await LoreRepository.GetLore(CurrentHero.ActualName);
            }

            OnPropertyChanged(nameof(CurrentHeroLore));
        }
    }
}
