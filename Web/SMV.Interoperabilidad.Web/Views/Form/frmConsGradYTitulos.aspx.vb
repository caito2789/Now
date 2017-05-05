﻿Imports System.IO
Imports Microsoft.Reporting.WebForms
Imports iTextSharp.text.pdf
Imports SMV.Interoperabilidad.BE
Imports SMV.Interoperabilidad.BL

Public Class frmConsGradYTitulos
    Inherits System.Web.UI.Page

#Region "Atributos"

    Public Property DatosMemoria() As SuneduBE
        Get
            If IsNothing(ViewState("_DatosMemoria_")) Then
                Return Nothing
            End If

            Return ViewState("_DatosMemoria_")
        End Get
        Set(ByVal value As SuneduBE)
            ViewState.Add("_DatosMemoria_", value)
        End Set
    End Property

    Public Property OrdenAsc1() As Boolean
        Get
            If IsNothing(ViewState("_OrdenAsc1_")) Then
                Return True
            End If
            Return ViewState("_OrdenAsc1_")
        End Get
        Set(ByVal value As Boolean)
            ViewState.Add("_OrdenAsc1_", value)
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

    Protected Sub gvDatos_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gvDatos.Sorting
        Try
            Dim objListRes As List(Of SuneduBE) = Nothing

            Me.OrdenAsc1 = Not Me.OrdenAsc1
            objListRes = ViewState("GrillaResultados")
            OrdenarGrilla(DirectCast(sender, GridView), objListRes, e.SortExpression, Me.OrdenAsc1)

        Catch ex As Exception
            LogErrores(ex.Message, ex.StackTrace)
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "Alerta", "alert('" & ex.Message & "');", True)
        End Try
    End Sub

    Protected Sub gvDatos_PreRender(sender As Object, e As EventArgs) Handles gvDatos.PreRender
        Try
            If Not Me.gvDatos.HeaderRow Is Nothing Then
                Me.gvDatos.HeaderRow.TableSection = TableRowSection.TableHeader
            End If
        Catch ex As Exception
            'ex.ToString()
        End Try
    End Sub

#End Region

