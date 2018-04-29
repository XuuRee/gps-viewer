using System.IO;
using System.Collections.Generic;
using PV178.Homeworks.HW05.Model;
using System.Text.RegularExpressions;


namespace PV178.Homeworks.HW05.Parsers.FuzzyFormatParsers
{
    public class FuzzyFormat1Parser : Parser
    {
        public override Track ParseTrack(Stream stream)
        {
            IList<GpsCoordinates> gps = new List<GpsCoordinates> {};
            using (StreamReader file = new StreamReader(stream))
            {
                string[] lines = file.ReadToEnd().Split('\n');
                foreach (string line in lines)
                {
                    if (!IsEmpty(line))
                    {
                        MatchCollection matches = Regex.Matches(line, @"(\d{2}[°]?(, minutes: |, min: |\s*['°?]\s*)\d{1,2}\s*([']{1,2})?\s*(and seconds: |(and)?[ ]?sec(s)?: )?\d{1,2}\.\d{1,4}|\d{2}\.\d{2,6}\s*((L|l)at|(L|l)on|E|N|e|n))");
                        gps.Add(GetFuzzyCoordinates(matches, line));
                    }
                }                
            }
            return new Track(gps);
        }
    }
}
