<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Views/Shared/mpPrincipal.Master" CodeBehind="frmConsVigPoderes.aspx.vb" Inherits="SMV.Interoperabilidad.Web.frmConsVigPoderes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
    function js_ValidarFiltros() {
        var blnddlZona = false; var blnddlOficina = false; var blntxtPartida = false; var blntxtAsiento = false;
        var blntxtApePaterno = false; var blntxtApeMaterno = false; var blntxtNombres = false; var blntxtCargo = false;
        var blntxtEmail = false;
        var strMjError = "";
        var strZona = $("#ddlZona").val();
        var strOficina = $("#ddlOficina").val();
        var strPartida = $("#txtPartida").val().trim();
        var strAsiento = $("#txtAsiento").val().trim();
        var strApePaterno = $("#txtApePaterno").val().trim();
        var strApeMaterno = $("#txtApeMaterno").val().trim();
        var strNombres = $("#txtNombres").val().trim();
        var strCargo = $("#txtCargo").val().trim();
        var strEmail = $("#txtEmail").val().trim();

        if ((strZona == "") || (parseInt(strZona) == 0)) {
            strMjError += "Debe seleccionar la Zona.\n"; blnddlZona = true;
        }
        if ((strOficina == "") || (parseInt(strOficina) == 0)) {
            strMjError += "Debe seleccionar la Oficina.\n"; blnddlOficina = true;
        }
        if (strPartida == "") {
            strMjError += "Debe ingresar la Partida.\n"; blntxtPartida = true;
        }
        if (strAsiento == "") {
            strMjError += "Debe ingresar el Asiento.\n"; blntxtAsiento = true;
        }
        if (strApePaterno == "") {
            strMjError += "Debe ingresar el Apellido Paterno.\n"; blntxtApePaterno = true;
        }
        if (strApeMaterno == "") {
            strMjError += "Debe ingresar el Apellido Materno.\n"; blntxtApeMaterno = true;
        }
        if (strNombres == "") {
            strMjError += "Debe ingresar los Nombres.\n"; blntxtNombres = true;
        }
        if (strCargo == "") {
            strMjError += "Debe ingresar el Cargo.\n"; blntxtCargo = true;
        }
        if (strEmail != "") {
            var strValidaEmail = js_EsEmail(strEmail);
            if (strValidaEmail == false) {
                strMjError += "Debe ingresar un Email válido.\n"; blntxtEmail = true;
            }
        }

        if (strMjError != "") {
            if (blnddlZona) { $("#valddlZona").show(); } else { $("#valddlZona").hide(); }
            if (blnddlOficina) { $("#valddlOficina").show(); } else { $("#valddlOficina").hide(); }
            if (blntxtPartida) { $("#valtxtPartida").show(); } else { $("#valtxtPartida").hide(); }
            if (blntxtAsiento) { $("#valtxtAsiento").show(); } else { $("#valtxtAsiento").hide(); }
            if (blntxtApePaterno) { $("#valtxtApePaterno").show(); } else { $("#valtxtApePaterno").hide(); }
            if (blntxtApeMaterno) { $("#valtxtApeMaterno").show(); } else { $("#valtxtApeMaterno").hide(); }
            if (blntxtNombres) { $("#valtxtNombres").show(); } else { $("#valtxtNombres").hide(); }
            if (blntxtCargo) { $("#valtxtCargo").show(); } else { $("#valtxtCargo").hide(); }
            if (blntxtEmail) { $("#valtxtEmail").show(); } else { $("#valtxtEmail").hide(); }
            alert(strMjError);
            return false;
        } else {
            return true;
        }
    }

    function js_LimpiarFiltros() {
        $("#ddlZona").val("0");
        $("#ddlOficina").val("0");
        $("#txtPartida").val("");
        $("#txtAsiento").val("");
        $("#txtApePaterno").val("");
        $("#txtApeMaterno").val("");
        $("#txtNombres").val("");
        $("#txtCargo").val("");
        $("#txtEmail").val("");
    }

    function js_ExportarPDF() {
        var strMjError = "";
        var strConsulta = $("#hdnConsulta").val().trim();
        var intConsulta = parseInt(strConsulta);

        if (intConsulta == 0) {
            strMjError += "Debe realizar la consulta, antes de exportar a PDF.\n";
        }

        if (strMjError != "") {
            alert(strMjError);
            return false;
        } else {
            return true;
        }
    }

    function js_VerPDF(xRuta) {
        var xUrl = "../Form/frmVerPDF.aspx?xRt=" + xRuta;
        js_PopupCenter(xUrl, 650, 600, 0, 0);
    }
