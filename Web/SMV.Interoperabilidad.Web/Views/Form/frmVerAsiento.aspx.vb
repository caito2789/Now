Imports System.IO
Imports SMV.Interoperabilidad.BE
Imports SMV.Interoperabilidad.BL

Public Class frmVerAsiento
    Inherits System.Web.UI.Page

#Region "Atributos"

    Public Property DatosMemoria() As SunarpBE
        Get
            If IsNothing(ViewState("_DatosMemoria_")) Then
                Return Nothing
            End If

            Return ViewState("_DatosMemoria_")
        End Get
        Set(ByVal value As SunarpBE)
            ViewState.Add("_DatosMemoria_", value)
        End Set
    End Property

#End Region

#Region "Formulario"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Me.CargarDatos()
                Me.RegistrarLog()
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
            Me.dvFormOK.Style("display") = "inline"
            Me.dvFormNOK.Style("display") = "none"

            Me.lblIDImagenAs.Text = String.Empty
            Me.imgVerAsiento.ImageUrl = String.Empty
            Me.txtDescMensaje.Text = String.Empty

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CargarDatos()
        Dim objDatosMemoriaBE As SunarpBE = Nothing
        objDatosMemoriaBE = New SunarpBE

        Try
            Dim objRespCons As ServicioIntermedio.SunarpVerImagenAsientoResponse = Nothing
            Dim proxy As ServicioIntermedio.ServiceIOClient = Nothing
            Dim strResultado As String = String.Empty, strTransaccion As String = String.Empty, strIDImgAsiento As String = String.Empty, _
                strTipo As String = String.Empty, strNroTotalPag As String = String.Empty, strNroPagRef As String = String.Empty, _
                strPag As String = String.Empty, strRutaWebFinal As String = String.Empty, strErrorArchivo As String = String.Empty
            Dim intResultadoArchivo As Integer = 0

            Me.LimpiarData()

            strTransaccion = Request.QueryString("xTran").ToString()
            strIDImgAsiento = Request.QueryString("xIDIm").ToString()
            strTipo = Request.QueryString("xTip").ToString()
            strNroTotalPag = Request.QueryString("xNTotPg").ToString()
            strNroPagRef = Request.QueryString("xNPgRef").ToString()
            strPag = Request.QueryString("xPg").ToString()

            Me.lblIDImagenAs.Text = strIDImgAsiento

            'Memoria
            objDatosMemoriaBE.Transaccion = strTransaccion
            objDatosMemoriaBE.NroTotalPag = strNroTotalPag
            objDatosMemoriaBE.IDImgAsiento = strIDImgAsiento
            objDatosMemoriaBE.Tipo = strTipo
            objDatosMemoriaBE.NroPagRef = strNroPagRef
            objDatosMemoriaBE.NroPag = strPag

            Try
                proxy = New ServicioIntermedio.ServiceIOClient
                objRespCons = proxy.SunarpVerImagenAsiento(New ServicioIntermedio.SunarpVerImagenAsientoRequest With {.CODAPP = Constantes.K_COD_APPLI, _
                                                                                                                      .CODUSER = FachadaSesion.Usuario.CodigoUsuario.Trim(), _
                                                                                                                      .Transaccion = strTransaccion, _
                                                                                                                      .IdImg = strIDImgAsiento, _
                                                                                                                      .Tipo = strTipo, _
                                                                                                                      .NroTotalPag = strNroTotalPag, _
                                                                                                                      .NroPagRef = strNroPagRef, _
                                                                                                                      .Pagina = strPag})
                If Not objRespCons Is Nothing Then
                    If objRespCons.Correcto And objRespCons.MensajeError = String.Empty Then
                        strResultado = objRespCons.Resultado
                        If Not strResultado Is Nothing Then

                            If strResultado.Trim() <> "" Then

                                'Memoria
                                objDatosMemoriaBE.RutaImagen = strResultado

                                Me.DescargarArchivo(strResultado, intResultadoArchivo, strRutaWebFinal, strErrorArchivo)
                                If intResultadoArchivo = Constantes.Cod_Resultado.K_OK Then

                                    Me.imgVerAsiento.ImageUrl = strRutaWebFinal

                                    objDatosMemoriaBE.NCODRESULTADO = Constantes.Cod_Resultado.K_OK
                                    objDatosMemoriaBE.VMENSAJE_RESULTADO = String.Empty

                                    Me.dvFormOK.Style("display") = "inline"
                                    Me.dvFormNOK.Style("display") = "none"

                                ElseIf intResultadoArchivo = Constantes.Cod_Resultado.K_Error Then

                                    objDatosMemoriaBE.NCODRESULTADO = Constantes.Cod_Resultado.K_Error
                                    objDatosMemoriaBE.VMENSAJE_RESULTADO = strErrorArchivo

                                    Me.dvFormOK.Style("display") = "none"
                                    Me.dvFormNOK.Style("display") = "inline"
                                End If
                            Else
                                Me.txtDescMensaje.Text = "El ruta del archivo esta vacia."

                                'Memoria
                                objDatosMemoriaBE.NCODRESULTADO = Constantes.Cod_Resultado.K_Error
                                objDatosMemoriaBE.VMENSAJE_RESULTADO = "El ruta del archivo esta vacia."

                                Me.dvFormOK.Style("display") = "none"
                                Me.dvFormNOK.Style("display") = "inline"
                            End If
                        Else
                            Me.txtDescMensaje.Text = "El resultado fue nulo."

                            'Memoria
                            objDatosMemoriaBE.NCODRESULTADO = Constantes.Cod_Resultado.K_Error
                            objDatosMemoriaBE.VMENSAJE_RESULTADO = "El resultado fue nulo."

                            Me.dvFormOK.Style("display") = "none"
                            Me.dvFormNOK.Style("display") = "inline"
                        End If
                    Else
                        'Memoria
                        objDatosMemoriaBE.NCODRESULTADO = Constantes.Cod_Resultado.K_Error
                        objDatosMemoriaBE.VMENSAJE_RESULTADO = objRespCons.MensajeError.ToString()

                        Me.txtDescMensaje.Text = objRespCons.MensajeError.ToString()

                        Me.dvFormOK.Style("display") = "none"
                        Me.dvFormNOK.Style("display") = "inline"
                    End If
                Else
                    'Memoria
                    objDatosMemoriaBE.NCODRESULTADO = Constantes.Cod_Resultado.K_Error
                    objDatosMemoriaBE.VMENSAJE_RESULTADO = "El servicio regreso vacío."

                    Me.txtDescMensaje.Text = "El servicio regreso vacío."

                    Me.dvFormOK.Style("display") = "none"
                    Me.dvFormNOK.Style("display") = "inline"
                End If
            Catch ex As Exception
                'Memoria
                objDatosMemoriaBE.NCODRESULTADO = Constantes.Cod_Resultado.K_Error
                objDatosMemoriaBE.VMENSAJE_RESULTADO = ex.Message.ToString()

                Me.txtDescMensaje.Text = ex.Message.ToString()

                Me.dvFormOK.Style("display") = "none"
                Me.dvFormNOK.Style("display") = "inline"
            End Try

            Me.DatosMemoria = objDatosMemoriaBE

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DescargarArchivo(ByVal strRutaImagenOri As String, ByRef intResultado As Integer, ByRef strRutaWebFinal As String, ByRef strError As String)
        intResultado = 0
        strRutaWebFinal = ""
        strError = ""
        Try
            Dim strNombreOriginal As String = String.Empty, rutaFileFinal As String = String.Empty, DirecArchivos As String = String.Empty, _
                rutaFileDestino As String = String.Empty

            strNombreOriginal = strRutaImagenOri.Substring(strRutaImagenOri.LastIndexOf("\") + 1)

            DirecArchivos = Server.MapPath(Constantes.K_RUTA_CARPETA_ARCHIVO_IMG)
            If Not IO.Directory.Exists(DirecArchivos) Then
                IO.Directory.CreateDirectory(DirecArchivos)
            End If

            rutaFileDestino = DirecArchivos & "/" & strNombreOriginal.Trim()
            If IO.File.Exists(rutaFileDestino) Then
                IO.File.Delete(rutaFileDestino)
            End If
            IO.File.Copy(strRutaImagenOri, rutaFileDestino, True)

            strRutaWebFinal = Constantes.K_RUTAWEB_IMGASIENTO & "/" & strNombreOriginal
            intResultado = Constantes.Cod_Resultado.K_OK

        Catch ex As Exception
            strError = ex.Message
            intResultado = Constantes.Cod_Resultado.K_Error
        End Try
    End Sub

    Protected Sub RegistrarLog()
        Try
            Dim strMsjResultado As String = String.Empty, strTransaccion As String = String.Empty, strIDImgAsiento As String = String.Empty, _
                strTipo As String = String.Empty, strNroTotalPag As String = String.Empty, strNroPagRef As String = String.Empty, _
                strNroPag As String = String.Empty, strRutaImagen As String = String.Empty
            Dim objRegistroBE As SunarpBE = Nothing, objDatosMemoria As SunarpBE = Nothing
            Dim intResultado As Integer = 0, intCodResultado As Integer = 0
            Dim objSunarpBL As SunarpBL = Nothing
            Dim intCodLog As Integer = 0

            objDatosMemoria = Me.DatosMemoria
            If Not objDatosMemoria Is Nothing Then

                intCodResultado = objDatosMemoria.NCODRESULTADO

                If objDatosMemoria.VMENSAJE_RESULTADO Is Nothing Then
                    strMsjResultado = ""
                Else
                    strMsjResultado = objDatosMemoria.VMENSAJE_RESULTADO
                End If

                If objDatosMemoria.Transaccion Is Nothing Then
                    strTransaccion = ""
                Else
                    strTransaccion = objDatosMemoria.Transaccion
                End If

                If objDatosMemoria.NroTotalPag Is Nothing Then
                    strNroTotalPag = ""
                Else
                    strNroTotalPag = objDatosMemoria.NroTotalPag
                End If

                If objDatosMemoria.IDImgAsiento Is Nothing Then
                    strIDImgAsiento = ""
                Else
                    strIDImgAsiento = objDatosMemoria.IDImgAsiento
                End If

                If objDatosMemoria.Tipo Is Nothing Then
                    strTipo = ""
                Else
                    strTipo = objDatosMemoria.Tipo
                End If

                If objDatosMemoria.NroPagRef Is Nothing Then
                    strNroPagRef = ""
                Else
                    strNroPagRef = objDatosMemoria.NroPagRef
                End If

                If objDatosMemoria.NroPag Is Nothing Then
                    strNroPag = ""
                Else
                    strNroPag = objDatosMemoria.NroPag
                End If

                If objDatosMemoria.RutaImagen Is Nothing Then
                    strRutaImagen = ""
                Else
                    strRutaImagen = objDatosMemoria.RutaImagen
                End If
            End If

            objRegistroBE = New SunarpBE
            objRegistroBE.TipoServicio = Constantes.TipoServicio.K_SUNARP_VER_ASIENTO
            objRegistroBE.VUSER = FachadaSesion.Usuario.CodigoUsuario.Trim()
            objRegistroBE.VCODAPLI = Constantes.K_COD_APPLI
            objRegistroBE.NCODRESULTADO = intCodResultado
            objRegistroBE.VMENSAJE_RESULTADO = strMsjResultado
            objRegistroBE.NCODACCION = Constantes.Accion.K_VER_IMAGEN
            objRegistroBE.DFECHA = Now
            objRegistroBE.Transaccion = strTransaccion
            objRegistroBE.NroTotalPag = strNroTotalPag
            objRegistroBE.IDImgAsiento = strIDImgAsiento
            objRegistroBE.Tipo = strTipo
            objRegistroBE.NroPagRef = strNroPagRef
            objRegistroBE.NroPag = strNroPag
            objRegistroBE.RutaImagen = strRutaImagen
            objSunarpBL = New SunarpBL()
            intResultado = objSunarpBL.RegistrarLog_VImg(objRegistroBE)

            intCodLog = objRegistroBE.CodLog

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

End Class
