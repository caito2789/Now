Imports System.IO
Imports SMV.Interoperabilidad.BE
Imports SMV.Interoperabilidad.BL

Public Class frmVerPDF
    Inherits System.Web.UI.Page

#Region "Formulario"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Me.CargarDatos()
            End If
        Catch ex As Exception
            LogErrores(ex.Message, ex.StackTrace)
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "Alerta", "alert('" & ex.Message & "');", True)
        End Try
    End Sub

#End Region

#Region "Métodos"

    Private Sub LimpiarData()
        Try
            Me.ltVerPDF.Text = String.Empty

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CargarDatos()
        Try
            Dim strRutaWebPDF As String = String.Empty, strVer As String = String.Empty

            Me.LimpiarData()

            strRutaWebPDF = Request.QueryString("xRt").Trim().ToString()

            'strVer = "<a class=""media"" style='width:100%' href=""" & strRutaWebPDF & """></a>"
            strVer = "<iframe style='width:100%;height:620px' src=""" & strRutaWebPDF & """></iframe>"
            Me.ltVerPDF.Text = strVer

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

End Class