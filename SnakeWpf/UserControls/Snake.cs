using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace SnakeWpf.UserControls
{
    public class Snake
    {
        private Grid mainGrid;

        public List<SnakeTileUC> Tiles { get; set; }

        public Snake(Grid mainGrid)
        {
            this.mainGrid = mainGrid;
            this.Tiles = new List<SnakeTileUC>();
            SnakeTileUC baseTile = new SnakeTileUC(this.mainGrid, Color.FromRgb(0, 255, 255), Color.FromRgb(0, 255, 111));
            baseTile.MoveTo(this.mainGrid.RowDefinitions.Count / 2, this.mainGrid.ColumnDefinitions.Count / 2);
            //GenerateMoreTiles(baseTile);

            this.Tiles.Add(baseTile);

            this.mainGrid.Children.Add(baseTile);
        }

        private void GenerateMoreTiles(SnakeTileUC baseTile)
        {
            for (int i = 1; i < 7; i++)
            {
                SnakeTileUC uc = new SnakeTileUC(this.mainGrid);
                uc.MoveTo(baseTile.Row + i, baseTile.Column);
                this.Tiles.Add(uc);

                Application.Current.Dispatcher.Invoke(DispatcherPriority.Send, new ThreadStart(delegate
                {
                    this.mainGrid.Children.Add(uc);
                }));
            }
        }

        public void MoveUp()
        {
            int lastRow;
            int lastColumn;

            SnakeTileUC firstUc = this.Tiles.ElementAt(0);
            lastRow = firstUc.Row;
            lastColumn = firstUc.Column;

            firstUc.MoveUp();
            DidFoundFood(firstUc);
            MoveOthers(lastRow, lastColumn);
        }

        public void MoveRight()
        {
            int lastRow;
            int lastColumn;

            SnakeTileUC firstUc = this.Tiles.ElementAt(0);
            lastRow = firstUc.Row;
            lastColumn = firstUc.Column;

            firstUc.MoveRight();
            DidFoundFood(firstUc);
            MoveOthers(lastRow, lastColumn);
        }

        public void MoveLeft()
        {
            int lastRow;
            int lastColumn;

            SnakeTileUC firstUc = this.Tiles.ElementAt(0);
            lastRow = firstUc.Row;
            lastColumn = firstUc.Column;

            firstUc.MoveLeft();
            DidFoundFood(firstUc);
            MoveOthers(lastRow, lastColumn);
        }

        public void MoveDown()
        {
            int lastRow;
            int lastColumn;

            SnakeTileUC firstUc = this.Tiles.ElementAt(0);
            lastRow = firstUc.Row;
            lastColumn = firstUc.Column;

            firstUc.MoveDown();
            DidFoundFood(firstUc);
            MoveOthers(lastRow, lastColumn);
        }

        private void DidFoundFood(SnakeTileUC snakeHeadTile)
        {
            List<UIElement> food = null;

            Task.Factory.StartNew(()=>
            {
                Application.Current.Dispatcher.Invoke(DispatcherPriority.Send, new ThreadStart(delegate
                {
                    food = this.mainGrid.Children.OfType<UIElement>().
                    Where(e => Grid.GetRow(e) == snakeHeadTile.Row
                        && Grid.GetColumn(e) == snakeHeadTile.Column).ToList();
                }));
            }).ContinueWith((s)=>
            {
                if (food != null && food.OfType<FoodTileUC>().Count() > 0)
                {
                    Application.Current.Dispatcher.Invoke(DispatcherPriority.Send, new ThreadStart(delegate
                    {
                        SnakeTileUC uc = new SnakeTileUC(this.mainGrid);
                        uc.AddToContainer(snakeHeadTile.Row, snakeHeadTile.Column);
                        this.Tiles.Add(uc);
                        this.mainGrid.Children.Remove(food.OfType<FoodTileUC>().First());
                    }));
                }
            });
        }

        private void MoveOthers(int lastRow, int lastColumn)
        {
            for (int i = 1; i < this.Tiles.Count; i++)
            {
                SnakeTileUC currentUc = this.Tiles.ElementAt(i);

                int nextRow = currentUc.Row;
                int nextColumn = currentUc.Column;

                currentUc.MoveTo(lastRow, lastColumn);

                lastRow = nextRow;
                lastColumn = nextColumn;
            }

            this.CheckDeath();
        }

        private void CheckDeath()
        {
            for (int i = 0; i < this.Tiles.Count; i++)
            {
                for (int j = 0; j < this.Tiles.Count; j++)
                {
                    if (this.Tiles.ElementAt(i) != this.Tiles.ElementAt(j))
                    {
                        if (this.Tiles.ElementAt(i).Row == this.Tiles.ElementAt(j).Row
                            &&
                            this.Tiles.ElementAt(i).Column == this.Tiles.ElementAt(j).Column)
                        {
                            OnSnakeDeath(new EventArgs());
                            return;
                        }
                    }
                }
            }
        }

        public event EventHandler SnakeDeath;

        protected virtual void OnSnakeDeath(EventArgs e)
        {
            EventHandler handler = SnakeDeath;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
