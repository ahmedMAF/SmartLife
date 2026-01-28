const address = document.getElementById("addresses");
const email = document.getElementById("emails");
const phone = document.getElementById("phones");
const whatsApp = document.getElementById("whatsup");

const iconAddress = document.getElementById("iconAddress");
const iconEmail = document.getElementById("iconEmail");
const iconPhone = document.getElementById("iconPhone");
const iconWhasapp = document.getElementById("iconWhasapp");

iconAddress.onclick = function(){
    let div = document.createElement("div");
    let cancel = document.createElement("button");
    cancel.textContent = can;
    cancel.className = "btn btn-danger mx-auto mt-2 mb-5 d-block";
    cancel.onclick = function(e) {
        e.target.parentElement.remove();
        a--;
    };
    let input = document.createElement("input");
    input.type = "text";
    input.name = `Contact.Addresses[${a}]`;
    input.placeholder = addressen;
    input.className = "form-control mt-2";
    div.appendChild(input);

    let input2 = document.createElement("input");
    input2.type = "text";
    input2.name = `Contact.AddressesAr[${a}]`;
    input2.placeholder = addressar;
    input2.className = "form-control mt-2";
    div.appendChild(input2);
    div.appendChild(cancel);
    address.appendChild(div);

    a++;
    AOS.refresh();
}

iconEmail.onclick = function(){
    let div = document.createElement("div");
    let input = document.createElement("input");
    input.type = "email";
    input.name = `Contact.Emails[${e}]`;
    input.placeholder = emailh;
    input.className = "form-control mt-2";
    let cancel = document.createElement("button");
    cancel.textContent = can;
    cancel.className = "btn btn-danger mx-auto mt-2 d-block";
    cancel.onclick = function(e) {
        e.target.parentElement.remove();
        e--;
    };
    div.appendChild(input);
    div.appendChild(cancel);
    email.appendChild(div);
    e++;
    AOS.refresh();
}

iconPhone.onclick = function(){
    let div = document.createElement("div");
    let input = document.createElement("input");
    input.type = "text";
    input.name = `Contact.Phones[${p}]`;
    input.placeholder = phoneh;
    input.className = "form-control mt-2";
    let cancel = document.createElement("button");
    cancel.textContent = can;
    cancel.className = "btn btn-danger mx-auto mt-2 d-block";
    cancel.onclick = function(e) {
        e.target.parentElement.remove();
        p--;
    };
    div.appendChild(input);
    div.appendChild(cancel);
    phone.appendChild(div);
    p++;
    AOS.refresh();
}

iconWhasapp.onclick = function(){
    let div = document.createElement("div");
    let input = document.createElement("input");
    input.type = "text";
    input.name = `Contact.WhatsApps[${w}]`;
    input.placeholder = whasapph;
    input.className = "form-control mt-2";
    let cancel = document.createElement("button");
    cancel.textContent = can;
    cancel.className = "btn btn-danger mx-auto mt-2 d-block";
    cancel.onclick = function(e) {
        e.target.parentElement.remove();
        w--;
    };
    div.appendChild(input);
    div.appendChild(cancel);
    whatsApp.appendChild(div);
    w++;
    AOS.refresh();
}