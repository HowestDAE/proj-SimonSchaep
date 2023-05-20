using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ToolDevProject.WPF.Model;
using ToolDevProject.WPF.Repository;
using ToolDevProject.WPF.View;

namespace ToolDevProject.WPF.ViewModel
{
    internal class OverviewPageVM : ObservableObject
    {
        //refs to other VMs
        public MainViewModel MainVM { get; set; }

        //repos
        public static IHeroesRepository HeroesRepository { get; set; }
        public static IAttributesRepository AttributesRepository { get; set; }

        private IHeroesRepository _localHeroesRepository { get; set; }
        private IHeroesRepository _apiHeroesRepository { get; set; }

        private IAttributesRepository _localAttributesRepository { get; set; }
        private IAttributesRepository _apiAttributesRepository { get; set; }

        //data
        private BaseHero _selectedHero;
        public BaseHero SelectedHero 
        {
            get
            {
                return _selectedHero;
            }
            set
            {
                _selectedHero = value;
                MainVM.SwitchPage();
            }
        }

        private List<BaseHero> _heroes;
        public List<BaseHero> Heroes
        {
            get
            {
                return _heroes;
            }
            set
            {
                _heroes = value;
                OnPropertyChanged(nameof(Heroes));
            }
        }

        //Switch repo
        public string SwitchRepoText
        {
            get
            {
                if (HeroesRepository is HeroesLocalRepository)
                {
                    return "Switch to API";
                }
                else
                {
                    return "Switch to Local";
                }
            }
        }
        public RelayCommand SwitchRepositoryCommand { get; set; }

        public void SwitchRepositories()
        {
            if(HeroesRepository is HeroesLocalRepository)
            {
                HeroesRepository = _apiHeroesRepository;
            }
            else
            {
                HeroesRepository = _localHeroesRepository;
            }
            LoadHeroes();

            //no api repository for attributes, since that data isn't available yet on the api
            //if (AttributesRepository is AttributesLocalRepository)
            //{
            //    AttributesRepository = _apiAttributesRepository;
            //}
            //else
            //{
            //    AttributesRepository = _localAttributesRepository;
            //}
            //LoadAttributes();

            OnPropertyChanged(nameof(SwitchRepoText));
        }

        //constructor
        public OverviewPageVM()
        {
            SwitchRepositoryCommand = new RelayCommand(SwitchRepositories);

            _localHeroesRepository = new HeroesLocalRepository();
            _apiHeroesRepository = new HeroesApiRepository();

            _localAttributesRepository = new AttributesLocalRepository();
            _apiAttributesRepository = new AttributesApiRepository();

            HeroesRepository = _localHeroesRepository;
            AttributesRepository = _localAttributesRepository;

            LoadHeroes();
            LoadAttributes();
        }

        //funcs
        private async void LoadHeroes()
        {
            Heroes = await HeroesRepository.GetHeroes();
        }

        private async void LoadAttributes()
        {
            await AttributesRepository.LoadAttributes();
        }
    }
}
