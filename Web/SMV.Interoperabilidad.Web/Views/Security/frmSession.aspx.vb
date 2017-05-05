Imports System.Web.Services
Imports System.Web.Script.Services

Public Class frmSession
    Inherits System.Web.UI.Page

#Region "Formulario"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    End Sub

#End Region

#Region "AJAX"

    <WebMethod()>
    Public Shared Function VerifySessionState() As String
        Dim strSession As String = "0"
        Try
            If IsNothing(FachadaSesion.Usuario) Then
                HttpContext.Current.Session.Abandon()
                HttpContext.Current.Session.RemoveAll()
                HttpContext.Current.Session.Clear()
                FormsAuthentication.SignOut()

                strSession = "1"
            End If
        Catch ex As Exception

        End Try
        Return strSession
    End Function

#End Region

End Class
