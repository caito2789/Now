
js_VerifySessionState = function () {

    $.ajax({
        type: "POST",
        url: "../../Views/Security/frmSession.aspx/VerifySessionState",
        data: "",
        contentType: "application/json; charset=utf-8",
        dataType: "JSON",
        asyn: true,
        //beforeSend: function (objeto) {},
        success: function (response) {
            var datos = (typeof response.d) == 'string' ? eval('(' + response.d + ')') : response.d;
            //alert(datos)
            //if (datos != null) {
            $("#HEstadoSession").val(datos);

            if (datos == "1") {
                //alert($("#htxtUrl").val())
                alert("Su sesión ha expirado.\nEsta página se cerrará.");
                document.location.href = $("#htxtUrl").val().trim();
                return;
            }
            //}
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) { alert(textStatus + ": " + XMLHttpRequest.responseText); }
    });

};



