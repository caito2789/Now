<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Views/Shared/mpPrincipal.Master" CodeBehind="frmConsAntPenales.aspx.vb" Inherits="SMV.Interoperabilidad.Web.frmConsAntPenales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
    function js_ValidarFiltros()
    {
        var blntxtApePaterno = false; var blntxtApeMaterno = false; var blntxtNombre1 = false; var blntxtDNI = false;
        var strMjError = "";
        var strDNI = $("#txtDNI").val().trim();
        var strApePaterno = $("#txtApePaterno").val().trim();
        var strApeMaterno = $("#txtApeMaterno").val().trim();
        var strNombre1 = $("#txtNombre1").val().trim();

        if ($("input[id='rbNombres']").is(':checked'))
        {
            if (strApePaterno == "") {
                strMjError += "Debe ingresar el Apellido Paterno.\n"; blntxtApePaterno = true;
            }
            if (strApeMaterno == "") {
                strMjError += "Debe ingresar el Apellido Materno.\n"; blntxtApeMaterno = true;
            }
            if (strNombre1 == "") {
                strMjError += "Debe ingresar el Nombres 1.\n"; blntxtNombre1 = true;
            }
        }
        else
        {
            if (strDNI == "") {
                strMjError += "Debe ingresar el DNI.\n"; blntxtDNI = true;
            } else {
                var intLngDNI = strDNI.length;
                if (intLngDNI < 8) {
                    strMjError += "El DNI debe tener 8 dígitos.\n"; blntxtDNI = true;
                }
            }
        }

        if (strMjError != "") {
            if (blntxtApePaterno) { $("#valtxtApePaterno").show(); } else { $("#valtxtApePaterno").hide(); }
            if (blntxtApeMaterno) { $("#valtxtApeMaterno").show(); } else { $("#valtxtApeMaterno").hide(); }
            if (blntxtNombre1) { $("#valtxtNombre1").show(); } else { $("#valtxtNombre1").hide(); }
            if (blntxtDNI) { $("#valtxtDNI").show(); } else { $("#valtxtDNI").hide(); }
            alert(strMjError);
            return false;
        } else {
            return true;
        }
    }

    function js_LimpiarFiltros()
    {
        $("#txtApePaterno").val("");
        $("#txtApeMaterno").val("");
        $("#txtNombre1").val("");
        $("#txtNombre2").val("");
        $("#txtNombre3").val("");
        $("#txtDNI").val("");
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
        <div class="dvTitFormA">INPE: Consulta de Antecedentes Penales</div>
    </header>   
    <div class="panel-body">
        <div class="dvTitFormB">
            <label class="FontTitulo">La consulta se conecta en línea al Servicio Web del INPE, disponible en la plataforma de Interoperabilidad del Estado.</label>
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
                                        <label class="control-label">Tipo Consulta</label>
                                    </div>                                    
                                    <div class="col-md-9 col-sm-9 col-xs-12 m-bot15">  
                                        <div class="dvFormInternoReniec">
                                            <asp:RadioButton ID="rbNombres" AutoPostBack="true" OnCheckedChanged="rbTipoConsulta_CheckedChanged" Text=" Por Nombres" 
                                                GroupName="rbTipoConsulta" ClientIDMode="Static" runat="server" />&nbsp;&nbsp;
                                            <asp:RadioButton ID="rbDNI" AutoPostBack="true" OnCheckedChanged="rbTipoConsulta_CheckedChanged" Text=" Por DNI" 
                                                GroupName="rbTipoConsulta" ClientIDMode="Static" runat="server" />                                            
                                        </div>
                                    </div>  
                                </div>
                            </div> 
                            <div id="dvTipoNombres" ClientIDMode="Static" runat="server">
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
                                            <label class="control-label">Nombres 1</label>
                                        </div>
                                        <div class="col-md-3 col-sm-3 col-xs-12 m-bot15">
                                            <asp:TextBox ID="txtNombre1" ClientIDMode="Static" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                            <div style="display:inline-block;"><div id="valtxtNombre1" style="display:none;color:red;">*</div></div>
                                        </div>
                                        <div class="col-md-3 col-sm-3 col-xs-12">
                                            <label class="control-label">Nombres 2</label>
                                        </div>
                                        <div class="col-md-3 col-sm-3 col-xs-12 m-bot15">
                                            <asp:TextBox ID="txtNombre2" ClientIDMode="Static" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>    
                                </div>
                                <div class="row">
                                    <div class="col-md-12 col-sm-12 col-xs-12">
                                        <div class="col-md-3 col-sm-3 col-xs-12">
                                            <label class="control-label">Nombres 3</label>
                                        </div>
                                        <div class="col-md-3 col-sm-3 col-xs-12 m-bot15">
                                            <asp:TextBox ID="txtNombre3" ClientIDMode="Static" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div> 
                            <div id="dvTipoDNI" ClientIDMode="Static" runat="server">
                                <div class="row">
                                    <div class="col-md-12 col-sm-12 col-xs-12">
                                        <div class="col-md-3 col-sm-3 col-xs-12">
                                            <label class="control-label">DNI</label>
                                        </div>
                                        <div class="col-md-9 col-sm-9 col-xs-12 m-bot15">
                                            <div class="dvFormInternoReniec">
                                                <asp:TextBox ID="txtDNI" ClientIDMode="Static" runat="server" MaxLength="8" onkeypress="return SoloNumeros(event);" Width="200px" CssClass="form-control"></asp:TextBox>
                                                <div style="display:inline-block;"><div id="valtxtDNI" style="display:none;color:red;">*</div></div>
                                            </div>
                                        </div>
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
                            <asp:AsyncPostBackTrigger ControlID="rbNombres" EventName="CheckedChanged" />
                            <asp:AsyncPostBackTrigger ControlID="rbDNI" EventName="CheckedChanged" />
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
