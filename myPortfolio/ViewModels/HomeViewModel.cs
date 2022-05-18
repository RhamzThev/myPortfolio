using myPortfolio.Commands;
using myPortfolio.Models;
using myPortfolio.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace myPortfolio.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {

        public string Name
        {
            get
            {
                return User.Name;
            }
        }

        public List<Game> Games {
            get
            {
                return Game.Games;
            } 
        }

        public ICommand SignOutCommand { get; }
        public ICommand ProfileCommand { get; }

        public HomeViewModel(Navigation navigation)
        {
            SignOutCommand = new SignOutCommand(navigation);
            ProfileCommand = new ProfileCommand(navigation);
        }
    }
}
