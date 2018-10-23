using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AlteryxGuiToolkit.Plugins;

namespace ContinuumXmlValidator
{
    public class XmlValidator : IPlugin
    {

        private System.Drawing.Bitmap _icon;
        private string _iconResource = "ContinuumXmlValidator.Resources.XMLValidator_171.png";

        public IPluginConfiguration GetConfigurationGui()
        {
            return new XmlValidatorUserControl();
        }

        public EntryPoint GetEngineEntryPoint()
        {
            var entryPoint = new EntryPoint("ContinuumXmlValidator.dll", "ContinuumXmlValidator.XmlValidatorNetPlugin", true);
            return entryPoint;
        }

        public System.Drawing.Image GetIcon()
        {
            // DIAG
            // To see the actual name of the embedded resource, as the assembly sees it.
            var arrResources = typeof(XmlValidator).Assembly.GetManifestResourceNames();
            // END DIAG

            if (_icon == null)
            {
                System.IO.Stream s = typeof(XmlValidator).Assembly.GetManifestResourceStream(_iconResource);
                if (s == null)
                {
                    throw new ArgumentNullException("Could not find local resource [" + _iconResource + "]");
                }

                _icon = (System.Drawing.Bitmap)System.Drawing.Bitmap.FromStream(s);
                _icon.MakeTransparent();
            }

            return _icon;
        }

        public Connection[] GetInputConnections()
        {
            return new Connection[] { new Connection("Input") };
        }

        public Connection[] GetOutputConnections()
        {
            return new Connection[] { new Connection("Output") };
        }

    }
}
