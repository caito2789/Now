Imports System.IO
Imports Microsoft.Reporting.WebForms
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports SMV.Interoperabilidad.BE
Imports SMV.Interoperabilidad.BL

Public Class frmConsListAsientos
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
            Dim objListRes As List(Of SunarpBE) = Nothing

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

    Protected Sub gvDatos_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvDatos.RowDataBound
        Try
            Dim strTransaccion As String = String.Empty, strIDImgAsiento As String = String.Empty, strTipo As String = String.Empty, _
                strNroTotalPag As String = String.Empty, strNroPagRef As String = String.Empty, strPagina As String = String.Empty
            Dim objAsiento As SunarpBE = Nothing
            Dim ibtnVerAsiento As ImageButton

            If (e.Row.RowType = DataControlRowType.Header) Then

                e.Row.Cells(1).Visible = False         'Transaccion
                e.Row.Cells(2).Visible = False         'NroTotalPag

            End If

            If (e.Row.RowType = DataControlRowType.DataRow) Then

                objAsiento = CType(e.Row.DataItem, SunarpBE)
                If Not objAsiento Is Nothing Then
                    strTransaccion = objAsiento.Transaccion
                    strNroTotalPag = objAsiento.NroTotalPag
                    strIDImgAsiento = objAsiento.IDImgAsiento
                    strTipo = objAsiento.Tipo
                    strNroPagRef = objAsiento.NroPagRef
                    strPagina = objAsiento.NroPag

                    ibtnVerAsiento = e.Row.FindControl("btnVerAsiento")
                    ibtnVerAsiento.OnClientClick = "javascript:js_VerAsiento('" & strTransaccion & "', '" & strIDImgAsiento & "','" & strTipo & "','" & strNroTotalPag & "','" & strNroPagRef & "','" & strPagina & "');"
                End If

                e.Row.Cells(1).Visible = False         'Transaccion
                e.Row.Cells(2).Visible = False         'NroTotalPag

            End If
        Catch ex As Exception
            LogErrores(ex.Message, ex.StackTrace)
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "Alerta", "alert('" & ex.Message & "');", True)
        End Try
    End Sub

#End Region

