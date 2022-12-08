"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/outputhub").build();

//Disable the send button until connection is established.
document.getElementById("submit_button").disabled = true;

connection.on("OutputChanged", function (value) {
    document.getElementById("checkbox_output18").checked=value;    
});

connection.start().then(function () {
    document.getElementById("submit_button").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("submit_button").addEventListener("click", function (event) {
    var value = document.getElementById("checkbox_output18").checked;
    connection.invoke("OutputChanged", value).catch(function (err) {
        return console.error(err.toString());
    });
    //event.preventDefault();
});