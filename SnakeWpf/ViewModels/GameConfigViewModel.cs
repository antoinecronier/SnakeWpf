using SnakeWpf.ViewModels.Base;
using SnakeWpf.ViewModels.ICommands;
using SnakeWpf.Views;
using SnakeWpfClassLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeWpf.ViewModels
{
    public class GameConfigViewModel : BaseViewModel<GameConfigPage>
    {
        public GameConfig GameConfig { get; set; }

        public StartGameCommand StartGame { get; set; }

        public GameConfigViewModel(GameConfigPage gameconfigpage)
        {
            this.currentPage = gameconfigpage;
            this.GameConfig = new GameConfig();
            this.LoadItems();
        }

        private void LoadItems()
        {
            GameConfig.Score = 1000;
            GameConfig.GridRate = 80;
            GameConfig.SnakeSpeed = 40;
            GameConfig.FoodAppearTime = 3;
            GameConfig.LifeNumber = 3;

            this.StartGame = new StartGameCommand();

        }
    }


}
