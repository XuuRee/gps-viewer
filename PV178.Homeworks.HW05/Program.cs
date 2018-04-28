using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PV178.Homeworks.HW05.Model;
using PV178.Homeworks.HW05.Parsers.FuzzyFormatParsers;
using PV178.Homeworks.HW05.Parsers.GpxParsers;

namespace PV178.Homeworks.HW05
{
    class Program
    {
        static void Main(string[] args)
        {
            //string json = @"C:\Users\Filip_000\Desktop\route01.json";
            //string gpx = @"C:\Users\Filip_000\Desktop\route02.gpx";
            //string fuzzy1 = @"C:\Users\Filip_000\Desktop\route03.ff1";
            string fuzzy2 = @"C:\Users\Filip_000\Desktop\route04.ff2";

            FuzzyFormat2Parser parser = new FuzzyFormat2Parser();
            Track result = parser.ParseTrack(fuzzy2);
            Console.WriteLine("Items: {0}", result.GPSPoints.Count);

            //FuzzyFormat1Parser parser = new FuzzyFormat1Parser();
            //Track result = parser.ParseTrack(fuzzy1);
            //Console.WriteLine("Items: {0}", result.GPSPoints.Count);

            //GpxXmlParser parser = new GpxXmlParser();
            //Track result = parser.ParseTrack(gpx);
            //Console.WriteLine("Items: {0}", result.GPSPoints.Count);

            //GpxJsonParser parser = new GpxJsonParser();
            //Track result = parser.ParseTrack(json);
            //Console.WriteLine("Items: {0}", result.GPSPoints.Count);

            Console.ReadLine();
        }
    }
}
