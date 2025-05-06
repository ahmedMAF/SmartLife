let a = -1;
let e = -1;
let p = -1;
let w = -1;

function addAddress(c) {
    if (a === -1) {
        const count = document.getElementById('address-initial-count');
        a = parseInt(count.value);
    }

    let input = document.createElement("input");
    input.type = "text";
    input.name = `Contacts[${c}].Addresses[${a}]`;
    input.placeholder = "Address";
    input.className = "form-control mt-2";
    document.getElementById(`addresses-${c}`).appendChild(input);
    a++;
}

function addEmail(c) {
    if (e === -1) {
        const count = document.getElementById('email-initial-count');
        e = parseInt(count.value);
    }

    let input = document.createElement("input");
    input.type = "email";
    input.name = `Contacts[${c}].Emails[${e}]`;
    input.placeholder = "Email";
    input.className = "form-control mt-2";
    document.getElementById(`emails-${c}`).appendChild(input);
    e++;
}

function addPhone(c) {
    if (p === -1) {
        const count = document.getElementById('phone-initial-count');
        p = parseInt(count.value);
    }

    let input = document.createElement("input");
    input.type = "text";
    input.name = `Contacts[${c}].Phones[${p}]`;
    input.placeholder = "Phone";
    input.className = "form-control mt-2";
    document.getElementById(`phones-${c}`).appendChild(input);
    p++;
}

function addWhatsApp(c) {
    if (w === -1) {
        const count = document.getElementById('whatsapp-initial-count');
        w = parseInt(count.value);
    }

    let input = document.createElement("input");
    input.type = "text";
    input.name = `Contacts[${c}].WhatsApps[${w}]`;
    input.placeholder = "WhatsApp";
    input.className = "form-control mt-2";
    document.getElementById(`whatsapps-${c}`).appendChild(input);
    w++;
}