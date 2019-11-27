using SnakeWpf.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
        private const int PLAY_TIME_DEFAULT = 1000;
        private const int SQUARE_SIZE = 20;
        private Snake snake;

        private static AutoResetEvent gamerTimerReseter = new AutoResetEvent(false);
        private Action gameTimer = () =>
        {
            while (true)
            {
                Task.Delay(TimeSpan.FromMilliseconds(PLAY_TIME_DEFAULT)).Wait();
                gamerTimerReseter.Set();
            }
        };

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
            EventManager.RegisterClassHandler(typeof(Window),
            Keyboard.KeyDownEvent, new KeyEventHandler(KeyDown), true);
            Task.Factory.StartNew(gameTimer);
        }

        CancellationTokenSource cTs = new CancellationTokenSource();

        private void KeyDown(object sender, KeyEventArgs e)
        {
            Task.Factory.StartNew(() =>
            {
                if (gamerTimerReseter.WaitOne(PLAY_TIME_DEFAULT))
                {
                    gamerTimerReseter.Reset();
                    cTs.Cancel();
                    switch (e.Key)
                    {
                        case Key.Up:
                            cTs = new CancellationTokenSource();
                            Task.Factory.StartNew(() =>
                            {
                                while (true)
                                {
                                    if (cTs.IsCancellationRequested)
                                    {
                                        //cTs.Token.ThrowIfCancellationRequested();
                                        return;
                                    }
                                    snake.MoveUp();
                                    Task.Delay(TimeSpan.FromMilliseconds(PLAY_TIME_DEFAULT)).Wait(cTs.Token);
                                }
                            }, cTs.Token);
                            break;
                        case Key.Right:
                            cTs = new CancellationTokenSource();
                            Task.Factory.StartNew(() =>
                            {
                                while (true)
                                {
                                    if (cTs.IsCancellationRequested)
                                    {
                                        //cTs.Token.ThrowIfCancellationRequested();
                                        return;
                                    }
                                    snake.MoveRight();
                                    Task.Delay(TimeSpan.FromMilliseconds(PLAY_TIME_DEFAULT)).Wait(cTs.Token);
                                }
                            }, cTs.Token);
                            break;
                        case Key.Left:
                            cTs = new CancellationTokenSource();
                            Task.Factory.StartNew(() =>
                            {
                                while (true)
                                {
                                    if (cTs.IsCancellationRequested)
                                    {
                                        //cTs.Token.ThrowIfCancellationRequested();
                                        return;
                                    }
                                    snake.MoveLeft();
                                    Task.Delay(TimeSpan.FromMilliseconds(PLAY_TIME_DEFAULT)).Wait(cTs.Token);
                                }
                            }, cTs.Token);
                            break;
                        case Key.Down:
                            cTs = new CancellationTokenSource();
                            Task.Factory.StartNew(() =>
                            {
                                while (true)
                                {
                                    if (cTs.IsCancellationRequested)
                                    {
                                        //cTs.Token.ThrowIfCancellationRequested();
                                        return;
                                    }
                                    snake.MoveDown();
                                    Task.Delay(TimeSpan.FromMilliseconds(PLAY_TIME_DEFAULT)).Wait(cTs.Token);
                                }
                            }, cTs.Token);
                            break;
                        default:
                            break;

                    }
                }
            });
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.MainGrid.Background = new SolidColorBrush(Color.FromArgb(50, 100, 150, 100));
            for (int i = 0; i < 16; i++)
            {
                RowDefinition row = new RowDefinition();
                this.MainGrid.RowDefinitions.Add(row);
            }

            for (int i = 0; i < 16; i++)
            {
                ColumnDefinition column = new ColumnDefinition();
                this.MainGrid.ColumnDefinitions.Add(column);
            }

            snake = new Snake(this.MainGrid);
            snake.SnakeDeath += Snake_SnakeDeath;

            FoodTileUC food1 = new FoodTileUC(this.MainGrid);
            food1.AddToContainer(1, 1);
            FoodTileUC food2 = new FoodTileUC(this.MainGrid);
            food2.AddToContainer(14, 14);
            FoodTileUC food3 = new FoodTileUC(this.MainGrid);
            food3.AddToContainer(1, 14);
            FoodTileUC food4 = new FoodTileUC(this.MainGrid);
            food4.AddToContainer(14, 1);

            FoodTileUC food5 = new FoodTileUC(this.MainGrid);
            food5.AddToContainer(5, 5);
            FoodTileUC food6 = new FoodTileUC(this.MainGrid);
            food6.AddToContainer(10, 10);
            FoodTileUC food7 = new FoodTileUC(this.MainGrid);
            food7.AddToContainer(5, 10);
            FoodTileUC food8 = new FoodTileUC(this.MainGrid);
            food8.AddToContainer(10, 5);
        }

        private void Snake_SnakeDeath(object sender, EventArgs e)
        {
            Console.WriteLine("SnakeDeath " + DateTime.Now);
        }
    }
}