</script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <header class="panel-heading" style="padding-bottom:1px;">        
        <div class="dvTitFormA">SUNARP: Consulta de Vigencia de Poder</div>
    </header>   
    <div class="panel-body">
        <div class="dvTitFormB">
            <label class="FontTitulo">La consulta se conecta en línea al Servicio Web de la SUNARP, disponible en la plataforma de Interoperabilidad del Estado.</label>
        </div> 
        <div class="tab-pane dvFormINPE">
            <div class="modal-header dvFondoTituloForm">
                <h4 class="modal-title dvTXTTitForm">Filtros de Consulta</h4>
            </div>
            <div class="modal-body">
                <div class="form-horizontal tasi-form">
                    <asp:UpdatePanel ID="uplFiltros" runat="server">
                        <ContentTemplate> 
                            <div class="row">
                                <div class="col-md-12 col-sm-12 col-xs-12">
                                    <div class="col-md-3 col-sm-3 col-xs-12">
                                        <label class="control-label">Zona</label>
                                    </div>
                                    <div class="col-md-3 col-sm-3 col-xs-12 m-bot15">
                                        <asp:DropDownList ID="ddlZona" AutoPostBack="true" OnSelectedIndexChanged="ddlZona_SelectedIndexChanged" CssClass="form-control" ClientIDMode="Static" runat="server"></asp:DropDownList>
                                        <div style="display:inline-block;"><div id="valddlZona" style="display:none;color:red;">*</div></div>
                                    </div>
                                    <div class="col-md-3 col-sm-3 col-xs-12">
                                        <label class="control-label">Oficina</label>
                                    </div>
                                    <div class="col-md-3 col-sm-3 col-xs-12 m-bot15">
                                        <asp:DropDownList ID="ddlOficina" CssClass="form-control" ClientIDMode="Static" runat="server"></asp:DropDownList>
                                        <div style="display:inline-block;"><div id="valddlOficina" style="display:none;color:red;">*</div></div>
                                    </div>
                                </div>
                            </div>     
                            <div class="row">
                                <div class="col-md-12 col-sm-12 col-xs-12">
                                    <div class="col-md-3 col-sm-3 col-xs-12">
                                        <label class="control-label">Partida</label>
                                    </div>
                                    <div class="col-md-3 col-sm-3 col-xs-12 m-bot15">                                            
                                        <asp:TextBox ID="txtPartida" ClientIDMode="Static" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>                                                                                   
                                        <div style="display:inline-block;"><div id="valtxtPartida" style="display:none;color:red;">*</div></div>
                                    </div>
                                    <div class="col-md-3 col-sm-3 col-xs-12">
                                        <label class="control-label">Asiento</label>
                                    </div>
                                    <div class="col-md-3 col-sm-3 col-xs-12 m-bot15">                                            
                                        <asp:TextBox ID="txtAsiento" ClientIDMode="Static" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>                                            
                                        <div style="display:inline-block;"><div id="valtxtAsiento" style="display:none;color:red;">*</div></div>
                                    </div>
                                </div>
                            </div>   
                            <div class="row">
                                <div class="col-md-12 col-sm-12 col-xs-12">
                                    <div class="col-md-3 col-sm-3 col-xs-12">
                                        <label class="control-label">Apellido Paterno</label>
                                    </div>
                                    <div class="col-md-3 col-sm-3 col-xs-12 m-bot15">                                            
                                        <asp:TextBox ID="txtApePaterno" ClientIDMode="Static" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>                                                                                   
                                        <div style="display:inline-block;"><div id="valtxtApePaterno" style="display:none;color:red;">*</div></div>
                                    </div>
                                    <div class="col-md-3 col-sm-3 col-xs-12">
                                        <label class="control-label">Apellido Materno</label>
                                    </div>
                                    <div class="col-md-3 col-sm-3 col-xs-12 m-bot15">                                            
                                        <asp:TextBox ID="txtApeMaterno" ClientIDMode="Static" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>                                            
                                        <div style="display:inline-block;"><div id="valtxtApeMaterno" style="display:none;color:red;">*</div></div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12 col-sm-12 col-xs-12">
                                    <div class="col-md-3 col-sm-3 col-xs-12">
                                        <label class="control-label">Nombres</label>
                                    </div>
                                    <div class="col-md-3 col-sm-3 col-xs-12 m-bot15">
                                        <asp:TextBox ID="txtNombres" ClientIDMode="Static" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                        <div style="display:inline-block;"><div id="valtxtNombres" style="display:none;color:red;">*</div></div>
                                    </div>
                                    <div class="col-md-3 col-sm-3 col-xs-12">
                                        <label class="control-label">Cargo</label>
                                    </div>
                                    <div class="col-md-3 col-sm-3 col-xs-12 m-bot15">
                                        <asp:TextBox ID="txtCargo" ClientIDMode="Static" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>
                                        <div style="display:inline-block;"><div id="valtxtCargo" style="display:none;color:red;">*</div></div>
                                    </div>
                                </div>    
                            </div>          
                            <div class="row">
                                <div class="col-md-12 col-sm-12 col-xs-12">
                                    <div class="col-md-3 col-sm-3 col-xs-12">
                                        <label class="control-label">Email</label>
                                    </div>
                                    <div class="col-md-6 col-sm-6 col-xs-12 m-bot15">
                                        <asp:TextBox ID="txtEmail" ClientIDMode="Static" runat="server" MaxLength="200" CssClass="form-control"></asp:TextBox>
                                        <div style="display:inline-block;"><div id="valtxtEmail" style="display:none;color:red;">*</div></div>
                                    </div>
                                </div>
                            </div>                 
                            <div class="row">
                                <div class="col-md-12 col-sm-12 col-xs-12">
                                    <div class="dvBtnFiltroDer">
                                        <asp:LinkButton ID="btnConsultar" OnClientClick="javascript:return js_ValidarFiltros();" runat="server" 
                                            OnClick="btnConsultar_Click" CssClass="btn btn-primary btnTxtBotones"><i class="fa fa fa-search fa-lg"></i> Consultar</asp:LinkButton>&nbsp;&nbsp;
                                        <asp:LinkButton ID="btnLimpiar" OnClientClick="javascript:js_LimpiarFiltros();" runat="server" 
                                            CssClass="btn btn-primary btnTxtBotones"><i class="fa fa fa-eraser fa-lg"></i> Limpiar</asp:LinkButton>&nbsp;&nbsp;
                                        <asp:LinkButton ID="btnExportarPDF" OnClientClick="javascript:return js_ExportarPDF();" runat="server"
                                                OnClick="btnExportarPDF_Click" CssClass="btn btn-warning btnTxtBotones"><i class="fa fa-file-text-o fa-lg"></i> Exportar a PDF</asp:LinkButton>
                                        <asp:HiddenField ID="hdnConsulta" ClientIDMode="Static" runat="server" />
                                    </div>  
                                </div>  
                            </div>      
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlZona" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>  
                </div> 
            </div> 
        </div>
        <div class="dvAltoForm"></div>
        <div class="tab-pane dvFormINPE">
            <div class="modal-header dvFondoTituloForm">
                <h4 class="modal-title dvTXTTitForm">Resultados de Consulta</h4>
            </div>
            <div class="modal-body">
                <div class="form-horizontal tasi-form">
                    <asp:UpdatePanel ID="upResultados" runat="server">
                        <ContentTemplate>  
                            <div id="dvFormOK" style="display:inline;" runat="server">                                     
                                <div class="row">
                                    <div class="col-md-12 col-sm-12 col-xs-12">
                                        <div class="col-md-3 col-sm-3 col-xs-12">
                                            <label class="control-label">Estado</label>
                                        </div>
                                        <div class="col-md-9 col-sm-9 col-xs-12 m-bot15">
                                            <div class="dvFormInternoReniec">
                                                <asp:TextBox ID="txtEstado" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                            </div>                                        
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12 col-sm-12 col-xs-12">
                                        <div class="col-md-3 col-sm-3 col-xs-12">
                                            <label class="control-label">Solicitud</label>
                                        </div>
                                        <div class="col-md-9 col-sm-9 col-xs-12 m-bot15">
                                            <div class="dvFormInternoReniec">
                                                <asp:TextBox ID="txtSolicitud" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>    
                                </div>
                                <div class="row">
                                    <div class="col-md-12 col-sm-12 col-xs-12">
                                        <div class="col-md-3 col-sm-3 col-xs-12">
                                            <label class="control-label">Fecha</label>
                                        </div>
                                        <div class="col-md-9 col-sm-9 col-xs-12 m-bot15">
                                            <div class="dvFormInternoReniec">
                                                <asp:TextBox ID="txtFecha" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>    
                                </div>
                            </div> 
                            <div id="dvFormNOK" style="display:none;" runat="server">
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
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnConsultar" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>            
        </div>
    </div>
</asp:Content>
