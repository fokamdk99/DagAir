"use strict";

var uniqueRoomId;

function SetUniqueRoomId(roomId){
    uniqueRoomId = roomId;
}

var settings = JSON.parse(data);

var connection = new signalR.HubConnectionBuilder().withUrl(settings["AdminNodeHub"]).withAutomaticReconnect().build();
var listCounter = 0;

connection.on("PoliciesEvaluationResultEvent", function (message) {
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    var p1 = document.createElement("p");

    var a1 = document.createElement("a");
    a1.classList.add("btn");
    a1.classList.add("btn-primary");
    a1.setAttribute("data-toggle", "collapse");
    a1.setAttribute("aria-expanded", "false");
    a1.setAttribute("role", "button");
    a1.setAttribute("aria-controls", "collapse" + listCounter);
    a1.href = "#collapse" + listCounter;
    a1.textContent = `message ${listCounter}`;

    var div1 = document.createElement("div");
    div1.classList.add("collapse");
    div1.id = "collapse" + listCounter;

    var div2 = document.createElement("div");
    div2.classList.add("card");
    div2.classList.add("card-body");
    div2.textContent = `${message}`;

    p1.appendChild(a1);
    div1.appendChild(div2);
    li.appendChild(p1);
    li.appendChild(div1);
    listCounter += 1;
});

connection.start({ withCredentials: false }).then(function () {
    console.log("connection started");
    console.log("room Id: {0}", uniqueRoomId);
}).catch(function (err) {
    return console.error(err.toString());
}).then(function () {
    connection.invoke("SubscribeToPoliciesEvaluationResultEvent", uniqueRoomId).catch(function (err) {
        return console.error(err.toString());
    });
}).catch(function (err) {
    return console.error(err)
});