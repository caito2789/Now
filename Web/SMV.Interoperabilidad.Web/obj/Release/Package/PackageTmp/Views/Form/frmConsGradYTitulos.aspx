<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Views/Shared/mpPrincipal.Master" CodeBehind="frmConsGradYTitulos.aspx.vb" Inherits="SMV.Interoperabilidad.Web.frmConsGradYTitulos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
    function js_ValidarFiltros() {
        var blntxtDNI = false;
        var strMjError = "";
        var strDNI = $("#txtDNI").val().trim();

        if (strDNI == "") {
            strMjError += "Debe ingresar el DNI.\n"; blntxtDNI = true;
        } else {
            var intLngDNI = strDNI.length;
            if (intLngDNI < 8) {
                strMjError += "El DNI debe tener 8 dígitos.\n"; blntxtDNI = true;
            }
        }

        if (strMjError != "") {
            if (blntxtDNI) { $("#valtxtDNI").show(); } else { $("#valtxtDNI").hide(); }
            alert(strMjError);
            return false;
        } else {
            return true;
        }
    }

    function js_LimpiarFiltros() {
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
        <div class="dvTitFormA">SUNEDU: Consulta de Grados y Títulos</div>
    </header>   
    <div class="panel-body">
        <div class="dvTitFormB">
            <label class="FontTitulo">La consulta se conecta en línea al Servicio Web de la SUNEDU, disponible en la plataforma de Interoperabilidad del Estado.</label>
        </div> 
        <div class="tab-pane">
            <div class="modal-header dvFondoTituloForm">
                <h4 class="modal-title dvTXTTitForm">Filtros de Consulta</h4>
            </div>
            <div class="modal-body">
                <div class="form-horizontal tasi-form">
                    <asp:UpdatePanel ID="uplFiltros" runat="server">
                        <ContentTemplate>                                   
                            <div class="row">
                                <div class="col-md-12 col-sm-12 col-xs-12">
                                    <div class="col-md-2 col-sm-3 col-xs-12">
                                        <label class="control-label">DNI</label>
                                    </div>                                    
                                    <div class="col-md-3 col-sm-6 col-xs-12 m-bot15">                                        
                                        <asp:TextBox ID="txtDNI" Width="200px" ClientIDMode="Static" runat="server" MaxLength="8" onkeypress="return SoloNumeros(event);" CssClass="form-control"></asp:TextBox>                                                                           
                                        <div style="display:inline-block;"><div id="valtxtDNI" style="display:none;color:red;">*</div></div>
                                    </div>  
                                    <div class="col-md-7 col-sm-6 col-xs-12">
                                        <div class="dvBtnFiltro">
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
        <div class="tab-pane">
            <div class="modal-header dvFondoTituloForm">
                <h4 class="modal-title dvTXTTitForm">Resultados de Consulta</h4>
            </div>
            <div class="modal-body">
                <div class="form-horizontal tasi-form">
                    <asp:UpdatePanel ID="upResultados" runat="server">
                        <ContentTemplate>  
                            <div id="dvFormOK" style="display:inline;" runat="server">                                     
                                 <div class="row">
                                    <div class="col-sm-12 col-xs-12"> 
                                        <section class="panel">
						                    <div class="panel-body" style="padding:0px">
						                        <div class="adv-table table-responsive">
                                                    <asp:UpdatePanel ID="upGrilla" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <asp:GridView ID="gvDatos" runat="server" AutoGenerateColumns="false" Width="100%" OnPreRender="gvDatos_PreRender"
                                                                CssClass="display table table2 table-hover table-bordered table-striped dataTable mGrid" aria-describedby="dynamic-table_info"
                                                                ShowHeaderWhenEmpty="true" EmptyDataText="No se encontraron resultados." AllowSorting="true" OnSorting="gvDatos_Sorting">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderStyle-Width="30px" ItemStyle-Width="30px" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="AlineaTextCenter">
                                                                        <HeaderTemplate>N°</HeaderTemplate>
                                                                        <ItemTemplate>
                                                                                <asp:Label ID="lblfila" runat="server" Text="<%# Container.DataItemIndex + 1 %>"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>                                                                    
                                                                    <asp:BoundField DataField="ApePaterno" HeaderText="Apellido Paterno" SortExpression="ApePaterno" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="AlineaTextIzq"/>
                                                                    <asp:BoundField DataField="ApeMaterno" HeaderText="Apellido Materno" SortExpression="ApeMaterno" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="AlineaTextIzq" />     
                                                                    <asp:BoundField DataField="Nombres" HeaderText="Nombres" SortExpression="Nombres" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="AlineaTextIzq" />
                                                                    <asp:BoundField DataField="TipoDocumento" HeaderText="Tipo Documento" SortExpression="TipoDocumento" HeaderStyle-Width="110px" ItemStyle-Width="110px" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="AlineaTextIzq"/>
                                                                    <asp:BoundField DataField="NroDocumento" HeaderText="Nro. Documento" SortExpression="NroDocumento" HeaderStyle-Width="110px" ItemStyle-Width="110px" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="AlineaTextCenter"/>
                                                                    <asp:BoundField DataField="Pais" HeaderText="País" SortExpression="Pais" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="AlineaTextIzq"/>
                                                                    <asp:BoundField DataField="Universidad" HeaderText="Universidad" SortExpression="Universidad" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="AlineaTextIzq" />  
                                                                    <asp:BoundField DataField="TitProfesional" HeaderText="Título Profesional" SortExpression="TitProfesional" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="AlineaTextIzq"/>  
                                                                    <asp:BoundField DataField="AbrTitulo" HeaderText="Abrev. Título" SortExpression="AbrTitulo" HeaderStyle-Width="50px" ItemStyle-Width="50px" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="AlineaTextCenter" /> 
                                                                    <asp:BoundField DataField="Especialidad" HeaderText="Especialidad" SortExpression="Especialidad" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="AlineaTextIzq"/>                                                                                                                                                                            
                                                                </Columns>
                                                            </asp:GridView>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="gvDatos" EventName="Sorting" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                        </section>
                                    </div>
                                </div>                                
                            </div> 
                            <div id="dvFormNOK" style="display:none;" runat="server">
                                <div class="row">
                                    <div class="col-md-12 col-sm-12 col-xs-12">
                                        <div class="col-md-2 col-sm-2 col-xs-12">
                                            <label class="control-label">Mensaje</label>
                                        </div>
                                        <div class="col-md-10 col-sm-10 col-xs-12 m-bot15">
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
