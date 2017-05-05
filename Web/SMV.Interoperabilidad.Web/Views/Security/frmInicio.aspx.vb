Imports SMV.Interoperabilidad.BL
Imports SMV.Interoperabilidad.BE
Imports System.Web.Services
Imports System.Web.Script.Services

Public Class frmInicio
    Inherits System.Web.UI.Page

#Region "Formulario"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.lblIntro.Text = BE.Constantes.K_MSJ_INTRO
            Me.ValidarMensajeUsuario()
        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region "Metodos"

    Public Sub ValidarMensajeUsuario()
        Try
            Dim intValida As Integer = 0
            Dim strLogin As String = String.Empty

            Me.lblMensaje.Text = ""
            Me.dvBloque.Style("display") = "none"

            'strLogin = FachadaSesion.Usuario.UsuarioLogon.Trim()
            'intValida = FachadaSesion.Usuario_Permisos

            'If intValida <= 0 Then
            '    Me.lblMensaje.Text = String.Format(BE.Constantes.K_MSJ_INICIO_VALIDA, strLogin)
            '    Me.dvBloque.Style("display") = ""
            'Else
            '    Me.lblMensaje.Text = ""
            '    Me.dvBloque.Style("display") = "none"
            'End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    <WebMethod()> _
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
    Public Shared Function SalirOpcion(arg As String) As String

        Dim intCodigo As Integer

        intCodigo = CInt(arg)

        If intCodigo <> 0 Then

            Using objSeguridadServiceClient As New AccesoSeguridad.SeguridadServiceClient()

                objSeguridadServiceClient.RegistrarSalida(New AccesoSeguridad.RegistrarSalidaRequest With {.NCOUTIL = intCodigo})

            End Using

        End If

        Return String.Empty

    End Function


#End Region

End Class