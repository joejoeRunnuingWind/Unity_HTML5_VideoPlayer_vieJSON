var fr = new FileReader();
var Frequence1;

function handleFileSelect(f)
{               
    if (window.File && window.FileReader && window.FileList && window.Blob) {

    } else {
        alert('The File APIs are not fully supported in this browser.');
        return;
	}   
    file = "Frequence1.json"
	fr.onloadend = receivedText;
	fr.readAsText(f);
//	alert(Frequence1);
}

handleFileSelect();      

function receivedText(){
	console.log("hihi");
	Frequence1 = JSON.parse(fr.result);
	var onJson = document.getElementById("onJson");
	onJson.onclick = function(){onJsonClickHandle()};
}

function onJsonClickHandle() {
	var test = document.getElementById("alert1V");
	alert(Frequence1);
	test.value = Frequence1[type];
	console.log("hihi");
}