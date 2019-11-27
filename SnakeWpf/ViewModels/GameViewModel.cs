using SnakeWpf.UserControls;
using SnakeWpf.ViewModels.Base;
using SnakeWpf.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SnakeWpf.ViewModels
{
    public class GameViewModel : BaseViewModel<GamePage>
    {
        private const int PLAY_TIME_DEFAULT = 1000;
        public Snake snake;
        CancellationTokenSource cTs = new CancellationTokenSource();

        public GameViewModel(GamePage gamepage)
        {
            this.currentPage = gamepage;
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
            Console.WriteLine("SnakeDeath " + DateTime.Now);
        }


    }
}
