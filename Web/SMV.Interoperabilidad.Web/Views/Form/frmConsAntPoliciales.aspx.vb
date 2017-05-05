Imports System.IO
Imports Microsoft.Reporting.WebForms
Imports iTextSharp.text.pdf
Imports SMV.Interoperabilidad.BE
Imports SMV.Interoperabilidad.BL

Public Class frmConsAntPoliciales
    Inherits System.Web.UI.Page

#Region "Atributos"

    Public Property DatosMemoria() As MininterBE
        Get
            If IsNothing(ViewState("_DatosMemoria_")) Then
                Return Nothing
            End If

            Return ViewState("_DatosMemoria_")
        End Get
        Set(ByVal value As MininterBE)
            ViewState.Add("_DatosMemoria_", value)
        End Set
    End Property

#End Region

#Region "Formulario"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Me.LimpiarData()
            End If
        Catch ex As Exception
            LogErrores(ex.Message, ex.StackTrace)
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "Alerta", "alert('" & ex.Message & "');", True)
        End Try
    End Sub

    Protected Sub btnConsultar_Click(sender As Object, e As EventArgs) Handles btnConsultar.Click
        Try
            Me.ConsultarServicio()
        Catch ex As Exception
            LogErrores(ex.Message, ex.StackTrace)
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "Alerta", "alert('" & ex.Message & "');", True)
        End Try
    End Sub

    Protected Sub btnExportarPDF_Click(sender As Object, e As EventArgs) Handles btnExportarPDF.Click
        Try
            Dim intCodLog As Integer = 0

            Me.RegistrarLog(intCodLog)
            If (intCodLog > 0) Then
                Me.ExportarPDF()
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Alerta", "alert('No se pudo exportar. No se registro correctamente el Log.');", True)
            End If

        Catch ex As Exception
            LogErrores(ex.Message, ex.StackTrace)
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "Alerta", "alert('" & ex.Message & "');", True)
        End Try
    End Sub

    Protected Sub rbTipoConsulta_CheckedChanged()
        Try
            Dim blPorNombres As Boolean = False, blPorDNI As Boolean = False

            blPorNombres = Me.rbNombres.Checked
            blPorDNI = Me.rbDNI.Checked

            If blPorNombres = True Then

                Me.dvTipoNombres.Style("display") = "inline"
                Me.dvTipoDNI.Style("display") = "none"

                Me.txtDNI.Text = String.Empty

            ElseIf blPorDNI = True Then

                Me.dvTipoNombres.Style("display") = "none"
                Me.dvTipoDNI.Style("display") = "inline"

                Me.txtApePaterno.Text = String.Empty
                Me.txtApeMaterno.Text = String.Empty
                Me.txtNombres.Text = String.Empty

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
            Me.rbNombres.Checked = True
            Me.rbDNI.Checked = False
            Me.dvTipoNombres.Style("display") = "inline"
            Me.dvTipoDNI.Style("display") = "none"

            Me.txtDNI.Text = String.Empty
            Me.txtApePaterno.Text = String.Empty
            Me.txtApeMaterno.Text = String.Empty
            Me.txtNombres.Text = String.Empty

            Me.txtDescMensaje.Text = String.Empty
            Me.hdnConsulta.Value = Constantes.ConsultoSW.K_NO

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub LimpiarConsulta()
        Try
            Me.txtDescMensaje.Text = String.Empty
            Me.hdnConsulta.Value = Constantes.ConsultoSW.K_NO

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Sub ConsultarServicio()
        Dim objDatosMemoriaBE As MininterBE = Nothing
        objDatosMemoriaBE = New MininterBE

        Try
            Dim objRespCons As ServicioIntermedio.MininterAntecedentesPolicialesResponse = Nothing
            Dim strResultado As String = String.Empty
            Dim proxy As ServicioIntermedio.ServiceIOClient = Nothing
            Dim strDNI As String = String.Empty, strApePaterno As String = String.Empty, strApeMaterno As String = String.Empty, _
                strNombres As String = String.Empty
            Dim blPorNombres As Boolean = False, blPorDNI As Boolean = False
            Dim intTipoConsulta As Integer = 0

            Me.LimpiarConsulta()

            blPorNombres = Me.rbNombres.Checked
            blPorDNI = Me.rbDNI.Checked
            strDNI = Me.txtDNI.Text.Trim()
            strApePaterno = Me.txtApePaterno.Text.Trim().ToUpper
            strApeMaterno = Me.txtApeMaterno.Text.Trim().ToUpper
            strNombres = Me.txtNombres.Text.Trim().ToUpper

            If blPorNombres = True Then
                intTipoConsulta = Constantes.TipoConsulta_MININTER.K_POR_NOMBRES
            ElseIf blPorDNI = True Then
                intTipoConsulta = Constantes.TipoConsulta_MININTER.K_POR_DNI
            End If

            'Memoria
            objDatosMemoriaBE.TipoConsulta = intTipoConsulta
            objDatosMemoriaBE.DNI = strDNI
            objDatosMemoriaBE.ApePaterno = strApePaterno
            objDatosMemoriaBE.ApeMaterno = strApeMaterno
            objDatosMemoriaBE.Nombres = strNombres

            If blPorNombres = True Then

                Try
                    proxy = New ServicioIntermedio.ServiceIOClient
                    objRespCons = proxy.MininterAntecedentesPoliciales(New ServicioIntermedio.MininterAntecedentesPolicialesRequest With {.CODAPP = Constantes.K_COD_APPLI, _
                                                                                                                                          .CODUSER = FachadaSesion.Usuario.CodigoUsuario.Trim(), _
                                                                                                                                          .TipoConsulta = intTipoConsulta, _
                                                                                                                                          .ApellidoPaterno = strApePaterno, _
                                                                                                                                          .ApellidoMaterno = strApeMaterno, _
                                                                                                                                          .Nombres = strNombres})
                    If Not objRespCons Is Nothing Then
                        If objRespCons.Correcto And objRespCons.MensajeError = String.Empty Then
                            strResultado = objRespCons.Resultado
                            If Not strResultado Is Nothing Then

                                Me.txtDescMensaje.Text = strResultado

                                'Memoria
                                objDatosMemoriaBE.MensajeRespOK = strResultado
                                objDatosMemoriaBE.NCODRESULTADO = Constantes.Cod_Resultado.K_OK
                                objDatosMemoriaBE.VMENSAJE_RESULTADO = String.Empty
                            Else
                                Me.txtDescMensaje.Text = "El resultado fue nulo."

                                'Memoria
                                objDatosMemoriaBE.NCODRESULTADO = Constantes.Cod_Resultado.K_Error
                                objDatosMemoriaBE.VMENSAJE_RESULTADO = "El resultado fue nulo."
                            End If
                        Else
                            'Memoria
                            objDatosMemoriaBE.NCODRESULTADO = Constantes.Cod_Resultado.K_Error
                            objDatosMemoriaBE.VMENSAJE_RESULTADO = objRespCons.MensajeError.ToString()

                            Me.txtDescMensaje.Text = objRespCons.MensajeError.ToString()
                        End If
                    Else
                        'Memoria
                        objDatosMemoriaBE.NCODRESULTADO = Constantes.Cod_Resultado.K_Error
                        objDatosMemoriaBE.VMENSAJE_RESULTADO = "El servicio regreso vacío."

                        Me.txtDescMensaje.Text = "El servicio regreso vacío."
                    End If
                Catch ex As Exception
                    'Memoria
                    objDatosMemoriaBE.NCODRESULTADO = Constantes.Cod_Resultado.K_Error
                    objDatosMemoriaBE.VMENSAJE_RESULTADO = ex.Message.ToString()

                    Me.txtDescMensaje.Text = ex.Message.ToString()
                End Try

            ElseIf blPorDNI = True Then

                Try
                    proxy = New ServicioIntermedio.ServiceIOClient
                    objRespCons = proxy.MininterAntecedentesPoliciales(New ServicioIntermedio.MininterAntecedentesPolicialesRequest With {.CODAPP = Constantes.K_COD_APPLI, _
                                                                                                                                          .CODUSER = FachadaSesion.Usuario.CodigoUsuario.Trim(), _
                                                                                                                                          .TipoConsulta = intTipoConsulta, _
                                                                                                                                          .DNI = strDNI})
                    If Not objRespCons Is Nothing Then
                        If objRespCons.Correcto And objRespCons.MensajeError = String.Empty Then
                            strResultado = objRespCons.Resultado
                            If Not strResultado Is Nothing Then

                                Me.txtDescMensaje.Text = strResultado

                                'Memoria
                                objDatosMemoriaBE.MensajeRespOK = strResultado
                                objDatosMemoriaBE.NCODRESULTADO = Constantes.Cod_Resultado.K_OK
                                objDatosMemoriaBE.VMENSAJE_RESULTADO = String.Empty
                            Else
                                Me.txtDescMensaje.Text = "El resultado fue nulo."

                                'Memoria
                                objDatosMemoriaBE.NCODRESULTADO = Constantes.Cod_Resultado.K_Error
                                objDatosMemoriaBE.VMENSAJE_RESULTADO = "El resultado fue nulo."
                            End If
                        Else
                            'Memoria
                            objDatosMemoriaBE.NCODRESULTADO = Constantes.Cod_Resultado.K_Error
                            objDatosMemoriaBE.VMENSAJE_RESULTADO = objRespCons.MensajeError.ToString()

                            Me.txtDescMensaje.Text = objRespCons.MensajeError.ToString()
                        End If
                    Else
                        'Memoria
                        objDatosMemoriaBE.NCODRESULTADO = Constantes.Cod_Resultado.K_Error
                        objDatosMemoriaBE.VMENSAJE_RESULTADO = "El servicio regreso vacío."

                        Me.txtDescMensaje.Text = "El servicio regreso vacío."
                    End If
                Catch ex As Exception
                    'Memoria
                    objDatosMemoriaBE.NCODRESULTADO = Constantes.Cod_Resultado.K_Error
                    objDatosMemoriaBE.VMENSAJE_RESULTADO = ex.Message.ToString()

                    Me.txtDescMensaje.Text = ex.Message.ToString()
                End Try

            End If

            Me.DatosMemoria = objDatosMemoriaBE
            Me.hdnConsulta.Value = Constantes.ConsultoSW.K_SI

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Sub RegistrarLog(ByRef intCodLog As Integer)
        Try
            Dim strMsjResultado As String = String.Empty, strApePaterno As String = String.Empty, strApeMaterno As String = String.Empty, _
                strNombres As String = String.Empty, strDNI As String = String.Empty, strMensajeRespOK As String = String.Empty
            Dim objRegistroBE As MininterBE = Nothing, objDatosMemoria As MininterBE = Nothing
            Dim intResultado As Integer = 0, intCodResultado As Integer = 0, intTipoConsulta As Integer = 0
            Dim objMininterBL As MininterBL = Nothing

            objDatosMemoria = Me.DatosMemoria
            If Not objDatosMemoria Is Nothing Then

                intCodResultado = objDatosMemoria.NCODRESULTADO

                If objDatosMemoria.VMENSAJE_RESULTADO Is Nothing Then
                    strMsjResultado = ""
                Else
                    strMsjResultado = objDatosMemoria.VMENSAJE_RESULTADO
                End If

                intTipoConsulta = objDatosMemoria.TipoConsulta

                If objDatosMemoria.DNI Is Nothing Then
                    strDNI = ""
                Else
                    strDNI = objDatosMemoria.DNI
                End If

                If objDatosMemoria.ApePaterno Is Nothing Then
                    strApePaterno = ""
                Else
                    strApePaterno = objDatosMemoria.ApePaterno
                End If

                If objDatosMemoria.ApeMaterno Is Nothing Then
                    strApeMaterno = ""
                Else
                    strApeMaterno = objDatosMemoria.ApeMaterno
                End If

                If objDatosMemoria.Nombres Is Nothing Then
                    strNombres = ""
                Else
                    strNombres = objDatosMemoria.Nombres
                End If

                If objDatosMemoria.MensajeRespOK Is Nothing Then
                    strMensajeRespOK = ""
                Else
                    strMensajeRespOK = objDatosMemoria.MensajeRespOK
                End If
            End If

            objRegistroBE = New MininterBE
            objRegistroBE.TipoServicio = Constantes.TipoServicio.K_MININTER_ANT_POLICIALES
            objRegistroBE.VUSER = FachadaSesion.Usuario.CodigoUsuario.Trim()
            objRegistroBE.VCODAPLI = Constantes.K_COD_APPLI
            objRegistroBE.NCODRESULTADO = intCodResultado
            objRegistroBE.VMENSAJE_RESULTADO = strMsjResultado
            objRegistroBE.NCODACCION = Constantes.Accion.K_EXPORTAR
            objRegistroBE.DFECHA = Now
            objRegistroBE.TipoConsulta = intTipoConsulta
            objRegistroBE.DNI = strDNI
            objRegistroBE.ApePaterno = strApePaterno
            objRegistroBE.ApeMaterno = strApeMaterno
            objRegistroBE.Nombres = strNombres
            objRegistroBE.MensajeRespOK = strMensajeRespOK
            objMininterBL = New MininterBL()
            intResultado = objMininterBL.RegistrarLog(objRegistroBE)

            intCodLog = objRegistroBE.CodLog
            Me.DatosMemoria.CodLog = intCodLog

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#Region "Exportar PDF"

    Protected Sub ExportarPDF()
        Try
            Dim strDirectorio As String = String.Empty, strMes As String = String.Empty, strDia As String = String.Empty
            Dim objFiltroBE As LogBE = Nothing
            Dim objReporteBL As LogBL = Nothing
            Dim objResultado As List(Of LogBE) = Nothing

            If (Now.Day < 10) Then
                strDia = "0" & Now.Day
            Else
                strDia = Now.Day
            End If
            If (Now.Month < 10) Then
                strMes = "0" & Now.Month
            Else
                strMes = Now.Month
            End If

            strDirectorio = Server.MapPath(Constantes.K_RUTA_CARPETA_GENERAR_PDF)
            If Not IO.Directory.Exists(strDirectorio) Then
                IO.Directory.CreateDirectory(strDirectorio)
            End If

            Me.DatosMemoria.RutaArchivoPDF = System.IO.Path.Combine(strDirectorio, "Reporte_MININTER_AntPoliciales_" & strDia & strMes & Now.Year.ToString() & Right("0" & Now.Hour.ToString(), 2) & Right("0" & Now.Minute.ToString(), 2) & Right("0" & Now.Second.ToString(), 2) & Constantes.K_EXTENSION_PDF)

            'Obtener datos cabecera
            objFiltroBE = New LogBE
            objFiltroBE.CodLog = Me.DatosMemoria.CodLog
            objReporteBL = New LogBL
            objResultado = objReporteBL.ObtenerCabeceraPDF(objFiltroBE)

            Me.GenerarArchivo(objResultado)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Sub GenerarArchivo(ByVal objResultado As List(Of LogBE))
        Try
            Dim strPath As String = String.Empty, strFormat As String = String.Empty, deviceInfo As String = Nothing, _
                strMimeType As String = String.Empty, strEncoding As String = String.Empty, strFileNameExtension As String = String.Empty, _
                strTituloPDF As String = String.Empty, strRutaArchivo As String = String.Empty, strRutaReporte As String = String.Empty
            Dim strFuente As String = String.Empty, strEntidad As String = String.Empty, strFechaHoraConsulta As String = String.Empty, _
                strCodLog As String = String.Empty, strMensajeError As String = String.Empty, strDNI As String = String.Empty, _
                strApePaterno As String = String.Empty, strApeMaterno As String = String.Empty, strNombres As String = String.Empty, _
                strMensajeOK As String = String.Empty
            Dim rptReporte As Microsoft.Reporting.WebForms.ReportViewer
            Dim lstParametros As List(Of ReportParameter)
            Dim warnings() As Warning = Nothing
            Dim streams() As String = Nothing
            Dim renderedBytes() As Byte = Nothing

            strFormat = Constantes.K_PDF
            strRutaReporte = Constantes.ConfiguracionReportePDF.K_URL_CARPETA_REPORTE

            strPath = System.IO.Path.Combine(Server.MapPath(strRutaReporte), Constantes.ConfiguracionReportePDF.K_RDLC_REPORTE_MININTER)

            'Datos cabecera
            If Not objResultado Is Nothing Then
                If objResultado.Count > 0 Then
                    If Not objResultado(0) Is Nothing Then
                        strTituloPDF = objResultado(0).TipoServicio
                        strFuente = objResultado(0).Fuente
                        strEntidad = objResultado(0).Entidad
                        strFechaHoraConsulta = objResultado(0).FechaRegistro
                    End If
                End If
            End If

            strCodLog = Me.DatosMemoria.CodLog

            'Filtros
            

            strDNI = Me.DatosMemoria.DNI
            strApePaterno = Me.DatosMemoria.ApePaterno
            strApeMaterno = Me.DatosMemoria.ApeMaterno
            strNombres = Me.DatosMemoria.Nombres

            'Resultado
            If Me.DatosMemoria.NCODRESULTADO = Constantes.Cod_Resultado.K_OK Then

                strMensajeOK = Me.DatosMemoria.MensajeRespOK

            ElseIf Me.DatosMemoria.NCODRESULTADO = Constantes.Cod_Resultado.K_Error Then

                strMensajeError = Me.DatosMemoria.VMENSAJE_RESULTADO

            End If

            rptReporte = New Microsoft.Reporting.WebForms.ReportViewer()
            rptReporte.LocalReport.ReportPath = strPath

            rptReporte.LocalReport.DataSources.Clear()

            lstParametros = New List(Of ReportParameter)
            lstParametros.Add(New ReportParameter("pTitulo", strTituloPDF))
            lstParametros.Add(New ReportParameter("pFuente", strFuente))
            lstParametros.Add(New ReportParameter("pEntidad", strEntidad))
            lstParametros.Add(New ReportParameter("pFechaHoraConsulta", strFechaHoraConsulta))
            lstParametros.Add(New ReportParameter("pCodLog", strCodLog))

            'Filtros
            lstParametros.Add(New ReportParameter("pDNI", strDNI))
            lstParametros.Add(New ReportParameter("pApePaterno", strApePaterno))
            lstParametros.Add(New ReportParameter("pApeMaterno", strApeMaterno))
            lstParametros.Add(New ReportParameter("pNombres", strNombres))

            'Resultados
            If Me.DatosMemoria.NCODRESULTADO = Constantes.Cod_Resultado.K_OK Then

                lstParametros.Add(New ReportParameter("pMensaje", strMensajeOK))

            ElseIf Me.DatosMemoria.NCODRESULTADO = Constantes.Cod_Resultado.K_Error Then

                lstParametros.Add(New ReportParameter("pMensaje", strMensajeError))

            End If

            rptReporte.LocalReport.SetParameters(lstParametros)
            rptReporte.LocalReport.Refresh()

            renderedBytes = rptReporte.LocalReport.Render(strFormat, deviceInfo, strMimeType, strEncoding, strFileNameExtension, streams, warnings)

            strRutaArchivo = Me.DatosMemoria.RutaArchivoPDF
            Using fs As New IO.FileStream(strRutaArchivo, IO.FileMode.Create)
                fs.Write(renderedBytes, 0, renderedBytes.Length)
                fs.Close()
            End Using

            Me.RendererWebForm2PDFArchive()

            Me.FirmarPDF()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub RendererWebForm2PDFArchive()
        Try
            Dim strFilePdfA As String = String.Empty, strFileName As String = String.Empty
            Dim doc As iTextSharp.text.Document
            Dim writer As iTextSharp.text.pdf.PdfAWriter
            Dim cb As PdfContentByte

            strFileName = Me.DatosMemoria.RutaArchivoPDF
            strFilePdfA = strFileName & Constantes.K_EXTENSION_PDF

            doc = New iTextSharp.text.Document(iTextSharp.text.PageSize.A4)
            writer = PdfAWriter.GetInstance(doc, New FileStream(strFilePdfA, FileMode.Create), PdfAConformanceLevel.PDF_A_1A)
            doc.Open()

            Using resizeReader As PdfReader = New PdfReader(strFileName)

                cb = writer.DirectContent

                For pageNumber = 1 To resizeReader.NumberOfPages

                    doc.SetPageSize(iTextSharp.text.PageSize.A4)
                    doc.NewPage()

                    Dim page As PdfImportedPage = writer.GetImportedPage(resizeReader, pageNumber)
                    cb.AddTemplate(page, 0, 0)

                Next

                writer.CreateXmpMetadata()
                doc.Close()
            End Using

            IO.File.Copy(strFilePdfA, strFileName, True)
            IO.File.Delete(strFilePdfA)

        Catch ex As Exception
            Throw ex 'ex.ToString()
        End Try
    End Sub

    Public Sub FirmarPDF()
        Try
            Dim strNombreFirmante As String = String.Empty, strRutaFirmados As String = String.Empty, strRutaReporte As String = String.Empty, _
                strMsjErrorFirmar As String = String.Empty, strRutaWebFirmado As String = String.Empty, strNomArchivoPDF As String = String.Empty
            Dim objFirmaBL As FirmaDllBL = Nothing

            strRutaReporte = Me.DatosMemoria.RutaArchivoPDF
            strRutaFirmados = Server.MapPath(Constantes.K_RUTA_CARPETA_FIRMADOS_PDF)
            strNombreFirmante = String.Format("{0} {1} {2}", FachadaSesion.Usuario.NombreUsuario, FachadaSesion.Usuario.ApellidoPaterno, FachadaSesion.Usuario.ApellidoMaterno)

            If Not IO.Directory.Exists(strRutaFirmados) Then
                IO.Directory.CreateDirectory(strRutaFirmados)
            End If

            'Firmar PDF
            objFirmaBL = New FirmaDllBL
            objFirmaBL.FirmarDocumento(strRutaReporte, strRutaFirmados, strNombreFirmante)
            If objFirmaBL.blError = True Then
                strMsjErrorFirmar = objFirmaBL.strMensajeError.Replace("'", "")
                strMsjErrorFirmar = strMsjErrorFirmar.Replace(vbCrLf, " ")

                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Alerta", "alert('Ocurrio un problema en firmar el PDF. " & strMsjErrorFirmar & "');", True)
            Else
                strNomArchivoPDF = strRutaReporte.Substring(strRutaReporte.LastIndexOf("\") + 1)
                strRutaWebFirmado = Constantes.K_RUTAWEB_FIRMA_PDF.Trim() & "/" & strNomArchivoPDF

                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Alerta", "js_VerPDF('" & strRutaWebFirmado & "');", True)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#End Region

End Class
