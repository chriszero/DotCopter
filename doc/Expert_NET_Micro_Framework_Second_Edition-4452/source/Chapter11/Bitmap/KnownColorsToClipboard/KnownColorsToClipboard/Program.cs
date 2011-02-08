//This console application get the known color definitions with RGB values from the Color class
//and generates a tab separated table and copies it into the clipboard
using System;
using System.Reflection;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace KnownColorsToClipboard
{
    class Program
    {
        [STAThread()] //needed for clipboard feature
        static void Main(string[] args)
        {
            StringBuilder sb = new StringBuilder();
            PropertyInfo[] props = typeof(Color).GetProperties(System.Reflection.BindingFlags.Static | BindingFlags.Public);
            foreach (PropertyInfo prop in props)
            {
                Color color = (Color)prop.GetValue(null, null);
                sb.AppendFormat("{0}\t0x{1:X2}\t0x{2:X2}\t0x{3:X2}", color.Name, color.R, color.G, color.B);
                sb.AppendLine();
            }
            Clipboard.SetText(sb.ToString());
        }
    }
}
