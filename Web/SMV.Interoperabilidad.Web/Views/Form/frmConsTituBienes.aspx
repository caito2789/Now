<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Views/Shared/mpPrincipal.Master" CodeBehind="frmConsTituBienes.aspx.vb" Inherits="SMV.Interoperabilidad.Web.frmConsTituBienes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
    function js_ValidarFiltros() {
        var blntxtRazonSocial = false; var blntxtApePaterno = false; var blntxtApeMaterno = false; var blntxtNombres = false;
        var strMjError = "";
        var strRazonSocial = $("#txtRazonSocial").val().trim();
        var strApePaterno = $("#txtApePaterno").val().trim();
        var strApeMaterno = $("#txtApeMaterno").val().trim();
        var strNombres = $("#txtNombres").val().trim();

        if ($("input[id='rbNombres']").is(':checked')) {
            if (strApePaterno == "") {
                strMjError += "Debe ingresar el Apellido Paterno.\n"; blntxtApePaterno = true;
            }
            if (strApeMaterno == "") {
                strMjError += "Debe ingresar el Apellido Materno.\n"; blntxtApeMaterno = true;
            }
            if (strNombres == "") {
                strMjError += "Debe ingresar los Nombres.\n"; blntxtNombres = true;
            }
        }
        else {
            if (strRazonSocial == "") {
                strMjError += "Debe ingresar la Razón Social.\n"; blntxtRazonSocial = true;
            }
        }

        if (strMjError != "") {
            if (blntxtApePaterno) { $("#valtxtApePaterno").show(); } else { $("#valtxtApePaterno").hide(); }
            if (blntxtApeMaterno) { $("#valtxtApeMaterno").show(); } else { $("#valtxtApeMaterno").hide(); }
            if (blntxtNombres) { $("#valtxtNombres").show(); } else { $("#valtxtNombres").hide(); }
            if (blntxtRazonSocial) { $("#valtxtRazonSocial").show(); } else { $("#valtxtRazonSocial").hide(); }
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
        $("#txtRazonSocial").val("");
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
        <div class="dvTitFormA">SUNARP: Consulta de Titularidad de Bienes de Consulta</div>
    </header>   
    <div class="panel-body">
        <div class="dvTitFormB">
            <label class="FontTitulo">La consulta se conecta en línea al Servicio Web del SUNARP, disponible en la plataforma de Interoperabilidad del Estado.</label>
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
                                        <label class="control-label">Tipo Consulta</label>
                                    </div>                                    
                                    <div class="col-md-10 col-sm-9 col-xs-12 m-bot15">  
                                        <div class="dvFormInternoSunarp">
                                            <asp:RadioButton ID="rbNombres" AutoPostBack="true" OnCheckedChanged="rbTipoConsulta_CheckedChanged" Text=" Por Nombres" 
                                                GroupName="rbTipoConsulta" ClientIDMode="Static" runat="server" />&nbsp;&nbsp;
                                            <asp:RadioButton ID="rbRazonS" AutoPostBack="true" OnCheckedChanged="rbTipoConsulta_CheckedChanged" Text=" Por Razón Social" 
                                                GroupName="rbTipoConsulta" ClientIDMode="Static" runat="server" />
                                        </div>
                                    </div>  
                                </div>
                            </div>                               
                            <div id="dvTipoNombres" ClientIDMode="Static" runat="server">
                                <div class="row">
                                    <div class="col-md-12 col-sm-12 col-xs-12">
                                        <div class="col-md-2 col-sm-3 col-xs-12">
                                            <label class="control-label">Apellido Paterno</label>
                                        </div>
                                        <div class="col-md-3 col-sm-3 col-xs-12 m-bot15">  
                                            <div class="dvFormInternoSunarp">                                                                                          
                                                <asp:TextBox ID="txtApePaterno" ClientIDMode="Static" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>                                                                                   
                                                <div style="display:inline-block;"><div id="valtxtApePaterno" style="display:none;color:red;">*</div></div>
                                            </div>
                                        </div>
                                        <div class="col-md-2 col-sm-3 col-xs-12">
                                            <label class="control-label">Apellido Materno</label>
                                        </div>
                                        <div class="col-md-3 col-sm-3 col-xs-12 m-bot15">  
                                            <div class="dvFormInternoSunarp">                                          
                                                <asp:TextBox ID="txtApeMaterno" ClientIDMode="Static" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>                                            
                                                <div style="display:inline-block;"><div id="valtxtApeMaterno" style="display:none;color:red;">*</div></div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12 col-sm-12 col-xs-12">
                                        <div class="col-md-2 col-sm-3 col-xs-12">
                                            <label class="control-label">Nombres</label>
                                        </div>
                                        <div class="col-md-3 col-sm-3 col-xs-12 m-bot15">
                                            <div class="dvFormInternoSunarp">
                                                <asp:TextBox ID="txtNombres" ClientIDMode="Static" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                                <div style="display:inline-block;"><div id="valtxtNombres" style="display:none;color:red;">*</div></div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>  
                            <div id="dvTipoRazonS" ClientIDMode="Static" runat="server">
                                <div class="row">
                                    <div class="col-md-12 col-sm-12 col-xs-12">
                                        <div class="col-md-2 col-sm-3 col-xs-12">
                                            <label class="control-label">Razón Social</label>
                                        </div>
                                        <div class="col-md-7 col-sm-6 col-xs-12 m-bot15">
                                            <div class="dvFormInternoSunarp">
                                                <asp:TextBox ID="txtRazonSocial" ClientIDMode="Static" runat="server" MaxLength="250" CssClass="form-control"></asp:TextBox>
                                                <div style="display:inline-block;"><div id="valtxtRazonSocial" style="display:none;color:red;">*</div></div>
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
                            <asp:AsyncPostBackTrigger ControlID="rbRazonS" EventName="CheckedChanged" />
                        </Triggers>
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
                            <div id="dvFormOK" ClientIDMode="Static" style="display:inline;" runat="server">
                                <div class="row">
                                    <div class="col-sm-12 col-xs-12"> 
                                        <section class="panel">
						                    <div class="panel-body" style="padding:0px">
						                        <div class="adv-table table-responsive">
                                                    <asp:UpdatePanel ID="upGrilla" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <asp:GridView ID="gvDatos" runat="server" AutoGenerateColumns="false" Width="100%" OnPreRender="gvDatos_PreRender"
                                                                CssClass="display table table2 table-hover table-bordered table-striped dataTable mGrid" aria-describedby="dynamic-table_info"
                                                                ShowHeaderWhenEmpty="true" EmptyDataText="No se encontraron resultados." AllowSorting="true" OnSorting="gvDatos_Sorting"
                                                                OnRowDataBound="gvDatos_RowDataBound">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderStyle-Width="30px" ItemStyle-Width="30px" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="AlineaTextCenter">
                                                                        <HeaderTemplate>N°</HeaderTemplate>
                                                                        <ItemTemplate>
                                                                                <asp:Label ID="lblfila" runat="server" Text="<%# Container.DataItemIndex + 1 %>"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>                                                                                                                                    
                                                                    <asp:BoundField DataField="ApePaterno" HeaderText="Apellido Paterno" SortExpression="ApePaterno" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="AlineaTextIzq"/>
                                                                    <asp:BoundField DataField="ApeMaterno" HeaderText="Apellido Materno" SortExpression="ApeMaterno" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="AlineaTextIzq"/>     
                                                                    <asp:BoundField DataField="Nombres" HeaderText="Nombres" SortExpression="Nombres" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="AlineaTextIzq"/>
                                                                    <asp:BoundField DataField="RazonSocial" HeaderText="Razón Social" SortExpression="RazonSocial" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="AlineaTextIzq"/> 
                                                                    <asp:BoundField DataField="TipoDocumento" HeaderText="Tipo Documento" SortExpression="TipoDocumento" HeaderStyle-Width="110px" ItemStyle-Width="110px" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="AlineaTextIzq"/>
                                                                    <asp:BoundField DataField="NroDocumento" HeaderText="Nro. Documento" SortExpression="NroDocumento" HeaderStyle-Width="110px" ItemStyle-Width="110px" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="AlineaTextCenter"/> 
                                                                    <asp:BoundField DataField="NumPartida" HeaderText="Número Partida" SortExpression="NumPartida" HeaderStyle-Width="110px" ItemStyle-Width="110px" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="AlineaTextCenter"/>
                                                                    <asp:BoundField DataField="Registro" HeaderText="Registro" SortExpression="Registro" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="AlineaTextCenter"/>    
                                                                    <asp:BoundField DataField="NumPlaca" HeaderText="Número Placa" SortExpression="NumPlaca" HeaderStyle-Width="110px" ItemStyle-Width="110px" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="AlineaTextCenter"/>                                                                                                                                                                                                          
                                                                    <asp:BoundField DataField="Zona" HeaderText="Zona" SortExpression="Zona" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="AlineaTextIzq" />  
                                                                    <asp:BoundField DataField="Oficina" HeaderText="Oficina" SortExpression="Oficina" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="AlineaTextIzq" />  
                                                                    <asp:BoundField DataField="Estado" HeaderText="Estado" SortExpression="Estado" HeaderStyle-Width="90px" ItemStyle-Width="90px" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="AlineaTextIzq"/>                                                                                                       
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
                            <div id="dvFormNOK" ClientIDMode="Static" style="display:none;" runat="server">
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
