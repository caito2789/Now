<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Views/Shared/mpPrincipal.Master" CodeBehind="frmMantenedor.aspx.vb" Inherits="SMV.Interoperabilidad.Web.frmMantenedor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript"> 
    function jsMostrarMant() {
        $("#divMantenimiento").modal('show');
    }

    function jsOcultarMant() {
        $("#divMantenimiento").modal('hide');
    }

    function js_ValidarFiltros() {
        var blnddlTipoDato = false;
        var strMjError = "";
        var strTipoDato = $("#ddlTipoDato").val().trim();

        if (strTipoDato == "0") {
            strMjError += "Debe seleccionar el Tipo de Dato.\n"; blnddlTipoDato = true;
        }

        if (strMjError != "") {
            if (blnddlTipoDato) { $("#valddlTipoDato").show(); } else { $("#valddlTipoDato").hide(); }
            alert(strMjError);
            return false;
        } else {
            return true;
        }
    }

    function js_ValidarDatos() {
        var strMjError = "";
        var strDescripcion = $("#txtDescripcion").val().trim();
        var strValor1 = $("#txtValor1").val().trim();
        var strEstado = $("#ddlEstado").val();
        
        if (strDescripcion == "") {
            strMjError += "Debe ingresar la Descripción.\n";
        }
        if (strValor1 == "") {
            strMjError += "Debe ingresar el Valor 1.\n";
        }
        if (strEstado == "") {
            strMjError += "Debe seleccionar el Estado.\n";
        }

        if (strMjError != "") {
            alert(strMjError);
            return false;
        } else {
            return true;
        }
    }
