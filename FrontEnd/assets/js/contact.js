const address = document.getElementById("addresses");
const email = document.getElementById("emails");
const phone = document.getElementById("phones");
const whatsApp = document.getElementById("whatsup");

const iconAddress = document.getElementById("iconAddress");
const iconEmail = document.getElementById("iconEmail");
const iconPhone = document.getElementById("iconPhone");
const iconWhasapp = document.getElementById("iconWhasapp");

let a = 0;
let e = 0;
let p = 0;
let w = 0;

iconAddress.onclick = function(){
    a++;
    let input = document.createElement("input");
    input.type = "text";
    input.name = `address${a}`;
    input.placeholder = "Address";
    input.className = "form-control mt-2";
    address.appendChild(input);
}
iconEmail.onclick = function(){
    e++;
    let input = document.createElement("input");
    input.type = "email";
    input.name = `email${e}`;
    input.placeholder = "Email";
    input.className = "form-control mt-2";
    email.appendChild(input);
}
iconPhone.onclick = function(){
    p++;
    let input = document.createElement("input");
    input.type = "text";
    input.name = `phone${p}`;
    input.placeholder = "Phone";
    input.className = "form-control mt-2";
    phone.appendChild(input);
}
iconWhasapp.onclick = function(){
    w++;
    let input = document.createElement("input");
    input.type = "text";
    input.name = `whatsapp${w}`;
    input.placeholder = "WhatsApp";
    input.className = "form-control mt-2";
    whatsApp.appendChild(input);
}