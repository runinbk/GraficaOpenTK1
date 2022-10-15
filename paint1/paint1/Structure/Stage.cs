using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace paint1.Structure
{
    class Stage
    {
        private Dictionary<string, Shape> shapes;
        private Point center;

        public Dictionary<string, Shape> Shapes { get => shapes; set => shapes = value; }
        public Point Center { get => center; set => center = value; }

        public Stage(Dictionary<string, Shape> shapes, Point center)
        {
            this.shapes = shapes;
            this.center = center;
            //// buscar una manera de haerlo de forma directa..
            foreach(var shape in shapes)
            {
                shape.Value.Center = center;
            }
        }


    }
}
