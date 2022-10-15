using System.Collections.Generic;
using Newtonsoft.Json;
using OpenTK.Graphics;
using System.Globalization;
using System.IO;
using paint1.Structure;

namespace paint1.Serealization
{
    class Serializer<T>
    {
        public void Serialize(string dir, T serializable)
        {
            string content = JsonConvert.SerializeObject(serializable, Formatting.Indented);
            File.WriteAllText(dir, content);
        }

        public T CarryJson(string dir)
        {
            if (!File.Exists(dir))
            {
                throw new FileNotFoundException("Address does not exist: \"" + dir + "\"");
            }

            string serialized;

            using (var reader = new StreamReader(dir))
            {
                serialized = reader.ReadToEnd();
            }

            T serializable = JsonConvert.DeserializeObject<T>(serialized);
            return serializable;
        }

        public static Shape CarryObj(string dir, Point center)
        {
            if (!File.Exists(dir))
            {
                throw new FileNotFoundException("Address does not exist: \"" + dir + "\"");
            }

            List<Point> pointers = new List<Point>();

            Dictionary<string, Polygon> sides = new Dictionary<string, Polygon>();

            using (StreamReader streamReader = new StreamReader(dir))
            {
                int Contador = 0;
                while (!streamReader.EndOfStream)
                {
                    List<string> words = new List<string>(streamReader.ReadLine().ToLower().Split(' '));
                    words.RemoveAll(s => s == string.Empty);

                    if (words.Count == 0)
                        continue;

                    string type = words[0];
                    words.RemoveAt(0);

                    switch (type)
                    {
                        // vertex
                        case "v":
                            pointers.Add(new Point(float.Parse(words[0], CultureInfo.InvariantCulture),
                                float.Parse(words[1], CultureInfo.InvariantCulture),
                                float.Parse(words[2], CultureInfo.InvariantCulture)));
                            break;

                        case "vt":
                            //textureVertices.Add(new Punto<float>(float.Parse(words[0], CultureInfo.InvariantCulture),
                            //float.Parse(words[1], CultureInfo.InvariantCulture),
                            //words.Count < 3 ? 0 : float.Parse(words[2], CultureInfo.InvariantCulture)));
                            break;

                        case "vn":
                            //normals.Add(
                            //new Punto<float>(float.Parse(words[0], CultureInfo.InvariantCulture),
                            //float.Parse(words[1], CultureInfo.InvariantCulture),
                            //float.Parse(words[2], CultureInfo.InvariantCulture)));
                            break;

                        // face
                        case "f":
                            Dictionary<string, Point> Vertices = new Dictionary<string, Point>();
                            int key = 0;
                            foreach (string w in words)
                            {
                                if (w.Length == 0)
                                    continue;

                                string[] comps = w.Split('/');

                                // subtract 1: indices start from 1, not 0
                                int index = int.Parse(comps[0]) - 1;
                                Vertices.Add(key.ToString(), new Point(pointers[index].x, pointers[index].y, pointers[index].z));

                                //if (comps.Length > 1 && comps[1].Length != 0)
                                //textureIndices.Add(uint.Parse(comps[1]) - 1);

                                //if (comps.Length > 2)
                                //normalIndices.Add(uint.Parse(comps[2]) - 1);

                                key++;
                            }

                            sides.Add(Contador.ToString(), new Polygon(Vertices, Color4.Red, center));

                            break;
                    }

                    Contador++;
                }

                return new Shape(sides, center);
            }
        }
    }
}
