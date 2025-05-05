const address = document.getElementById("addresses");
const email = document.getElementById("emails");
const phone = document.getElementById("phones");
const whatsApp = document.getElementById("whatsup");

const iconAddress = document.getElementById("iconAddress");
const iconEmail = document.getElementById("iconEmail");
const iconPhone = document.getElementById("iconPhone");
const iconWhasapp = document.getElementById("iconWhasapp");

let a = -1;
let e = -1;
let p = -1;
let w = -1;

iconAddress.onclick = function () {
    if (a === -1) {
        const count = document.getElementById('address-initial-count');
        a = parseInt(count.value);
    }

    let input = document.createElement("input");
    input.type = "text";
    input.name = `Contact.Addresses[${a}]`;
    input.placeholder = "Address";
    input.className = "form-control mt-2";
    address.appendChild(input);
    a++;
}

iconEmail.onclick = function () {
    if (e === -1) {
        const count = document.getElementById('email-initial-count');
        e = parseInt(count.value);
    }

    let input = document.createElement("input");
    input.type = "email";
    input.name = `Contact.Emails[${e}]`;
    input.placeholder = "Email";
    input.className = "form-control mt-2";
    email.appendChild(input);
    e++;
}
iconPhone.onclick = function () {
    if (p === -1) {
        const count = document.getElementById('phone-initial-count');
        p = parseInt(count.value);
    }

    let input = document.createElement("input");
    input.type = "text";
    input.name = `Contact.Phones[${p}]`;
    input.placeholder = "Phone";
    input.className = "form-control mt-2";
    phone.appendChild(input);
    p++;
}
iconWhasapp.onclick = function () {
    if (w === -1) {
        const count = document.getElementById('whatsapp-initial-count');
        w = parseInt(count.value);
    }

    let input = document.createElement("input");
    input.type = "text";
    input.name = `Contact.WhatsApps[${w}]`;
    input.placeholder = "WhatsApp";
    input.className = "form-control mt-2";
    whatsApp.appendChild(input);
    w++;
}