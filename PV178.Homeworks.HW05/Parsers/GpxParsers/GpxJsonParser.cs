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
            // jedna implementovana funkce, dalsi se volaji navzajem.
            using (StreamReader stream = File.OpenText(filePath))         // simplify
            {
                return ParseTrack(stream.BaseStream);   // bad
            }
                /*
                IList<GpsCoordinates> gps = new List<GpsCoordinates> {};
                using (StreamReader file = File.OpenText(filePath))         // simplify
                {
                    using (JsonTextReader reader = new JsonTextReader(file))
                    {
                        JArray objects = (JArray) JToken.ReadFrom(reader);
                        foreach (JObject item in objects.Children<JObject>())
                        {
                            GpsCoordinates coordinate = 
                                new GpsCoordinates((double) item.GetValue("Latitude"), (double) item.GetValue("Longitude"));
                            gps.Add(coordinate);
                        }
                    }
                }
                return new Track(gps);
                */
            }

        public Track ParseTrack(Stream stream)
        {
            IList<GpsCoordinates> gps = new List<GpsCoordinates> { };
            using (JsonTextReader reader = new JsonTextReader(new StreamReader(stream)))
            {
                JArray objects = (JArray)JToken.ReadFrom(reader);
                foreach (JObject item in objects.Children<JObject>())
                {
                    GpsCoordinates coordinate =
                        new GpsCoordinates((double) item.GetValue("Latitude"), (double) item.GetValue("Longitude"));
                    gps.Add(coordinate);
                }
            }
            return new Track(gps);
        }

        public Track ParseTrack(byte[] bytes)
        {
            throw new System.NotImplementedException();
        }
    }
}
