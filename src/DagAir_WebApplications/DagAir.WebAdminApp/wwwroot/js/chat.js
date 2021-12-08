"use strict";

var settings = JSON.parse(data);

var connection = new signalR.HubConnectionBuilder().withUrl(settings["AdminNodeHub"]).withAutomaticReconnect().build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("PoliciesEvaluationResultEvent", function (message) {
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    // We can assign user-supplied strings to an element's textContent because it
    // is not interpreted as markup. If you're assigning in any other way, you 
    // should be aware of possible script injection concerns.
    li.textContent = `Status: ${message}`;
});

connection.start({ withCredentials: false }).then(function () {
    document.getElementById("sendButton").disabled = false;
    console.log("connection started")
}).catch(function (err) {
    return console.error(err.toString());
}).then(function () {
    connection.invoke("SubscribeToPoliciesEvaluationResultEvent", "1538f2d5-a453-ec11-b57a-00155d0da468").catch(function (err) {
        return console.error(err.toString());
    });
}).catch(function (err) {
    return console.error(err)
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    connection.invoke("SubscribeToPoliciesEvaluationResultEvent", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});