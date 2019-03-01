using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.IO;

namespace ExternalLib
{
    public class MyExternalClass
    {
        public static object[,] LoadFile()
        {
            object[,] obj = new object[0, 0];
            string path = "room";
            path += ".txt";

            StreamReader reader = File.OpenText(path);
            string line;

            string definitions = @"\b[\D+]\b([0-9]?,?[0-9]+)";
            Regex defRgx = new Regex(definitions);

            List<int> map = new List<int>();


            while ((line = reader.ReadLine()) != null)
            {
                if (!line.StartsWith("##"))
                {
                    if (line.StartsWith("[misc]"))
                    {
                        line = reader.ReadLine();

                        string[] results = Array.Empty<string>();

                        while (!line.StartsWith("[end]"))
                        {
                            char[] delimiter = { ',', '=' };

                            string result = defRgx.Match(line).ToString();
                            results = result.Split(delimiter);

                            Console.WriteLine(String.Join("", results));
                            line = reader.ReadLine();
                        }
                        int.TryParse(results[1], out int width);
                        int.TryParse(results[2], out int height);
                        obj = new object[width, height];
                    }
                    if (line.StartsWith("[block definitions"))
                    {
                        line = reader.ReadLine();

                        while (!line.StartsWith("[end]"))
                        {
                            char[] delimiter = { ',', '=' };

                            string result = defRgx.Match(line).ToString();
                            string[] results = result.Split(delimiter);

                            Console.WriteLine(String.Join("", results));
                            line = reader.ReadLine();
                        }
                    }
                    if (line.StartsWith("[room definitions]"))
                    {
                        line = reader.ReadLine();
                        int row = 0;

                        while (!line.StartsWith("[end]"))
                        {

                            string result = line;

                            for (int i = 0; i < line.Length; ++i)
                            {
                                obj[i,row] = result[i];
                            }

                            Console.WriteLine(result);
                            line = reader.ReadLine();

                            row++;
                        }
                    }
                }
            }
            return obj;
        }

        public string Serializer()
        {
            return "";
        }
    }
}