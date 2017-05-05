Imports SMV.Interoperabilidad.BL
Imports SMV.Interoperabilidad.BE

Public Class mpPrincipal
    Inherits System.Web.UI.MasterPage

#Region "Eventos"
    Protected Overrides Sub OnInit(e As System.EventArgs)
        MyBase.OnInit(e)
        Me.ValidarAcceso(Page)
        Me.lblLogin.Text = FachadaSesion.Usuario.NombreUsuario
        Cargar_Menu()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Me.TituloPrincipal.Text = BE.Constantes.K_TITULO_APP
            Me.htxtUrl.Value = String.Format("{0}?ReturnURL={1}", UrlPagina.K_FORMULARIO_LOGINJQ, HttpContext.Current.Server.UrlEncode(HttpContext.Current.Request.RawUrl))
        End If
    End Sub

    Protected Sub lnkLogin_Click(sender As Object, e As EventArgs) Handles lnkLogin.Click
        HttpContext.Current.Session.Abandon()
        HttpContext.Current.Session.RemoveAll()
        HttpContext.Current.Session.Clear()
        FormsAuthentication.SignOut()
        HttpContext.Current.Response.Redirect(UrlPagina.K_FORMULARIO_LOGIN)
    End Sub

    Protected Sub smPrincipal_AsyncPostBackError(sender As Object, e As AsyncPostBackErrorEventArgs) Handles smPrincipal.AsyncPostBackError
        If Not IsNothing(e.Exception.Data("ExtraInfo")) Then
            smPrincipal.AsyncPostBackErrorMessage =
               e.Exception.Message &
               e.Exception.Data("ExtraInfo").ToString()
        Else
            smPrincipal.AsyncPostBackErrorMessage = e.Exception.Message
        End If
    End Sub
#End Region

