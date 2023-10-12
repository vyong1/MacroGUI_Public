using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Shared.Serialization
{
    public static class XmlSerialize
    {
        public static void Serialize(string path, object obj)
        {
            XmlSerializer x = new XmlSerializer(obj.GetType());
            string filepath = Path.Combine(path);

            using (FileStream fs = new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                x.Serialize(fs, obj);
            }
        }

        public static T Deserialize<T>(string path)
        {
            XmlSerializer x = new XmlSerializer(typeof(T));
            string filepath = Path.Combine(path);

            using (FileStream fs = new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.Read))
            {
                return (T)(x.Deserialize(fs));
            }
        }
    }
}
