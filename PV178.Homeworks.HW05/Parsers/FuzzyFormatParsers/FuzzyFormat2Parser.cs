using System.IO;
using System.Collections.Generic;
using PV178.Homeworks.HW05.Model;
using System.Text.RegularExpressions;


namespace PV178.Homeworks.HW05.Parsers.FuzzyFormatParsers
{
    public class FuzzyFormat2Parser : Parser
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
                        MatchCollection matches = Regex.Matches(line, @"(\d{2}(, min: |°?, minutes: |\s*(['°?])\s*)()?\d{2}( sec: | and seconds: |\s*([']{2}?|[']?)\s*)( and secs: )?((\d{1,2}\.\d{1,4})|\d$)|\d{2}\.\d{3,6}\s*?(Latitude|Longitude|lat|lon|E|N|e|n))");
                        gps.Add(GetFuzzyCoordinates(matches, line));
                    }
                }
            }
            return new Track(gps);
        }
    }
}
