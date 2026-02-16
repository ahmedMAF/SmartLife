const model = document.getElementById("model");

const iconmodel = document.getElementById("iconmodel");

let m = 0;

iconmodel.onclick = function () {
    let div = document.createElement("div");

    let input = document.createElement("input");
    input.type = "text";
    input.name = `NewScripts[${m}].Name`;
    input.placeholder = nameen;
    input.className = "form-control mt-2";

    let input1 = document.createElement("textarea");
    input1.name = `NewScripts[${m}].Content`;
    input1.placeholder = contenten;
    input1.className = "form-control mt-2";

    let label = document.createElement("label");
    label.textContent = locationen;
    label.className = "mt-3 ms-1 d-block";

    let select = document.createElement("select");
    select.name = `NewScripts[${m}].Location`;
    select.className = "select mt-2 w-100";

    const locations = [
        { value: 0, text: "Top Head" },
        { value: 1, text: "Bottom Head" },
        { value: 2, text: "Top Body" },
        { value: 3, text: "Bottom Body" }
    ];

    locations.forEach(loc => {
        let option = document.createElement("option");
        option.value = loc.value;
        option.textContent = loc.text;
        select.appendChild(option);
    });

    let cancel = document.createElement("button");
    cancel.type = "button";
    cancel.textContent = can;
    cancel.className = "btn btn-danger mx-auto mt-2 mb-5 d-block";
    cancel.onclick = function (e) {
        e.target.parentElement.remove();
        m--;
    };

    div.appendChild(input);
    div.appendChild(input1);
    div.appendChild(label);
    div.appendChild(select);
    div.appendChild(cancel);

    model.appendChild(div);

    m++;

    AOS.refresh();
}

