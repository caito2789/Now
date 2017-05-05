Imports SMV.Interoperabilidad.BL
Imports SMV.Interoperabilidad.BE

Public Class frmLogin
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.ltNombreAplicacion.Text = BE.Constantes.K_TITULO_APP
    End Sub

    Protected Sub LoginButton_Click(sender As Object, e As EventArgs) Handles LoginButton.Click
        Try
            If Page.IsValid Then
                Me.FailureText.Text = String.Empty
                Try
                    If Me.ValidarUsuario(Me.txtUsuario.Text.Trim().ToUpper(), Me.txtPassword.Text.Trim().ToLower) Then
                        FormsAuthentication.RedirectFromLoginPage(Me.txtUsuario.Text.Trim(), Me.chkRecordar.Checked)
                    End If
                Catch ex As Exception
                    Me.FailureText.Text = ex.Message
                End Try
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Function ValidarUsuario(pstrUsuario As String, pstrPassword As String) As Boolean
        Dim blRespuesta As Boolean = False
        Try
            Using objUsuarioBL As New UsuarioBL
                Dim lobjUsuarioBE = objUsuarioBL.ValidarUsuario(New UsuarioBE With {.UsuarioLogon = pstrUsuario, _
                                                                                    .UsuarioPassword = pstrPassword, _
                                                                                    .TipoUsuario = ConfigurationManager.AppSettings("TipoUsuario").ToString() _
                                                                                    }, _
                                                                ConfigurationManager.AppSettings("CodigoAplicacion").ToString())

                lobjUsuarioBE.EstacionUsuario = Split(System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).HostName, ".")(0).ToUpper()
                FachadaSesion.Usuario = lobjUsuarioBE

            End Using
            blRespuesta = True
        Catch ex As Exception
            Throw ex
            blRespuesta = False
        End Try
        Return blRespuesta
    End Function

End Class