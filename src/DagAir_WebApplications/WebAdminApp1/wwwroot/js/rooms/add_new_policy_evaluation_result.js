export function addNewPolicyEvaluationResult(message, listCounter, measurementDate){
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
    let date1 = measurementDate.split("-");
    let yyyy = date1[0];
    let mm = date1[1];
    let date2 = date1[2].split("T");
    let dd = date2[0];
    let date3 = date1[2].split(":");
    let date4 = date3[0].split("T");
    let hh = date4[1];
    let m = date3[1];
    let date5 = date3[2].split(".");
    let ss = date5[0];
    a1.textContent = `${dd}/${mm}/${yyyy} ${hh}:${m}:${ss}`;

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
    return li;
}

export function sayHello(number){
    console.log(`Saying Hello! {number}`);
}