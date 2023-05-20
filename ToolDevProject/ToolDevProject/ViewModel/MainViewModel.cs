using CommunityToolkit.Mvvm.ComponentModel;
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
        private OverviewPage _mainPage = new OverviewPage();

        public OverviewPage MainPage { get => _mainPage; set => _mainPage = value; }

        private DetailPage _heroPage;

        public DetailPage HeroPage { get => _heroPage; set => _heroPage = value; }

        private Page _currentPage;
        public Page CurrentPage { get => _currentPage; set => _currentPage = value; }

        private RelayCommand _switchPageCommand;
        public RelayCommand SwitchPageCommand { get => _switchPageCommand; set => _switchPageCommand = value; }

        public MainViewModel()
        {
            SwitchPageCommand = new RelayCommand(SwitchPage);
            MainPage = new OverviewPage();
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
