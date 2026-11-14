using System.Windows.Forms;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Runtime;

namespace Civil3DTemplate.Commands
{
    public class ShowMessage
    {
        /// <summary>
        /// Shows a message window in Civil 3D.
        /// Run it from the "Show Message" ribbon button or type SHOW_MESSAGE.
        /// </summary>
        [CommandMethod("SHOW_MESSAGE")]
        public void Run()
        {
            Document? doc = Autodesk.AutoCAD.ApplicationServices.Core.Application.DocumentManager.MdiActiveDocument;
            if (doc == null) return;

            MessageBox.Show(
                "Hello from my project!\n\nThe Civil 3D Template plug-in is working.",
                "My Project",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
    }
}
