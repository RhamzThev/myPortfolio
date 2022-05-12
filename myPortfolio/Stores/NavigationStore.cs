using myPortfolio.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace myPortfolio.Stores
{
    public class NavigationStore
    {
        public BaseViewModel CurrentViewModel { get; set; }
    }
}
