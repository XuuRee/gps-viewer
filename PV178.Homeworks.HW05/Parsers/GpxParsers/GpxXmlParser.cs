using System.IO;
using System.Linq;
using System.Xml.Linq;
using PV178.Homeworks.HW05.Model;
using PV178.Homeworks.HW05.Utils;


namespace PV178.Homeworks.HW05.Parsers.GpxParsers
{
    public class GpxXmlParser : Parser
    {
        #region XmlElementNames

        private const string TrackElement = "{http://www.topografix.com/GPX/1/1}trk";

        private const string TrackSequenceElement = "{http://www.topografix.com/GPX/1/1}trkseg";

        private const string TrackpointElement = "{http://www.topografix.com/GPX/1/1}trkpt";

        #endregion

        public override Track ParseTrack(Stream stream)
        {
            XElement root = XElement.Load(stream);
            var gps = root.Element(TrackElement)
                .Elements(TrackSequenceElement)
                .Elements(TrackpointElement)
                .Select(x => new {
                    coordinate = new GpsCoordinates(Helpers.ParseDouble(x.FirstAttribute.Value),
                        Helpers.ParseDouble(x.LastAttribute.Value))
                })
                .Select(x => x.coordinate)
                .ToList();
            return new Track(gps);
        }
    }
}
