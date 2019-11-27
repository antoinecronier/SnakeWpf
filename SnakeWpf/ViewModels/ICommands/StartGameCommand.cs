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
        public event EventHandler CanExecuteChanged;

        public StartGameCommand()
        {
            Console.WriteLine("StartGameCommand");
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object sender)
        {
            //GamePage gamepage = new GamePage();
            Console.WriteLine("StartGameCommand");
        }
    }
}
