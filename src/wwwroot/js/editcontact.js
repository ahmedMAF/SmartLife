function addAddress(c) {
    const count = document.getElementById(`address-initial-count-${c}`);
    let a = parseInt(count.value);

    let input = document.createElement("input");
    input.type = "text";
    input.name = `Contacts[${c}].Addresses[${a}]`;
    input.placeholder = "Address";
    input.className = "form-control mt-2";
    document.getElementById(`addresses-${c}`).appendChild(input);
    a++;
    count.value = a;
}

function addEmail(c) {
    const count = document.getElementById(`email-initial-count-${c}`);
    let e = parseInt(count.value);

    let input = document.createElement("input");
    input.type = "email";
    input.name = `Contacts[${c}].Emails[${e}]`;
    input.placeholder = "Email";
    input.className = "form-control mt-2";
    document.getElementById(`emails-${c}`).appendChild(input);

    e++;
    count.value = e;
}

function addPhone(c) {
    const count = document.getElementById(`phone-initial-count-${c}`);
    let p = parseInt(count.value);

    let input = document.createElement("input");
    input.type = "text";
    input.name = `Contacts[${c}].Phones[${p}]`;
    input.placeholder = "Phone";
    input.className = "form-control mt-2";
    document.getElementById(`phones-${c}`).appendChild(input);

    p++;
    count.value = p;
}

function addWhatsApp(c) {
    const count = document.getElementById(`whatsapp-initial-count-${c}`);
    let w = parseInt(count.value);

    let input = document.createElement("input");
    input.type = "text";
    input.name = `Contacts[${c}].WhatsApps[${w}]`;
    input.placeholder = "WhatsApp";
    input.className = "form-control mt-2";
    document.getElementById(`whatsapps-${c}`).appendChild(input);

    w++;
    count.value = w;
}