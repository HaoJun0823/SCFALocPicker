using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace SCFALocPicker
{
    internal class Program
    {


        static StringBuilder builder = new StringBuilder();
        static StringBuilder duplicate_builder = new StringBuilder();
        static HashSet<string> collection = new HashSet<string>();
        static List<string> list = new List<string>();

        static string db;

        static void Main(string[] args)
        {


            db = File.ReadAllText(System.AppDomain.CurrentDomain.BaseDirectory + "strings_db.lua");

            list.AddRange(Directory.GetFiles(args[0], "*.bp", SearchOption.AllDirectories).ToList<string>());
            list.AddRange(Directory.GetFiles(args[0], "*.lua", SearchOption.AllDirectories).ToList<string>());



            foreach(string path in list)
            {

                Console.WriteLine(path);

                string text = File.ReadAllText(path);

                //builder.Append(path);
                //builder.Append("\n");

                foreach(Match match in Regex.Matches(text, "'<LOC .*'"))

                {
                    //builder.Append(match.Value);
                    //builder.Append("\n");
                    Console.WriteLine(match.Value);
                    collection.Add(match.Value);

                }

                foreach (Match match in Regex.Matches(text, "\"<LOC .*\""))

                {
                    //builder.Append(match.Value);
                    //builder.Append("\n");
                    Console.WriteLine(match.Value);
                    collection.Add(match.Value);

                }




            }


            foreach (string str in collection)
            {

                string head = str.Substring(6,str.IndexOf('>')-6);
                string data = str.Substring(str.IndexOf('>')+1,((str.Length-1)-1)-str.IndexOf('>'));

                

                if (db.Contains(head))
                {

                    duplicate_builder.Append("# "+head + "=\"" + data + "\"\n");
                    Console.WriteLine("Duplicate Key:" + head + ",Value:" + data);
                }
                else
                {
                    builder.Append(head + "=\"" + data + "\"\n");
                    Console.WriteLine("Key:" + head + ",Value:" + data);
                }

                
            }

            
            builder.Append(duplicate_builder);

            Console.WriteLine(System.AppDomain.CurrentDomain.BaseDirectory + Path.GetFileNameWithoutExtension(args[0]));
            File.WriteAllText(System.AppDomain.CurrentDomain.BaseDirectory + Path.GetFileNameWithoutExtension(args[0])+".txt",builder.ToString());

        }
    }
}
