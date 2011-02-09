using System;
using System.IO;
using System.Runtime.Serialization;
using System.Windows.Forms;
using DotCopter.Avionics;
using DotCopter.Commons.Serialization;

namespace DotCopter.TelemetryReader
{
    public partial class Form1 : Form
    {
        private string _savePath;
        public Form1()
        {
            InitializeComponent();
        }

        private void BtnLoadFileClick(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            
            if(openFileDialog.ShowDialog()==DialogResult.OK)
            {
                string path = openFileDialog.FileName;
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.DefaultExt = ".csv";
                if(saveFileDialog.ShowDialog()==DialogResult.OK)
                {
                    _savePath = saveFileDialog.FileName;
                    ReadFile(path);
                }
            }
        }

        private void ReadFile(string readPath)
        {

        }

        private void FoundMessageHandler(ArraySegment<byte> messagedata)
        {
            INETMFSerializable  obj = (INETMFSerializable) FormatterServices.GetUninitializedObject(typeof (AircraftPrincipalAxes));
            INETMFSerializable deserializedObject = (INETMFSerializable) obj.Deserialize(messagedata.Array);
            WriteToScreen(deserializedObject);
            WriteToFile(deserializedObject);
        }

        private void WriteToFile(INETMFSerializable deserializedObject)
        {
            TextWriter writer = new StreamWriter(_savePath, true);
            writer.WriteLine(deserializedObject.ToString());
        }

        private void WriteToScreen(INETMFSerializable deserializedObject)
        {
            txtOutput.Text = deserializedObject.ToString() + Environment.NewLine + txtOutput.Text;
        }

        private static void btnCreateSample_Click(object sender, EventArgs e)
        {
            //TelemetryData axes = new TelemetryData();
            //PersistenceWriter logger = new PersistenceWriter(@"C:\testing\text", new TelemetryFormatter());
            //logger.Write(axes);
            //logger.Flush();
        }
    }
}
