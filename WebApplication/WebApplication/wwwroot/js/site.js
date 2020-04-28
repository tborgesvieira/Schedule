// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/updateHub").build();

connection.on("ReceiveMessage", function (message) {
    console.log(message);
    document.getElementById("update").innerHTML = message;
});

connection.start();