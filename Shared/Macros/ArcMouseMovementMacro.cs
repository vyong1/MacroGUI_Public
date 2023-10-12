using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shared.Macros
{
    public class ArcMouseMovementMacro : Macro
    {
        public override MacroGroup Group => MacroGroup.Standard;

        public override void Execute()
        {
            var path = GetCirclePath(300, 500, 500);
            foreach (Point point in path)
            {
                Thread.Sleep(5);
                MoveMouseTo(point.X, point.Y);
            }
        }

        private List<Point> GetLinePath()
        {
            var path = new List<Point>();
            for (int i = 100; i < 500; i++)
            {
                path.Add(new Point(i, i));
            }
            return path;
        }

        private List<Point> GetCirclePath(int radius, int offsetX, int offsetY)
        {
            var path = new List<Point>();

            for (int i = 0; i < 360; i++)
            {
                var rad = degToRad(i);
                var x = (int)(System.Math.Cos(rad) * radius);
                var y = (int)(System.Math.Sin(rad) * radius);
                
                path.Add(new Point(x + offsetX, y + offsetY));
            }

            return path;
        }

        private double degToRad(double deg)
        {
            return deg * 0.01745329251; // pi/180 = 0.01745329251
        }
    }
}
