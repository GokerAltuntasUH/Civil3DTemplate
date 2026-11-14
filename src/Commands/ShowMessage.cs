using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;

namespace Civil3DTemplate.Commands
{
    public class ShowMessage
    {
        /// <summary>
        /// Writes a short message to the Civil 3D command line.
        /// Run it from the "Show Message" ribbon button or type SHOW_MESSAGE.
        /// </summary>
        [CommandMethod("SHOW_MESSAGE")]
        public void Run()
        {
            Document? doc = Autodesk.AutoCAD.ApplicationServices.Core.Application.DocumentManager.MdiActiveDocument;
            if (doc == null) return;

            Editor ed = doc.Editor;
            ed.WriteMessage("\nHello from my project! The Civil 3D Template plug-in is working.");
        }
    }
}
