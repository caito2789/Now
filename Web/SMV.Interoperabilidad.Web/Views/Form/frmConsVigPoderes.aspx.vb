Imports System.IO
Imports Microsoft.Reporting.WebForms
Imports iTextSharp.text.pdf
Imports SMV.Interoperabilidad.BE
Imports SMV.Interoperabilidad.BL

Public Class frmConsVigPoderes
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

    Protected Sub ddlZona_SelectedIndexChanged()
        Try
            Dim objFiltrosBE As SunarpBE = Nothing
            Dim objOficinasBL As SunarpBL = Nothing
            Dim objListaOficinas As List(Of SunarpBE) = Nothing
            Dim strCodZona As String = String.Empty

            strCodZona = Me.ddlZona.SelectedValue

            If (strCodZona = "0") Then
                objListaOficinas = New List(Of SunarpBE)

                Me.ddlOficina.Items.Clear()
                Me.ddlOficina.DataSource = objListaOficinas
            Else
                objFiltrosBE = New SunarpBE
                objFiltrosBE.NombreTabla = Constantes.K_NOM_TABLA_OFICINA
                objFiltrosBE.CodigoCampo1 = String.Empty
                objFiltrosBE.CodigoCampo2 = strCodZona
                objOficinasBL = New SunarpBL
                objListaOficinas = objOficinasBL.ObtenerListaOficinas(objFiltrosBE)

                Me.ddlOficina.Items.Clear()
                Me.ddlOficina.DataSource = objListaOficinas
                Me.ddlOficina.DataTextField = "DescripcionValor"
                Me.ddlOficina.DataValueField = "Valor1"
            End If

            Me.ddlOficina.DataBind()
            Me.ddlOficina.Items.Insert(0, "[ SELECCIONE ]")
            Me.ddlOficina.Items(0).Value = "0"

        Catch ex As Exception
            LogErrores(ex.Message, ex.StackTrace)
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "Alerta", "alert('" & ex.Message & "');", True)
        End Try
    End Sub

#End Region

