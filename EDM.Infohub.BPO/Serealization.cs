using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace EDM.Infohub.BPO
{
    public static class Serealization
    {
        public static T Deserialize<T>(string input, string root) where T : class
        {
            using (StringReader sr = new StringReader(input))
            {
                var ser = new XmlSerializer(typeof(T));
                var result = (T)ser.Deserialize(sr);
                return result;
            }

            //XmlAttributes atts = new XmlAttributes();
            //atts.XmlRoot = new XmlRootAttribute(root);
            //XmlAttributeOverrides xover = new XmlAttributeOverrides();
            //xover.Add(typeof(T), atts);

            //using StringReader sr = new StringReader(input);
            //using XmlTextReader reader = new XmlTextReader(sr);
            //reader.Namespaces = true;
            //XmlSerializer serializer = new XmlSerializer(typeof(T), xover);
            //return (T)serializer.Deserialize(reader);
        }


        public static string Serialize<T>(T input) where T : class
        {

            XmlSerializer ser = new XmlSerializer(typeof(T));
            var xml = "";

            using (var sww = new StringWriterUtf8())
            {
                using XmlWriter writer = XmlWriter.Create(sww);
                ser.Serialize(writer, input);
                xml = sww.ToString(); // Your XML
            }
            return xml;
        }
    }

    public class StringWriterUtf8 : System.IO.StringWriter
    {
        public override Encoding Encoding
        {
            get
            {
                return Encoding.UTF8;
            }
        }
    }
}

