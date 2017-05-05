<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Views/Shared/mpPrincipal.Master" CodeBehind="frmConsListAsientos.aspx.vb" Inherits="SMV.Interoperabilidad.Web.frmConsListAsientos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
    function js_ValidarFiltros() {
        var blnddlZona = false; var blnddlOficina = false; var blntxtPartida = false; var blnddlRegistro = false;
        var strMjError = "";
        var strZona = $("#ddlZona").val();
        var strOficina = $("#ddlOficina").val();
        var strPartida = $("#txtPartida").val().trim();
        var strRegistro = $("#ddlRegistro").val();

        if ((strZona == "") || (parseInt(strZona) == 0)) {
            strMjError += "Debe seleccionar la Zona.\n"; blnddlZona = true;
        }
        if ((strOficina == "") || (parseInt(strOficina) == 0)) {
            strMjError += "Debe seleccionar la Oficina.\n"; blnddlOficina = true;
        }
        if (strPartida == "") {
            strMjError += "Debe ingresar la Partida.\n"; blntxtPartida = true;
        }        
        if ((strRegistro == "") || (parseInt(strRegistro) == 0)) {
            strMjError += "Debe seleccionar el Registro.\n"; blnddlRegistro = true;
        }

        if (strMjError != "") {
            if (blnddlZona) { $("#valddlZona").show(); } else { $("#valddlZona").hide(); }
            if (blnddlOficina) { $("#valddlOficina").show(); } else { $("#valddlOficina").hide(); }
            if (blntxtPartida) { $("#valtxtPartida").show(); } else { $("#valtxtPartida").hide(); }
            if (blnddlRegistro) { $("#valddlRegistro").show(); } else { $("#valddlRegistro").hide(); }
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
        $("#ddlRegistro").val("0");
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

    function js_VerAsiento(xTran, xIDIm, xTip, xNTotPg, xNPgRef, xPg) {
        var xUrl = "../Form/frmVerAsiento.aspx?xTran=" + xTran + "&xIDIm=" + xIDIm + "&xTip=" + xTip + "&xNTotPg=" + xNTotPg + "&xNPgRef=" + xNPgRef + "&xPg=" + xPg;
        js_PopupCenter(xUrl, 600, 620, 1, 1);
    }

    function js_VerPDF(xRuta) {
        var xUrl = "../Form/frmVerPDF.aspx?xRt=" + xRuta;
        js_PopupCenter(xUrl, 650, 600, 0, 0);
    }
</script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <header class="panel-heading" style="padding-bottom:1px;">        
        <div class="dvTitFormA">SUNARP: Listado de Asientos</div>
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
                                        <label class="control-label">Registro</label>
                                    </div>
                                    <div class="col-md-3 col-sm-3 col-xs-12 m-bot15">                                            
                                        <asp:DropDownList ID="ddlRegistro" CssClass="form-control" ClientIDMode="Static" runat="server"></asp:DropDownList>                                          
                                        <div style="display:inline-block;"><div id="valddlRegistro" style="display:none;color:red;">*</div></div>
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
                                                                    <asp:BoundField DataField="Transaccion" HeaderText="Transacción" SortExpression="Transaccion" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="AlineaTextCenter"/>    
                                                                    <asp:BoundField DataField="NroTotalPag" HeaderText="NroTotalPag" SortExpression="NroTotalPag" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="AlineaTextCenter" />                                                                                                                                     
                                                                    <asp:BoundField DataField="IDImgAsiento" HeaderText="Código Imagen" SortExpression="IDImgAsiento" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="AlineaTextCenter"/>
                                                                    <asp:BoundField DataField="Tipo" HeaderText="Tipo" SortExpression="Tipo" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="AlineaTextIzq"/>     
                                                                    <asp:BoundField DataField="CantidadPag" HeaderText="Nro. Páginas" SortExpression="CantidadPag" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="AlineaTextCenter"/>
                                                                    <asp:BoundField DataField="NroPagRef" HeaderText="Nro. Pág. Ref." SortExpression="NroPagRef" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="AlineaTextCenter"/> 
                                                                    <asp:BoundField DataField="NroPag" HeaderText="Página" SortExpression="NroPag" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="AlineaTextCenter" />
                                                                    <asp:TemplateField HeaderText="Ver Asiento" HeaderStyle-Width="60px" ItemStyle-Width="60px" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="AlineaTextCenter">
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton ID="btnVerAsiento" runat="server" CssClass="dvCentrar" ToolTip="Ver Asiento" ImageUrl="~/Content/Images/img_Asiento.png" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
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
                                <%--<div class="row">
                                    <div class="col-md-12 col-sm-12 col-xs-12">
                                        <div class="col-md-3 col-sm-3 col-xs-12">
                                            <label class="control-label">Código Mensaje</label>
                                        </div>
                                        <div class="col-md-9 col-sm-9 col-xs-12 m-bot15">
                                            <div class="dvFormInternoReniec">
                                                <asp:TextBox ID="txtCodMensaje" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>    
                                </div>--%>
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
