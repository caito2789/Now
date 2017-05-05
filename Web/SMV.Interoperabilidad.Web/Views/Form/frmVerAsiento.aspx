<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmVerAsiento.aspx.vb" Inherits="SMV.Interoperabilidad.Web.frmVerAsiento" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <meta name="keyword" content="" />
    <title>Sistema de Interoperabilidad - Ver Asiento</title>
    <link href="../../Content/Styles/Core/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../Content/Styles/Core/bootstrap-reset.css" rel="stylesheet" type="text/css" />
    <link href="../../Content/Styles/Core/bootstrap-select.css" rel="stylesheet" type="text/css" />
    <link href="../../Content/Styles/Core/smv-estilos.css" rel="stylesheet" type="text/css" />
    <link href="../../Content/Styles/Core/style-responsive.css" rel="stylesheet" type="text/css" />
    <link href="../../Content/assets/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" /> 
    <link href="../../Content/Styles/smv-interoperabilidad.css" rel="stylesheet" type="text/css" />
    <!--[if gte IE 9]
    <style type="text/css">
    .gradient {
        filter: none;
    }
    </style>
    <![endif]-->
    <!--[if lt IE 9]>
        <script src="../../Content/Scripts/Core/html5shiv.js" type="text/javascript"></script>
        <script src="../../Content/Scripts/Core/respond.min.js" type="text/javascript"></script>
    <![endif]-->
</head>
<body>    
<form id="formPrincipal" runat="server">
    <asp:ScriptManager ID="smPrincipal" runat="server" AsyncPostBackTimeout="0">
        <Scripts>
            <asp:ScriptReference Path="~/Content/Scripts/Core/smv-mp-principal-init.js" />
        </Scripts>
    </asp:ScriptManager>
    <section id="main-content">
        <section class="wrapper" style="margin-top:5px; padding-top:0;">
	    	<div class="row">
                <div class="col-md-12" style="padding:1px 1px">
                    <section class="panel col-sm-12">
                        <header class="panel-heading" style="padding-bottom:5px;">        
                            <div class="dvTitFormA">SUNARP: Consulta de Ver Asiento - ID Imagen Asiento <asp:Label ID="lblIDImagenAs" runat="server"></asp:Label></div>
                        </header> 
                        <div class="panel-body"> 
                            <div class="tab-pane">
                                <div class="modal-body">
                                    <div class="form-horizontal tasi-form">            
                                        <div id="dvFormOK" runat="server">
                                            <div class="row">  
                                                <div class="col-md-12 col-sm-12 col-xs-12">
                                                    <asp:Image ID="imgVerAsiento"  CssClass="dvImgArchivo" ToolTip="" runat="server" />
                                                </div>                                 
                                            </div>
                                        </div>
                                        <div id="dvFormNOK" runat="server">
                                            <div class="row">
                                                <div class="col-md-12 col-sm-12 col-xs-12">
                                                    <div class="col-md-3 col-sm-3 col-xs-12">
                                                        <label class="control-label">Mensaje</label>
                                                    </div>
                                                    <div class="col-md-9 col-sm-9 col-xs-12 m-bot15">
                                                        <div class="dvFormInternoReniec">
                                                            <asp:TextBox ID="txtDescMensaje" runat="server" CssClass="form-control" TextMode="MultiLine" ReadOnly="true" Rows="3"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>    
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="dvRowVerAsie">
                                    <div class="AltoVerAsie"></div>
                                </div>
                            </div>
                        </div>
                    </section>
                </div> 
		    </div>
        </section>
    </section>
</form>
</body>
</html>
