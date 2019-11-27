using SnakeWpf.UserControls;
using SnakeWpf.ViewModels.Base;
using SnakeWpf.Views;
using SnakeWpfClassLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace SnakeWpf.ViewModels
{
    public class GameViewModel : BaseViewModel<GamePage>
    {
        private const int PLAY_TIME_DEFAULT = 1000;

        private int NbofHits = 0;

        public GameConfig gameconfig;

        public Snake snake;
        CancellationTokenSource cTs = new CancellationTokenSource();

        public GameViewModel(GamePage gamepage, GameConfig gameConfig)
        {
            this.currentPage = gamepage;
            this.gameconfig = gameConfig;
        }

        private static AutoResetEvent gamerTimerReseter = new AutoResetEvent(false);
        public Action gameTimer = () =>
        {
            while (true)
            {
                Task.Delay(TimeSpan.FromMilliseconds(PLAY_TIME_DEFAULT)).Wait();
                gamerTimerReseter.Set();
            }
        };

        public void KeyDown(object sender, KeyEventArgs e)
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

        public void Snake_SnakeDeath(object sender, EventArgs e)
        {
            NbofHits = NbofHits + 1;

            if (NbofHits == gameconfig.LifeNumber)
            {
                MessageBox.Show("GAME OVER LOOSER !");
                Application.Current.Dispatcher.Invoke(DispatcherPriority.Send, new ThreadStart(delegate
                {
                    Navigate<GameConfigPage>();
                }));
            }
               
        }


    }
}
