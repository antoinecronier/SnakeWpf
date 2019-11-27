using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SnakeWpf.Views.Base
{
    public abstract class BasePage : Page
    {
        public object Obj { get; set; }

        public BasePage()
        {
            this.Loaded += BasePage_Loaded;
        }

        private void BasePage_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        public Window GetWindow()
        {
            return Window.GetWindow(this);
        }
    }
}
