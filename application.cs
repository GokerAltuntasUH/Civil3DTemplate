using Autodesk.AutoCAD.Runtime;
using System;
using System.IO;
using System.Reflection;
using System.Windows.Media.Imaging;

[assembly: ExtensionApplication(typeof(Civil3DTemplate.UI.RibbonUI))]

namespace Civil3DTemplate.UI
{
    /// <summary>
    /// Entry point of the plug-in. Civil 3D calls Initialize() when the DLL is loaded;
    /// the ribbon tab is created once the application is idle (i.e. the UI is ready).
    /// </summary>
    public class RibbonUI : IExtensionApplication
    {
        public void Initialize()
        {
            Autodesk.AutoCAD.ApplicationServices.Core.Application.Idle += OnIdle;
        }

        private void OnIdle(object? sender, EventArgs e)
        {
            Autodesk.AutoCAD.ApplicationServices.Core.Application.Idle -= OnIdle;
            CreateRibbon();
        }

        public void Terminate() { }

        private void CreateRibbon()
        {
            var ribbonControl = Autodesk.Windows.ComponentManager.Ribbon;
            if (ribbonControl == null) return;

            // Ribbon tab: "My Project"
            var myTab = new Autodesk.Windows.RibbonTab
            {
                Title = "My Project",
                Id = "MyProject_Tab"
            };
            ribbonControl.Tabs.Add(myTab);

            // Panel: "General"
            var generalPanelSource = new Autodesk.Windows.RibbonPanelSource
            {
                Title = "General"
            };

            System.Windows.Media.ImageSource message_32 = LoadPngFromResources("Civil3DTemplate.Resources.Message_32.png");

            // Button: "Show Message" -> runs the SHOW_MESSAGE command
            var showMessageButton = new Autodesk.Windows.RibbonButton
            {
                Text = "Show" + Environment.NewLine + "Message",
                Size = Autodesk.Windows.RibbonItemSize.Large,
                Orientation = System.Windows.Controls.Orientation.Vertical,
                LargeImage = message_32,
                ShowText = true,
                ShowImage = true,
                Id = "MyProject_ShowMessage",
                CommandHandler = new RibbonCommandHandler("SHOW_MESSAGE")
            };

            generalPanelSource.Items.Add(showMessageButton);

            var generalPanel = new Autodesk.Windows.RibbonPanel { Source = generalPanelSource };
            myTab.Panels.Add(generalPanel);

            // To add more buttons: create a command (see src/Commands/ShowMessage.cs),
            // an icon in Resources (32x32 PNG, Build Action = Embedded Resource),
            // and a RibbonButton as above.
        }

        private static System.Windows.Media.ImageSource LoadPngFromResources(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var stream = assembly.GetManifestResourceStream(resourceName)
                ?? throw new FileNotFoundException($"Resource '{resourceName}' not found.");

            stream.Position = 0;

            var image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = stream;
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.EndInit();
            image.Freeze();
            return image;
        }
    }
}
