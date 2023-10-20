"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

document.getElementById("submit_button").disabled = true;

connection.on("Output18", function (value) {
    console.log(`Output18 received, value is ${value}`)
    document.getElementById("checkbox_output18").checked=value;
});

connection.start().then(function () {
    document.getElementById("submit_button").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("submit_button").addEventListener("click", function (event) {
    var value = document.getElementById("checkbox_output18").checked;
    console.log(value);
    connection.invoke("OutputChanged", value).catch(function (err) {
        return console.error(err.toString());
    });
    //event.preventDefault();
});