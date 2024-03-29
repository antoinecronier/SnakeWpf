﻿using SnakeWpf.UserControls.Base;
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
    public partial class SnakeTileUC : TileUC
    {
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

        public SnakeTileUC(Grid container, Color stroke, Color fill) : this(container)
        {
            this.tile.Stroke = new SolidColorBrush(stroke);
            this.tile.Fill = new SolidColorBrush(fill);
        }
    }
}
