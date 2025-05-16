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
    input.name = `Contact.Addresses[${a}]`;
    input.placeholder = addressen;
    input.className = "form-control mt-2";
    address.appendChild(input);

    let input2 = document.createElement("input");
    input2.type = "text";
    input2.name = `Contact.AddressesAr[${a}]`;
    input2.placeholder = addressar;
    input2.className = "form-control mt-2";
    address.appendChild(input2);

    a++;
}

iconEmail.onclick = function(){
    let input = document.createElement("input");
    input.type = "email";
    input.name = `Contact.Emails[${e}]`;
    input.placeholder = emailh;
    input.className = "form-control mt-2";
    email.appendChild(input);
    e++;
}

iconPhone.onclick = function(){
    let input = document.createElement("input");
    input.type = "text";
    input.name = `Contact.Phones[${p}]`;
    input.placeholder = phoneh;
    input.className = "form-control mt-2";
    phone.appendChild(input);
    p++;
}

iconWhasapp.onclick = function(){
    let input = document.createElement("input");
    input.type = "text";
    input.name = `Contact.WhatsApps[${w}]`;
    input.placeholder = whasapph;
    input.className = "form-control mt-2";
    whatsApp.appendChild(input);
    w++;
}