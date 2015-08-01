using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;

namespace Kinect.Server
{
    /// <summary>
    /// Handles depth frame serialization.
    /// </summary>
    public static class DepthSerializer
    {
        /// <summary>
        /// The depth bitmap source.
        /// </summary>
        static WriteableBitmap _depthBitmap = null;

        /// <summary>
        /// The RGB depth values.
        /// </summary>
        static byte[] _depthPixels = null;

        /// <summary>
        /// Depth frame width.
        /// </summary>
        static int _depthWidth;

        /// <summary>
        /// Depth frame height.
        /// </summary>
        static int _depthHeight;

        /// <summary>
        /// Depth frame stride.
        /// </summary>
        static int _depthStride;

        /// <summary>
        /// The actual depth values.
        /// </summary>
        static ushort[] _depthData = null;



        abstract class DepthMap
        {
            protected ushort minDepth;
            protected ushort maxDepth;

            protected DepthMap(ushort minDepth, ushort maxDepth)
            {
                this.minDepth = minDepth;
                this.maxDepth = maxDepth;
            }

            public abstract Color GetColorForDepth(ushort depth);
        }

        /// <summary>
        /// Serializes a depth frame.
        /// </summary>
        /// <param name="frame">The specified depth frame.</param>
        /// <returns>A binary representation of the frame.</returns>
        public static byte[] Serialize(this DepthFrame frame)
        {
            // START SERIALIZE
                    /// <summary>
        /// The DPI.
        /// </summary>
        double DPI = 96.0;

        /// <summary>
        /// Default format.
        /// </summary>
        PixelFormat FORMAT = PixelFormats.Bgra32;

        /// <summary>
        /// Bytes per pixel.
        /// </summary>
        int BYTES_PER_PIXEL = (PixelFormats.Bgr32.BitsPerPixel + 7) / 8;

            // Depth frame (512x424)
            int depthWidth = frame.FrameDescription.Width;
            int depthHeight = frame.FrameDescription.Height;
            WriteableBitmap _bitmap = null;

            int width = frame.FrameDescription.Width;
            int height = frame.FrameDescription.Height;

            ushort minDepth = frame.DepthMinReliableDistance;
            ushort maxDepth = frame.DepthMaxReliableDistance;

            ushort[] depthData = new ushort[width * height];
            byte[] pixelData = new byte[width * height * (PixelFormats.Bgr32.BitsPerPixel + 7) / 8];

            if (_bitmap == null)
            {
                _depthData = new ushort[depthWidth * depthHeight];
                _bitmap = new WriteableBitmap(depthWidth, depthHeight, DPI, DPI, FORMAT, null);
            }

            if ((depthWidth * depthHeight) == _depthData.Length)
            {
                frame.CopyFrameDataToArray(depthData);
            }

            

            int colorIndex = 0;
            for (int depthIndex = 0; depthIndex < depthData.Length; ++depthIndex)
            {
                ushort depth = depthData[depthIndex];
                byte intensity = (byte)(depth >= minDepth && depth <= maxDepth ? depth : 0);

                pixelData[colorIndex++] = intensity; // Blue
                pixelData[colorIndex++] = intensity; // Green
                pixelData[colorIndex++] = intensity; // Red

                ++colorIndex;
            }
            PixelFormat format = PixelFormats.Bgr32;
            int stride = width * format.BitsPerPixel / 8;

            _depthBitmap = new WriteableBitmap(width, height, DPI, DPI, FORMAT, null);

            _depthBitmap.WritePixels(new Int32Rect(0, 0, width, height), pixelData, stride, 0);

            //return FrameSerializer.CreateBlob(_depthBitmap, Constants.CAPTURE_FILE_DEPTH);

            
            
            //return FrameSerializer.CreateBlob(bitmap, Constants.CAPTURE_FILE_DEPTH);
            return FrameSerializer.CreateBlob(_depthBitmap, Constants.CAPTURE_FILE_DEPTH);

            /*
            if (_depthBitmap == null)
            {
                _depthWidth = frame.FrameDescription.Width;
                _depthHeight = frame.FrameDescription.Height;
                _depthStride = _depthWidth * Constants.PIXEL_FORMAT.BitsPerPixel / 8;
                //_depthData = new short[frame.PixelDataLength];
                _depthData = new ushort[frame.FrameDescription.LengthInPixels];
                _depthPixels = new byte[_depthHeight * _depthWidth * 4];
                _depthBitmap = new WriteableBitmap(_depthWidth, _depthHeight, Constants.DPI, Constants.DPI, Constants.PIXEL_FORMAT, null);
            }

            frame.CopyFrameDataToArray(_depthData);

            for (int depthIndex = 0, colorIndex = 0; depthIndex < _depthData.Length && colorIndex < _depthPixels.Length; depthIndex++, colorIndex += 4)
            {
                // Get the depth value.
                int depth = _depthData[depthIndex];    //DepthImageFrame.PlayerIndexBitmaskWidth;

                // Equal coloring for monochromatic histogram.
                byte intensity = (byte)(255 - (255 * Math.Max(depth - Constants.MIN_DEPTH_DISTANCE, 0) / (Constants.MAX_DEPTH_DISTANCE_OFFSET)));

                _depthPixels[colorIndex + 0] = intensity;
                _depthPixels[colorIndex + 1] = intensity;
                _depthPixels[colorIndex + 2] = intensity;
            }

            _depthBitmap.WritePixels(new Int32Rect(0, 0, _depthWidth, _depthHeight), _depthPixels, _depthStride, 0);*/

            //return FrameSerializer.CreateBlob(_depthBitmap, Constants.CAPTURE_FILE_DEPTH);



            // END SERIALIZE
        }
    }
}
