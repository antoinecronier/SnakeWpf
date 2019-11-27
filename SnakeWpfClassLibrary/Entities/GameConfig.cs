using SnakeWpfClassLibrary.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeWpfClassLibrary.Entities
{
    public class GameConfig : EntityNotifierBase
    {

        private int score;

        private float gridrate;

        private float snakespeed;

        private int foodappeartime;

        private int lifenumber;

        public int Score
        {
            get => score;
            set { score = value; OnPropertyChanged("Score"); }
        }

        public float GridRate
        {
            get => gridrate;
            set { gridrate = value; OnPropertyChanged("GridRate"); }
        }

        public float SnakeSpeed
        {
            get => snakespeed;
            set { snakespeed = value; OnPropertyChanged("SnakeSpeed"); }
        }

        public int FoodAppearTime
        {
            get => foodappeartime;
            set { foodappeartime = value; OnPropertyChanged("FoodAppearTime"); }
        }

        public int LifeNumber
        {
            get => lifenumber;
            set { lifenumber = value; OnPropertyChanged("LifeNumber"); }
        }
    }
}

      
