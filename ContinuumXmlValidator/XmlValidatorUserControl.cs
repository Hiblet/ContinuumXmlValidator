using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using AlteryxGuiToolkit.Plugins;
using System.Xml;



namespace ContinuumXmlValidator
{
    public partial class XmlValidatorUserControl : UserControl, IPluginConfiguration
    {
        public XmlValidatorUserControl()
        {
            InitializeComponent();
        }

        public Control GetConfigurationControl(
            AlteryxGuiToolkit.Document.Properties docProperties,
            XmlElement eConfig,
            XmlElement[] eIncomingMetaInfo,
            int nToolId,
            string strToolName)
        {
            XmlInputConfiguration xmlConfig = XmlInputConfiguration.LoadFromConfiguration(eConfig);

            if (xmlConfig == null)
                return this;

            ///////////////////////////////////////////////////////////////////
            // Populate GUI Controls with saved config information
            //


            ///////////////////////
            // FIELD COMBOX BOXES
            //

            setComboBox(comboBoxDataField, xmlConfig, "DataField", eIncomingMetaInfo);
            setComboBox(comboBoxSchemaField, xmlConfig, "SchemaField", eIncomingMetaInfo);


            ///////////////////////////////////////////////////////////////////
            // Output Field
            //

            string outputField = xmlConfig.OutputField;
            textBoxOutputField.Text = outputField;

            return this;
        }

        private void saveSubForComboBox(ComboBox cbox, XmlElement eConfig, string key, string valueDefault)
        {
            XmlElement xe = XmlHelpers.GetOrCreateChildNode(eConfig, key);
            var selectedItem = cbox.SelectedItem;
            xe.InnerText = selectedItem == null ? valueDefault : selectedItem.ToString();
        }

        public void SaveResultsToXml(XmlElement eConfig, out string strDefaultAnnotation)
        {
            saveSubForComboBox(comboBoxDataField, eConfig, Constants.DATAFIELDKEY, Constants.DEFAULTDATAFIELD);
            saveSubForComboBox(comboBoxSchemaField, eConfig, Constants.SCHEMAFIELDKEY, Constants.DEFAULTSCHEMAFIELD);

            // Output Field
            XmlElement xe = XmlHelpers.GetOrCreateChildNode(eConfig, Constants.OUTPUTFIELDKEY);
            string outputField = textBoxOutputField.Text;
            xe.InnerText = string.IsNullOrWhiteSpace(outputField) ? Constants.DEFAULTOUTPUTFIELD : outputField;

            // Set the default annotation.
            strDefaultAnnotation = "";
        }


        // Helper
        private static bool isStringType(string fieldType)
        {
            return string.Equals(fieldType, "string", StringComparison.OrdinalIgnoreCase) ||
                   string.Equals(fieldType, "v_string", StringComparison.OrdinalIgnoreCase) ||
                   string.Equals(fieldType, "wstring", StringComparison.OrdinalIgnoreCase) ||
                   string.Equals(fieldType, "v_wstring", StringComparison.OrdinalIgnoreCase);
        }


        private void setComboBox(ComboBox cbox, XmlInputConfiguration xmlConfig, string key, XmlElement[] eIncomingMetaInfo)
        {
            string target = (string)xmlConfig[key];

            cbox.Items.Clear();

            if (eIncomingMetaInfo == null || eIncomingMetaInfo[0] == null)
            {
                // No incoming connection;  Just add the field and select it
                cbox.Items.Add(target);
                cbox.SelectedIndex = 0;
            }
            else
            {
                // We have an incoming connection

                var xmlElementMetaInfo = eIncomingMetaInfo[0];
                var xmlElementRecordInfo = xmlElementMetaInfo.FirstChild;
                foreach (XmlElement elementChild in xmlElementRecordInfo)
                {
                    string fieldName = elementChild.GetAttribute("name");
                    string fieldType = elementChild.GetAttribute("type");

                    if (isStringType(fieldType))
                        cbox.Items.Add(fieldName);
                }

                // If the messageField matches a possible field in the combo box, make it the selected field.
                // If the field does not match, do not select anything and blank the field.
                if (!string.IsNullOrWhiteSpace(target))
                {
                    int selectedIndex = cbox.FindStringExact(target);
                    if (selectedIndex >= 0)
                        cbox.SelectedIndex = selectedIndex; // Found; Select this item                    
                }

            } // end of "if (eIncomingMetaInfo == null || eIncomingMetaInfo[0] == null)"
        }

    }
}
