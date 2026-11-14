using System;
using System.Windows.Input;
using Autodesk.AutoCAD.ApplicationServices;

namespace Civil3DTemplate.UI
{
    /// <summary>
    /// Connects a ribbon button to a Civil 3D command: clicking the button
    /// sends the command name to the command line of the active drawing.
    /// </summary>
    public class RibbonCommandHandler(string command) : ICommand
    {
        private readonly string _command = command;

        public bool CanExecute(object? parameter) => true;

        public void Execute(object? parameter)
        {
            var doc = Application.DocumentManager.MdiActiveDocument;
            if (doc != null)
            {
                doc.SendStringToExecute(_command + " ", true, false, false);
            }
        }

        public event EventHandler? CanExecuteChanged
        {
            add { }
            remove { }
        }
    }
}
