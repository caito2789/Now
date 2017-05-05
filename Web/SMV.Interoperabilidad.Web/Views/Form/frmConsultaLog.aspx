<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Views/Shared/mpPrincipal.Master" CodeBehind="frmConsultaLog.aspx.vb" Inherits="SMV.Interoperabilidad.Web.frmConsultaLog" %>

<%@ Register Src="~/Views/Controles/ucwSalida.ascx" TagPrefix="uc1" TagName="ucwSalida" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">   
    $(function () {
        load_grid();
    });

    function load_grid() {
        $('.input-datepicker').datepicker();
    }

    function js_ValidarFiltros() {
        var blntxtFECINI = false; var blntxtFECFIN = false;
        var strMjError = "";
        var strFechaIni = $("#txtFECINI").val().trim();
        var strFechaFin = $("#txtFECFIN").val().trim();

        if (strFechaIni == "") {
            strMjError += "Debe ingresar la Fecha Inicio.\n"; blntxtFECINI = true;
        }
        if (strFechaFin == "") {
            strMjError += "Debe ingresar la Fecha Fin.\n"; blntxtFECFIN = true;
        }
        if ((strFechaIni != "") && (strFechaFin != "")) {
            if (js_CompareDates2(strFechaFin, "dd/MM/yyyy", strFechaIni, "dd/MM/yyyy") != 1) {
                strMjError += "La Fecha Fin debe ser mayor o igual a la Fecha Inicio."; blntxtFECFIN = true;
            }
        }

        if (strMjError != "") {
            if (blntxtFECINI) { $("#valtxtFECINI").show(); } else { $("#valtxtFECINI").hide(); }
            if (blntxtFECFIN) { $("#valtxtFECFIN").show(); } else { $("#valtxtFECFIN").hide(); }
            alert(strMjError);
            return false;
        } else {
            return true;
        }
    }

    function js_LimpiarFiltros() {
        $("#ddlTipoServicio").val("0");
        $("#txtFECINI").val("");
        $("#txtFECFIN").val("");
        $("#ddlUsuario").val("0");
        $("#ddlTipoEvento").val("0");

        $("#hdnDSTipoServ").val("");
        $("#hdnDSCodLog").val("");
    }

    function js_VerDetalle(xTS, xLg) {
        $("#hdnDSTipoServ").val(xTS);
        $("#hdnDSCodLog").val(xLg);

        var myObject = new Object();
        myObject.CodTipoServicio = xTS;
        myObject.CodLog = xLg;
        myObject = JSON.stringify({ pobjFiltro: myObject });

        $.ajax({
            type: "POST",
            url: "frmConsultaLog.aspx/CargarDetalleSalida",
            contentType: "application/json; charset=utf-8",
            data: myObject,
            dataType: "JSON",
            asyn: true,
            beforeSend: function (objeto) {
                $('#tblGrillaSalida tbody').empty();
                $('#tblGrillaSalida tbody').html('<tr><td style="text-align:center;" colspan="2"><div><img src="../../Content/Images/ico-load.gif"/></div></td></tr>');
            },
            success: function (response) {
                //alert(response.d); 
                $('#tblGrillaSalida thead').empty();
                $('#tblGrillaSalida tbody').empty();

                var intCuenta = 0;
                var datos = (typeof response.d) == 'string' ? eval('(' + response.d + ')') : response.d;
                if (datos != null) {
                    if (datos.length > 0) {

                        if (xTS == 6) {   //SUNARP - Titularidad de Bienes

                            //Cabecera
                            $('#tblGrillaSalida thead').append('<tr role="row">' +
                                    '<th style="width:30px; text-transform:uppercase;">N°</th>' +
                                    '<th style="text-transform:uppercase;">Apellido Paterno</th>' +
                                    '<th style="text-transform:uppercase;">Apellido Materno</th>' +
                                    '<th style="text-transform:uppercase;">Nombres</th>' +
                                    '<th style="text-transform:uppercase;">Razón Social</th>' +
                                    '<th style="text-transform:uppercase;">Tipo Documento</th>' +
                                    '<th style="text-transform:uppercase;">Nro. Documento</th>' +
                                    '<th style="text-transform:uppercase;">Número Partida</th>' +
                                    '<th style="text-transform:uppercase;">Registro</th>' +
                                    '<th style="text-transform:uppercase;">Número Placa</th>' +
                                    '<th style="text-transform:uppercase;">Zona</th>' +
                                    '<th style="text-transform:uppercase;">Oficina</th>' +
                                    '<th style="text-transform:uppercase;">Estado</th>' +
                               '</tr>');

                            //Filas
                            intCuenta = 0;
                            for (var i = 0; i < datos.length; i++) {

                                intCuenta = i + 1;

                                if ((intCuenta % 2) == 0) {
                                    strPintaFila = ' class="trPintalFila"';
                                } else {
                                    strPintaFila = "";
                                }                                

                                $('#tblGrillaSalida tbody').append('<tr ' + strPintaFila + '>' +
                                        '<td style="text-align:center;">' + intCuenta + '</td>' +
                                        '<td style="text-align:left;">' + datos[i].ApePaterno + '</td>' +
                                        '<td style="text-align:left;">' + datos[i].ApeMaterno + '</td>' +
                                        '<td style="text-align:left;">' + datos[i].Nombres + '</td>' +
                                        '<td style="text-align:left;">' + datos[i].RazonSocial + '</td>' +
                                        '<td style="text-align:center;">' + datos[i].TipoDocumento + '</td>' +
                                        '<td style="text-align:center;">' + datos[i].NroDocumento + '</td>' +
                                        '<td style="text-align:center;">' + datos[i].NumPartida + '</td>' +
                                        '<td style="text-align:center;">' + datos[i].Registro + '</td>' +
                                        '<td style="text-align:center;">' + datos[i].NumPlaca + '</td>' +
                                        '<td style="text-align:left;">' + datos[i].Zona + '</td>' +
                                        '<td style="text-align:left;">' + datos[i].Oficina + '</td>' +
                                        '<td style="text-align:center;">' + datos[i].Estado + '</td>' +
                                    '</tr>');
                            }

                        } else if (xTS == 8) {   //SUNARP - Lista de Asientos

                            //Cabecera
                            $('#tblGrillaSalida thead').append('<tr role="row">' +
                                    '<th style="width:30px; text-transform:uppercase;">N°</th>' +
                                    '<th style="text-transform:uppercase;">Transacción</th>' +
                                    '<th style="text-transform:uppercase;">Nro. Total Pág.</th>' +
                                    '<th style="text-transform:uppercase;">ID Imagen Asiento</th>' +
                                    '<th style="text-transform:uppercase;">Nro. Páginas</th>' +
                                    '<th style="text-transform:uppercase;">Tipo</th>' +
                                    '<th style="text-transform:uppercase;">Nro. Pág. Ref.</th>' +
                                    '<th style="text-transform:uppercase;">Página</th>' +
                               '</tr>');

                            //Filas
                            intCuenta = 0;
                            for (var i = 0; i < datos.length; i++) {

                                intCuenta = i + 1;

                                if ((intCuenta % 2) == 0) {
                                    strPintaFila = ' class="trPintalFila"';
                                } else {
                                    strPintaFila = "";
                                }

                                $('#tblGrillaSalida tbody').append('<tr ' + strPintaFila + '>' +
                                        '<td style="text-align:center;">' + intCuenta + '</td>' +
                                        '<td style="text-align:center;">' + datos[i].Transaccion + '</td>' +
                                        '<td style="text-align:center;">' + datos[i].NroTotalPag + '</td>' +
                                        '<td style="text-align:left;">' + datos[i].IDImgAsiento + '</td>' +
                                        '<td style="text-align:center;">' + datos[i].CantidadPag + '</td>' +
                                        '<td style="text-align:left;">' + datos[i].Tipo + '</td>' +
                                        '<td style="text-align:center;">' + datos[i].NroPagRef + '</td>' +
                                        '<td style="text-align:center;">' + datos[i].NroPag + '</td>' +
                                    '</tr>');
                            }

                        } if (xTS == 10) {   //SUNEDU - Grados y Titulos

                            //Cabecera
                            $('#tblGrillaSalida thead').append('<tr role="row">' +
                                    '<th style="width:30px; text-transform:uppercase;">N°</th>' +
                                    '<th style="text-transform:uppercase;">Apellido Paterno</th>' +
                                    '<th style="text-transform:uppercase;">Apellido Materno</th>' +
                                    '<th style="text-transform:uppercase;">Nombres</th>' +
                                    '<th style="text-transform:uppercase;">Tipo Documento</th>' +
                                    '<th style="text-transform:uppercase;">Nro. Documento</th>' +
                                    '<th style="text-transform:uppercase;">País</th>' +
                                    '<th style="text-transform:uppercase;">Universidad</th>' +
                                    '<th style="text-transform:uppercase;">Título Profesional</th>' +
                                    '<th style="text-transform:uppercase;">Abreviatura Título</th>' +
                                    '<th style="text-transform:uppercase;">Especialidad</th>' +
                               '</tr>');

                            //Filas
                            intCuenta = 0;
                            for (var i = 0; i < datos.length; i++) {

                                intCuenta = i + 1;

                                if ((intCuenta % 2) == 0) {
                                    strPintaFila = ' class="trPintalFila"';
                                } else {
                                    strPintaFila = "";
                                }

                                $('#tblGrillaSalida tbody').append('<tr ' + strPintaFila + '>' +
                                        '<td style="text-align:center;">' + intCuenta + '</td>' +
                                        '<td style="text-align:left;">' + datos[i].ApePaterno + '</td>' +
                                        '<td style="text-align:left;">' + datos[i].ApeMaterno + '</td>' +
                                        '<td style="text-align:left;">' + datos[i].Nombres + '</td>' +
                                        '<td style="text-align:center;">' + datos[i].TipoDocumento + '</td>' +
                                        '<td style="text-align:center;">' + datos[i].NroDocumento + '</td>' +
                                        '<td style="text-align:left;">' + datos[i].Pais + '</td>' +
                                        '<td style="text-align:center;">' + datos[i].Universidad + '</td>' +
                                        '<td style="text-align:left;">' + datos[i].TitProfesional + '</td>' +
                                        '<td style="text-align:center;">' + datos[i].AbrTitulo + '</td>' +
                                        '<td style="text-align:center;">' + datos[i].Especialidad + '</td>' +
                                    '</tr>');
                            }

                        }

                    } else {
                        alert("Ocurrio un problema al cargar el Detalle de la Salida.");
                    }
                } else {
                    alert("Ocurrio un problema al cargar el Detalle de la Salida.");
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) { alert(textStatus + ": " + XMLHttpRequest.responseText); }
        });

    }
</script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <header class="panel-heading" style="padding-bottom:1px;">        
        <div style="padding-right:0px;">Consulta de Logs</div>
    </header>
    <div class="panel-body">
        <div class="dvTitFormB">
            <label class="FontTitulo">Consulta de Logs sobre las consultas realizadas con los Servicios Web.</label>
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
                                    <div class="col-md-1 col-sm-2 col-xs-12">
                                        <label class="control-label">Tipo Servicio</label>
                                    </div>
                                    <div class="col-md-3 col-sm-10 col-xs-12 m-bot15">
                                        <asp:DropDownList ID="ddlTipoServicio" CssClass="form-control" ClientIDMode="Static" runat="server"></asp:DropDownList>
                                    </div> 
                                    <div class="col-md-1 col-sm-2 col-xs-12">
                                        <label class="control-label">Fecha Inicio</label>
                                    </div>
                                    <div class="col-md-2 col-sm-4 col-xs-12 m-bot15">                                          
                                        <div style="width:130px"><div style="float:right;"><div id="valtxtFECINI" style="display:none; color: red;">*</div></div> 
                                            <div id="dpfchIni" style="width:100px;" class="input-append date dpYears input-datepicker" data-date="" data-date-format="dd/mm/yyyy" data-date-viewmode="years">
                                                <asp:TextBox ID="txtFECINI" ClientIDMode="Static" runat="server" CssClass="form-control" data-date-format="dd/mm/yyyy" Width="100px" MaxLength="10"></asp:TextBox>                                                                                  
                                                <span class="input-group-btn add-on">
                                                    <button id="btnFI" type="button" class="btn btn-primary" title="Fecha Inicio" style="height:27px; width:28px;"><i class="fa fa-calendar fa-lg"></i></button>
                                                </span> 
                                            </div>   
                                        </div> 
                                    </div>
                                    <div class="col-md-1 col-sm-2 col-xs-12">
                                        <label class="control-label">Fecha Fin</label>
                                    </div>
                                    <div class="col-md-4 col-sm-4 col-xs-12 m-bot15">
                                        <div style="width:130px"><div style="float:right;"><div id="valtxtFECFIN" style="display:none; color: red;">*</div></div> 
                                            <div id="dpfchFin" style="width:100px;" class="input-append date dpYears input-datepicker" data-date="" data-date-format="dd/mm/yyyy" data-date-viewmode="years">
                                                <asp:TextBox ID="txtFECFIN" ClientIDMode="Static" runat="server" CssClass="form-control" data-date-format="dd/mm/yyyy" Width="100px" MaxLength="10"></asp:TextBox>                                                  
                                                <span class="input-group-btn add-on">
                                                    <button id="btnFF" type="button" class="btn btn-primary" title="Fecha Fin" style="height:27px; width:28px;"><i class="fa fa-calendar fa-lg"></i></button>
                                                </span>
                                            </div>       
                                        </div>                                 
                                    </div>
                                </div>
                                <div class="col-md-12 col-sm-12 col-xs-12">
                                    <div class="col-md-1 col-sm-2 col-xs-12">
                                        <label class="control-label">Usuario</label>
                                    </div>
                                    <div class="col-md-3 col-sm-4 col-xs-12 m-bot15">
                                        <asp:DropDownList ID="ddlUsuario" CssClass="form-control" ClientIDMode="Static" runat="server"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-1 col-sm-2 col-xs-12">
                                        <label class="control-label">Tipo Evento</label>
                                    </div>
                                    <div class="col-md-2 col-sm-4 col-xs-12 m-bot15">
                                        <asp:DropDownList ID="ddlTipoEvento" CssClass="form-control" ClientIDMode="Static" runat="server"></asp:DropDownList>
                                    </div>  
                                    <div class="col-md-5 col-sm-12 col-xs-12 m-bot15">
                                        <div class="dvBtnFiltroDer">
                                            <asp:LinkButton ID="btnConsultar" OnClientClick="javascript:return js_ValidarFiltros();" runat="server" 
                                                OnClick="btnConsultar_Click" CssClass="btn btn-primary btnTxtBotones"><i class="fa fa fa-search fa-lg"></i> Consultar</asp:LinkButton>&nbsp;&nbsp;
                                            <asp:LinkButton ID="btnLimpiar" OnClientClick="javascript:js_LimpiarFiltros();" runat="server" 
                                                CssClass="btn btn-primary btnTxtBotones"><i class="fa fa fa-eraser fa-lg"></i> Limpiar</asp:LinkButton>&nbsp;&nbsp;
                                            <asp:LinkButton ID="btnExportarExcel" runat="server" OnClick="btnExportarExcel_Click" 
                                                CssClass="btn btn-warning btnTxtBotones"><i class="fa fa-table fa-lg"></i> Exportar a Excel</asp:LinkButton>                                        
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
                                                                <asp:BoundField DataField="TipoServicio" HeaderText="Tipo Servicio" SortExpression="TipoServicio" HeaderStyle-Width="110px" ItemStyle-Width="110px" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="AlineaTextIzq"/>
                                                                <asp:BoundField DataField="URLTipoServicio" HeaderText="URL Tipo Servicio" SortExpression="URLTipoServicio" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="AlineaTextIzq"/>
                                                                <asp:BoundField DataField="TipoEvento" HeaderText="Tipo Evento" SortExpression="TipoEvento" HeaderStyle-Width="90px" ItemStyle-Width="90px" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="AlineaTextIzq"/>
                                                                <asp:BoundField DataField="Usuario" HeaderText="Usuario" SortExpression="Usuario" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="AlineaTextIzq"/>
                                                                <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha del Evento" SortExpression="FechaRegistro" HeaderStyle-Width="80px" ItemStyle-Width="80px" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="AlineaTextCenter"/>                                                                                                                                                                                                                                  
                                                                <asp:BoundField DataField="TipoResultado" HeaderText="Tipo Resultado" SortExpression="TipoResultado" HeaderStyle-Width="80px" ItemStyle-Width="80px" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="AlineaTextIzq"/>
                                                                <asp:BoundField DataField="Entrada" HeaderText="Entrada" SortExpression="Entrada" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="AlineaTextIzq"/>
                                                                <asp:TemplateField HeaderText="Salida" SortExpression="Salida" HeaderStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Literal ID="ltVerDetalle" runat="server"></asp:Literal>                                                                        
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>  
                                                                <asp:BoundField DataField="CodTipoServicio" HeaderText="CodTipoServicio" />
                                                                <asp:BoundField DataField="CodTipoResultado" HeaderText="CodTipoResultado" />
                                                                <asp:BoundField DataField="CodLog" HeaderText="CodLog" />
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
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnConsultar" EventName="Click" />
                            <asp:PostBackTrigger ControlID="btnExportarExcel"/>
                        </Triggers>
                    </asp:UpdatePanel>
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
                <asp:BoundField DataField="TipoServicio" HeaderText="Tipo Servicio" HeaderStyle-Width="110px" ItemStyle-Width="110px" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="AlineaTextIzq"/>
                <asp:BoundField DataField="URLTipoServicio" HeaderText="URL Tipo Servicio" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="AlineaTextIzq"/>
                <asp:BoundField DataField="TipoEvento" HeaderText="Tipo Evento" HeaderStyle-Width="90px" ItemStyle-Width="90px" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="AlineaTextIzq"/>
                <asp:BoundField DataField="Usuario" HeaderText="Usuario" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="AlineaTextIzq"/>
                <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha del Evento" HeaderStyle-Width="80px" ItemStyle-Width="80px" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="AlineaTextCenter"/>                                                                                                                                                                                                                                  
                <asp:BoundField DataField="TipoResultado" HeaderText="Tipo Resultado" HeaderStyle-Width="80px" ItemStyle-Width="80px" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="AlineaTextIzq"/>
                <asp:BoundField DataField="Entrada" HeaderText="Entrada" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="AlineaTextIzq"/>
                <asp:BoundField DataField="Salida" HeaderText="Salida" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="AlineaTextIzq"/>
            </Columns>
        </asp:GridView>
    </div>    
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="opciones" runat="server">
    <div id="VerDetSalida" style="display:none;" aria-hidden="true" role="dialog" tabindex="1" class="modal fade" data-backdrop="static">
        <uc1:ucwSalida runat="server" id="ucwSalida" />
    </div>
</asp:Content>
