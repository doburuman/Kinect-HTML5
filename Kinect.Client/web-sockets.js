window.onload = function () {
    var status = document.getElementById("status");
    var canvas = document.getElementById("canvas");
    var buttonColor = document.getElementById("color");
    var buttonDepth = document.getElementById("depth");
    var context = canvas.getContext("2d");

    var camera = new Image();

    camera.onload = function () {
        context.drawImage(camera, 0, 0);
    }

    if (!window.WebSocket) {
        status.innerHTML = "Your browser does not support web sockets! :'-(";
        return;
    }

    status.innerHTML = "Connecting to server...";

    // Initialize a new web socket.
    var socket = new WebSocket("ws://localhost:8181");

    // Connection established.
    socket.onopen = function () {
        status.innerHTML = "Connection successful.";
    };

    // Connection closed.
    socket.onclose = function () {
        status.innerHTML = "Connection closed.";
    }

    // Receive data FROM the server!
    socket.onmessage = function (event) {
        if (typeof event.data === "string") {
            // SKELETON DATA

            // Get the data in JSON format.
            var jsonObject = JSON.parse(event.data);

            // Display the skeleton joints.
			context.beginPath();
			
			// head to neck
			context.moveTo(jsonObject.skeletons[i].joints[3].x,jsonObject.skeletons[i].joints[3].y);
			context.lineTo(jsonObject.skeletons[i].joints[2].x,jsonObject.skeletons[i].joints[2].y);
			context.stroke();
			
			// neck to spine shoulder
			context.moveTo(jsonObject.skeletons[i].joints[2].x,jsonObject.skeletons[i].joints[2].y);
			context.lineTo(jsonObject.skeletons[i].joints[20].x,jsonObject.skeletons[i].joints[20].y);
			
			// spine shoulder to left shoulder
			context.moveTo(jsonObject.skeletons[i].joints[20].x,jsonObject.skeletons[i].joints[20].y);
			context.lineTo(jsonObject.skeletons[i].joints[4].x,jsonObject.skeletons[i].joints[4].y);
			
			// spine shoulder to right shoulder
			context.moveTo(jsonObject.skeletons[i].joints[20].x,jsonObject.skeletons[i].joints[20].y);
			context.lineTo(jsonObject.skeletons[i].joints[8].x,jsonObject.skeletons[i].joints[8].y);
			
			// right shoulder to right elbow
			context.moveTo(jsonObject.skeletons[i].joints[8].x,jsonObject.skeletons[i].joints[8].y);
			context.lineTo(jsonObject.skeletons[i].joints[9].x,jsonObject.skeletons[i].joints[9].y);
			context.stroke();
			
			// right elbow to right wrist
			context.moveTo(jsonObject.skeletons[i].joints[9].x,jsonObject.skeletons[i].joints[9].y);
			context.lineTo(jsonObject.skeletons[i].joints[10].x,jsonObject.skeletons[i].joints[10].y);
			context.stroke();
			
			// right wrist to right hand
			context.moveTo(jsonObject.skeletons[i].joints[10].x,jsonObject.skeletons[i].joints[10].y);
			context.lineTo(jsonObject.skeletons[i].joints[11].x,jsonObject.skeletons[i].joints[11].y);
			context.stroke();
			
			// right hand to right hand tip
			context.moveTo(jsonObject.skeletons[i].joints[11].x,jsonObject.skeletons[i].joints[11].y);
			context.lineTo(jsonObject.skeletons[i].joints[23].x,jsonObject.skeletons[i].joints[23].y);
			context.stroke();
			
			// right hand to right hand thumb
			context.moveTo(jsonObject.skeletons[i].joints[11].x,jsonObject.skeletons[i].joints[11].y);
			context.lineTo(jsonObject.skeletons[i].joints[24].x,jsonObject.skeletons[i].joints[24].y);
			context.stroke();
			
			// left shoulder to left elbow
			context.moveTo(jsonObject.skeletons[i].joints[4].x,jsonObject.skeletons[i].joints[4].y);
			context.lineTo(jsonObject.skeletons[i].joints[5].x,jsonObject.skeletons[i].joints[5].y);
			context.stroke();
			
			// left elbow to left wrist
			context.moveTo(jsonObject.skeletons[i].joints[5].x,jsonObject.skeletons[i].joints[5].y);
			context.lineTo(jsonObject.skeletons[i].joints[6].x,jsonObject.skeletons[i].joints[6].y);
			context.stroke();
			
			// left wrist to left hand
			context.moveTo(jsonObject.skeletons[i].joints[6].x,jsonObject.skeletons[i].joints[6].y);
			context.lineTo(jsonObject.skeletons[i].joints[7].x,jsonObject.skeletons[i].joints[7].y);
			context.stroke();
			
			// left hand to left hand tip
			context.moveTo(jsonObject.skeletons[i].joints[7].x,jsonObject.skeletons[i].joints[7].y);
			context.lineTo(jsonObject.skeletons[i].joints[21].x,jsonObject.skeletons[i].joints[21].y);
			context.stroke();
			
			// left hand to left thumb
			context.moveTo(jsonObject.skeletons[i].joints[7].x,jsonObject.skeletons[i].joints[7].y);
			context.lineTo(jsonObject.skeletons[i].joints[22].x,jsonObject.skeletons[i].joints[22].y);
			context.stroke();
			
			// spine shoulder to spine mid
			context.moveTo(jsonObject.skeletons[i].joints[20].x,jsonObject.skeletons[i].joints[20].y);
			context.lineTo(jsonObject.skeletons[i].joints[1].x,jsonObject.skeletons[i].joints[1].y);
			context.stroke();
			
			// spine mid to spine base
			context.moveTo(jsonObject.skeletons[i].joints[1].x,jsonObject.skeletons[i].joints[1].y);
			context.lineTo(jsonObject.skeletons[i].joints[0].x,jsonObject.skeletons[i].joints[0].y);
			context.stroke();
			
			// spine base to hip left
			context.moveTo(jsonObject.skeletons[i].joints[0].x,jsonObject.skeletons[i].joints[0].y);
			context.lineTo(jsonObject.skeletons[i].joints[12].x,jsonObject.skeletons[i].joints[12].y);
			context.stroke();
			
			// spine base to hip right
			context.moveTo(jsonObject.skeletons[i].joints[0].x,jsonObject.skeletons[i].joints[0].y);
			context.lineTo(jsonObject.skeletons[i].joints[16].x,jsonObject.skeletons[i].joints[16].y);
			context.stroke();
			
			// spine base to knee right
			context.moveTo(jsonObject.skeletons[i].joints[16].x,jsonObject.skeletons[i].joints[16].y);
			context.lineTo(jsonObject.skeletons[i].joints[17].x,jsonObject.skeletons[i].joints[17].y);
			context.stroke();
			
			// knee right to ankle right
			context.moveTo(jsonObject.skeletons[i].joints[17].x,jsonObject.skeletons[i].joints[17].y);
			context.lineTo(jsonObject.skeletons[i].joints[18].x,jsonObject.skeletons[i].joints[18].y);
			context.stroke();
			
			// ankle right to foot right
			context.moveTo(jsonObject.skeletons[i].joints[18].x,jsonObject.skeletons[i].joints[18].y);
			context.lineTo(jsonObject.skeletons[i].joints[19].x,jsonObject.skeletons[i].joints[19].y);
			context.stroke();
			
			// spine base to knee left
			context.moveTo(jsonObject.skeletons[i].joints[12].x,jsonObject.skeletons[i].joints[12].y);
			context.lineTo(jsonObject.skeletons[i].joints[13].x,jsonObject.skeletons[i].joints[13].y);
			context.stroke();
			
			// knee left to ankle left
			context.moveTo(jsonObject.skeletons[i].joints[13].x,jsonObject.skeletons[i].joints[13].y);
			context.lineTo(jsonObject.skeletons[i].joints[14].x,jsonObject.skeletons[i].joints[14].y);
			context.stroke();
			
			// ankle left to foot left
			context.moveTo(jsonObject.skeletons[i].joints[14].x,jsonObject.skeletons[i].joints[14].y);
			context.lineTo(jsonObject.skeletons[i].joints[15].x,jsonObject.skeletons[i].joints[15].y);
			context.stroke();
			
			context.closePath();
        }
        else if (event.data instanceof Blob) {
            // RGB FRAME DATA
            // 1. Get the raw data.
            var blob = event.data;

            // 2. Create a new URL for the blob object.
            window.URL = window.URL || window.webkitURL;

            var source = window.URL.createObjectURL(blob);

            // 3. Update the image source.
            camera.src = source;

            // 4. Release the allocated memory.
            window.URL.revokeObjectURL(source);
        }
    };

    buttonColor.onclick = function () {
        socket.send("Color");
    }

    buttonDepth.onclick = function () {
        socket.send("Depth");
    }
};