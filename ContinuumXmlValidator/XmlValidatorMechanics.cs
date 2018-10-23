using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml.Schema; // XMLSchemaSet
using System.Xml.Linq; // XDocument
using System.Xml; // XmlReader
using System.IO; // StringReader

namespace ContinuumXmlValidator
{
    public class XmlValidatorMechanics
    {

        public static XmlSchemaSet getXmlSchemaSet(string schemas)
        {
            if (string.IsNullOrWhiteSpace(schemas))
                return null;

            // Break list by character in SCHEMA_DELIM
            IEnumerable<string> files = schemas.Split(Constants.SCHEMA_DELIM);

            if (!files.Any())
                return null;

            // Should have a string, or multiple strings, pointing to possible files.

            XmlSchemaSet setOfSchemas = new XmlSchemaSet();
            foreach (string file in files)
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    XmlSchema xmlSchema = XmlSchema.Read(sr, new ValidationEventHandler(validationCallback));
                    if (xmlSchema != null)
                        setOfSchemas.Add(xmlSchema);
                }
            }

            return setOfSchemas;
        }

        private static void validationCallback(object sender, ValidationEventArgs args)
        {
            throw args.Exception;
        }
    }
}
