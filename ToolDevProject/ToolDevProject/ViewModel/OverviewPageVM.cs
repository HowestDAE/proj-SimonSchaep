﻿using CommunityToolkit.Mvvm.ComponentModel;
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
                OnPropertyChanged(nameof(SelectedHero));
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

        private List<string> _roles;
        public List<string> Roles
        {
            get
            {
                return _roles;
            }
            set
            {
                _roles = value;
                OnPropertyChanged(nameof(Roles));
            }
        }

        private List<string> _attributes;
        public List<string> Attributes
        {
            get
            {
                return _attributes;
            }
            set
            {
                _attributes = value;
                OnPropertyChanged(nameof(Attributes));
            }
        }

        //filters
        private string _selectedAttribute = "all";
        public string SelectedAttribute
        {
            get { return _selectedAttribute; }
            set
            {
                _selectedAttribute = value;
                UpdateFilters();
            }
        }

        private string _selectedRole = "all";
        public string SelectedRole
        {
            get { return _selectedRole; }
            set
            {
                _selectedRole = value;
                UpdateFilters();
            }
        }

        private string _selectedNameContains = "";
        public string SelectedNameContains
        {
            get { return _selectedNameContains; }
            set
            {
                _selectedNameContains = value;
                UpdateFilters();
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
            UpdateFilters();

            //no api repository for attributes, since that data isn't available yet on the api
            //if (AttributesRepository is AttributesLocalRepository)
            //{
            //    AttributesRepository = _apiAttributesRepository;
            //}
            //else
            //{
            //    AttributesRepository = _localAttributesRepository;
            //}
            //LoadAttributeStats();

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
            LoadRoles();
            LoadAttributeStats();
        }

        //funcs
        private async void LoadHeroes()
        {
            Heroes = await HeroesRepository.GetHeroes();
        }

        private async void LoadAttributes()
        {
            Attributes = await HeroesRepository.GetAttributes();
        }

        private async void LoadRoles()
        {
            Roles = await HeroesRepository.GetRoles();
        }

        private async void LoadAttributeStats()
        {
            await AttributesRepository.LoadAttributes();
        }

        private void UpdateFilters()
        {
            Heroes = HeroesRepository.GetHeroes(SelectedAttribute, SelectedRole, SelectedNameContains);
        }
    }
}
