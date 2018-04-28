using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using PV178.Homeworks.HW05.Model;
using PV178.Homeworks.HW05.Utils;

namespace PV178.Homeworks.HW05.Parsers.FuzzyFormatParsers
{
    public class FuzzyFormat1Parser : IGpsParser
    {

        public Track ParseTrack(string filePath)
        {
            // abstraktni tridy
            using (FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                return ParseTrack(stream);
            }
        }
        
        public Track ParseTrack(Stream stream)
        {
            IList<GpsCoordinates> gps = new List<GpsCoordinates> { };
            //string[] route03 = Route03ExpectedOutput.Split(',');
            using (StreamReader file = new StreamReader(stream))
            {
                string fuzzy1 = file.ReadToEnd();
                string[] lines = fuzzy1.Split('\n');
                //int i = 0;
                foreach (string line in lines)
                {
                    if (line != "")
                    { 
                        GpsCoordinates coordinate = ParseLine(line);
                        gps.Add(coordinate);
                        //if (coordinate.ToString() != route03[i])
                        //{
                        //    Console.WriteLine((i + 1) + " " + coordinate.ToString() + "... spatne: " + route03[i]);
                        //    Console.ReadLine();
                        //}
                        //else
                        //{ 
                        //    Console.WriteLine((i + 1) + " " + coordinate.ToString() + "... spravne: " + route03[i]);
                        //}
                        //i += 1;
                    }
                }
                return new Track(gps);
            }
        }

        public Track ParseTrack(byte[] bytes)
        {
            using (Stream stream = new MemoryStream(bytes))
            {
                return ParseTrack(stream);
            }
        }

        private GpsCoordinates ParseLine(string line)
        {
            //(?(?=(\D*\d+\.\d+\D+\d+\.\d+\D*))\d+\.\d+|else)
            // (?:\D*?\d+\.\d+\D+?){2}
            MatchCollection matches;
            if (Regex.IsMatch(line, @"^\D*\d+\.\d+\D+\d+\.\d+\D*$"))    // pouze dve souradnice, ve tvaru double double
            {                                                           // uprostred mozna \D*
                matches = Regex.Matches(line, @"\d+\.\d+");
                double latitude = Helpers.ParseDouble(matches[0].Value);
                double longitude = Helpers.ParseDouble(matches[1].Value);
                var result = CheckCoordinatesOrder(latitude, longitude);
                return new GpsCoordinates(result.Item1, result.Item2);
            }
            // not 0-9
            else if (Regex.IsMatch(line, @"^\D*\d+[^\.0-9]*\d+[^\.0-9]*\d+\.\d+\D+\d+[^\.0-9]*\d+[^\.0-9]*\d+\.\d+\D*$"))   // pouze dve souradnice, ve tvaru stupne, minuty a sekundy
            {
                matches = Regex.Matches(line, @"(\d+\.\d+|\d+)");
                double latitude = Helpers.ConvertDMSToDD(int.Parse(matches[0].Value), int.Parse(matches[1].Value), Helpers.ParseDecimal(matches[2].Value));
                double longitude = Helpers.ConvertDMSToDD(int.Parse(matches[3].Value), int.Parse(matches[4].Value), Helpers.ParseDecimal(matches[5].Value));
                var result = CheckCoordinatesOrder(latitude, longitude);    // really?
                return new GpsCoordinates(result.Item1, result.Item2);
            }
            else if (Regex.IsMatch(line, @"^.*\d+\.\d+[\s]*(N|North|north|lat|latitude|Latitude).*\d+\.\d+[\s]*(E|East|east|lon|longitude|Longitude).*$"))      // druhy typ
            {
                matches = Regex.Matches(line, @"(\d+\.\d+[\s]*(N|North|north|lat|latitude|Latitude)|\d+\.\d+[\s]*(E|East|east|lon|longitude|Longitude))");      // zjednodusit, rovnou vzit
                Match latitude = Regex.Match(matches[0].Value, @"\d+\.\d+");
                Match longitude = Regex.Match(matches[1].Value, @"\d+\.\d+");
                var result = CheckCoordinatesOrder(Helpers.ParseDouble(latitude.Value), Helpers.ParseDouble(longitude.Value));    // really?
                return new GpsCoordinates(result.Item1, result.Item2);
            }
            else
            {
                matches = Regex.Matches(line, @"\d+\D\d+\D\d+\.\d+\D{2}((N|North|north|lat|latitude|Latitude)|(E|East|east|lon|longitude|Longitude))");
                MatchCollection latitude = Regex.Matches(matches[0].Value, @"\d+\.\d+|\d+");
                MatchCollection longitude = Regex.Matches(matches[1].Value, @"\d+\.\d+|\d+");
                double lat = Helpers.ConvertDMSToDD(int.Parse(latitude[0].Value), int.Parse(latitude[1].Value), Helpers.ParseDecimal(latitude[2].Value));
                double lon = Helpers.ConvertDMSToDD(int.Parse(longitude[0].Value), int.Parse(longitude[1].Value), Helpers.ParseDecimal(longitude[2].Value));
                var result = CheckCoordinatesOrder(lat, lon);
                return new GpsCoordinates(result.Item1, result.Item2);
            }
        }

        private Tuple<double, double> CheckCoordinatesOrder(double latitude, double longitude)
        {
            if (latitude.CompareTo(longitude) < 0)
            {
                return new Tuple<double, double>(longitude, latitude);
            }
            return new Tuple<double, double>(latitude, longitude);
        }
    }
}
