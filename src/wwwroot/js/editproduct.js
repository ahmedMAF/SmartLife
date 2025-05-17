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
    let input = document.createElement("input");
    input.type = "text";
    input.name = `Features[${f}].Name`;
    input.placeholder = nameen;
    input.className = "form-control mt-2";

    let inputa = document.createElement("input");
    inputa.type = "text";
    inputa.name = `Features[${f}].NameAr`;
    inputa.placeholder = namear;
    inputa.className = "form-control mt-2";

    let input1 = document.createElement("textarea");
    input1.name = `Features[${f}].Description`;
    input1.placeholder = descen;
    input1.className = "form-control mt-2";

    let input1a = document.createElement("textarea");
    input1a.name = `Features[${f}].DescriptionAr`;
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
    input4.name = `Features[${f}].GooglePlay`;
    input4.placeholder = gp;
    input4.className = "form-control mt-2";

    let input5 = document.createElement("input");
    input5.type = "text";
    input5.name = `Features[${f}].AppStore`;
    input5.placeholder = as;
    input5.className = "form-control mt-2 mb-5";

    feature.appendChild(input);
    feature.appendChild(inputa);
    feature.appendChild(input1);
    feature.appendChild(input1a);
    feature.appendChild(input22);
    feature.appendChild(input2);
    feature.appendChild(input33);
    feature.appendChild(input3);
    feature.appendChild(input4);
    feature.appendChild(input5);

    f++;
}

iconmodel.onclick = function(){
    let input = document.createElement("input");
    input.type = "text";
    input.name = `Models[${f}].Name`;
    input.placeholder = nameen;
    input.className = "form-control mt-2";

    let inputa = document.createElement("input");
    inputa.type = "text";
    inputa.name = `Models[${f}].NameAr`;
    inputa.placeholder = namear;
    inputa.className = "form-control mt-2";

    let input1 = document.createElement("textarea");
    input1.name = `Models[${f}].Description`;
    input1.placeholder = descen;
    input1.className = "form-control mt-2";

    let input1a = document.createElement("textarea");
    input1a.name = `Models[${f}].DescriptionAr`;
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
    input4.name = `Models[${m}].GooglePlay`;
    input4.placeholder = gp;
    input4.className = "form-control mt-2";

    let input5 = document.createElement("input");
    input5.type = "text";
    input5.name = `Models[${m}].AppStore`;
    input5.placeholder = as;
    input5.className = "form-control mt-2 mb-5";

    model.appendChild(input);
    model.appendChild(inputa);
    model.appendChild(input1);
    model.appendChild(input1a);
    model.appendChild(input22);
    model.appendChild(input2);
    model.appendChild(input33);
    model.appendChild(input3);
    model.appendChild(input4);
    model.appendChild(input5);

    m++;
}

iconphoto.onclick = function(){
     let input = document.createElement("input");
    input.type = "text";
    input.name = `PhotoDetails[${p}].Name`;
    input.placeholder = nameen;
    input.className = "form-control mt-2";
    
    let inputa = document.createElement("input");
    inputa.type = "text";
    inputa.name = `PhotoDetails[${p}].NameAr`;
    inputa.placeholder = namear;
    inputa.className = "form-control mt-2";

    let input1a = document.createElement("textarea");
    input1a.name = `PhotoDetails[${p}].Description`;
    input1a.placeholder = descen;
    input1a.className = "form-control mt-2";

    let input1 = document.createElement("textarea");
    input1.name = `PhotoDetails[${p}].DescriptionAr`;
    input1.placeholder = descar;
    input1.className = "form-control mt-2";

    let input2 = document.createElement("input");
    input2.type = "file";
    input2.name = "PhotoFiles";
    input2.className = "form-control mt-2 mb-5";

    photo.appendChild(input);
    photo.appendChild(inputa);
    photo.appendChild(input1);
    photo.appendChild(input1a);
    photo.appendChild(input2);

    p++;
}

iconvideo.onclick = function(){
    let input = document.createElement("input");
    input.type = "text";
    input.name = `VideoUrls[${v}]`;
    input.placeholder = vid;
    input.className = "form-control mt-2";
    video.appendChild(input);
    v++;
}