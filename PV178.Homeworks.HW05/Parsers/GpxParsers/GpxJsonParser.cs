using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PV178.Homeworks.HW05.Model;


namespace PV178.Homeworks.HW05.Parsers.GpxParsers
{
    public class GpxJsonParser : Parser
    {
        public override Track ParseTrack(Stream stream)
        {
            IList<GpsCoordinates> gps = new List<GpsCoordinates> {};
            using (StreamReader streamReader = new StreamReader(stream))
            {
                using (JsonTextReader reader = new JsonTextReader(streamReader))
                {
                    JArray objects = (JArray)JToken.ReadFrom(reader);
                    foreach (JObject item in objects.Children<JObject>())
                    {
                        double latitude = (double) item.GetValue("Latitude");
                        double longitude = (double)item.GetValue("Longitude");
                        gps.Add(new GpsCoordinates(latitude, longitude));
                    }
                }
            }
            return new Track(gps);
        }
    }
}
