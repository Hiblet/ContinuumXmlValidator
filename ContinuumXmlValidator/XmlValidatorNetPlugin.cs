using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml;
using AlteryxRecordInfoNet;
using System.Xml.Schema; // XmlSchemaSet
using System.Xml.Linq; // XDocument


namespace ContinuumXmlValidator
{
    public class XmlValidatorNetPlugin : INetPlugin, IIncomingConnectionInterface
    {

        private int _toolID; // Integer identifier provided by Alteryx, this tools tool number.
        private EngineInterface _engineInterface; // Reference provided by Alteryx so that we can talk to the engine.
        private XmlElement _xmlProperties; // Xml configuration for this custom tool

        private PluginOutputConnectionHelper _outputHelper;

        private RecordInfo _recordInfoIn;
        private RecordInfo _recordInfoOut;

        private RecordCopier _recordCopier;

        // App Specific Variables
        private string _dataField = Constants.DEFAULTDATAFIELD;
        private string _schemaField = Constants.DEFAULTSCHEMAFIELD;
        private string _outputField = Constants.DEFAULTOUTPUTFIELD;


        public void PI_Init(int nToolID, EngineInterface engineInterface, XmlElement pXmlProperties)
        {
            _toolID = nToolID;
            _engineInterface = engineInterface;
            _xmlProperties = pXmlProperties;

            // Use the information in the pXmlProperties parameter to get the user control info

            XmlElement configElement = XmlHelpers.GetFirstChildByName(_xmlProperties, "Configuration", true);
            if (configElement != null)
            {
                getConfigSetting(configElement, Constants.DATAFIELDKEY, ref _dataField);
                getConfigSetting(configElement, Constants.SCHEMAFIELDKEY, ref _schemaField);
                getConfigSetting(configElement, Constants.OUTPUTFIELDKEY, ref _outputField);
            }

            _outputHelper = new AlteryxRecordInfoNet.PluginOutputConnectionHelper(_toolID, _engineInterface);
        }


        public IIncomingConnectionInterface PI_AddIncomingConnection(string pIncomingConnectionType, string pIncomingConnectionName)
        {
            return this;
        }

        public bool PI_AddOutgoingConnection(string pOutgoingConnectionName, OutgoingConnection outgoingConnection)
        {
            _outputHelper.AddOutgoingConnection(outgoingConnection);
            return true;
        }

        public bool PI_PushAllRecords(long nRecordLimit)
        {
            return true;
        }

        public void PI_Close(bool bHasErrors)
        {
            _outputHelper.Close();
        }

        public bool ShowDebugMessages()
        {
            return true;
        }

        public XmlElement II_GetPresortXml(XmlElement pXmlProperties)
        {
            return null;
        }

        public bool II_Init(RecordInfo recordInfo)
        {
            _recordInfoIn = recordInfo;
            prep(); // This allows zero record run to succeed and fixes problem with downstream tool complaining about a stream not being initialized.
            return true;
        }

        public void II_Close()
        { }

        public void II_UpdateProgress(double dPercent)
        {
            // Since our progress is directly proportional to he progress of the
            // upstream tool, we can simply output it's percentage as our own.
            if (_engineInterface.OutputToolProgress(_toolID, dPercent) != 0)
            {
                // If this returns anything but 0, then the user has canceled the operation.
                throw new AlteryxRecordInfoNet.UserCanceledException();
            }

            // Have the PluginOutputConnectionHelper ask the downstream tools to update their progress.
            _outputHelper.UpdateProgress(dPercent);
        }

