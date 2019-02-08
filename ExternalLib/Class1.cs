using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.IO;

namespace ExternalLib
{
    public class MyExternalClass
    {
        public static string LoadFile()
        {
            string path = "room";
            path += ".txt";

            //using (FileStream file = File.OpenRead(path))
            //{
            //    byte[] b = new byte[1024];
            //    UTF8Encoding temp = new UTF8Encoding(true);

            //    while (file.Read(b,0,b.Length) > 0)
            //    {
            //        Console.WriteLine(temp.GetString(b));

            //    }
            //}
            StreamReader reader = File.OpenText(path);
            string line;
            string definitions = @"\b[\D+]\b([0-9]?,?[0-9]+)";
            Regex defRgx = new Regex(definitions);

            List<int> map = new List<int>();
            int[,] maps = new int[0, 0];

            string match = "";

            while ((line = reader.ReadLine()) != null)
            {
                if (!line.StartsWith("##"))
                {
                    if (line.StartsWith("[misc]"))
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

                        while (!line.StartsWith("[end]"))
                        {

                            string result = line;

                            for (int i = 0; i < line.Length; ++i)
                            {
                                map.Add(line[i]);
                            }

                            Console.WriteLine(result);
                            line = reader.ReadLine();
                        }
                    }
                }
            }
            return match;
        }
    }
}