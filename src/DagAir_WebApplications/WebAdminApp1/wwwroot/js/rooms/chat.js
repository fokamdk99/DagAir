"use strict";

import {addNewPolicyEvaluationResult} from "./add_new_policy_evaluation_result.js";

function main(){
    var settings = JSON.parse(data);

    var connection = new signalR.HubConnectionBuilder().withUrl(settings["AdminNodeHub"]).withAutomaticReconnect().build();
    var listCounter = 0;

    connection.on("PoliciesEvaluationResultEvent", function (message) {
        var li = addNewPolicyEvaluationResult(message, listCounter);
        document.getElementById("messagesList").prepend(li);
        listCounter += 1;
    });

    connection.start({ withCredentials: false }).then(function () {
        console.log("room Id: {0}", roomId);
    }).catch(function (err) {
        return console.error(err.toString());
    }).then(function () {
        connection.invoke("SubscribeToPoliciesEvaluationResultEvent", roomId).catch(function (err) {
            return console.error(err.toString());
        });
    }).catch(function (err) {
        return console.error(err)
    });
}

main();





