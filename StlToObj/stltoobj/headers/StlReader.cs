using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stltoobj
{
    internal class StlReader
    {
        public StlReader()
        {
        }

        public void ReadFile1(Triangulation triangulation)
        {
            using (StreamReader readFromFile = new StreamReader("D:\\Om_Workspace\\C#\\PractiseWork\\stltoobj\\stltoobj\\resources\\cube.stl"))
            {
                Dictionary<Point3D, int> mp = new Dictionary<Point3D, int>();
                Dictionary<Point3D, int> mappedNormals = new Dictionary<Point3D, int>();
                string line;

                while ((line = readFromFile.ReadLine()) != null)
                {
                    if (line.Contains("facet normal"))
                    {
                        double x, y, z;
                        string[] tokens = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        if (tokens.Length == 5 &&
                            double.TryParse(tokens[2], out x) &&
                            double.TryParse(tokens[3], out y) &&
                            double.TryParse(tokens[4], out z))
                        {
                            Point3D point = new Point3D((float)x, (float)y, (float)z);


                            if (!mappedNormals.ContainsKey(point))
                            {
                                mappedNormals[point] = triangulation.UniqueNormals.Count;
                                triangulation.UniqueNormals.Add(point);
                            }

                            int normalIndex = mappedNormals[point];
                            // Do something with normalIndex if needed
                        }
                    }

                    if (line.Contains("vertex"))
                    {
                        string[] tokens = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        if (tokens.Length == 4 &&
                            double.TryParse(tokens[1], out double x) &&
                            double.TryParse(tokens[2], out double y) &&
                            double.TryParse(tokens[3], out double z))
                        {
                            Point3D point = new Point3D((float)x, (float)y, (float)z);


                            if (!mp.ContainsKey(point))
                            {
                                triangulation.UniquePoints.Add(point);
                                mp[point] = triangulation.UniquePoints.Count;
                            }

                            int vertexIndex = mp[point];
                            // Do something with vertexIndex if needed
                        }
                    }
                }
            }
        }
    }
}
