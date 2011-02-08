using System;
using System.IO;
using System.Text;
using System.Xml;
using Microsoft.SPOT;

namespace XmlReaderSample
{
    public class Program
    {
        public static void Main()
        {

            string xmlText =
                         "<?xml version=\"1.0\" encoding=\"utf-8\" ?>" +
                         "<Catalog>" +
                           "<Product Type=\"abcd\" Category=\"ab\">" +
                             "<ID>F10</ID>" +
                             "<Name>ProductX</Name>" +
                             "<Price>47.76</Price>" +
                           "</Product>" +
                           "<Product>" +
                             "<ID>F11</ID>" +
                             "<Name>ProductY</Name>" +
                             "<Price>57.76</Price>" +
                           "</Product>" +
                         "</Catalog>";
            byte[] xmlBytes = Encoding.UTF8.GetBytes(xmlText);
            MemoryStream xmlStream = new MemoryStream(xmlBytes);

            using (XmlReader r = XmlTextReader.Create(xmlStream))
            {
                r.Read();
                while (!r.EOF)
                {
                    if (r.IsStartElement() && r.LocalName == "Product")
                    {
                        Debug.Print("Product");
                        // Read attributes
                        string typeAttr = r.GetAttribute("Type");
                        if (typeAttr != null)
                            Debug.Print(" Type attr=" + typeAttr);
                        string catAttr = r.GetAttribute("Category");
                        if (catAttr != null)
                            Debug.Print(" Category attr=" + catAttr);
                        // Read property elements
                        // Read until end of product
                        while (r.Read() &&
                               r.LocalName != "Product")
                        {
                            if (r.IsStartElement())
                            {
                                Debug.Print(" " + r.LocalName + "=" + r.ReadElementString());
                            }
                        }
                    }
                    else
                      r.Read();
                }
            }
        }
    }
}
