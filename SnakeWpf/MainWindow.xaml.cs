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
                                        cTs.Token.ThrowIfCancellationRequested();
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
                                        cTs.Token.ThrowIfCancellationRequested();
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
                                        cTs.Token.ThrowIfCancellationRequested();
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
                                        cTs.Token.ThrowIfCancellationRequested();
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
                this.MainGrid.RowDefinitions.Add(new RowDefinition());
            }

            for (int i = 0; i < 16; i++)
            {
                this.MainGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            snake = new Snake(this.MainGrid);
            snake.SnakeDeath += Snake_SnakeDeath;
        }

        private void Snake_SnakeDeath(object sender, EventArgs e)
        {
            Console.WriteLine("SnakeDeath " + DateTime.Now);
        }
    }
}
