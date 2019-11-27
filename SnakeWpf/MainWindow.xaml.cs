using SnakeWpf.UserControls;
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

namespace SnakeWpf
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SnakeTileUC tile;

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
            EventManager.RegisterClassHandler(typeof(Window),
            Keyboard.KeyDownEvent, new KeyEventHandler(KeyDown), true);
        }

        private void KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up:
                    tile.MoveUp();
                    break;
                case Key.Right:
                    tile.MoveRight();
                    break;
                case Key.Left:
                    tile.MoveLeft();
                    break;
                case Key.Down:
                    tile.MoveDown();
                    break;
                default:
                    break;
            }
        }

        

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
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

            tile = new SnakeTileUC(this.MainGrid);
            tile.MoveTo(this.MainGrid.RowDefinitions.Count / 2, this.MainGrid.ColumnDefinitions.Count / 2);
            
            this.MainGrid.Children.Add(tile);
        }
    }
}
