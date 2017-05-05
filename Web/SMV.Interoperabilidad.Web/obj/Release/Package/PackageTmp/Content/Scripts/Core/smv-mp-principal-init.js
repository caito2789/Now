function beforeAsyncPostBack() {
    $("#load").show();
    
}

function afterAsyncPostBack() {
    $("#load").hide();
    if (typeof load_grid != 'undefined') {
        load_grid();
    }
}

Sys.Application.add_init(appl_init);

function appl_init() {
    var pgRegMgr = Sys.WebForms.PageRequestManager.getInstance();
    pgRegMgr.add_beginRequest(BeginHandler);
    pgRegMgr.add_endRequest(EndHandler);
}

function BeginHandler() {
    beforeAsyncPostBack();
}

function EndHandler(sender, args) {
    afterAsyncPostBack();
    if (args.get_error() != undefined) {
        var errorMessage;
        if (args.get_response().get_statusCode() == '200') {
            errorMessage = args.get_error().message;
        }
        else {
            // Error occurred somewhere other than the server page.
            errorMessage = 'Se ha producido un error inesperado.';
        }
        args.set_errorHandled(true);
        alert(errorMessage);
    }
}
 