using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ToolDevProject.WPF.Repository;

namespace ToolDevProject.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IRepository _repository;

        public MainWindow()
        {
            InitializeComponent();

            //_repository = new LocalRepository();
            _repository = new ApiRepository();
            
            LoadHeroes();
        }

        private async void LoadHeroes()
        {
            var heroes = await _repository.GetHeroes();
        }
    }
}
