using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PV178.Homeworks.HW05.Model;

namespace PV178.Homeworks.HW05.Parsers.GpxParsers
{
    public class GpxJsonParser : IGpsParser
    {
        public Track ParseTrack(string filePath)
        {
            using (FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                return ParseTrack(stream);
            }
        }

        public Track ParseTrack(Stream stream)
        {
            IList<GpsCoordinates> gps = new List<GpsCoordinates> {};
            using (JsonTextReader reader = new JsonTextReader(new StreamReader(stream)))    // bad practice
            {
                JArray objects = (JArray)JToken.ReadFrom(reader);
                foreach (JObject item in objects.Children<JObject>())
                {    
                    gps.Add(new GpsCoordinates((double)item.GetValue("Latitude"), (double)item.GetValue("Longitude")));
                }
            }
            return new Track(gps);
        }

        public Track ParseTrack(byte[] bytes)
        {
            using (Stream stream = new MemoryStream(bytes))
            {
                return ParseTrack(stream);
            }
        }
    }
}
