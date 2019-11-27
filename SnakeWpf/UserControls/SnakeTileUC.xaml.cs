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
using System.Windows.Threading;

namespace SnakeWpf.UserControls
{
    /// <summary>
    /// Logique d'interaction pour SnakeUC.xaml
    /// </summary>
    public partial class SnakeTileUC : UserControl
    {
        public int Row { get; set; }
        public int Column { get; set; }
        //public int PreviousRow { get; set; }
        //public int PreviousColumn { get; set; }
        public Grid Container { get; set; }

        public SnakeTileUC()
        {
            InitializeComponent();
            this.tile.Stroke = new SolidColorBrush(Color.FromRgb(0, 111, 0));
            this.tile.Fill = new SolidColorBrush(Color.FromRgb(0, 111, 111));
        }

        public SnakeTileUC(Grid container) : this()
        {
            this.Container = container;
        }

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
    }
}
