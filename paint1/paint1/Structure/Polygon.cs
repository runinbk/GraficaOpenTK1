using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace paint1.Structure
{
    class Polygon
    {
        private Dictionary<string, Point> points;
        private Color4 color;
        private Point center;

        public Dictionary<string, Point> Points { get => Points1; set => Points1 = value; }
        public Dictionary<string, Point> Points1 { get => points; set => points = value; }
        public Color4 Color { get => color; set => color = value; }
        public Point Center { get => center; set => center = value; }

        public Polygon() { }

        public Polygon(Dictionary<string, Point> points, Color4 color, Point center)
        {
            this.Points1 = points;
            this.Color = color;
            this.Center = center;
        }

        public void DrawPolygons()
        {
            PrimitiveType primitiveType = PrimitiveType.LineLoop;
            Draw(primitiveType);
        }

        public void Draw(PrimitiveType primitiveType)
        {
            GL.Begin(primitiveType);
            GL.Color3(Color.R, Color.G, Color.B);
            //////// se puede intriducir las vertices todas en conjunto
            ///         y no asi uno por uno
            foreach(Point p in Points.Values)
            {
                GL.Vertex3(Center.x + p.x, Center.y + p.y, Center.z + p.z);
            }
            //////////////
            GL.End();
        }
    }
}
