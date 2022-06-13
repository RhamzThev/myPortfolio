using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace myPortfolio.Commands
{
    /// <summary>
    /// Class <c>BaseCommand</c> inherits <c>ICommand</c>. A Base Command class for inherited higher-level commands.
    /// </summary>
    public abstract class BaseCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public virtual bool CanExecute(object parameter)
        {
            return true;
        }

        public abstract void Execute(object parameter);

        /// <summary>
        /// Defines a method that is called when changes occur that affect whether or not the command should execute.
        /// </summary>
        protected void OnCanExecutedChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
