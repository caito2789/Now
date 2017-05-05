<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Views/Shared/mpPrincipal.Master" CodeBehind="frmInicio.aspx.vb" Inherits="SMV.Interoperabilidad.Web.frmInicio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="opciones" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <header class="panel-heading" style="padding-bottom:25px;">
        <div style="float:left;" class="col-xs-12 col-sm-12 col-md-12 col-lg-12"> Bienvenido al Sistema de Interoperabilidad</div>  
    </header>
    <div class="panel-body">
        <div class="tab-pane" id="atencion">
            <div class="row">
                <div class="col-sm-12">
                    <section class="panel">
                        <div class="panel-body" style="padding:0px">
                            <div class="row">
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                    <div id="dvBloque" style="display:none;" runat="server">
                                        <div class="row">
                                            <div class="dvBloqueInfo AlineaCenter">
                                                <asp:Label ID="lblMensaje" CssClass="lblMsjInicio" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="AltoInicio"></div>
                                        </div>
                                    </div>
                                </div>
                            </div>                               
                            <div class="row">
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                    <div class="row">
                                        <div class="dvBloqueIntro AlineaCenter">
                                            <asp:Label ID="lblIntro" CssClass="lblMsjInicio" runat="server"></asp:Label>
                                        </div>
                                    </div>                                    
                                </div>
                            </div>                        
                        </div>
                    </section>
                </div>
            </div>
        </div>
    </div>
</asp:Content> 
