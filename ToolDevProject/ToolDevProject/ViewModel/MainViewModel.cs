﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using ToolDevProject.WPF.Model;
using ToolDevProject.WPF.View;

namespace ToolDevProject.WPF.ViewModel
{
    internal class MainViewModel : ObservableObject
    {
        public OverviewPage MainPage;

        public DetailPage HeroPage;

        public Page CurrentPage { get; set; }

        public MainViewModel()
        {
            MainPage = new OverviewPage();
            (MainPage.DataContext as OverviewPageVM).MainVM = this;
            HeroPage = new DetailPage();
            CurrentPage = MainPage;
        }

        public void SwitchPage()
        {
            if (CurrentPage is OverviewPage)
            {
                BaseHero selectedHero = (MainPage.DataContext as OverviewPageVM).SelectedHero;
                if (selectedHero == null) return;

                DetailPageVM detail = (HeroPage.DataContext as DetailPageVM);
                detail.CurrentHero = selectedHero;
                (HeroPage.DataContext as DetailPageVM).CurrentHero = selectedHero;
                CurrentPage = HeroPage;
            }
            else
            {
                CurrentPage = MainPage;
            }
            OnPropertyChanged(nameof(CurrentPage));
        }
    }
}
