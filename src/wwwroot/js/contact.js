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
    let input = document.createElement("input");
    input.type = "text";
    input.name = `Contact.Addresses[${a}]`;
    input.placeholder = "Address";
    input.className = "form-control mt-2";
    address.appendChild(input);
    a++;
}
iconEmail.onclick = function(){
    let input = document.createElement("input");
    input.type = "email";
    input.name = `Contact.Emails[${e}]`;
    input.placeholder = "Email";
    input.className = "form-control mt-2";
    email.appendChild(input);
    e++;
}
iconPhone.onclick = function(){
    let input = document.createElement("input");
    input.type = "text";
    input.name = `Contact.Phones[${p}]`;
    input.placeholder = "Phone";
    input.className = "form-control mt-2";
    phone.appendChild(input);
    p++;
}
iconWhasapp.onclick = function(){
    let input = document.createElement("input");
    input.type = "text";
    input.name = `Contact.WhatsApps[${w}]`;
    input.placeholder = "WhatsApp";
    input.className = "form-control mt-2";
    whatsApp.appendChild(input);
    w++;
}