        public bool II_PushRecord(RecordData recordDataIn)
        {
            // Prepare the output
            AlteryxRecordInfoNet.Record recordOut = _recordInfoOut.CreateRecord();
            recordOut.Reset();

            // Transfer existing data
            //copyRecordData(recordDataIn, recordOut);
            _recordCopier.Copy(recordOut, recordDataIn);

            // Collect data from fields
            string data = getFieldBaseStringData(_dataField, recordDataIn);
            string schema = getFieldBaseStringData(_schemaField, recordDataIn);

            // Shortcut out if sanity checks fail
            if (string.IsNullOrWhiteSpace(data)) return setOutput("MISSING_XML_DATA", recordOut);
            if (string.IsNullOrWhiteSpace(schema)) return setOutput("MISSING_XML_SCHEMA", recordOut);
            if (!data.TrimStart().StartsWith("<")) return setOutput("DATA_NOT_XML", recordOut);

            // We have two non-empty strings.
            
            // Schemas are expected as filenames, separated by semicolons.
            XmlSchemaSet setOfSchemas = XmlValidatorMechanics.getXmlSchemaSet(schema);

            XDocument doc = XDocument.Parse(data);

            List<string> errors = new List<string>();

            doc.Validate(setOfSchemas, (o, e) =>
            {
                errors.Add(e.Message);
            }, true);

            string allErrors = string.Join("|", errors);

            // If no errors, say VALID, else, say the errors.
            if (string.IsNullOrWhiteSpace(allErrors))
                return setOutput("VALID", recordOut);
            else
                return setOutput(allErrors, recordOut);
        }





        /////////////////////////////////////////////////////////////////////// 
        // HELPERS
        //

        private bool setOutput(string output, Record recordOut)
        {
            // Output Field is last
            AlteryxRecordInfoNet.FieldBase fbOut = _recordInfoOut[(int)_recordInfoIn.NumFields()];
            fbOut.SetFromString(recordOut, output);

            _outputHelper.PushRecord(recordOut.GetRecord());

            return true;
        }

        private void getConfigSetting(XmlElement configElement, string key, ref string memberToSet)
        {
            XmlElement xe = XmlHelpers.GetFirstChildByName(configElement, key, false);
            if (xe != null)
            {
                if (!string.IsNullOrWhiteSpace(xe.InnerText))
                    memberToSet = xe.InnerText;
            }
        }

        private void prep()
        {
            // Exit if already done (safety)
            if (_recordInfoOut != null)
                return;

            _recordInfoOut = new AlteryxRecordInfoNet.RecordInfo();

            populateRecordInfoOut();

            _recordCopier = new RecordCopier(_recordInfoOut, _recordInfoIn, true);

            uint countFields = _recordInfoIn.NumFields();
            for (int i = 0; i < countFields; ++i)
            {
                var fieldName = _recordInfoIn[i].GetFieldName();

                var newFieldNum = _recordInfoOut.GetFieldNum(fieldName, false);
                if (newFieldNum == -1)
                    continue;

                _recordCopier.Add(newFieldNum, i);
            }

            _recordCopier.DoneAdding();

            _outputHelper.Init(_recordInfoOut, "Output", null, _xmlProperties);
        }

        private void populateRecordInfoOut()
        {
            _recordInfoOut = new AlteryxRecordInfoNet.RecordInfo();

            // Copy the fieldbase structure of the incoming record
            uint countFields = _recordInfoIn.NumFields();
            for (int i = 0; i < countFields; ++i)
            {
                FieldBase fbIn = _recordInfoIn[i];
                _recordInfoOut.AddField(fbIn.GetFieldName(), fbIn.FieldType, (int)fbIn.Size, fbIn.Scale, fbIn.GetSource(), fbIn.GetDescription());
            }

            // Add the output column at the end
            _recordInfoOut.AddField(_outputField, FieldType.E_FT_String, Constants.OUTPUTFIELDSIZE, 0, "", "");
        }


        private string getFieldBaseStringData(string fieldName, RecordData recordDataIn)
        {
            FieldBase fb = null;
            try
            {
                fb = _recordInfoIn.GetFieldByName(fieldName, true);
            }
            catch
            {
                throw new Exception($"The field [{fieldName}] was not found.");
            }

            return fb.GetAsString(recordDataIn);
        }


    }
}
