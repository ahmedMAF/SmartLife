const feature = document.getElementById("feature");
const model = document.getElementById("model");
const photo = document.getElementById("photo");
const video = document.getElementById("video");

const iconfeature = document.getElementById("iconfeature");
const iconmodel = document.getElementById("iconmodel");
const iconphoto = document.getElementById("iconphoto");
const iconvideo = document.getElementById("iconvideo");

let f = 0;
let m = 0;
let p = 0;
let v = 0;
		
iconfeature.onclick = function(){
    let div = document.createElement("div");
    let input = document.createElement("input");
    input.type = "text";
    input.name = `Product.Features[${f}].Name`;
    input.placeholder = nameen;
    input.className = "form-control mt-2";

    let inputa = document.createElement("input");
    inputa.type = "text";
    inputa.name = `Product.Features[${f}].NameAr`;
    inputa.placeholder = namear;
    inputa.className = "form-control mt-2";

    let input1 = document.createElement("textarea");
    input1.name = `Product.Features[${f}].Description`;
    input1.placeholder = descen;
    input1.className = "form-control mt-2";

    let input1a = document.createElement("textarea");
    input1a.name = `Product.Features[${f}].DescriptionAr`;
    input1a.placeholder = descar;
    input1a.className = "form-control mt-2";

    let input22 = document.createElement("label");
    input22.className = "w-100 mb-1 mt-3 ps-2 text-start textar";
    input22.innerText = img;

    let input2 = document.createElement("input");
    input2.type = "file";
    input2.name = "FeatureImages";
    input2.className = "form-control mt-2";

    let input33 = document.createElement("label");
    input33.className = "w-100 mb-1 mt-3 ps-2 text-start textar";
    input33.innerText = data;

    let input3 = document.createElement("input");
    input3.type = "file";
    input3.name = "FeatureDataSheets";
    input3.className = "form-control mt-2";

    let input4 = document.createElement("input");
    input4.type = "text";
    input4.name = `Product.Features[${f}].GooglePlay`;
    input4.placeholder = gp;
    input4.className = "form-control mt-2";

    let input5 = document.createElement("input");
    input5.type = "text";
    input5.name = `Product.Features[${f}].AppStore`;
    input5.placeholder = as;
    input5.className = "form-control mt-2";

    let cancel = document.createElement("button");
    cancel.textContent = can;
    cancel.className = "btn btn-danger mx-auto mt-2 mb-5 d-block";
    cancel.onclick = function(e) {
        e.target.parentElement.remove();
        f--;
    };

    div.appendChild(input);
    div.appendChild(inputa);
    div.appendChild(input1);
    div.appendChild(input1a);
    div.appendChild(input22);
    div.appendChild(input2);
    div.appendChild(input33);
    div.appendChild(input3);
    div.appendChild(input4);
    div.appendChild(input5);
    div.appendChild(cancel);
    feature.appendChild(div);

    f++;
    AOS.refresh();
}

iconmodel.onclick = function(){
    let div = document.createElement("div");
    let input = document.createElement("input");
    input.type = "text";
    input.name = `Product.Models[${m}].Name`;
    input.placeholder = nameen;
    input.className = "form-control mt-2";

    let inputa = document.createElement("input");
    inputa.type = "text";
    inputa.name = `Product.Models[${m}].NameAr`;
    inputa.placeholder = namear;
    inputa.className = "form-control mt-2";

    let input1 = document.createElement("textarea");
    input1.name = `Product.Models[${m}].Description`;
    input1.placeholder = descen;
    input1.className = "form-control mt-2";

    let input1a = document.createElement("textarea");
    input1a.name = `Product.Models[${m}].DescriptionAr`;
    input1a.placeholder = descar;
    input1a.className = "form-control mt-2";

    let input22 = document.createElement("label");
    input22.className = "w-100 mb-1 mt-3 ps-2 text-start textar";
    input22.innerText = img;

    let input2 = document.createElement("input");
    input2.type = "file";
    input2.name = "ModelImages";
    input2.className = "form-control mt-2";

    let input33 = document.createElement("label");
    input33.className = "w-100 mb-1 mt-3 ps-2 text-start textar";
    input33.innerText = data;

    let input3 = document.createElement("input");
    input3.type = "file";
    input3.name = "ModelDataSheets";
    input3.className = "form-control mt-2";

    let input4 = document.createElement("input");
    input4.type = "text";
    input4.name = `Product.Models[${m}].GooglePlay`;
    input4.placeholder = gp;
    input4.className = "form-control mt-2";

    let input5 = document.createElement("input");
    input5.type = "text";
    input5.name = `Product.Models[${m}].AppStore`;
    input5.placeholder = as;
    input5.className = "form-control mt-2";

    let cancel = document.createElement("button");
    cancel.textContent = can;
    cancel.className = "btn btn-danger mx-auto mt-2 mb-5 d-block";
    cancel.onclick = function(e) {
        e.target.parentElement.remove();
        m--;
    };

    div.appendChild(input);
    div.appendChild(inputa);
    div.appendChild(input1);
    div.appendChild(input1a);
    div.appendChild(input22);
    div.appendChild(input2);
    div.appendChild(input33);
    div.appendChild(input3);
    div.appendChild(input4);
    div.appendChild(input5);
    div.appendChild(cancel);
    model.appendChild(div);

    m++;
    AOS.refresh();
}

iconphoto.onclick = function(){
    let div = document.createElement("div");
    let input = document.createElement("input");
    input.type = "text";
    input.name = `Product.Photos[${p}].Name`;
    input.placeholder = nameen;
    input.className = "form-control mt-2";
    
    let inputa = document.createElement("input");
    inputa.type = "text";
    inputa.name = `Product.Photos[${p}].NameAr`;
    inputa.placeholder = namear;
    inputa.className = "form-control mt-2";

    let input1a = document.createElement("textarea");
    input1a.name = `Product.Photos[${p}].Description`;
    input1a.placeholder = descen;
    input1a.className = "form-control mt-2";

    let input1 = document.createElement("textarea");
    input1.name = `Product.Photos[${p}].DescriptionAr`;
    input1.placeholder = descar;
    input1.className = "form-control mt-2";

    let input2 = document.createElement("input");
    input2.type = "file";
    input2.name = "PhotoFiles";
    input2.className = "form-control mt-2";

    let cancel = document.createElement("button");
    cancel.textContent = can;
    cancel.className = "btn btn-danger mx-auto mt-2 mb-5 d-block";
    cancel.onclick = function(e) {
        e.target.parentElement.remove();
        p--;
    };

    div.appendChild(input);
    div.appendChild(inputa);
    div.appendChild(input1a);
    div.appendChild(input1);
    div.appendChild(input2);
    div.appendChild(cancel);
    photo.appendChild(div);

    p++;
    AOS.refresh();
}

iconvideo.onclick = function(){
    let div = document.createElement("div");
    let input = document.createElement("input");
    input.type = "text";
    input.name = `VideoUrls[${v}]`;
    input.placeholder = vid;
    input.className = "form-control mt-2";

    let cancel = document.createElement("button");
    cancel.textContent = can;
    cancel.className = "btn btn-danger mx-auto mt-2 d-block";
    cancel.onclick = function(e) {
        e.target.parentElement.remove();
        v--;
    };

    div.appendChild(input);
    div.appendChild(cancel);
    video.appendChild(div);

    v++;
    AOS.refresh();
}