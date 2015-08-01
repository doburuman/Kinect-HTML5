using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Microsoft.Kinect;
using System.Windows;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace Kinect.Server
{
    /// <summary>
    /// Serializes a Kinect skeleton to JSON fromat.
    /// </summary>
    public static class SkeletonSerializer
    {
        [DataContract]
        class JSONSkeletonCollection
        {
            [DataMember(Name = "skeletons")]
            public List<JSONSkeleton> Skeletons { get; set; }
        }

        [DataContract]
        class JSONSkeleton
        {
            [DataMember(Name = "id")]
            public string ID { get; set; }

            [DataMember(Name = "joints")]
            public List<JSONJoint> Joints { get; set; }
        }

        [DataContract]
        class JSONJoint
        {
            [DataMember(Name = "name")]
            public string Name { get; set; }

            [DataMember(Name = "x")]
            public string X { get; set; }

            [DataMember(Name = "y")]
            public string Y { get; set; }

            [DataMember(Name = "z")]
            public string Z { get; set; }
        }

        public static void Log(string logMessage, TextWriter w)
        {
           /* w.Write("\r\nLog Entry : ");
            w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                DateTime.Now.ToLongDateString());
            w.WriteLine("  :");
            w.WriteLine("  :{0}", logMessage);
            w.WriteLine("-------------------------------");*/
        }


        /// <summary>
        /// Serializes an array of Kinect skeletons into an array of JSON skeletons.
        /// </summary>
        /// <param name="skeletons">The Kinect skeletons.</param>
        /// <param name="mapper">The coordinate mapper.</param>
        /// <param name="mode">Mode (color or depth).</param>
        /// <returns>A JSON representation of the skeletons.</returns>
        public static string Serialize(this List<Body> skeletons, CoordinateMapper mapper, Mode mode)
        {
            JSONSkeletonCollection jsonSkeletons = new JSONSkeletonCollection { Skeletons = new List<JSONSkeleton>() };

            foreach (Body skeleton in skeletons) //check if the skeleton is tracked first
            {
                if (skeleton.IsTracked)
                {
                    JSONSkeleton jsonSkeleton = new JSONSkeleton
                    {
                        ID = skeleton.TrackingId.ToString(),
                        Joints = new List<JSONJoint>()
                    };


                    IReadOnlyDictionary<JointType, Joint> joints = skeleton.Joints;
                    var jointPoints = new Dictionary<JointType, Point>();

                    foreach (JointType jointType in joints.Keys)
                    {
                        Joint joint = joints[jointType];


                        Point point = new Point();

                        switch (mode)
                        {
                            case Mode.Color:
                                ColorSpacePoint colorPoint = mapper.MapCameraPointToColorSpace(joint.Position);
                                point.X = colorPoint.X;
                                point.Y = colorPoint.Y;
                                break;
                            case Mode.Depth:
                                DepthSpacePoint depthPoint = mapper.MapCameraPointToDepthSpace(joint.Position);
                                point.X = depthPoint.X;
                                point.Y = depthPoint.Y;
                                break;
                            default:
                                break;
                        }


                        jsonSkeleton.Joints.Add(new JSONJoint
                        {
                            Name = joint.JointType.ToString().ToLower(),
                            X = point.X.ToString(),
                            Y = point.Y.ToString(),
                            Z = joint.Position.Z.ToString()
                        });
                    }

                    jsonSkeletons.Skeletons.Add(jsonSkeleton);
                }
            }
        

            return Serialize(jsonSkeletons);
        
        }


        /// <summary>
        /// Serializes an object to JSON.
        /// </summary>
        /// <param name="obj">The specified object.</param>
        /// <returns>A JSON representation of the object.</returns>
        private static string Serialize(object obj)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());

            using (MemoryStream ms = new MemoryStream())
            {
                serializer.WriteObject(ms, obj);

                return Encoding.Default.GetString(ms.ToArray());
            }
        }


    }
}
