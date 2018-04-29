using System.IO;
using System.Xml.Linq;
using System.Globalization;
using System.Linq;
using PV178.Homeworks.HW05.Model;


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
            // utils, pomocne metody pro parsovani souradnic
            XElement root = XElement.Load(stream);  // uzavrit stream?
            var gps = root.Element(TrackElement)
                .Elements(TrackSequenceElement)
                .Elements(TrackpointElement)
                .Select(x => new {
                    coordinate = new GpsCoordinates(double.Parse(x.FirstAttribute.Value, CultureInfo.InvariantCulture),
                        double.Parse(x.LastAttribute.Value, CultureInfo.InvariantCulture))
                })
                .Select(x => x.coordinate)
                .ToList();
            return new Track(gps);
        }
    }
}
