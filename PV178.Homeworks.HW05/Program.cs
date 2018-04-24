using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PV178.Homeworks.HW05.Model;
using PV178.Homeworks.HW05.Parsers.GpxParsers;

namespace PV178.Homeworks.HW05
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Users\Filip_000\Desktop\route01.json";
            GpxJsonParser parser = new GpxJsonParser();
            Track result = parser.ParseTrack(path);
            //Console.WriteLine(result.GPSPoints.ToString());
            /*
            string json = @"
                [ 
                    { ""General"" : ""At this time we do not have any frequent support requests."" },
                    { ""Support"" : ""For support inquires, please see our support page."" }
                ]";
            
            JArray objects = JArray.Parse(json);
            foreach (var item in objects)
            {
                Console.WriteLine(item.ToString());
            }
            */
            Console.ReadLine();
        }
    }
}
