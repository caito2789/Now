<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmLogin.aspx.vb" Inherits="SMV.Interoperabilidad.Web.frmLogin" %>

<!DOCTYPE html>
<html>
<head runat="server">
<meta content="text/html; charset=utf-8" http-equiv="Content-Type"/>
<meta content="IE=edge,chrome=1" http-equiv="X-UA-Compatible"/>
<meta content="width=device-width, initial-scale=1.0" name="viewport"/>
<title>Sistema de Interoperabilidad | Superintendencia del Mercado de Valores</title>
<link href="../../Content/Styles/Core/bootstrap.min.css" rel="stylesheet" type="text/css" />
<link href="<%=Page.ResolveClientUrl("~/Content/Styles/smv-seguridad.css")%>" rel="stylesheet" type="text/css" />
<script src="../../Content/Scripts/smv_Interoperabilidad.js"></script>
</head>
<body>
<form id="frmLogin" runat="server">
    <div class="dvCenterT">
        <div class="col-sm-12 col-md-12 col-lg-12">
            <div class="page">
                <div class="row">
                    <div class="dvCab">                        
                        <div class="header-titulo">
                            <div class="col-xs-1"></div>
                            <div class="col-xs-11">
                                <asp:Label ID="ltNombreAplicacion" CssClass="txtCabLg" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="h-login">
                        <div class="HSeguridad">Iniciar Sesión</div>
                        <div class="TxtLogin">Especifique su nombre de usuario y contraseña.</div>
                        <div class="failureNotification">
                            <asp:Literal ID="FailureText" runat="server"></asp:Literal>
                            <asp:ValidationSummary ID="LoginUserValidationSummary" runat="server" ValidationGroup="LoginUserValidationGroup" />
                        </div>
                        <div class="dvRow">
                            <div class="CuadroLg">
                                <div class="TitLgCuadro">Información de Acceso</div>
                                <div class="dvRow">
                                    <asp:Label ID="UserNameLabel" CssClass="TxtLogin2" runat="server" AssociatedControlID="txtUsuario">Nombre de Usuario:</asp:Label>
                                </div>
                                <div class="dvRowA">
                                    <asp:TextBox ID="txtUsuario" onkeypress="return SoloNumerosyLetras(event);" MaxLength="15" CssClass="textEntry" runat="server" TabIndex="1"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="txtUsuario"
                                        CssClass="failureNotification" ErrorMessage="El nombre de usuario es obligatorio."
                                        ToolTip="El nombre de usuario es obligatorio." ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="dvRow">
                                    <asp:Label ID="PasswordLabel" CssClass="TxtLogin2" runat="server" AssociatedControlID="txtPassword">Contraseña:
                                        &nbsp;<asp:HyperLink ID="lnkChangeClave" runat="server" NavigateUrl="http://10.0.2.86:8082/Interfaces/Seguridad/CambioClaveEmail.aspx"
                                        Target="_blank" Text="¿Cambiar de Contraseña?" CssClass="hplnkLogin" TabIndex="3"></asp:HyperLink> /
                                    <asp:HyperLink ID="lnkResetClave" runat="server" NavigateUrl="http://10.0.2.86:8082/Interfaces/Seguridad/CambioClave.aspx"
                                        Target="_blank" Text="¿Ha olvidado la Contraseña?" CssClass="hplnkLogin" TabIndex="4"></asp:HyperLink></asp:Label>
                                </div>
                                <div class="dvRowA">
                                    <asp:TextBox ID="txtPassword" MaxLength="15" CssClass="passwordEntry" runat="server" TextMode="Password" TabIndex="2"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="txtPassword"
                                        CssClass="failureNotification" ErrorMessage="La contraseña es obligatoria." ToolTip="La contraseña es obligatoria."
                                        ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="dvRowB">
                                    <asp:CheckBox ID="chkRecordar" TabIndex="5" runat="server" />
                                    <asp:Label ID="RememberMeLabel" runat="server" AssociatedControlID="chkRecordar" CssClass="TxtLogin2 inline">Mantenerme Conectado</asp:Label>
                                </div>
                            </div>                            
                        </div>
                        <div class="dvRowC"></div>
                        <div class="dvRow">
                            <div class="dvCenter">
                                <asp:Button ID="LoginButton" CssClass="btnLogin" runat="server" CommandName="Login" Text="Iniciar Sesi&oacute;n"
                                    ValidationGroup="LoginUserValidationGroup" OnClick="LoginButton_Click" TabIndex="6" />
                            </div>
                        </div>
                        <div class="dvRowD"></div>
                    </div>
                </div>
            </div>   
            <div class="dvFooter">
                <img src="<%=Page.ResolveClientUrl("~/Content/Images/img_footer.jpg")%>" style="margin:0 auto; display:table;" alt="Ministerio de Econom&iacute;a y Finanzas" width="358" height="44"/>
                <div style="margin:0 auto; display:table;">
                    <label class="PieLogin">Av. Santa Cruz 315 - Miraflores, Lima - Perú.</label><br/>
                    <label class="PieLogin">Telf. (+511) 610-6300 | 
                        <a class="PieLogin" href="http://www.smv.gob.pe/Frm_Contactenos.aspx?data=442BF686DEADB420A7726AFCB63A21D40EA9F8A837">Contáctenos</a>
                    </label>
                </div>
            </div>           
        </div>
    </div>
</form>
</body>
</html>
