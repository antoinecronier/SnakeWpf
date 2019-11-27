using SnakeWpf.ViewModels;
using SnakeWpf.Views.Base;
using SnakeWpfClassLibrary.Entities;
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

namespace SnakeWpf.Views
{
    /// <summary>
    /// Logique d'interaction pour GameConfigPage.xaml
    /// </summary>
    public partial class GameConfigPage : BasePage
    {
        private GameConfigViewModel vm;

        public GameConfigPage()
        {
            InitializeComponent();
            vm = new GameConfigViewModel(this);            
            this.DataContext = vm;
            this.Loaded += GameConfigPage_Loaded;
        }

        private void GameConfigPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigating += NavigationService_Navigating;
            (this.Parent as NavigationWindow).ShowsNavigationUI = false;
        }

        private void NavigationService_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.Forward)
            {
                e.Cancel = true;
            }
            else if (e.NavigationMode == NavigationMode.New && this.Parent != null)
            {
                (this.Parent as NavigationWindow).ShowsNavigationUI = true;
            }
        }
    }
}
