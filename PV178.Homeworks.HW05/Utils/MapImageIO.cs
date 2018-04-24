using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace PV178.Homeworks.HW05.Utils
{
    /// <summary>
    /// Utility class for image I/O operations
    /// </summary>
    public class MapImageIO
    {
        public static ImageFormat MapImgFormat => ImageFormat.Jpeg;

        /// <summary>
        /// Loads image to stream
        /// </summary>
        /// <param name="filePath">The full path of the image</param>
        /// <returns>Stream with image data</returns>
        public static Stream LoadImgToStream(string filePath)
        {
            // TODO

            throw new NotImplementedException();
        }

        /// <summary>
        /// Saves image to file, to set desired quality, see this link:
        /// https://msdn.microsoft.com/en-us/library/bb882583(v=vs.110).aspx
        /// </summary>
        /// <param name="stream">Stream with image data</param>
        /// <param name="outputPath">Output image path (without file name)</param>
        /// <param name="fileName">Image file name</param>
        public static void SaveImgToFile(Stream stream, string outputPath, string fileName)
        {
            // TODO

            throw new NotImplementedException();
        }
    }
}
