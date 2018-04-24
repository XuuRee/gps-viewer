using System;
using System.Globalization;

namespace PV178.Homeworks.HW05.Utils
{
    public static class Helpers
    {
        public static double ParseDouble(string stringToParse)
            => double.Parse(stringToParse.Trim(), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);

        public static decimal ParseDecimal(string stringToParse)
            => decimal.Parse(stringToParse.Trim(), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);

        /// <summary>
        /// Converts GPS coordinate from DMS (degrees, minutes, secondes)
        /// to DD (decimal degrees) format.
        /// </summary>
        /// <param name="degrees">value in degrees °</param>
        /// <param name="minutes">value in minutes '</param>
        /// <param name="seconds">value in seconds ''</param>
        /// <returns>Coordinate in decimal degree representation</returns>
        public static double ConvertDMSToDD(int degrees, int minutes, decimal seconds)
        {
            var decimalValue = Math.Round(degrees + minutes / 60m + seconds / 3600m, 6);
            return double.Parse(decimalValue.ToString(CultureInfo.InvariantCulture),
                NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);
        }
    }
}
