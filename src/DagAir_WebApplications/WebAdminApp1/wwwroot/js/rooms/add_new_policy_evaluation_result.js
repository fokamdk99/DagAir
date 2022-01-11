function addNewPolicyEvaluationResult(message, listCounter){
    var li = document.createElement("li");
    
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
    div2.textContent = "div2 content 234";

    p1.appendChild(a1);
    div1.appendChild(div2);
    li.appendChild(p1);
    li.appendChild(div1);
    
    return li;
}

var uniqueRoomId;

function setUniqueRoomId(roomId){
    uniqueRoomId = roomId;
}

function getUniqueRoomId(){
    return uniqueRoomId;
}