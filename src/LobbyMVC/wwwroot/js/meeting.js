"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/lobby").build();

$(function (){
    connection.start().then(function () {
        alert("Connected to lobby");
    }).catch(function (err) {
        return console.error(err.toString());
    });

});