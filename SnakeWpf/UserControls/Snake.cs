using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
            SnakeTileUC baseTile = new SnakeTileUC(this.mainGrid);
            baseTile.MoveTo(this.mainGrid.RowDefinitions.Count / 2, this.mainGrid.ColumnDefinitions.Count / 2);
            GenerateMoreTiles(baseTile);

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
            MoveOthers(lastRow, lastColumn);
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
