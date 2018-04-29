using System;
using System.IO;
using System.Text.RegularExpressions;
using PV178.Homeworks.HW05.Model;
using PV178.Homeworks.HW05.Utils;


namespace PV178.Homeworks.HW05.Parsers
{
    public abstract class Parser : IGpsParser
    {
        public abstract Track ParseTrack(Stream stream);

        public Track ParseTrack(string filePath)
        {
            using (FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                return ParseTrack(stream);
            }
        }
        
        public Track ParseTrack(byte[] bytes)
        {
            using (Stream stream = new MemoryStream(bytes))
            {
                return ParseTrack(stream);
            }
        }

        /// <summary>
        /// Get fuzzy coordinates from line.
        /// </summary>
        /// <param name="matches">matches from </param>
        /// <param name="line">matches from </param>
        /// <returns>Extracted Track</returns>
        protected GpsCoordinates GetFuzzyCoordinates(MatchCollection matches, string line)
        {
            Tuple<double, double> coordinates;
            if (matches.Count == 0)
            {
                coordinates = GetRawCoordinates(line);
            }
            else
            {
                coordinates = GetRawCoordinates(matches[0].ToString() + " " + matches[1].ToString());
            }
            return new GpsCoordinates(coordinates.Item1, coordinates.Item2);
        }

        /// <summary>
        /// Get raw coordinates from parse line.
        /// </summary>
        /// <param name="line">parse line</param>
        /// <returns>Tuple with latitude and longitude.</returns>
        private Tuple<double, double> GetRawCoordinates(string line)
        {
            double latitude, longitude;
            MatchCollection coordinates = Regex.Matches(line, @"(\d{1,2}\.\d+|\d{2}|\d)");
            if (coordinates.Count == 2)
            {
                latitude = Helpers.ParseDouble(coordinates[0].Value);
                longitude = Helpers.ParseDouble(coordinates[1].Value);
            }   
            else
            {
                latitude = Helpers.ConvertDMSToDD(int.Parse(coordinates[0].Value), int.Parse(coordinates[1].Value), Helpers.ParseDecimal(coordinates[2].Value));
                longitude = Helpers.ConvertDMSToDD(int.Parse(coordinates[3].Value), int.Parse(coordinates[4].Value), Helpers.ParseDecimal(coordinates[5].Value));
            }
            return CheckFuzzyCoordinatesOrder(latitude, longitude);
        }

        /// <summary>
        /// Check if given coordinates are in right order.
        /// </summary>
        /// <param name="latitude">latitude</param>
        /// <param name="longitude">longitude</param>
        /// <returns>Tuple with latitude and longitude in right order.</returns>
        private Tuple<double, double> CheckFuzzyCoordinatesOrder(double latitude, double longitude)
        {
            if (latitude.CompareTo(longitude) < 0)
            {
                return new Tuple<double, double>(longitude, latitude);
            }
            return new Tuple<double, double>(latitude, longitude);
        }

        /// <summary>
        /// Check if given string is empty.
        /// </summary>
        /// <returns>True if string is empty, false otherwise.</returns>
        protected bool IsEmpty(string str)
        {
            return str.Length == 0;
        }
    }
}
