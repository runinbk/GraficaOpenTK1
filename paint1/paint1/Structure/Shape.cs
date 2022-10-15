using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace paint1.Structure
{
    class Shape
    {
        private Dictionary<string, Polygon> polygons;
        private Point center;

        public Dictionary<string, Polygon> Polygons { get => polygons; set => polygons = value; }
        public Point Center { get => center; set => center = value; }

        public Shape() { }

        public Shape(Dictionary<string, Polygon> polygons, Point center)
        {
            this.Polygons = polygons;
            this.Center = center;
            /////////// buscar una manera de hacerlo de forma
            /////////// directa y no asi uno por uno
            foreach(var polygon in polygons)
            {
                polygon.Value.Center = center;
            }
        }

        public void DrawShape()
        {
            //// buscar una manera de hacerlo directo
            foreach(var poligon in polygons)
            {
                poligon.Value.DrawPolygons();
            }
        }
    }
}
