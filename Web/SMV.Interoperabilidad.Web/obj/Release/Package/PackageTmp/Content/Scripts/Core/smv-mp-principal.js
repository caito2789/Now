//window.alert = function (message) {
//    var ventanaCS = '<div title="Mensaje de Información del Sistema"><div style="vertical-align:middle; text-align:center;">' + message + '</center></div>';

//    $(ventanaCS).dialog({
//        /*autoOpen: false,*/
//        //appendTo: "form",
//        modal: true,
//        width: 300,
//        buttons: [{ text: "Aceptar", click: function () { $(this).dialog("close"); } }]
//    }).parent().css('z-index', '1000');
//};

window.onbeforeunload = function () {

    if ($("#hdnOpcion").val() != 0) {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "../Security/frmInicio.aspx/SalirOpcion",
            data: JSON.stringify({ arg: $("#hdnOpcion").val() }),
            dataType: "json",
            async: true,
            dataFilter: function (data) { return data; },
            success: function (data) {
                //alert(data.d);
            }
        });
    }
};

var VerDocumento = function (rutaDoc) {
    window.open("../../Controles/VerDocumento.aspx?rutaDoc=" + rutaDoc, "doc", "status=no,directories=no,menubar=no,toolbar=no,scrollbars=yes,location=no,resizable=yes,titlebar=no,width=780,height=550,screenX=50,screenY=50");
};

function AbrePopup(url, ancho, alto) {
    var posicion_x;
    var posicion_y;
    posicion_x = (screen.width / 2) - (ancho / 2);
    posicion_y = (screen.height / 2) - (alto / 2);
    window.open(url, "popup" + ancho, "width=" + ancho + "px,height=" + alto + "px,menubar=0,toolbar=0,directories=0,scrollbars=0,resizable=0,left=" + posicion_x + "px,top=" + posicion_y + "px");
}