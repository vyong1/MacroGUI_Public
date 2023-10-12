using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Shared.Serialization
{
    /// <summary>
    /// XML serialization, but with custom path handling
    /// </summary>
    public static class ConfigXmlSerializer
    {
        public static bool AlreadySerialized<T>()
        {
            return File.Exists(GetFilepath(typeof(T)));
        }

        public static void Serialize(object j)
        {
            try
            {
                XmlSerializer x = new XmlSerializer(j.GetType());
                string filepath = GetFilepath(j.GetType());
                string dir = Path.GetDirectoryName(filepath);
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                if (File.Exists(filepath))
                {
                    File.Delete(filepath);
                }

                using (FileStream fs = new FileStream(filepath, FileMode.Create, FileAccess.Write))
                {
                    x.Serialize(fs, j);
                }
            }
            catch (Exception)
            {
                // TODO - Probably something better we can do here
                return;
            }
        }

        public static T Deserialize<T>() where T : new()
        {
            try
            {
                if (!AlreadySerialized<T>())
                {
                    Serialize(new T());
                }

                XmlSerializer x = new XmlSerializer(typeof(T));
                string filepath = GetFilepath(typeof(T));
                object deser;

                using (FileStream fs = new FileStream(filepath, FileMode.Open, FileAccess.Read))
                {
                    deser = x.Deserialize(fs);
                }

                return (T)deser;
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to load config from file, using default app configs instead");
                T obj = new T();
                Serialize(obj);
                return obj;
            }
        }

        private static string GetFilepath(Type t)
        {
            string fullPath = Assembly.GetExecutingAssembly().Location;
            string theDirectory = Path.GetDirectoryName(fullPath);
            return Path.Combine(theDirectory, "Serialized", $"{t.Name}.xml");
        }
    }
}