#Region "Metodos"

    Private Sub LimpiarData()
        Try
            Dim objResultado As List(Of SunarpBE) = Nothing, objListaRegistros As List(Of SunarpBE) = Nothing
            Dim objFiltrosBE As SunarpBE = Nothing, objFiltrosZonaBE As SunarpBE = Nothing
            Dim objListaZonas As List(Of SunarpBE) = Nothing, objListaOficinas As List(Of SunarpBE) = Nothing
            Dim objRegistrosBL As SunarpBL = Nothing
            Dim objZonasBL As SunarpBL = Nothing

            'Zonas
            objFiltrosZonaBE = New SunarpBE
            objFiltrosZonaBE.NombreTabla = Constantes.K_NOM_TABLA_ZONA
            objZonasBL = New SunarpBL
            objListaZonas = objZonasBL.ObtenerListaZonas(objFiltrosZonaBE)

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

            objFiltrosBE = New SunarpBE
            objFiltrosBE.NombreTabla = Constantes.K_NOM_TABLA_REGISTRO
            objRegistrosBL = New SunarpBL
            objListaRegistros = objRegistrosBL.ObtenerListaRegistros(objFiltrosBE)
            Me.ddlRegistro.Items.Clear()
            Me.ddlRegistro.DataSource = objListaRegistros
            Me.ddlRegistro.DataTextField = "DescripcionValor"
            Me.ddlRegistro.DataValueField = "Valor1"
            Me.ddlRegistro.DataBind()
            Me.ddlRegistro.Items.Insert(0, "[ SELECCIONE ]")
            Me.ddlRegistro.Items(0).Value = "0"

            Me.txtPartida.Text = String.Empty

            'Me.txtTransaccion.Text = String.Empty
            'Me.txtNroTotalPag.Text = String.Empty

            objResultado = New List(Of SunarpBE)
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
            Dim objResultado As List(Of SunarpBE) = Nothing

            'Me.txtTransaccion.Text = String.Empty
            'Me.txtNroTotalPag.Text = String.Empty

            objResultado = New List(Of SunarpBE)
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
        Dim objDatosMemoriaBE As SunarpBE = Nothing
        objDatosMemoriaBE = New SunarpBE

        Try
            Dim objRespCons As ServicioIntermedio.SunarpListaAsientoResponse = Nothing
            Dim objListResultadoBE As List(Of ServicioIntermedio.SunarpAsientoBE) = Nothing
            Dim proxy As ServicioIntermedio.ServiceIOClient = Nothing
            Dim strZona As String = String.Empty, strOficina As String = String.Empty, strNumPartida As String = String.Empty, _
                strRegistro As String = String.Empty, strPartida As String = String.Empty, strIDImgAsiento As String = String.Empty, _
                strTipo As String = String.Empty, strNroPagina As String = String.Empty, strNroPagRef As String = String.Empty, _
                strCantidadPag As String = String.Empty, strNroTotalPag As String = String.Empty, strTransaccion As String = String.Empty, _
                strZonaF As String = String.Empty, strOficinaF As String = String.Empty
            Dim objListResultadoFinal As List(Of SunarpBE) = Nothing
            Dim objItemBE As SunarpBE = Nothing

            Me.LimpiarConsulta()

            strZona = Me.ddlZona.SelectedValue.Trim()
            strOficina = Me.ddlOficina.SelectedValue.Trim()
            strRegistro = Me.ddlRegistro.SelectedValue.Trim()
            strPartida = Me.txtPartida.Text.Trim()

            strZonaF = Me.ddlZona.Items(Me.ddlZona.SelectedIndex).Text.Trim()
            strOficinaF = Me.ddlOficina.Items(Me.ddlOficina.SelectedIndex).Text.Trim()

            'Memoria
            objDatosMemoriaBE.Zona = strZona
            objDatosMemoriaBE.Oficina = strOficina
            objDatosMemoriaBE.Registro = strRegistro
            objDatosMemoriaBE.NumPartida = strPartida
            objDatosMemoriaBE.ZonaTXT = strZonaF
            objDatosMemoriaBE.OficinaTXT = strOficinaF

            Try
                proxy = New ServicioIntermedio.ServiceIOClient
                objRespCons = proxy.SunarpListaAsiento(New ServicioIntermedio.SunarpListaAsientoRequest With {.CODAPP = Constantes.K_COD_APPLI, _
                                                                                                              .CODUSER = FachadaSesion.Usuario.CodigoUsuario.Trim(), _
                                                                                                              .Zona = strZona, _
                                                                                                              .Oficina = strOficina, _
                                                                                                              .Registro = strRegistro, _
                                                                                                              .Partida = strPartida})
                If Not objRespCons Is Nothing Then
                    If objRespCons.Correcto And objRespCons.MensajeError = String.Empty Then
                        objListResultadoBE = objRespCons.Resultado
                        If Not objListResultadoBE Is Nothing Then

                            objListResultadoFinal = New List(Of SunarpBE)
                            For Each itemBE In objListResultadoBE

                                If itemBE.IdImgAsiento Is Nothing Then
                                    strIDImgAsiento = ""
                                Else
                                    strIDImgAsiento = itemBE.IdImgAsiento.Trim()
                                End If

                                If itemBE.Tipo Is Nothing Then
                                    strTipo = ""
                                Else
                                    strTipo = itemBE.Tipo.Trim()
                                End If

                                If itemBE.CantPaginas Is Nothing Then
                                    strCantidadPag = ""
                                Else
                                    strCantidadPag = itemBE.CantPaginas.Trim()
                                End If

                                If itemBE.NroPagRef Is Nothing Then
                                    strNroPagRef = ""
                                Else
                                    strNroPagRef = itemBE.NroPagRef.Trim()
                                End If

                                If itemBE.NroPagina Is Nothing Then
                                    strNroPagina = ""
                                Else
                                    strNroPagina = itemBE.NroPagina.Trim()
                                End If

                                If itemBE.Transaccion Is Nothing Then
                                    strTransaccion = ""
                                Else
                                    strTransaccion = itemBE.Transaccion.Trim()
                                End If

                                If itemBE.TotalPag Is Nothing Then
                                    strNroTotalPag = ""
                                Else
                                    strNroTotalPag = itemBE.TotalPag.Trim()
                                End If

                                objItemBE = New SunarpBE
                                objItemBE.Transaccion = strTransaccion
                                objItemBE.NroTotalPag = strNroTotalPag
                                objItemBE.IDImgAsiento = strIDImgAsiento
                                objItemBE.Tipo = strTipo
                                objItemBE.CantidadPag = strCantidadPag
                                objItemBE.NroPagRef = strNroPagRef
                                objItemBE.NroPag = strNroPagina
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
            Dim strMsjResultado As String = String.Empty, strZona As String = String.Empty, strOficina As String = String.Empty, _
                strRegistro As String = String.Empty, strNumPartida As String = String.Empty, strTransaccion As String = String.Empty, _
                strNroTotalPag As String = String.Empty
            Dim objLista As List(Of SunarpBE) = Nothing
            Dim objRegistroBE As SunarpBE = Nothing, objDatosMemoria As SunarpBE = Nothing, objVerBE As SunarpBE = Nothing
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

                If objDatosMemoria.Registro Is Nothing Then
                    strRegistro = ""
                Else
                    strRegistro = objDatosMemoria.Registro
                End If

                If objDatosMemoria.NumPartida Is Nothing Then
                    strNumPartida = ""
                Else
                    strNumPartida = objDatosMemoria.NumPartida
                End If

                If objDatosMemoria.objLista Is Nothing Then
                    objLista = Nothing

                    strTransaccion = ""
                    strNroTotalPag = ""
                Else
                    objLista = objDatosMemoria.objLista

                    objVerBE = objLista.Item(1)
                    If Not objVerBE Is Nothing Then
                        strTransaccion = objVerBE.Transaccion
                        strNroTotalPag = objVerBE.NroTotalPag
                    Else
                        strTransaccion = ""
                        strNroTotalPag = ""
                    End If
                End If
            End If

            objRegistroBE = New SunarpBE
            objRegistroBE.TipoServicio = Constantes.TipoServicio.K_SUNARP_LIST_ASIENTOS
            objRegistroBE.VUSER = FachadaSesion.Usuario.CodigoUsuario.Trim()
            objRegistroBE.VCODAPLI = Constantes.K_COD_APPLI
            objRegistroBE.NCODRESULTADO = intCodResultado
            objRegistroBE.VMENSAJE_RESULTADO = strMsjResultado
            objRegistroBE.NCODACCION = Constantes.Accion.K_EXPORTAR
            objRegistroBE.DFECHA = Now
            objRegistroBE.Zona = strZona
            objRegistroBE.Oficina = strOficina
            objRegistroBE.Registro = strRegistro
            objRegistroBE.NumPartida = strNumPartida
            objRegistroBE.Transaccion = strTransaccion
            objRegistroBE.NroTotalPag = strNroTotalPag
            objRegistroBE.objLista = objLista
            objSunarpBL = New SunarpBL()
            intResultado = objSunarpBL.RegistrarLog_LA(objRegistroBE)

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

            Me.DatosMemoria.RutaArchivoPDF = System.IO.Path.Combine(strDirectorio, "Reporte_SUNARP_ListaAsientos_" & strDia & strMes & Now.Year.ToString() & Now.Hour.ToString() & Now.Minute.ToString() & Now.Second.ToString() & Constantes.K_EXTENSION_PDF)

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
                strOficina As String = String.Empty, strPartida As String = String.Empty, strRegistro As String = String.Empty, _
                strRutaImagen_WS As String = String.Empty, strMsjError_WS As String = String.Empty, strRutaWebImagen As String = String.Empty
            Dim rptReporte As Microsoft.Reporting.WebForms.ReportViewer
            Dim lstParametros As List(Of ReportParameter)
            Dim warnings() As Warning = Nothing
            Dim streams() As String = Nothing
            Dim renderedBytes() As Byte = Nothing
            Dim objLista As List(Of SunarpBE) = Nothing
            Dim intRes_WS As Integer = 0
            strFormat = Constantes.K_PDF
            strRutaReporte = Constantes.ConfiguracionReportePDF.K_URL_CARPETA_REPORTE

            If Me.DatosMemoria.NCODRESULTADO = Constantes.Cod_Resultado.K_OK Then
                strPath = System.IO.Path.Combine(Server.MapPath(strRutaReporte), Constantes.ConfiguracionReportePDF.K_RDLC_REPORTE_SUNARP_LA_OK)
            ElseIf Me.DatosMemoria.NCODRESULTADO = Constantes.Cod_Resultado.K_Error Then
                strPath = System.IO.Path.Combine(Server.MapPath(strRutaReporte), Constantes.ConfiguracionReportePDF.K_RDLC_REPORTE_SUNARP_LA_ERROR)
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
            strRegistro = Me.DatosMemoria.Registro

            'Resultado
            If Me.DatosMemoria.NCODRESULTADO = Constantes.Cod_Resultado.K_OK Then

                objLista = Me.DatosMemoria.objLista

                For Each item In objLista

                    Me.ObtenerImagenAsiento(item, intRes_WS, strRutaImagen_WS, strMsjError_WS, strRutaWebImagen)

                    If intRes_WS = Constantes.Cod_Resultado.K_OK Then

                        item.ResAsiento_WS = intRes_WS
                        item.RutaImagen = strRutaImagen_WS
                        item.RutaArchivoPDF = strRutaWebImagen

                    ElseIf intRes_WS = Constantes.Cod_Resultado.K_Error Then

                        item.ResAsiento_WS = intRes_WS
                        item.RutaImagen = strMsjError_WS

                    End If

                Next

                Me.DatosMemoria.objLista = objLista

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
            lstParametros.Add(New ReportParameter("pZona", strZona))
            lstParametros.Add(New ReportParameter("pOficina", strOficina))
            lstParametros.Add(New ReportParameter("pPartida", strPartida))
            lstParametros.Add(New ReportParameter("pRegistro", strRegistro))

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
            Dim strFilePdfA As String = String.Empty, strFileName As String = String.Empty, strImageURL As String = String.Empty, _
                strTituloImg As String = String.Empty
            Dim doc As iTextSharp.text.Document
            Dim writer As iTextSharp.text.pdf.PdfAWriter
            Dim cb As PdfContentByte
            Dim objLista As List(Of SunarpBE) = Nothing
            Dim jpg As iTextSharp.text.Image

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

                'Reporte -----------------------------------------------------------------------//
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

                'Listar imagenes de asientos --------------------------------------------------//
                If Me.DatosMemoria.NCODRESULTADO = Constantes.Cod_Resultado.K_OK Then

                    objLista = Me.DatosMemoria.objLista
                    For Each item In objLista

                        If item.ResAsiento_WS = Constantes.Cod_Resultado.K_OK Then

                            doc.SetPageSize(iTextSharp.text.PageSize.A4)
                            doc.NewPage()

                            Dim pageWidth = doc.PageSize.Width - 20
                            Dim pageHeight = doc.PageSize.Height - 20

                            strImageURL = item.RutaArchivoPDF
                            jpg = iTextSharp.text.Image.GetInstance(strImageURL)
                            jpg.ScaleToFit(pageWidth, pageHeight)   'Resize image depend upon your need
                            jpg.SpacingBefore = 10.0F    'Give space before image
                            jpg.SpacingAfter = 1.0F   'Give some space after the image
                            jpg.Alignment = Element.ALIGN_CENTER

                            'strTituloImg = "ID Imagen Asiento: " & item.IDImgAsiento
                            'doc.Add(New Paragraph(strTituloImg, FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)))

                            doc.Add(jpg)

                        End If

                    Next

                End If

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

