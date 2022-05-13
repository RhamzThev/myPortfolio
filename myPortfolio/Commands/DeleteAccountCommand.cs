using myPortfolio.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace myPortfolio.Commands
{
    public class DeleteAccountCommand : BaseCommand
    {
        private readonly Navigation _navigation;

        public DeleteAccountCommand(Navigation navigation)
        {
            _navigation = navigation;
        }

        public override void Execute(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}
