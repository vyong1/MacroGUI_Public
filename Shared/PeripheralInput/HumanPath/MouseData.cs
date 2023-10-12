using Shared.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.PeripheralInput
{
    public class MouseData
    {
        public DateTime Time { get; set; }
        public TimeSpan DeltaTime { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public MouseData(MousePoint p)
        {
            Time = DateTime.Now;
            DeltaTime = TimeSpan.Zero;
            X = p.X;
            Y = p.Y;
        }

        public MouseData() { }

        public List<MouseData> FromFile(string path)
        {
            if (!File.Exists(path))
            {
                return null;
            }

            return XmlSerialize.Deserialize<List<MouseData>>(path);
        }
    }
}
