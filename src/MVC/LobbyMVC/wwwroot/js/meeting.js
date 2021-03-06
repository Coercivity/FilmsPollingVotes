"use strict";

var hubConnection = new signalR.HubConnectionBuilder().withUrl("/lobby").build();

hubConnection.on("AddItem", function (data) {

    let elem = document.createElement("p");
    let img = document.createElement('img');
    let button = document.createElement('button');

    let messageObject = JSON.parse(data);

    button.onclick = function () {
        document.getElementById("chatroom").removeChild(elem);
        document.getElementById("chatroom").removeChild(img);
        document.getElementById("chatroom").removeChild(button);
        hubConnection.invoke("RemoveItem", messageObject.Film.Id, getGroupName());
    }
    button.data = messageObject.Film.NameRu;

    img.src = messageObject.Film.PosterUrl;
    console.log(messageObject);

    elem.appendChild(document.createTextNode(messageObject.Film.Description));
    let firstElem = document.getElementById("chatroom").firstChild;
    document.getElementById("chatroom").insertBefore(elem, firstElem);
    document.getElementById("chatroom").insertBefore(img, firstElem);
    document.getElementById("chatroom").insertBefore(button, firstElem);

});


hubConnection.on("UpdateItemsForNewConnection", function (data) {

    let messageObject = JSON.parse(data);

    for (var i = 0; i < messageObject.length; i++) {
        let elem = document.createElement("p");
        var img = document.createElement('img');

        let button = document.createElement('button');


        let messageObject = JSON.parse(data);

        let filmId = messageObject[i].Film.Id;

        button.onclick = function () {
            document.getElementById("chatroom").removeChild(elem);
            document.getElementById("chatroom").removeChild(img);
            document.getElementById("chatroom").removeChild(button);
            hubConnection.invoke("RemoveItem", filmId, getGroupName());
        }

        img.src = messageObject[i].Film.PosterUrl

        elem.appendChild(document.createTextNode(messageObject[i].Film.Description));
        let firstElem = document.getElementById("chatroom").firstChild;
        document.getElementById("chatroom").insertBefore(elem, firstElem);
        document.getElementById("chatroom").insertBefore(img, firstElem);
        document.getElementById("chatroom").insertBefore(button, firstElem);

    }

});


hubConnection.on("UpdateAllItems", function (data) {

    let messageObject = JSON.parse(data);

    document.getElementById("chatroom").innerHTML = '';

    for (var i = 0; i < messageObject.length; i++) {
        let elem = document.createElement("p");
        var img = document.createElement('img');

        let button = document.createElement('button');

        let messageObject = JSON.parse(data);
        let filmId = messageObject[i].Film.Id;

        button.onclick = function () {
            document.getElementById("chatroom").removeChild(elem);
            document.getElementById("chatroom").removeChild(img);
            document.getElementById("chatroom").removeChild(button);
            hubConnection.invoke("RemoveItem", filmId, getGroupName());
        }

        img.src = messageObject[i].Film.PosterUrl

        elem.appendChild(document.createTextNode(messageObject[i].Film.Description));
        let firstElem = document.getElementById("chatroom").firstChild;
        document.getElementById("chatroom").insertBefore(elem, firstElem);
        document.getElementById("chatroom").insertBefore(img, firstElem);
        document.getElementById("chatroom").insertBefore(button, firstElem);

    }

});


hubConnection.on("PresentWinner", function (data) {

    let messageObject = JSON.parse(data);

    for (var i = 0; i < messageObject.length; i++) {
        let elem = document.createElement("p");
        var img = document.createElement('img');

        let button = document.createElement('button');


        let messageObject = JSON.parse(data);

        let filmId = messageObject[i].Film.Id;

        button.onclick = function () {
            document.getElementById("chatroom").removeChild(elem);
            document.getElementById("chatroom").removeChild(img);
            document.getElementById("chatroom").removeChild(button);
            hubConnection.invoke("RemoveItem", filmId, getGroupName());
        }

        img.src = messageObject[i].Film.PosterUrl

        elem.appendChild(document.createTextNode(messageObject[i].Film.Description));
        let firstElem = document.getElementById("chatroom").firstChild;
        document.getElementById("chatroom").insertBefore(elem, firstElem);
        document.getElementById("chatroom").insertBefore(img, firstElem);
        document.getElementById("chatroom").insertBefore(button, firstElem);

    }
});



hubConnection.on("PresentWinner", function (winnerObject, films) {
    console.log(winnerObject);
    console.log(films);
});


//Get winner
document.getElementById('winnerButton').onclick = function () {
    hubConnection.invoke("GetLobbyWinner", getGroupName());
};


//Adding new user on connect
hubConnection.on("AddUser", function (userName) {
    let elem = document.createElement("p");
    elem.appendChild(document.createTextNode(userName));
    let firstElem = document.getElementById("users").firstChild;
    document.getElementById("users").insertBefore(elem, firstElem);
});


hubConnection.on("OnConnect", function (connectionId) {
    hubConnection.invoke("AddToGroup", getGroupName());
    hubConnection.invoke("UpdateItems", getGroupName(), false);
});


hubConnection.on("OnDisconnect", function (connectionId) {
    hubConnection.invoke("RemoveFromGroup", getGroupName());
});


//Sending item to everyone in group
document.getElementById("sendBtn").addEventListener("click", function (e) {

    let message = document.getElementById("message").value;
    hubConnection.invoke("AddItem", message, getGroupName());

});


function getGroupName() {
    return window.location.href.substring(window.location.href.lastIndexOf('/') + 1).toString();
}


hubConnection.start();