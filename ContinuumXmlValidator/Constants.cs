using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContinuumXmlValidator
{
    public class Constants
    {
        // Keys for XML data storage
        public static string DATAFIELDKEY = "ContinuumXmlValidatorDataField"; 
        public static string SCHEMAFIELDKEY = "ContinuumXmlValidatorSchemaField";

        public static string OUTPUTFIELDKEY = "ContinuumXmlValidatorOutputField";  // Output


        // Default Values
        public static string DEFAULTDATAFIELD = "XmlData";
        public static string DEFAULTSCHEMAFIELD = "XmlSchema";

        public static string DEFAULTOUTPUTFIELD = "XmlErrors";
        public static int OUTPUTFIELDSIZE = 16383;

        // Delimiter
        public static char SCHEMA_DELIM = ';';
    }
}
