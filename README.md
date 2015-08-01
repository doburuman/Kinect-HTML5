Kinect-HTML5 (Version 2)
============

Display Kinect version 2 data on an HTML5 canvas using WebSockets.

* Color frames
* Depth frames
* Bodies

![Displaying Kinect body data on an HTML5 canvas](https://raw.github.com/LightBuzz/Kinect-HTML5/master/Screenshot.png)

Description
---
This project connects a Kinect-enabled application to an HTML5 web page and displays the users' bodies.

The application acts as a WebSocket server, transmitting new body data whenever Kinect version 2 frames are available. The web page uses WebSockets to get the Kinect version 2 data and display it on a canvas.

Prerequisites
---
* [Kinect for Windows version 2](http://www.amazon.com/Microsoft-Kinect-for-Windows-V2/dp/B00KZIVEXO) sensor
* [Kinect for Windows SDK v2.0](http://www.microsoft.com/en-au/download/details.aspx?id=44561)
* [Microsoft Windows 8, 8.1 or 10] (http://www.microsoft.com/en-au/windows)

WebSockets
---
Read more about WebSockets in the book [Getting Started with HTML5 WebSocket Programming, by Vangos Pterneas](http://amzn.to/19cvMj9).

Credits
---
* This code is based upon the Kinect version 1 code developed by [Vangos Pterneas](http://pterneas.com) for [LightBuzz](http://lightbuzz.com)
* Kinect version 2 port developed by [Callum Parker](http://callumparker.com) & [Soojeong Yoo](http://soojeongyoo.com) from the University of Sydney
* The WebSocket server application uses [Fleck, by Jason Staten](https://github.com/statianzo/Fleck)

License
---
You are free to use these libraries in personal and commercial projects by attributing the original creator of Vitruvius. Licensed under [Apache v2 License](https://github.com/LightBuzz/Kinect-HTML5/blob/master/LICENSE).
