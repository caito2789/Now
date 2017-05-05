<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Views/Shared/mpPrincipal.Master" CodeBehind="frmConsAntJudicial.aspx.vb" Inherits="SMV.Interoperabilidad.Web.frmConsAntJudicial" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
    function js_ValidarFiltros() {
        var blntxtApePaterno = false; var blntxtApeMaterno = false; var blntxtNombres = false;
        var strMjError = "";
        var strApePaterno = $("#txtApePaterno").val().trim(); 
        var strApeMaterno = $("#txtApeMaterno").val().trim();
        var strNombres = $("#txtNombres").val().trim();

        if (strApePaterno == "") {
            strMjError += "Debe ingresar el Apellido Paterno.\n"; blntxtApePaterno = true;
        }
        if (strApeMaterno == "") {
            strMjError += "Debe ingresar el Apellido Materno.\n"; blntxtApeMaterno = true;
        }
        if (strNombres == "") {
            strMjError += "Debe ingresar los Nombres.\n"; blntxtNombres = true;
        }

        if (strMjError != "") {
            if (blntxtApePaterno) { $("#valtxtApePaterno").show(); } else { $("#valtxtApePaterno").hide(); }
            if (blntxtApeMaterno) { $("#valtxtApeMaterno").show(); } else { $("#valtxtApeMaterno").hide(); }
            if (blntxtNombres) { $("#valtxtNombres").show(); } else { $("#valtxtNombres").hide(); }
            alert(strMjError);
            return false;
        } else {
            return true;
        }
    }

    function js_LimpiarFiltros() {
        $("#txtApePaterno").val("");
        $("#txtApeMaterno").val("");
        $("#txtNombres").val("");
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
        <div class="dvTitFormA">PODER JUDICIAL: Consulta de Antecedentes Judiciales</div>
    </header>   
    <div class="panel-body">
        <div class="dvTitFormB">
            <label class="FontTitulo">La consulta se conecta en línea al Servicio Web del PODER JUDICIAL, disponible en la plataforma de Interoperabilidad del Estado.</label>
        </div> 
        <div class="tab-pane dvFormReniec">
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
                                        <label class="control-label">Apellido Paterno</label>
                                    </div>                                    
                                    <div class="col-md-9 col-sm-9 col-xs-12 m-bot15">  
                                        <div class="dvFormInternoReniec">                                      
                                            <asp:TextBox ID="txtApePaterno" ClientIDMode="Static" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>  
                                            <div style="float:right;"><div id="valtxtApePaterno" style="display:none;color:red;">*</div></div>
                                        </div>                                        
                                    </div>  
                                </div>
                            </div> 
                            <div class="row">
                                <div class="col-md-12 col-sm-12 col-xs-12">
                                    <div class="col-md-3 col-sm-3 col-xs-12">
                                        <label class="control-label">Apellido Materno</label>
                                    </div>                                    
                                    <div class="col-md-9 col-sm-9 col-xs-12 m-bot15"> 
                                        <div class="dvFormInternoReniec">                                       
                                            <asp:TextBox ID="txtApeMaterno" ClientIDMode="Static" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>                                                                                                                       
                                            <div style="float:right;"><div id="valtxtApeMaterno" style="display:none;color:red;">*</div></div>
                                        </div>
                                        
                                    </div>  
                                </div>
                            </div>                               
                            <div class="row">
                                <div class="col-md-12 col-sm-12 col-xs-12">
                                    <div class="col-md-3 col-sm-3 col-xs-12">
                                        <label class="control-label">Nombres</label>
                                    </div>                                    
                                    <div class="col-md-9 col-sm-9 col-xs-12 m-bot15">    
                                        <div class="dvFormInternoReniec">                                                                                
                                            <asp:TextBox ID="txtNombres" ClientIDMode="Static" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox> 
                                            <div style="float:right;"><div id="valtxtNombres" style="display:none;color:red;">*</div></div> 
                                        </div>                                         
                                    </div>  
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
                            </div>      
                        </ContentTemplate>
                    </asp:UpdatePanel>  
                </div> 
            </div> 
        </div>
        <div class="dvAltoForm"></div>
        <div class="tab-pane dvFormReniec">
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
