using SnakeWpf.UserControls;
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
    /// Logique d'interaction pour GamePage.xaml
    /// </summary>
    public partial class GamePage : BasePage
    {
        private GameViewModel vm;

        public GamePage()
        {
            InitializeComponent();
            vm = new GameViewModel(this);
            this.DataContext = vm;
            this.Loaded += GamePage_Loaded;
            EventManager.RegisterClassHandler(typeof(Window),
            Keyboard.KeyDownEvent, new KeyEventHandler(vm.KeyDown), true);
            Task.Factory.StartNew(vm.gameTimer);
        }

        public GamePage(GameConfig obj) : this()
        {
            this.vm.gameconfig = obj;
        }

        private void GamePage_Loaded(object sender, RoutedEventArgs e)
        {
            this.MainGrid.Background = new SolidColorBrush(Color.FromArgb(50, 100, 150, 100));
            for (int i = 0; i < 16; i++)
            {
                this.MainGrid.RowDefinitions.Add(new RowDefinition());
            }

            for (int i = 0; i < 16; i++)
            {
                this.MainGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            vm.snake = new Snake(this.MainGrid);
            vm.snake.SnakeDeath += vm.Snake_SnakeDeath;
        }
    }
}
