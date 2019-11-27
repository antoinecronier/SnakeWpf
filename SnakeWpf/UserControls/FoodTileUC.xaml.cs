using SnakeWpf.UserControls.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace SnakeWpf.UserControls
{
    /// <summary>
    /// Logique d'interaction pour FoodTileUC.xaml
    /// </summary>
    public partial class FoodTileUC : TileUC
    {
        public FoodTileUC()
        {
            InitializeComponent();
            this.tile.Stroke = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            this.tile.Fill = new SolidColorBrush(Color.FromRgb(0, 111, 111));
        }

        public FoodTileUC(Grid container) : this()
        {
            this.Container = container;
        }
    }
}
