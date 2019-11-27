using SnakeWpf.Views.Base;
using SnakeWpfClassLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeWpf.ViewModels.Base
{
    public abstract class BaseViewModel<TPage> where TPage : BasePage, new()
    {
        protected TPage currentPage;

        public void Navigate<T>() where T : BasePage, new()
        {
            currentPage.GetWindow().Content = new T();
        }

        public void Navigate<T>(GameConfig obj ) where T : BasePage, new()
        {
            currentPage.GetWindow().Content = new T();
        }

    }
}