#Region "Metodos"

    Private Sub LimpiarData()
        Try
            Dim objResultado As List(Of SuneduBE) = Nothing

            Me.txtDNI.Text = String.Empty

            'Grilla
            objResultado = New List(Of SuneduBE)
            Me.gvDatos.DataSource = objResultado
            Me.gvDatos.DataBind()
            ViewState("GrillaResultados") = objResultado

            Me.txtDescMensaje.Text = String.Empty
            Me.hdnConsulta.Value = Constantes.ConsultoSW.K_NO

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub LimpiarConsulta()
        Try
            Dim objResultado As List(Of SuneduBE) = Nothing

            'Grilla
            objResultado = New List(Of SuneduBE)
            Me.gvDatos.DataSource = objResultado
            Me.gvDatos.DataBind()
            ViewState("GrillaResultados") = objResultado

            Me.txtDescMensaje.Text = String.Empty
            Me.hdnConsulta.Value = Constantes.ConsultoSW.K_NO

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Sub ConsultarServicio()
        Dim objDatosMemoriaBE As SuneduBE = Nothing
        objDatosMemoriaBE = New SuneduBE

        Try
            Dim objRespCons As ServicioIntermedio.SuneduGradosTitulosResponse = Nothing
            Dim objListResultadoBE As List(Of ServicioIntermedio.SuneduGradoAcademicoBE) = Nothing
            Dim proxy As ServicioIntermedio.ServiceIOClient = Nothing
            Dim strDNI As String = String.Empty, strAbrTitulo As String = String.Empty, strApePaterno As String = String.Empty, _
                strApeMaterno As String = String.Empty, strNombres As String = String.Empty, strTipoDocumento As String = String.Empty, _
                strNroDocumento As String = String.Empty, strPais As String = String.Empty, strUniversidad As String = String.Empty, _
                strTitProfesional As String = String.Empty, strEspecialidad As String = String.Empty
            Dim objListResultadoFinal As List(Of SuneduBE) = Nothing
            Dim objItemBE As SuneduBE = Nothing            

            Me.LimpiarConsulta()

            strDNI = Me.txtDNI.Text.Trim()

            'Memoria
            objDatosMemoriaBE.DNI = strDNI

            Try
                proxy = New ServicioIntermedio.ServiceIOClient
                objRespCons = proxy.SuneduGradosTitulos(New ServicioIntermedio.SuneduGradosTitulosRequest With {.CODAPP = Constantes.K_COD_APPLI, _
                                                                                                                .CODUSER = FachadaSesion.Usuario.CodigoUsuario.Trim(), _
                                                                                                                .DNI = strDNI})
                If Not objRespCons Is Nothing Then
                    If objRespCons.Correcto And objRespCons.MensajeError = String.Empty Then
                        objListResultadoBE = objRespCons.Resultado
                        If Not objListResultadoBE Is Nothing Then

                            objListResultadoFinal = New List(Of SuneduBE)
                            For Each itemBE In objListResultadoBE

                                If itemBE.ApePaterno Is Nothing Then
                                    strApePaterno = ""
                                Else
                                    strApePaterno = itemBE.ApePaterno.Trim()
                                End If

                                If itemBE.ApeMaterno Is Nothing Then
                                    strApeMaterno = ""
                                Else
                                    strApeMaterno = itemBE.ApeMaterno.Trim()
                                End If

                                If itemBE.Nombres Is Nothing Then
                                    strNombres = ""
                                Else
                                    strNombres = itemBE.Nombres.Trim()
                                End If

                                If itemBE.Tipodocumento Is Nothing Then
                                    strTipoDocumento = ""
                                Else
                                    strTipoDocumento = itemBE.Tipodocumento.Trim()
                                End If

                                If itemBE.NumeroDocumento Is Nothing Then
                                    strNroDocumento = ""
                                Else
                                    strNroDocumento = itemBE.NumeroDocumento.Trim()
                                End If

                                If itemBE.Pais Is Nothing Then
                                    strPais = ""
                                Else
                                    strPais = itemBE.Pais.Trim()
                                End If

                                If itemBE.Universidad Is Nothing Then
                                    strUniversidad = ""
                                Else
                                    strUniversidad = itemBE.Universidad.Trim()
                                End If

                                If itemBE.TituloProfesional Is Nothing Then
                                    strTitProfesional = ""
                                Else
                                    strTitProfesional = itemBE.TituloProfesional.Trim()
                                End If

                                If itemBE.AbreviaturaTitulo Is Nothing Then
                                    strAbrTitulo = ""
                                Else
                                    strAbrTitulo = itemBE.AbreviaturaTitulo.Trim()
                                End If

                                If itemBE.Especialidad Is Nothing Then
                                    strEspecialidad = ""
                                Else
                                    strEspecialidad = itemBE.Especialidad.Trim()
                                End If

                                objItemBE = New SuneduBE                                
                                objItemBE.ApePaterno = strApePaterno
                                objItemBE.ApeMaterno = strApeMaterno
                                objItemBE.Nombres = strNombres
                                objItemBE.TipoDocumento = strTipoDocumento
                                objItemBE.NroDocumento = strNroDocumento
                                objItemBE.Pais = strPais
                                objItemBE.Universidad = strUniversidad
                                objItemBE.TitProfesional = strTitProfesional
                                objItemBE.AbrTitulo = strAbrTitulo
                                objItemBE.Especialidad = strEspecialidad
                                objListResultadoFinal.Add(objItemBE)
                            Next

                            Me.gvDatos.DataSource = objListResultadoFinal
                            Me.gvDatos.DataBind()
                            ViewState("GrillaResultados") = objListResultadoFinal

                            'Memoria
                            objDatosMemoriaBE.objLista = objListResultadoFinal
                            objDatosMemoriaBE.NCODRESULTADO = Constantes.Cod_Resultado.K_OK
                            objDatosMemoriaBE.VMENSAJE_RESULTADO = String.Empty

                            Me.dvFormOK.Style("display") = "inline"
                            Me.dvFormNOK.Style("display") = "none"
                        Else
                            'Memoria
                            objDatosMemoriaBE.NCODRESULTADO = Constantes.Cod_Resultado.K_Error
                            objDatosMemoriaBE.VMENSAJE_RESULTADO = "El resultado fue nulo."

                            Me.txtDescMensaje.Text = "El resultado fue nulo."

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
            Me.hdnConsulta.Value = Constantes.ConsultoSW.K_SI

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Sub RegistrarLog(ByRef intCodLog As Integer)
        Try
            Dim strMsjResultado As String = String.Empty, strDNI As String = String.Empty
            Dim objLista As List(Of SuneduBE) = Nothing
            Dim objRegistroBE As SuneduBE = Nothing, objDatosMemoria As SuneduBE = Nothing
            Dim intResultado As Integer = 0, intCodResultado As Integer = 0
            Dim objSuneduBL As SuneduBL = Nothing

            objDatosMemoria = Me.DatosMemoria
            If Not objDatosMemoria Is Nothing Then

                intCodResultado = objDatosMemoria.NCODRESULTADO

                If objDatosMemoria.VMENSAJE_RESULTADO Is Nothing Then
                    strMsjResultado = ""
                Else
                    strMsjResultado = objDatosMemoria.VMENSAJE_RESULTADO
                End If

                If objDatosMemoria.DNI Is Nothing Then
                    strDNI = ""
                Else
                    strDNI = objDatosMemoria.DNI
                End If

                If objDatosMemoria.objLista Is Nothing Then
                    objLista = Nothing
                Else
                    objLista = objDatosMemoria.objLista
                End If
            End If

            objRegistroBE = New SuneduBE
            objRegistroBE.TipoServicio = Constantes.TipoServicio.K_SUNEDU_GRADOSYTIT
            objRegistroBE.VUSER = FachadaSesion.Usuario.CodigoUsuario.Trim()
            objRegistroBE.VCODAPLI = Constantes.K_COD_APPLI
            objRegistroBE.NCODRESULTADO = intCodResultado
            objRegistroBE.VMENSAJE_RESULTADO = strMsjResultado
            objRegistroBE.NCODACCION = Constantes.Accion.K_EXPORTAR
            objRegistroBE.DFECHA = Now
            objRegistroBE.DNI = strDNI
            objRegistroBE.objLista = objLista
            objSuneduBL = New SuneduBL()
            intResultado = objSuneduBL.RegistrarLog(objRegistroBE)

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
            If Not System.IO.Directory.Exists(strDirectorio) Then
                System.IO.Directory.CreateDirectory(strDirectorio)
            End If

            Me.DatosMemoria.RutaArchivoPDF = System.IO.Path.Combine(strDirectorio, "Reporte_SUNEDU_GradosYTitulos_" & strDia & strMes & Now.Year.ToString() & Now.Hour.ToString() & Now.Minute.ToString() & Now.Second.ToString() & Constantes.K_EXTENSION_PDF)

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
                strCodLog As String = String.Empty, strMensajeError As String = String.Empty, strDNI As String = String.Empty
            Dim rptReporte As Microsoft.Reporting.WebForms.ReportViewer
            Dim lstParametros As List(Of ReportParameter)
            Dim warnings() As Warning = Nothing
            Dim streams() As String = Nothing
            Dim renderedBytes() As Byte = Nothing
            Dim objLista As List(Of SuneduBE) = Nothing

            strFormat = Constantes.K_PDF
            strRutaReporte = Constantes.ConfiguracionReportePDF.K_URL_CARPETA_REPORTE

            If Me.DatosMemoria.NCODRESULTADO = Constantes.Cod_Resultado.K_OK Then
                strPath = System.IO.Path.Combine(Server.MapPath(strRutaReporte), Constantes.ConfiguracionReportePDF.K_RDLC_REPORTE_SUNEDU_OK)
            ElseIf Me.DatosMemoria.NCODRESULTADO = Constantes.Cod_Resultado.K_Error Then
                strPath = System.IO.Path.Combine(Server.MapPath(strRutaReporte), Constantes.ConfiguracionReportePDF.K_RDLC_REPORTE_SUNEDU_ERROR)
            End If

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

            'Resultado
            If Me.DatosMemoria.NCODRESULTADO = Constantes.Cod_Resultado.K_OK Then

                objLista = Me.DatosMemoria.objLista

            ElseIf Me.DatosMemoria.NCODRESULTADO = Constantes.Cod_Resultado.K_Error Then

                strMensajeError = Me.DatosMemoria.VMENSAJE_RESULTADO

            End If

            rptReporte = New Microsoft.Reporting.WebForms.ReportViewer()
            rptReporte.LocalReport.ReportPath = strPath

            rptReporte.LocalReport.DataSources.Clear()

            'Resultados
            If Me.DatosMemoria.NCODRESULTADO = Constantes.Cod_Resultado.K_OK Then

                rptReporte.LocalReport.DataSources.Add(New ReportDataSource("DSGrilla", objLista))

            End If

            lstParametros = New List(Of ReportParameter)
            lstParametros.Add(New ReportParameter("pTitulo", strTituloPDF))
            lstParametros.Add(New ReportParameter("pFuente", strFuente))
            lstParametros.Add(New ReportParameter("pEntidad", strEntidad))
            lstParametros.Add(New ReportParameter("pFechaHoraConsulta", strFechaHoraConsulta))
            lstParametros.Add(New ReportParameter("pCodLog", strCodLog))

            'Filtros
            lstParametros.Add(New ReportParameter("pDNI", strDNI))

            'Resultados
            If Me.DatosMemoria.NCODRESULTADO = Constantes.Cod_Resultado.K_Error Then

                lstParametros.Add(New ReportParameter("pMensajeError", strMensajeError))

            End If

            rptReporte.LocalReport.SetParameters(lstParametros)
            rptReporte.LocalReport.Refresh()

            renderedBytes = rptReporte.LocalReport.Render(strFormat, deviceInfo, strMimeType, strEncoding, strFileNameExtension, streams, warnings)

            strRutaArchivo = Me.DatosMemoria.RutaArchivoPDF
            Using fs As New System.IO.FileStream(strRutaArchivo, System.IO.FileMode.Create)
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

            If Me.DatosMemoria.NCODRESULTADO = Constantes.Cod_Resultado.K_OK Then

                doc = New iTextSharp.text.Document(iTextSharp.text.PageSize.A4.Rotate())   'Landscape

            ElseIf Me.DatosMemoria.NCODRESULTADO = Constantes.Cod_Resultado.K_Error Then

                doc = New iTextSharp.text.Document(iTextSharp.text.PageSize.A4)

            End If

            writer = PdfAWriter.GetInstance(doc, New FileStream(strFilePdfA, FileMode.Create), PdfAConformanceLevel.PDF_A_1A)
            doc.Open()

            Using resizeReader As PdfReader = New PdfReader(strFileName)

                cb = writer.DirectContent

                For pageNumber = 1 To resizeReader.NumberOfPages

                    If Me.DatosMemoria.NCODRESULTADO = Constantes.Cod_Resultado.K_OK Then

                        doc.SetPageSize(iTextSharp.text.PageSize.A4.Rotate())  'Landscape

                    ElseIf Me.DatosMemoria.NCODRESULTADO = Constantes.Cod_Resultado.K_Error Then

                        doc.SetPageSize(iTextSharp.text.PageSize.A4)

                    End If

                    doc.NewPage()

                    Dim page As PdfImportedPage = writer.GetImportedPage(resizeReader, pageNumber)
                    cb.AddTemplate(page, 0, 0)

                Next

                writer.CreateXmpMetadata()
                doc.Close()
            End Using

            System.IO.File.Copy(strFilePdfA, strFileName, True)
            System.IO.File.Delete(strFilePdfA)

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

            If Not System.IO.Directory.Exists(strRutaFirmados) Then
                System.IO.Directory.CreateDirectory(strRutaFirmados)
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