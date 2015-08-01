using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fleck;
using Microsoft.Kinect;
using System.IO;

namespace Kinect.Server
{
    class Program
    {
        static List<IWebSocketConnection> _clients = new List<IWebSocketConnection>();

        //static Skeleton[] _skeletons = new Skeleton[6];
        KinectSensor _sensor;
        MultiSourceFrameReader _reader;
        IList<Body> _bodies;
        

        // Initialise
        static void Main(string[] args)
        {
            /*
            // write to log
            using (StreamWriter writer = File.AppendText("logFile.txt"))
            {
                Log("Server Started... ", writer);

                writer.Close();
            }*/

            InitializeConnection();
            Program serv = new Program();
            serv.InitilizeKinect();
           // InitilizeKinect();

            Console.ReadLine();
        }

        static Mode _mode = Mode.Color;

        static CoordinateMapper _coordinateMapper;

        // Framereader
        void Reader_MultiSourceFrameArrived(object sender, MultiSourceFrameArrivedEventArgs e)
        {
            var reference = e.FrameReference.AcquireFrame();

            // Color
            using (var frame = reference.ColorFrameReference.AcquireFrame())
            {
                if (frame != null)
                {
                    if (_mode == Mode.Color)
                    {
                        var blob = frame.Serialize();

                        foreach (var socket in _clients)
                        {
                           //socket.Send(blob);
                        }
                    }
                }
            }

            // Depth
            using (var frame = reference.DepthFrameReference.AcquireFrame())
            {
                if (frame != null)
                {
                    if (_mode == Mode.Depth)
                    {
                        var blob = frame.Serialize();

                        foreach (var socket in _clients)
                        {
                            //socket.Send(blob);
                        }
                    }
                }
            }

            // Body
            using (var frame = reference.BodyFrameReference.AcquireFrame())
            {
                if (frame != null)
                {
                    _bodies = new Body[frame.BodyFrameSource.BodyCount];

                    frame.GetAndRefreshBodyData(_bodies);

                    var users = _bodies.ToList();
                   

                    if (users.Count > 0)
                    {

                        //string json = users.Serialize(_coordinateMapper, _mode);
                        string json = users.Serialize(_coordinateMapper, _mode);

                        foreach (var body in _bodies)
                        {
                            if (body != null)
                            {
                                foreach(var socket in _clients)
                                {
                                    socket.Send(json);

                                    // Find the right hand state
                                    switch (body.HandRightState)
                                    {
                                        case HandState.Open:
                                            socket.Send("R-Open");
                                            break;
                                        case HandState.Closed:
                                            socket.Send("R-Closed");
                                            break;
                                        case HandState.Lasso:
                                            break;
                                        case HandState.Unknown:
                                            socket.Send("none");
                                            break;
                                        case HandState.NotTracked:
                                            socket.Send("none");
                                            break;
                                        default:
                                            break;
                                    }

                                    // Find the left hand state
                                    switch (body.HandLeftState)
                                    {
                                        case HandState.Open:
                                            socket.Send("L-Open");
                                            break;
                                        case HandState.Closed:
                                            socket.Send("L-Closed");
                                            break;
                                        case HandState.Lasso:
                                            break;
                                        case HandState.Unknown:
                                            socket.Send("none");
                                            break;
                                        case HandState.NotTracked:
                                            socket.Send("none");
                                            break;
                                        default:
                                            break;
                                    }     
                                }
                            }
                        }
                    }
                }
            }
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



        private static void InitializeConnection()
        {
            var server = new WebSocketServer("ws://127.0.0.1:8181");

            server.Start(socket =>
            {
                socket.OnOpen = () =>
                {
                    _clients.Add(socket);
                };

                socket.OnClose = () =>
                {
                    _clients.Remove(socket);
                };

                socket.OnMessage = message =>
                {
                    switch (message)
                    {
                        case "Color":
                            _mode = Mode.Color;
                            break;
                        case "Depth":
                            _mode = Mode.Depth;
                            break;
                        default:
                            break;
                    }

                    Console.WriteLine("Switched to " + message);
                };
            });
        }

        private void InitilizeKinect()
        {
            //var sensor = KinectSensor.KinectSensors.SingleOrDefault();

            _sensor = KinectSensor.GetDefault();


            if (_sensor != null)
            {
                _sensor.Open();
            }

            _reader = _sensor.OpenMultiSourceFrameReader(FrameSourceTypes.Color |
                                                 FrameSourceTypes.Depth |
                                                 FrameSourceTypes.Infrared |
                                                 FrameSourceTypes.Body);
            _reader.MultiSourceFrameArrived += Reader_MultiSourceFrameArrived;

            _coordinateMapper = _sensor.CoordinateMapper;

        }
    }
}
