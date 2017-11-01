using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml;

namespace ContinuumXmlValidator
{
    public class XmlInputConfiguration
    {
        public string DataField { get; private set; }
        public string SchemaField { get; private set; }
        public string OutputField { get; private set; }



        // Note that the constructor is private.  Instances are created through the LoadFromConfigration method.
        private XmlInputConfiguration(
            string dataField,
            string schemaField,
            string outputField)
        {
            DataField = dataField;
            SchemaField = schemaField;
            OutputField = outputField;
        }

        public static string getStringFromConfig(XmlElement eConfig, string key, string valueDefault)
        {
            string sReturn = valueDefault;

            XmlElement xe = eConfig.SelectSingleNode(key) as XmlElement;
            if (xe != null)
            {
                if (!string.IsNullOrEmpty(xe.InnerText))
                    sReturn = xe.InnerText;
            }

            return sReturn;
        }

        public static XmlInputConfiguration LoadFromConfiguration(XmlElement eConfig)
        {
            string dataField = getStringFromConfig(eConfig, Constants.DATAFIELDKEY, Constants.DEFAULTDATAFIELD);
            string schemaField = getStringFromConfig(eConfig, Constants.SCHEMAFIELDKEY, Constants.DEFAULTSCHEMAFIELD);
            string outputField = getStringFromConfig(eConfig, Constants.OUTPUTFIELDKEY, Constants.DEFAULTOUTPUTFIELD);

            return new XmlInputConfiguration(dataField, schemaField, outputField);
        }

        // Property Name Accessor
        public object this[string propertyName]
        {
            get { return this.GetType().GetProperty(propertyName).GetValue(this, null); }
            set { this.GetType().GetProperty(propertyName).SetValue(this, value, null); }
        }
    }
}