#Region "Imagen Asiento PDF"

    Public Sub ObtenerImagenAsiento(ByVal objFiltroBE As SunarpBE, ByRef intRes_WS As Integer, ByRef strRutaImagen_WS As String, _
                                    ByRef strMsjError_WS As String, ByRef strRutaWebImagen As String)
        Try
            Dim objRespCons As ServicioIntermedio.SunarpVerImagenAsientoResponse = Nothing
            Dim proxy As ServicioIntermedio.ServiceIOClient = Nothing
            Dim strResultado As String = String.Empty, strRutaWebFinal As String = String.Empty, strErrorArchivo As String = String.Empty
            Dim intResultadoArchivo As Integer = 0
            Dim strTransaccion As String = String.Empty, strIDImgAsiento As String = String.Empty, strTipo As String = String.Empty, _
                strNroTotalPag As String = String.Empty, strNroPagRef As String = String.Empty, strPag As String = String.Empty

            If Not objFiltroBE Is Nothing Then
                strTransaccion = objFiltroBE.Transaccion
                strIDImgAsiento = objFiltroBE.IDImgAsiento
                strTipo = objFiltroBE.Tipo
                strNroTotalPag = objFiltroBE.NroTotalPag
                strNroPagRef = objFiltroBE.NroPagRef
                strPag = objFiltroBE.NroPag
            End If

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
                                strRutaImagen_WS = strResultado

                                Me.DescargarArchivo(strResultado, intResultadoArchivo, strRutaWebFinal, strErrorArchivo)
                                If intResultadoArchivo = Constantes.Cod_Resultado.K_OK Then

                                    strRutaWebImagen = strRutaWebFinal

                                    intRes_WS = Constantes.Cod_Resultado.K_OK
                                    strMsjError_WS = String.Empty

                                ElseIf intResultadoArchivo = Constantes.Cod_Resultado.K_Error Then

                                    intRes_WS = Constantes.Cod_Resultado.K_Error
                                    strMsjError_WS = strErrorArchivo

                                End If
                            Else
                                'Memoria
                                intRes_WS = Constantes.Cod_Resultado.K_Error
                                strMsjError_WS = "El ruta del archivo esta vacia."

                            End If
                        Else
                            'Memoria
                            intRes_WS = Constantes.Cod_Resultado.K_Error
                            strMsjError_WS = "El resultado fue nulo."
                        End If
                    Else
                        'Memoria
                        intRes_WS = Constantes.Cod_Resultado.K_Error
                        strMsjError_WS = objRespCons.MensajeError.ToString()
                    End If
                Else
                    'Memoria
                    intRes_WS = Constantes.Cod_Resultado.K_Error
                    strMsjError_WS = "El servicio regreso vacío."

                End If
            Catch ex As Exception
                'Memoria
                intRes_WS = Constantes.Cod_Resultado.K_Error
                strMsjError_WS = ex.Message.ToString()
            End Try

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
            If Not System.IO.Directory.Exists(DirecArchivos) Then
                System.IO.Directory.CreateDirectory(DirecArchivos)
            End If

            rutaFileDestino = DirecArchivos & "/" & strNombreOriginal.Trim()
            If System.IO.File.Exists(rutaFileDestino) Then
                System.IO.File.Delete(rutaFileDestino)
            End If
            System.IO.File.Copy(strRutaImagenOri, rutaFileDestino, True)

            strRutaWebFinal = Constantes.K_RUTAWEB_IMGASIENTO & "/" & strNombreOriginal
            intResultado = Constantes.Cod_Resultado.K_OK

        Catch ex As Exception
            strError = ex.Message
            intResultado = Constantes.Cod_Resultado.K_Error
        End Try
    End Sub

#End Region

#End Region

End Class
