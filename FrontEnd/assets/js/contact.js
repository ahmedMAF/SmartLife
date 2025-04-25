const address = document.getElementById("addresses");
const email = document.getElementById("emails");
const phone = document.getElementById("phones");
const whatsApp = document.getElementById("whatsup");

const iconAddress = document.getElementById("iconAddress");
const iconEmail = document.getElementById("iconEmail");
const iconPhone = document.getElementById("iconPhone");
const iconWhasapp = document.getElementById("iconWhasapp");

iconAddress.onclick = function(){
    let input = document.createElement("input");
    input.type = "text";
    input.name = "address";
    input.placeholder = "Address";
    input.className = "form-control mt-2";
    address.appendChild(input);
}
iconEmail.onclick = function(){
    let input = document.createElement("input");
    input.type = "email";
    input.name = "email";
    input.placeholder = "Email";
    input.className = "form-control mt-2";
    email.appendChild(input);
}
iconPhone.onclick = function(){
    let input = document.createElement("input");
    input.type = "text";
    input.name = "phone";
    input.placeholder = "Phone";
    input.className = "form-control mt-2";
    phone.appendChild(input);
}
iconWhasapp.onclick = function(){
    let input = document.createElement("input");
    input.type = "text";
    input.name = "whatsapp";
    input.placeholder = "WhatsApp";
    input.className = "form-control mt-2";
    whatsApp.appendChild(input);
}