using SnakeWpf.ViewModels.Base;
using SnakeWpf.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SnakeWpf.ViewModels.ICommands
{
    public class StartGameCommand : ICommand
    {
        private GameConfigViewModel gameconfigviewmodel;

        public event EventHandler CanExecuteChanged;


        public StartGameCommand(GameConfigViewModel obj)
        {
            this.gameconfigviewmodel = obj;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object sender)
        {
            //GamePage gamepage = new GamePage();
            gameconfigviewmodel.Navigate<GamePage>();
        }
    }
}
