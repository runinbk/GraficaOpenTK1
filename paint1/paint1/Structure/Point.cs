using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace paint1.Structure
{
    class Point
    {
       
        //attributes
        private float ejeX { get; set; }
        private float ejeY { get; set; }
        private float ejeZ { get; set; }
        //properties
        public float x { get { return ejeX; } set { ejeX = value; } }
        public float y { get { return ejeY; } set { ejeY = value; } }
        public float z { get { return ejeZ; } set { ejeZ = value; } }

        //constructors
        public Point(float x, float y, float z)
        {
            this.ejeX = x;
            this.ejeY = y;
            this.ejeZ = z;
        }
        
        public Point() : this(0, 0, 0) { }
        
        public Point(Point p)
        {
            this.ejeX = p.ejeX;
            this.ejeY = p.ejeY;
            this.ejeZ = p.ejeZ;
        }

        public Point(float valor)
        {
            this.ejeX = this.ejeY = this.ejeZ = valor;
        }

        public Point sum(Point m, Point n)
        {
            float a = m.ejeX + n.ejeX;
            float b = m.ejeY + n.ejeY;
            float c = m.ejeZ + n.ejeZ;
            return new Point(a, b, c);
        }
    }
}