</script>    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <header class="panel-heading" style="padding-bottom:1px;">        
        <div class="dvTitFormA">Configuración de Datos</div>
    </header> 
    <div class="panel-body">
        <div class="dvTitFormB">
            <label class="FontTitulo">Mantenimiento de Tablas maestras.</label>
        </div> 
        <div class="tab-pane">
            <div class="modal-header dvFondoTituloForm">
                <h4 class="modal-title dvTXTTitForm">Filtros de Consulta</h4>
            </div>
            <div class="modal-body">
                <div class="form-horizontal tasi-form">                    
                    <div class="row">
                        <div class="col-md-12 col-sm-12 col-xs-12">
                            <div class="col-md-2 col-sm-2 col-xs-12">
                                <label class="control-label">Tipo Dato</label>
                            </div>
                            <div class="col-md-5 col-sm-6 col-xs-12 m-bot15">
                                <asp:DropDownList ID="ddlTipoDato" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                                <div style="display:inline-block;"><div id="valddlTipoDato" style="display:none;color:red;">*</div></div>
                            </div>  
                            <div class="col-md-5 col-sm-4 col-xs-12 m-bot15">                                
                                <asp:UpdatePanel ID="uplFiltros" runat="server">
                                    <ContentTemplate>                                             
                                        <asp:LinkButton ID="btnConsultar" OnClientClick="javascript:return js_ValidarFiltros();" runat="server" 
                                            OnClick="btnConsultar_Click" CssClass="btn btn-primary btnTxtBotones"><i class="fa fa fa-search fa-lg"></i> Consultar</asp:LinkButton>&nbsp;&nbsp;
                                        <asp:LinkButton ID="btnExportarExcel" runat="server" OnClick="btnExportarExcel_Click" 
                                            CssClass="btn btn-warning btnTxtBotones"><i class="fa fa-table fa-lg"></i> Exportar a Excel</asp:LinkButton> 
                                    </ContentTemplate>
                                </asp:UpdatePanel>                                                           
                            </div> 
                        </div>
                    </div>                        
                </div> 
            </div> 
        </div>
        <div class="dvAltoForm"></div>
        <div class="tab-pane">
            <div class="modal-header dvFondoTituloForm">
                <h4 class="modal-title dvTXTTitForm">Resultados de la Consulta</h4>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-sm-12 col-xs-12"> 
                        <section class="panel">
						    <div class="panel-body" style="padding:0px">
						        <div class="adv-table table-responsive">
                                    <asp:UpdatePanel ID="upGrilla" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:GridView ID="gvDatos" runat="server" AutoGenerateColumns="false" Width="100%" OnPreRender="gvDatos_PreRender"
                                                CssClass="display table table2 table-hover table-bordered table-striped dataTable mGrid" aria-describedby="dynamic-table_info"
                                                ShowHeaderWhenEmpty="true" EmptyDataText="No se encontraron resultados." AllowSorting="true" OnSorting="gvDatos_Sorting" >
                                                <Columns>
                                                    <asp:TemplateField HeaderStyle-Width="30px" ItemStyle-Width="30px" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="AlineaTextCenter">
                                                        <HeaderTemplate>N°</HeaderTemplate>
                                                        <ItemTemplate>
                                                                <asp:Label ID="lblfila" runat="server" Text="<%# Container.DataItemIndex + 1 %>"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>  
                                                    <asp:BoundField DataField="IdTablaHija" HeaderText="Código" SortExpression="IdTablaHija" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="AlineaTextCenter"/>
                                                    <asp:BoundField DataField="Descripcion" HeaderText="Descripción" SortExpression="Descripcion" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="AlineaTextIzq"/>
                                                    <asp:BoundField DataField="Valor1" HeaderText="Valor 1" SortExpression="Valor1" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="AlineaTextIzq"/>
                                                    <asp:BoundField DataField="Valor2" HeaderText="Valor 2" SortExpression="Valor2" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="AlineaTextIzq"/>                                                                                                                                                                                                                                  
                                                    <asp:BoundField DataField="DesEstado" HeaderText="Estado" SortExpression="DesEstado" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="AlineaTextIzq"/>
                                                    <asp:TemplateField HeaderText="Editar" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="30px">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ibnEditar" runat="server" CommandName="Editar" CommandArgument='<%# Eval("IdTablaHija") %>' 
                                                                ToolTip="Editar" ImageUrl="~/Content/Images/imgDetalle.png" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>                                                      
                                                </Columns>
                                            </asp:GridView>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnConsultar" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </section>
                    </div>
                </div>                                 
            </div>
        </div>
    </div>
    <div id="dvGeneraExcel" style="display:none;">
        <asp:GridView ID="gvExcel" runat="server" AutoGenerateColumns="false" Width="100%" EmptyDataText="No se encontraron resultados."
            CssClass="display table table2 table-hover table-bordered table-striped dataTable mGrid" aria-describedby="dynamic-table_info">
            <Columns>
                <asp:TemplateField HeaderStyle-Width="30px" ItemStyle-Width="30px" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="AlineaTextCenter">
                    <HeaderTemplate>N°</HeaderTemplate>
                    <ItemTemplate>
                            <asp:Label ID="lblfila" runat="server" Text="<%# Container.DataItemIndex + 1 %>"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>  
                <asp:BoundField DataField="IdTablaHija" HeaderText="Código" HeaderStyle-Width="90px" ItemStyle-Width="90px" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="AlineaTextCenter"/>
                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" HeaderStyle-Width="120px" ItemStyle-Width="120px" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="AlineaTextIzq"/>
                <asp:BoundField DataField="Valor1" HeaderText="Valor 1" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="AlineaTextIzq"/>
                <asp:BoundField DataField="Valor2" HeaderText="Valor 2" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="AlineaTextIzq"/>                                                                                                                                                                                                                                  
                <asp:BoundField DataField="DesEstado" HeaderText="Estado" HeaderStyle-Width="80px" ItemStyle-Width="80px" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="AlineaTextCenter"/>
            </Columns>
        </asp:GridView>
    </div>    
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="opciones" runat="server">
    <div id="divMantenimiento" aria-hidden="true" role="dialog" tabindex="1" class="modal fade" data-backdrop="static">
        <div class="modal-dialog PopDetSalida">
            <div class="modal-content">
                <div class="modal-header">
                    <button aria-hidden="true" data-dismiss="modal" class="close" type="button">×</button>
                    <h4 class="modal-title">Manteniento de Datos</h4>
                </div>
                <asp:UpdatePanel ID="upnlMant" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-body">
			                <div class="form-horizontal tasi-form">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <label class="control-label col-md-2">Código</label>
                                                <div class="col-md-4 m-bot15">
                                                    <asp:TextBox ID="txtCodigo" ClientIDMode="Static" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div> 
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <label class="control-label col-md-2">Descripción</label>
                                                <div class="col-md-10 m-bot15">
                                                    <asp:TextBox ID="txtDescripcion" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>                                        
                                        </div>  
                                        <div class="row">
                                            <div class="col-md-6">
                                                <label class="control-label col-md-4">Valor 1</label>
                                                <div class="col-md-8 m-bot15">
                                                    <asp:TextBox ID="txtValor1" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div> 
                                            <div class="col-md-6">
                                                <label class="control-label col-md-4">Valor 2</label>
                                                <div class="col-md-8 m-bot15">
                                                    <asp:TextBox ID="txtValor2" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>    
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <label class="control-label col-md-2">Estado</label>
                                                <div class="col-md-4 m-bot15">
                                                    <asp:DropDownList ID="ddlEstado" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                            </div>                                        
                                        </div>    
                                    </div>                                 
                                </div>   
                            </div>                            
                        </div>
                        <div class="modal-footer">
                            <div class="col-md-12 m-bot15">
                                <asp:LinkButton ID="btnGuardar" OnClientClick="javascript:return js_ValidarDatos();" runat="server" 
                                    CssClass="btn btn-primary"><i class="fa fa-refresh fa-lg"></i> Guardar</asp:LinkButton>
                                <button type="button" class="btn btn-default" data-dismiss="modal"><i class="fa fa-window-close fa-lg"></i> Cerrar </button>
                            </div>                                
                        </div>
                    </ContentTemplate>
            </asp:UpdatePanel>  
            </div>
        </div>
    </div>
</asp:Content>
