const address = document.getElementById("addresses");
const email = document.getElementById("emails");
const phone = document.getElementById("phones");
const whatsApp = document.getElementById("whatsup");

const iconAddress = document.getElementById("iconAddress");
const iconEmail = document.getElementById("iconEmail");
const iconPhone = document.getElementById("iconPhone");
const iconWhasapp = document.getElementById("iconWhasapp");

iconAddress.onclick = function(){
    a++;
    let input = document.createElement("input");
    input.type = "text";
    input.name = `Contact.Addresses[${a}]`;
    input.placeholder = "Address";
    input.className = "form-control mt-2";
    address.appendChild(input);
}
iconEmail.onclick = function(){
    e++;
    let input = document.createElement("input");
    input.type = "email";
    input.name = `Contact.Emails[${e}]`;
    input.placeholder = "Email";
    input.className = "form-control mt-2";
    email.appendChild(input);
}
iconPhone.onclick = function(){
    p++;
    let input = document.createElement("input");
    input.type = "text";
    input.name = `Contact.Phones[${p}]`;
    input.placeholder = "Phone";
    input.className = "form-control mt-2";
    phone.appendChild(input);
}
iconWhasapp.onclick = function(){
    w++;
    let input = document.createElement("input");
    input.type = "text";
    input.name = `Contact.WhatsApps[${w}]`;
    input.placeholder = "WhatsApp";
    input.className = "form-control mt-2";
    whatsApp.appendChild(input);
}