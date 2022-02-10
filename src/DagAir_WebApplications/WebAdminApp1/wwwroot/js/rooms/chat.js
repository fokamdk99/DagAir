"use strict";

import {addNewPolicyEvaluationResult} from "./add_new_policy_evaluation_result.js";

function main(){
    var settings = JSON.parse(data);

    var connection = new signalR.HubConnectionBuilder().withUrl(settings["AdminNodeHub"]).withAutomaticReconnect().build();
    var listCounter = 0;

    connection.on("PoliciesEvaluationResultEvent", function (currentMeasurement) {
        var li = addNewPolicyEvaluationResult(currentMeasurement.message, listCounter, currentMeasurement.measurementDate);
        document.getElementById("messagesList").prepend(li);
        listCounter += 1;
        document.getElementById("current-measurement-temperature").textContent = `Temperature: ${currentMeasurement.temperature}`;
        document.getElementById("current-measurement-illuminance").textContent = `Illuminance: ${currentMeasurement.illuminance}`;
        document.getElementById("current-measurement-humidity").textContent = `Humidity: ${currentMeasurement.humidity}`;
        document.getElementById("current-policy-temperature").textContent = `Expected temperature level: ${currentMeasurement.roompolicydto.temperature}`;
        document.getElementById("current-policy-illuminance").textContent = `Expected illuminance level: ${currentMeasurement.roompolicydto.illuminance}`;
        document.getElementById("current-policy-humidity").textContent = `Expected humidity level: ${currentMeasurement.roompolicydto.humidity}`;
    });

    document.getElementById("getCurrentMeasurement").addEventListener("click", async () => {
        try {
            await connection.invoke("GetCurrentMeasurement", roomId);
        } catch (err) {
            console.error(err);
        }
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





