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
using System.Windows.Threading;

namespace SnakeWpf
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int PLAY_TIME_DEFAULT = 300;
        private const int SQUARE_SIZE = 20;
        private const int MAP_HEIGHT = 10;
        private const int MAP_WIDTH = 20;
        private const int FOOD_GENERATION_MAX = 15 * PLAY_TIME_DEFAULT;
        private const int FOOD_GENERATION_MIN = 5 * PLAY_TIME_DEFAULT;
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

        private Action foodGenerator(Grid mainGrid)
        {
            Random rand = new Random();
            while (true)
            {
                Task.Delay(TimeSpan.FromMilliseconds(rand.Next(FOOD_GENERATION_MIN,FOOD_GENERATION_MAX))).Wait();
                Application.Current.Dispatcher.Invoke(DispatcherPriority.Send, new ThreadStart(delegate
                {
                    List<UIElement> elements = null;
                    int row;
                    int column;
                    do
                    {
                        row = rand.Next(mainGrid.RowDefinitions.Count);
                        column = rand.Next(mainGrid.ColumnDefinitions.Count);

                        elements = mainGrid.Children.OfType<UIElement>().
                        Where(e => Grid.GetRow(e) == row
                            && Grid.GetColumn(e) == column).ToList();
                    } while (elements.Count != 0);
                    
                    FoodTileUC food = new FoodTileUC(mainGrid);
                    food.AddToContainer(row, column);
                }));
            }
        }

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
                    switch (e.Key)
                    {
                        case Key.Up:
                            gamerTimerReseter.Reset();
                            cTs.Cancel();
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
                            gamerTimerReseter.Reset();
                            cTs.Cancel();
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
                            gamerTimerReseter.Reset();
                            cTs.Cancel();
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
                            gamerTimerReseter.Reset();
                            cTs.Cancel();
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
            for (int i = 0; i < MAP_HEIGHT; i++)
            {
                RowDefinition row = new RowDefinition();
                this.MainGrid.RowDefinitions.Add(row);
            }

            for (int i = 0; i < MAP_WIDTH; i++)
            {
                ColumnDefinition column = new ColumnDefinition();
                this.MainGrid.ColumnDefinitions.Add(column);
            }

            snake = new Snake(this.MainGrid);
            snake.SnakeDeath += Snake_SnakeDeath;

            Task.Factory.StartNew(()=> {
                foodGenerator(this.MainGrid);
            });
        }

        private void Snake_SnakeDeath(object sender, EventArgs e)
        {
            Console.WriteLine("SnakeDeath " + DateTime.Now);
        }
    }
}