#Region "Metodos"
    Public Sub ValidarAcceso(pPagina As Page)
        If IsNothing(FachadaSesion.Usuario) Then
            HttpContext.Current.Session.Abandon()
            HttpContext.Current.Session.RemoveAll()
            HttpContext.Current.Session.Clear()
            FormsAuthentication.SignOut()
            HttpContext.Current.Response.Redirect(String.Format("{0}?ReturnURL={1}", UrlPagina.K_FORMULARIO_LOGIN, HttpContext.Current.Server.UrlEncode(HttpContext.Current.Request.RawUrl)))

        ElseIf Not Request.RawUrl.Contains(Globales.UrlPagina.K_FORMULARIO_DEFAULT.Substring(1)) Then
            'Obtener Datos de la opción
            Dim oOpcionMenu As UsuarioPerfilOpcionBE = (From o In FachadaSesion.Usuario.ListaUsuarioPerfilOpcion
                         Where o.NombrePagina = If(Request.ApplicationPath <> "/", _
                                                   Request.RawUrl.Replace("/Views", String.Empty).Replace(Request.ApplicationPath, String.Empty).Substring(1).Replace(".aspx", String.Empty), _
                                                   Request.RawUrl.Replace("/Views", String.Empty).Substring(1).Replace(".aspx", String.Empty)) _
                                               ).OrderByDescending(Function(o) o.CodigoSecuencial).FirstOrDefault

            'Globales.LogErrores(Request.ApplicationPath & " - " & Request.RawUrl, If(Request.ApplicationPath <> "/", _
            '                                                   Request.RawUrl.Replace("/Views", String.Empty).Replace(Request.ApplicationPath, String.Empty).Substring(1).Replace(".aspx", String.Empty), _
            '                                                   Request.RawUrl.Replace("/Views", String.Empty).Substring(1).Replace(".aspx", String.Empty)))
            If oOpcionMenu Is Nothing Then
                HttpContext.Current.Response.Redirect(UrlPagina.K_FORMULARIO_DEFAULT)
            Else
                Me.RegistrarAcceso(oOpcionMenu)
            End If
        End If
    End Sub

    Protected Sub RegistrarAcceso(oOpcionMenu As UsuarioPerfilOpcionBE)
        If Not IsPostBack Then
            Dim oparam As New AccesoSeguridad.RegistrarAccesoRequest()
            oparam.CodigoAplicacion = oOpcionMenu.CodigoAplicacion
            oparam.CodigoOpcion = oOpcionMenu.CodigoOpcion
            oparam.CodigoUsuario = oOpcionMenu.CodigoUsuario
            oparam.DireccionIP = Me.GetIp()

            'Nombre de PC Cliente
            Try
                Dim hostEntry As System.Net.IPHostEntry = System.Net.Dns.GetHostEntry(oparam.DireccionIP)
                oparam.NombrePC = hostEntry.HostName
            Catch ex As Exception
                Try
                    oparam.NombrePC = Split(System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).HostName, ".")(0).ToUpper()
                Catch e As Exception
                    oparam.NombrePC = String.Empty
                End Try
            End Try

            Dim oSeguridadServiceClient As New AccesoSeguridad.SeguridadServiceClient()

            Me.hdnOpcion.Value = oSeguridadServiceClient.RegistrarAcceso(oparam).NCOUTIL
        End If
    End Sub

    Protected Function GetIp() As String
        Dim currentRequest As HttpRequest = HttpContext.Current.Request
        Dim ipAddress As String = currentRequest.ServerVariables("HTTP_X_FORWARDED_FOR")

        If ipAddress Is Nothing Then
            ipAddress = currentRequest.ServerVariables("REMOTE_ADDR")
        Else
            If ipAddress.ToLower() = "unknown" Then
                ipAddress = currentRequest.ServerVariables("REMOTE_ADDR")
            End If
        End If

        Return ipAddress
    End Function

    Private Sub Cargar_Menu()
        Dim strMenu As String = String.Empty

        Dim listaMenu = FachadaSesion.Usuario.ListaUsuarioPerfilOpcion
        Dim padre = listaMenu
        Dim MenuPrincipal = padre.FindAll(Function(menu) menu.CodigoSecuencial.Length = 2)

        For Each objListaMenu In MenuPrincipal
            Dim liReferencia = "<li class=''> <a href='../../Views/" & objListaMenu.NombrePagina & ".aspx'>" & objListaMenu.Denominacion & "</a>"
            strMenu = strMenu & liReferencia & "[sub]</li>"
            If AgregarSubMenu(strMenu, objListaMenu.CodigoSecuencial, 2, 0) Then
                strMenu = strMenu.Replace(liReferencia, "<li class=''> <a>" & objListaMenu.Denominacion & "</a>")
                'strMenu = strMenu.Replace(liReferencia, "<li class=''> <a data-toggle='collapse' href='#" & objListaMenu.CodigoSecuencial & "'>" & objListaMenu.Denominacion & "</a>")
            End If
            strMenu = strMenu.Replace("[sub]", "")
        Next

        litMenuPrin.Text = strMenu
        'ltMenu.Text = strMenu
    End Sub

    Private Function AgregarSubMenu(ByRef pstrMenu As String, param As String, length As Integer, nivel As Integer) As Boolean
        Dim tieneHijos As Boolean = False
        length = length + 2
        nivel = nivel + 1

        Dim listaMenu = FachadaSesion.Usuario.ListaUsuarioPerfilOpcion
        Dim listaHija = listaMenu.FindAll(Function(menu) menu.CodigoSecuencial.StartsWith(param) And menu.CodigoSecuencial.Length = length)

        Dim strSubMenu As String = ""
        If listaHija.Count > 0 Then
            strSubMenu = strSubMenu & "<ul>[sub]</ul>"
            'strSubMenu = strSubMenu & "<div id='" & param & "' class='panel-collapse collapse'><ul>[sub]</ul></div>"
        Else
            strSubMenu = strSubMenu & "[sub]"
        End If

        For Each objListaMenu In listaHija
            tieneHijos = True
            Dim liReferencia = "<li class='nivelmenu-" & nivel.ToString & "'> <a href='../../Views/" & objListaMenu.NombrePagina & ".aspx'>" & objListaMenu.Denominacion & "</a>"
            strSubMenu = strSubMenu.Replace("[sub]", liReferencia & "[sub]</li>")
            If AgregarSubMenu(strSubMenu, objListaMenu.CodigoSecuencial, length, nivel) Then
                'Se borra la url:
                strSubMenu = strSubMenu.Replace(liReferencia, "<li class='nivelmenu-" & nivel.ToString & "'> <a>" & objListaMenu.Denominacion & "</a>")
            Else
                strSubMenu = strSubMenu.Replace("[sub]</li>", "</li>[sub]")
            End If
        Next
        pstrMenu = pstrMenu.Replace("[sub]", strSubMenu.ToString())

        Return tieneHijos
    End Function

#End Region

End Class
