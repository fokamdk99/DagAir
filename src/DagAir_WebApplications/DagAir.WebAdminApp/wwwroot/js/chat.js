"use strict";

var uniqueRoomId;

function SetUniqueRoomId(roomId){
    uniqueRoomId = roomId;
}

var settings = JSON.parse(data);

var connection = new signalR.HubConnectionBuilder().withUrl(settings["AdminNodeHub"]).withAutomaticReconnect().build();

connection.on("PoliciesEvaluationResultEvent", function (message) {
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    // We can assign user-supplied strings to an element's textContent because it
    // is not interpreted as markup. If you're assigning in any other way, you 
    // should be aware of possible script injection concerns.
    li.textContent = `Status: ${message}`;
});

connection.start({ withCredentials: false }).then(function () {
    console.log("connection started");
    console.log("room Id: {0}", uniqueRoomId);
}).catch(function (err) {
    return console.error(err.toString());
}).then(function () {
    console.log("connection started!!")
    connection.invoke("SubscribeToPoliciesEvaluationResultEvent", uniqueRoomId).catch(function (err) {
        return console.error(err.toString());
    });
}).catch(function (err) {
    return console.error(err)
});