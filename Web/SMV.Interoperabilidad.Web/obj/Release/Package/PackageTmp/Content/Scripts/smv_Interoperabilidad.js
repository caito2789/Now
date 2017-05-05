//--------- Funcion de solo numeros ---------------------//
var SoloNumeros = function (event) {
    event = event || window.event;
    var charCode = event.keyCode || event.which;
    var first = (charCode <= 57 && charCode >= 48);
    return first;
}

//--------- Funcion de solo numeros o guiones (-) ---------------------//
var SoloNumerosGuion = function (event) {
    event = event || window.event;
    var charCode = event.keyCode || event.which;
    var first = ((charCode <= 57 && charCode >= 48) || (charCode == 45));
    return first;
}

function js_EsEmail(email) {
    var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    return regex.test(email);
}

function js_PopupCenter(xUrl, xWidth, xHeight, xRes, xScro) {
    /************************************************************************************* 
    Descripción :    Función que muestra centrado en pantalla un form Popup.
    Autor 		:	 Diana Astola.
    Fecha/hora	:    27.03.2017  
    *************************************************************************************/
    if (xUrl != "") {
        //Fixes dual-screen position                         Most browsers      Firefox
        var dualScreenLeft = window.screenLeft != undefined ? window.screenLeft : screen.left;
        var dualScreenTop = window.screenTop != undefined ? window.screenTop : screen.top;

        var xWidthTmp = window.innerWidth ? window.innerWidth : document.documentElement.clientWidth ? document.documentElement.clientWidth : screen.width;
        var xHeightTmp = window.innerHeight ? window.innerHeight : document.documentElement.clientHeight ? document.documentElement.clientHeight : screen.height;

        var xLeft = ((xWidthTmp / 2) - (xWidth / 2)) + dualScreenLeft;
        var xTop = ((xHeightTmp / 2) - (xHeight / 2)) + dualScreenTop;
        var newWindow = window.open(xUrl, '_blank', 'scrollTo,menubar=0,toolbar=0,directories=0,location=0,resizable=' + xRes + ',scrollbars=' + xScro + ', width=' + xWidth + ', height=' + xHeight + ', top=' + xTop + ', left=' + xLeft);

        //Puts focus on the newWindow
        if (window.focus) {
            newWindow.focus();
        }
    }
}