#Region "Métodos"

    Private Sub LimpiarData()
        Try
            Dim objListaZonas As List(Of SunarpBE) = Nothing, objListaOficinas As List(Of SunarpBE) = Nothing
            Dim objFiltrosBE As SunarpBE = Nothing
            Dim objZonasBL As SunarpBL = Nothing

            'Zonas
            objFiltrosBE = New SunarpBE
            objFiltrosBE.NombreTabla = Constantes.K_NOM_TABLA_ZONA
            objZonasBL = New SunarpBL
            objListaZonas = objZonasBL.ObtenerListaZonas(objFiltrosBE)

            Me.ddlZona.Items.Clear()
            Me.ddlZona.DataSource = objListaZonas
            Me.ddlZona.DataTextField = "DescripcionValor"
            Me.ddlZona.DataValueField = "Valor1"
            Me.ddlZona.DataBind()
            Me.ddlZona.Items.Insert(0, "[ SELECCIONE ]")
            Me.ddlZona.Items(0).Value = "0"

            'Oficinas
            objListaOficinas = New List(Of SunarpBE)
            Me.ddlOficina.Items.Clear()
            Me.ddlOficina.DataSource = objListaOficinas
            Me.ddlOficina.DataBind()
            Me.ddlOficina.Items.Insert(0, "[ SELECCIONE ]")
            Me.ddlOficina.Items(0).Value = "0"

            Me.txtPartida.Text = String.Empty
            Me.txtAsiento.Text = String.Empty
            Me.txtApePaterno.Text = String.Empty
            Me.txtApeMaterno.Text = String.Empty
            Me.txtNombres.Text = String.Empty
            Me.txtCargo.Text = String.Empty
            Me.txtEmail.Text = String.Empty

            Me.txtEstado.Text = String.Empty
            Me.txtSolicitud.Text = String.Empty
            Me.txtFecha.Text = String.Empty

            Me.txtDescMensaje.Text = String.Empty
            Me.hdnConsulta.Value = Constantes.ConsultoSW.K_NO

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub LimpiarConsulta()
        Try
            Me.txtEstado.Text = String.Empty
            Me.txtSolicitud.Text = String.Empty
            Me.txtFecha.Text = String.Empty

            Me.txtDescMensaje.Text = String.Empty
            Me.hdnConsulta.Value = Constantes.ConsultoSW.K_NO

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Sub ConsultarServicio()
        Dim objDatosMemoriaBE As SunarpBE = Nothing
        objDatosMemoriaBE = New SunarpBE

        Try
            Dim objRespCons As ServicioIntermedio.SunarpVigenciaPoderResponse = Nothing
            Dim objResultadoBE As ServicioIntermedio.SunarpVigenciaPoderBE = Nothing
            Dim proxy As ServicioIntermedio.ServiceIOClient = Nothing
            Dim strZona As String = String.Empty, strOficina As String = String.Empty, strPartida As String = String.Empty, _
                strAsiento As String = String.Empty, strApePaterno As String = String.Empty, strApeMaterno As String = String.Empty, _
                strNombres As String = String.Empty, strCargo As String = String.Empty, strEmail As String = String.Empty, _
                strZonaF As String = String.Empty, strOficinaF As String = String.Empty

            Me.LimpiarConsulta()

            strZona = Me.ddlZona.SelectedValue.Trim()
            strOficina = Me.ddlOficina.SelectedValue.Trim()
            strPartida = Me.txtPartida.Text.Trim()
            strAsiento = Me.txtAsiento.Text.Trim()
            strApePaterno = Me.txtApePaterno.Text.Trim()
            strApeMaterno = Me.txtApeMaterno.Text.Trim()
            strNombres = Me.txtNombres.Text.Trim()
            strCargo = Me.txtCargo.Text.Trim()
            strEmail = Me.txtEmail.Text.Trim()

            strZonaF = Me.ddlZona.Items(Me.ddlZona.SelectedIndex).Text.Trim()
            strOficinaF = Me.ddlOficina.Items(Me.ddlOficina.SelectedIndex).Text.Trim()

            'Memoria
            objDatosMemoriaBE.Zona = strZona
            objDatosMemoriaBE.Oficina = strOficina
            objDatosMemoriaBE.NumPartida = strPartida
            objDatosMemoriaBE.NumAsiento = strAsiento
            objDatosMemoriaBE.ApePaterno = strApePaterno
            objDatosMemoriaBE.ApeMaterno = strApeMaterno
            objDatosMemoriaBE.Nombres = strNombres
            objDatosMemoriaBE.Cargo = strCargo
            objDatosMemoriaBE.Email = strEmail
            objDatosMemoriaBE.ZonaTXT = strZonaF
            objDatosMemoriaBE.OficinaTXT = strOficinaF

            Try
                proxy = New ServicioIntermedio.ServiceIOClient
                objRespCons = proxy.SunarpVigenciaPoder(New ServicioIntermedio.SunarpVigenciaPoderRequest With {.CODAPP = Constantes.K_COD_APPLI, _
                                                                                                                .CODUSER = FachadaSesion.Usuario.CodigoUsuario.Trim(), _
                                                                                                                .ApellidoPaterno = strApePaterno, _
                                                                                                                .ApellidoMaterno = strApeMaterno, _
                                                                                                                .Nombre = strNombres, _
                                                                                                                .Zona = strZona, _
                                                                                                                .Oficina = strOficina, _
                                                                                                                .Partida = strPartida, _
                                                                                                                .Asiento = strAsiento, _
                                                                                                                .Cargo = strCargo, _
                                                                                                                .Email = strEmail})
                If Not objRespCons Is Nothing Then
                    If objRespCons.Correcto And objRespCons.MensajeError = String.Empty Then
                        objResultadoBE = objRespCons.Resultado
                        If Not objResultadoBE Is Nothing Then

                            Me.txtEstado.Text = objResultadoBE.Estado.Trim()
                            Me.txtSolicitud.Text = objResultadoBE.Solicitud.Trim()
                            Me.txtFecha.Text = objResultadoBE.Fecha.Trim()

                            'Memoria
                            objDatosMemoriaBE.Estado = objResultadoBE.Estado.Trim()
                            objDatosMemoriaBE.Solicitud = objResultadoBE.Solicitud.Trim()
                            objDatosMemoriaBE.Fecha = objResultadoBE.Fecha.Trim()
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
            Dim strMsjResultado As String = String.Empty, strApePaterno As String = String.Empty, strApeMaterno As String = String.Empty, _
                strNombres As String = String.Empty, strZona As String = String.Empty, strOficina As String = String.Empty, _
                strPartida As String = String.Empty, strAsiento As String = String.Empty, strCargo As String = String.Empty, _
                strEmail As String = String.Empty, strEstado As String = String.Empty, strSolicitud As String = String.Empty, _
                strFecha As String = String.Empty
            Dim objRegistroBE As SunarpBE = Nothing, objDatosMemoria As SunarpBE = Nothing
            Dim intResultado As Integer = 0, intCodResultado As Integer = 0
            Dim objSunarpBL As SunarpBL = Nothing

            objDatosMemoria = Me.DatosMemoria
            If Not objDatosMemoria Is Nothing Then

                intCodResultado = objDatosMemoria.NCODRESULTADO

                If objDatosMemoria.VMENSAJE_RESULTADO Is Nothing Then
                    strMsjResultado = ""
                Else
                    strMsjResultado = objDatosMemoria.VMENSAJE_RESULTADO
                End If

                If objDatosMemoria.Zona Is Nothing Then
                    strZona = ""
                Else
                    strZona = objDatosMemoria.Zona
                End If

                If objDatosMemoria.Oficina Is Nothing Then
                    strOficina = ""
                Else
                    strOficina = objDatosMemoria.Oficina
                End If

                If objDatosMemoria.NumPartida Is Nothing Then
                    strPartida = ""
                Else
                    strPartida = objDatosMemoria.NumPartida
                End If

                If objDatosMemoria.NumAsiento Is Nothing Then
                    strAsiento = ""
                Else
                    strAsiento = objDatosMemoria.NumAsiento
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

                If objDatosMemoria.Cargo Is Nothing Then
                    strCargo = ""
                Else
                    strCargo = objDatosMemoria.Cargo
                End If

                If objDatosMemoria.Email Is Nothing Then
                    strEmail = ""
                Else
                    strEmail = objDatosMemoria.Email
                End If

                If objDatosMemoria.Estado Is Nothing Then
                    strEstado = ""
                Else
                    strEstado = objDatosMemoria.Estado
                End If

                If objDatosMemoria.Solicitud Is Nothing Then
                    strSolicitud = ""
                Else
                    strSolicitud = objDatosMemoria.Solicitud
                End If

                If objDatosMemoria.Fecha Is Nothing Then
                    strFecha = ""
                Else
                    strFecha = objDatosMemoria.Fecha
                End If
            End If

            objRegistroBE = New SunarpBE
            objRegistroBE.TipoServicio = Constantes.TipoServicio.K_SUNARP_VIG_PODER
            objRegistroBE.VUSER = FachadaSesion.Usuario.CodigoUsuario.Trim()
            objRegistroBE.VCODAPLI = Constantes.K_COD_APPLI
            objRegistroBE.NCODRESULTADO = intCodResultado
            objRegistroBE.VMENSAJE_RESULTADO = strMsjResultado
            objRegistroBE.NCODACCION = Constantes.Accion.K_EXPORTAR
            objRegistroBE.DFECHA = Now
            objRegistroBE.Zona = strZona
            objRegistroBE.Oficina = strOficina
            objRegistroBE.NumPartida = strPartida
            objRegistroBE.NumAsiento = strAsiento
            objRegistroBE.ApePaterno = strApePaterno
            objRegistroBE.ApeMaterno = strApeMaterno
            objRegistroBE.Nombres = strNombres
            objRegistroBE.Cargo = strCargo
            objRegistroBE.Email = strEmail
            objRegistroBE.Estado = strEstado
            objRegistroBE.Solicitud = strSolicitud
            objRegistroBE.Fecha = strFecha
            objSunarpBL = New SunarpBL()
            intResultado = objSunarpBL.RegistrarLog_VP(objRegistroBE)

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

            Me.DatosMemoria.RutaArchivoPDF = System.IO.Path.Combine(strDirectorio, "Reporte_SUNARP_VigenciaPoder_" & strDia & strMes & Now.Year.ToString() & Now.Hour.ToString() & Now.Minute.ToString() & Now.Second.ToString() & Constantes.K_EXTENSION_PDF)

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
                strCodLog As String = String.Empty, strMensajeError As String = String.Empty, strZona As String = String.Empty, _
                strApePaterno As String = String.Empty, strApeMaterno As String = String.Empty, strNombres As String = String.Empty, _
                strOficina As String = String.Empty, strPartida As String = String.Empty, strAsiento As String = String.Empty, _
                strCargo As String = String.Empty, strEmail As String = String.Empty, strEstado As String = String.Empty, _
                strSolicitud As String = String.Empty, strFecha As String = String.Empty
            Dim rptReporte As Microsoft.Reporting.WebForms.ReportViewer
            Dim lstParametros As List(Of ReportParameter)
            Dim warnings() As Warning = Nothing
            Dim streams() As String = Nothing
            Dim renderedBytes() As Byte = Nothing

            strFormat = Constantes.K_PDF
            strRutaReporte = Constantes.ConfiguracionReportePDF.K_URL_CARPETA_REPORTE

            If Me.DatosMemoria.NCODRESULTADO = Constantes.Cod_Resultado.K_OK Then
                strPath = System.IO.Path.Combine(Server.MapPath(strRutaReporte), Constantes.ConfiguracionReportePDF.K_RDLC_REPORTE_SUNARP_VP_OK)
            ElseIf Me.DatosMemoria.NCODRESULTADO = Constantes.Cod_Resultado.K_Error Then
                strPath = System.IO.Path.Combine(Server.MapPath(strRutaReporte), Constantes.ConfiguracionReportePDF.K_RDLC_REPORTE_SUNARP_VP_ERROR)
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
            strZona = Me.DatosMemoria.ZonaTXT & " (" & Me.DatosMemoria.Zona & ")"
            strOficina = Me.DatosMemoria.OficinaTXT & "(" & Me.DatosMemoria.Oficina & ")"
            strPartida = Me.DatosMemoria.NumPartida
            strAsiento = Me.DatosMemoria.NumAsiento
            strApePaterno = Me.DatosMemoria.ApePaterno
            strApeMaterno = Me.DatosMemoria.ApeMaterno
            strNombres = Me.DatosMemoria.Nombres
            strCargo = Me.DatosMemoria.Cargo
            strEmail = Me.DatosMemoria.Email

            'Resultado
            If Me.DatosMemoria.NCODRESULTADO = Constantes.Cod_Resultado.K_OK Then

                strEstado = Me.DatosMemoria.Estado
                strSolicitud = Me.DatosMemoria.Solicitud
                strFecha = Me.DatosMemoria.Fecha

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
            lstParametros.Add(New ReportParameter("pZona", strZona))
            lstParametros.Add(New ReportParameter("pOficina", strOficina))
            lstParametros.Add(New ReportParameter("pPartida", strPartida))
            lstParametros.Add(New ReportParameter("pAsiento", strAsiento))
            lstParametros.Add(New ReportParameter("pApePaterno", strApePaterno))
            lstParametros.Add(New ReportParameter("pApeMaterno", strApeMaterno))
            lstParametros.Add(New ReportParameter("pNombres", strNombres))
            lstParametros.Add(New ReportParameter("pCargo", strCargo))
            lstParametros.Add(New ReportParameter("pEmail", strEmail))

            'Resultados
            If Me.DatosMemoria.NCODRESULTADO = Constantes.Cod_Resultado.K_OK Then

                lstParametros.Add(New ReportParameter("pEstado", strEstado))
                lstParametros.Add(New ReportParameter("pSolicitud", strSolicitud))
                lstParametros.Add(New ReportParameter("pFecha", strFecha))

            ElseIf Me.DatosMemoria.NCODRESULTADO = Constantes.Cod_Resultado.K_Error Then

                lstParametros.Add(New ReportParameter("pMensajeError", strMensajeError))

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
