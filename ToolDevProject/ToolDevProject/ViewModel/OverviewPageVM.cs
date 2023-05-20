using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolDevProject.WPF.Repository;

namespace ToolDevProject.WPF.ViewModel
{
    internal class OverviewPageVM
    {
        public static IHeroesRepository HeroesRepository { get; set; }
        public static IAttributesRepository AttributesRepository { get; set; }

        public OverviewPageVM()
        {
            //_repository = new LocalRepository();
            HeroesRepository = new HeroesApiRepository();

            AttributesRepository = new AttributesLocalRepository();
            //AttributesRepository = new AttributesApiRepository();

            LoadHeroes();
        }

        private async void LoadHeroes()
        {
            var heroes = await HeroesRepository.GetHeroes();
        }
    }
}
