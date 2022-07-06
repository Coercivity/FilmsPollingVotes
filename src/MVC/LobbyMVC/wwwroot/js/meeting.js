"use strict";

var hubConnection = new signalR.HubConnectionBuilder().withUrl("/lobby").build();




hubConnection.on("Send", function (data) {


    let elem = document.createElement("p");

    let messageObject = JSON.parse(data);
    console.log(messageObject);

    elem.appendChild(document.createTextNode(messageObject.Description));
    let firstElem = document.getElementById("chatroom").firstChild;
    document.getElementById("chatroom").insertBefore(elem, firstElem);
    

});

hubConnection.on("OnConnect", function (connectionId) {

    hubConnection.invoke("AddToGroup", getGroupName());
});



document.getElementById("sendBtn").addEventListener("click", function (e) {

    
    let message = document.getElementById("message").value;

    hubConnection.invoke("Send", message, getGroupName());

});


function getGroupName() {
    return window.location.href.substring(window.location.href.lastIndexOf('/') + 1).toString();
}


hubConnection.start();