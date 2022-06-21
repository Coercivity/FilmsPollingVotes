"use strict";

var hubConnection = new signalR.HubConnectionBuilder().withUrl("/lobby").build();




hubConnection.on("Send", function (data) {

    let elem = document.createElement("p");

    let messageObject = JSON.parse(data);

    

    elem.appendChild(document.createTextNode(messageObject));
    let firstElem = document.getElementById("chatroom").firstChild;
    document.getElementById("chatroom").insertBefore(elem, firstElem);

});

document.getElementById("sendBtn").addEventListener("click", function (e) {

    let message = document.getElementById("message").value;
    hubConnection.invoke("Send", message);
});

hubConnection.start();