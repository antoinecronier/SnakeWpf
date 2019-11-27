using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace SnakeWpf.UserControls.Base
{
    public abstract class TileUC : UserControl
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public Grid Container { get; set; }
        
        public void MoveTo(int row, int column)
        {
            this.Row = row;
            this.Column = column;

            Application.Current.Dispatcher.Invoke(DispatcherPriority.Send, new ThreadStart(delegate
            {
                Grid.SetRow(this, row);
                Grid.SetColumn(this, column);
            }));
        }

        public void MoveUp()
        {
            if (this.Row - 1 >= 0)
            {
                this.MoveTo(this.Row - 1, this.Column);
            }
        }

        public void MoveRight()
        {
            if (this.Column + 1 < this.Container.ColumnDefinitions.Count)
            {
                this.MoveTo(this.Row, this.Column + 1);
            }
        }

        public void MoveLeft()
        {
            if (this.Column - 1 >= 0)
            {
                this.MoveTo(this.Row, this.Column - 1);
            }
        }

        public void MoveDown()
        {
            if (this.Row + 1 < this.Container.RowDefinitions.Count)
            {
                this.MoveTo(this.Row + 1, this.Column);
            }
        }

        public void AddToContainer()
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Send, new ThreadStart(delegate
            {
                this.Container.Children.Add(this);
            }));
        }

        public void AddToContainer(int row, int column)
        {
            this.MoveTo(row, column);
            this.AddToContainer();
        }
    }
}
