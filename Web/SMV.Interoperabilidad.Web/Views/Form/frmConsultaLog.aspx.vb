Imports System.IO
Imports System.Linq
Imports System.Web.Services
Imports SMV.Interoperabilidad.BE
Imports SMV.Interoperabilidad.BL

Public Class frmConsultaLog
    Inherits System.Web.UI.Page

#Region "Atributos"

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
            Me.ConsultarLogs()
        Catch ex As Exception
            LogErrores(ex.Message, ex.StackTrace)
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "Alerta", "alert('" & ex.Message & "');", True)
        End Try
    End Sub

    Protected Sub btnExportarExcel_Click(sender As Object, e As EventArgs) Handles btnExportarExcel.Click
        Try
            Me.ExportarExcel()
        Catch ex As Exception
            LogErrores(ex.Message, ex.StackTrace)
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "Alerta", "alert('" & ex.Message & "');", True)
        End Try
    End Sub

    Protected Sub gvDatos_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gvDatos.Sorting
        Try
            Dim objListRes As List(Of LogBE) = Nothing

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

                Me.gvDatos.HeaderRow.Cells(9).Style("display") = "none"
                Me.gvDatos.HeaderRow.Cells(10).Style("display") = "none"
                Me.gvDatos.HeaderRow.Cells(11).Style("display") = "none"
            End If
        Catch ex As Exception
            'ex.ToString()
        End Try
    End Sub

    Protected Sub gvDatos_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvDatos.RowDataBound
        Try
            Dim strCodTipoServicio As String = String.Empty, strCodLog As String = String.Empty
            Dim intSalida As Integer = 0, intTipoResultado As Integer = 0
            Dim objDato As LogBE = Nothing
            Dim ltVerDetalle As Literal

            objDato = CType(e.Row.DataItem, LogBE)
            If Not objDato Is Nothing Then

                intTipoResultado = objDato.CodTipoResultado

                'Valida Forms con grillas
                If ((objDato.CodTipoServicio = Constantes.TipoServicio.K_SUNEDU_GRADOSYTIT) Or _
                    (objDato.CodTipoServicio = Constantes.TipoServicio.K_SUNARP_LIST_ASIENTOS) Or _
                    (objDato.CodTipoServicio = Constantes.TipoServicio.K_SUNARP_TITU_BIENES)) Then
                    intSalida = 1
                Else
                    intSalida = 2
                End If

                If (e.Row.RowType = DataControlRowType.DataRow) Then

                    e.Row.Cells(9).Style("display") = "none"
                    e.Row.Cells(10).Style("display") = "none"
                    e.Row.Cells(11).Style("display") = "none"

                    If (intSalida = 1) Then

                        If (intTipoResultado = Constantes.Cod_Resultado.K_OK) Then

                            'Salida (Boton)
                            strCodTipoServicio = objDato.CodTipoServicio.ToString()
                            strCodLog = objDato.CodLog.ToString()

                            ltVerDetalle = CType(e.Row.FindControl("ltVerDetalle"), Literal)
                            ltVerDetalle.Text = "<a href=""#"" data-toggle=""modal"" onclick=""js_VerDetalle('" & strCodTipoServicio & "','" & strCodLog & "')"" data-target=""#VerDetSalida""><img src=""" & Page.ResolveClientUrl("~/Content/Images/imgDetalle.png") & """ title=""Ver Detalle"" width=""16"" height=""16"" /></a>"
                            e.Row.Cells(8).CssClass = "AlineaTextCenter"

                        ElseIf (intTipoResultado = Constantes.Cod_Resultado.K_Error) Then

                            'Salida (Texto)
                            ltVerDetalle = CType(e.Row.FindControl("ltVerDetalle"), Literal)
                            ltVerDetalle.Text = objDato.Salida
                            e.Row.Cells(8).CssClass = "AlineaTextIzq"

                        End If

                    ElseIf (intSalida = 2) Then

                        'Salida (Texto)
                        ltVerDetalle = CType(e.Row.FindControl("ltVerDetalle"), Literal)
                        ltVerDetalle.Text = objDato.Salida
                        e.Row.Cells(8).CssClass = "AlineaTextIzq"

                    End If

                End If

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
            Dim objResultado As List(Of LogBE) = Nothing, objListaTipoServicio As List(Of LogBE) = Nothing, objListaTipoEventos As List(Of LogBE) = Nothing
            Dim objFiltrosTipoServicioBE As LogBE = Nothing, objFiltrosTipoEventoBE As LogBE = Nothing
            Dim objTipoServicioBL As LogBL = Nothing, objTipoEventoBL As LogBL = Nothing
            Dim objListaUsuarios As List(Of UsuarioBE) = Nothing
            Dim objUsuarioBL As UsuarioBL = Nothing

            'Tipo Servicio
            objFiltrosTipoServicioBE = New LogBE
            objFiltrosTipoServicioBE.NombreTabla = Constantes.K_NOM_TABLA_TIPO_SERVICIO
            objTipoServicioBL = New LogBL
            objListaTipoServicio = objTipoServicioBL.ObtenerListaTipoServicio(objFiltrosTipoServicioBE)

            Me.ddlTipoServicio.Items.Clear()
            Me.ddlTipoServicio.DataSource = objListaTipoServicio
            Me.ddlTipoServicio.DataTextField = "DescripcionValor"
            Me.ddlTipoServicio.DataValueField = "Valor1"
            Me.ddlTipoServicio.DataBind()
            Me.ddlTipoServicio.Items.Insert(0, "[ TODOS ]")
            Me.ddlTipoServicio.Items(0).Value = "0"

            'Tipo Evento
            objFiltrosTipoEventoBE = New LogBE
            objFiltrosTipoEventoBE.NombreTabla = Constantes.K_NOM_TABLA_TIPO_EVENTO
            objTipoEventoBL = New LogBL
            objListaTipoEventos = objTipoEventoBL.ObtenerListaTipoEvento(objFiltrosTipoEventoBE)

            Me.ddlTipoEvento.Items.Clear()
            Me.ddlTipoEvento.DataSource = objListaTipoEventos
            Me.ddlTipoEvento.DataTextField = "DescripcionValor"
            Me.ddlTipoEvento.DataValueField = "Valor1"
            Me.ddlTipoEvento.DataBind()
            Me.ddlTipoEvento.Items.Insert(0, "[ TODOS ]")
            Me.ddlTipoEvento.Items(0).Value = "0"

            'Usuarios
            objUsuarioBL = New UsuarioBL
            objListaUsuarios = objUsuarioBL.ObtenerListaUsuarios()

            Me.ddlUsuario.Items.Clear()
            Me.ddlUsuario.DataSource = objListaUsuarios
            Me.ddlUsuario.DataTextField = "NomCompletoUsuario"
            Me.ddlUsuario.DataValueField = "CodigoUsuario"
            Me.ddlUsuario.DataBind()
            Me.ddlUsuario.Items.Insert(0, "[ TODOS ]")
            Me.ddlUsuario.Items(0).Value = "0"

            'Fechas
            Me.txtFECINI.Text = Now.ToString("dd/MM/yyyy")
            Me.txtFECFIN.Text = Now.ToString("dd/MM/yyyy")

            'Grilla
            objResultado = New List(Of LogBE)
            Me.gvDatos.DataSource = objResultado
            Me.gvDatos.DataBind()
            ViewState("GrillaResultados") = objResultado

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ConsultarLogs()
        Try
            Dim strCodTipoServicio As String = String.Empty, strFechaInicio As String = String.Empty, strFechaFin As String = String.Empty, _
                strCodUsuario As String = String.Empty, strCodTipoEvento As String = String.Empty
            Dim intCodTipoServicio As Integer = 0, intCodTipoEvento As Integer = 0
            Dim objListaResultado As List(Of LogBE) = Nothing
            Dim objResultadoBL As LogBL = Nothing
            Dim objFiltroBE As LogBE = Nothing

            strCodTipoServicio = Me.ddlTipoServicio.SelectedValue
            strFechaInicio = Me.txtFECINI.Text.Trim()
            strFechaFin = Me.txtFECFIN.Text.Trim()
            strCodUsuario = Me.ddlUsuario.SelectedValue
            strCodTipoEvento = Me.ddlTipoEvento.SelectedValue

            Integer.TryParse(strCodTipoServicio, intCodTipoServicio)
            Integer.TryParse(strCodTipoEvento, intCodTipoEvento)

            objFiltroBE = New LogBE
            objFiltroBE.CodTipoServicio = intCodTipoServicio
            objFiltroBE.FechaInicio = strFechaInicio
            objFiltroBE.FechaFin = strFechaFin
            objFiltroBE.CodUsuario = strCodUsuario
            objFiltroBE.CodTipoEvento = intCodTipoEvento
            objResultadoBL = New LogBL
            objListaResultado = objResultadoBL.ObtenerListaLogs(objFiltroBE)

            If Not objListaResultado Is Nothing Then

                Me.gvDatos.DataSource = objListaResultado
                Me.gvDatos.DataBind()
                ViewState("GrillaResultados") = objListaResultado

            Else
                objListaResultado = New List(Of LogBE)
                Me.gvDatos.DataSource = objListaResultado
                Me.gvDatos.DataBind()
                ViewState("GrillaResultados") = objListaResultado
            End If
            
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    <WebMethod()>
    Public Shared Function CargarDetalleSalida(ByVal pobjFiltro As LogBE) As List(Of LogBE)
        Dim objResp As List(Of LogBE) = Nothing
        Dim objDetalleSalida As LogBL = Nothing

        objDetalleSalida = New LogBL
        objResp = objDetalleSalida.CargarDetalleSalida(pobjFiltro)
        objDetalleSalida = Nothing
        Return objResp
    End Function

    Private Sub ExportarExcel()
        Try
            Dim strNombreReporte As String = String.Empty, strMes As String = String.Empty, strDia As String = String.Empty, _
                strArray As String = String.Empty, strTxtCelda As String = String.Empty, strEncuentra As String = String.Empty, _
                strColumna As String = String.Empty
            Dim intPasar As Integer = 0, intCuentaTmp As Integer = 0, intCodTipoResultado As Integer = 0, intCodLog As Integer = 0, _
                intCodTipoServicio As Integer = 0, intCuentaE As Integer = 0
            Dim objResultado As List(Of LogBE) = Nothing, objResDetSal As List(Of LogBE) = Nothing
            Dim dtConsulta As DataTable = Nothing
            Dim objDatoBE As LogBE = Nothing
            Dim ltVerDetalle As Literal
            Dim strArrayFin As String() = Nothing
            Dim objFiltroBE As LogBE = Nothing
            Dim objLogBL As LogBL = Nothing

            objResultado = DirectCast(ViewState("GrillaResultados"), List(Of LogBE))
            If Not objResultado Is Nothing Then
                If objResultado.Count > 0 Then

                    dtConsulta = New DataTable
                    If Me.gvDatos.HeaderRow IsNot Nothing Then
                        For i As Integer = 0 To Me.gvDatos.Columns.Count - 1
                            If ((Me.gvDatos.Columns(i).ToString().Trim() <> "TemplateField") And _
                                (Me.gvDatos.Columns(i).ToString().Trim() <> "CodTipoServicio") And _
                                (Me.gvDatos.Columns(i).ToString().Trim() <> "CodTipoResultado") And _
                                (Me.gvDatos.Columns(i).ToString().Trim() <> "CodLog")) Then

                                If Me.gvDatos.Columns(i).ToString().Trim() = "Tipo Servicio" Then
                                    strColumna = "TipoServicio"
                                ElseIf Me.gvDatos.Columns(i).ToString().Trim() = "URL Tipo Servicio" Then
                                    strColumna = "URLTipoServicio"
                                ElseIf Me.gvDatos.Columns(i).ToString().Trim() = "Tipo Evento" Then
                                    strColumna = "TipoEvento"
                                ElseIf Me.gvDatos.Columns(i).ToString().Trim() = "Usuario" Then
                                    strColumna = "Usuario"
                                ElseIf Me.gvDatos.Columns(i).ToString().Trim() = "Fecha del Evento" Then
                                    strColumna = "FechaRegistro"
                                ElseIf Me.gvDatos.Columns(i).ToString().Trim() = "Tipo Resultado" Then
                                    strColumna = "TipoResultado"
                                ElseIf Me.gvDatos.Columns(i).ToString().Trim() = "Entrada" Then
                                    strColumna = "Entrada"
                                ElseIf Me.gvDatos.Columns(i).ToString().Trim() = "Salida" Then
                                    strColumna = "Salida"
                                End If
                                dtConsulta.Columns.Add(HttpUtility.HtmlDecode(strColumna))
                            Else
                                strArray = Convert.ToString(i.ToString() + ",") & strArray
                            End If
                        Next

                        If strArray.Trim() <> "" Then
                            strArray = strArray.Substring(0, strArray.Length - 1)
                            strArrayFin = strArray.Split(",")
                        End If

                    End If

                    'add each of the data rows to the table
                    For Each row As GridViewRow In Me.gvDatos.Rows
                        Dim dr As DataRow
                        dr = dtConsulta.NewRow()

                        intCuentaTmp = -1  'Inicializo

                        For intCuentaE = 0 To row.Cells.Count - 1

                            intPasar = 1   'Inicializo

                            If strArray.Trim() <> "" Then
                                strEncuentra = Array.Find(Of String)(strArrayFin, Function(s) s.Equals(intCuentaE.ToString()))

                                If strEncuentra IsNot Nothing Then
                                    If strEncuentra.Length > 0 Then
                                        intPasar = 0
                                    End If
                                End If

                            End If

                            If intPasar = 1 Then
                                intCuentaTmp = intCuentaTmp + 1

                                Integer.TryParse(row.Cells(9).Text.Trim(), intCodTipoServicio)
                                Integer.TryParse(row.Cells(10).Text.Trim(), intCodTipoResultado)
                                Integer.TryParse(row.Cells(11).Text.Trim(), intCodLog)

                                If ((intCodTipoServicio = Constantes.TipoServicio.K_SUNARP_TITU_BIENES) Or _
                                    (intCodTipoServicio = Constantes.TipoServicio.K_SUNARP_LIST_ASIENTOS) Or _
                                    (intCodTipoServicio = Constantes.TipoServicio.K_SUNEDU_GRADOSYTIT)) Then

                                    If (intCuentaE = 8) Then   'Celda - Salida (Boton)

                                        If (intCodTipoResultado = Constantes.Cod_Resultado.K_Error) Then

                                            ltVerDetalle = CType(row.Cells(intCuentaE).FindControl("ltVerDetalle"), Literal)
                                            strTxtCelda = ltVerDetalle.Text.Trim()

                                        Else

                                            objFiltroBE = New LogBE
                                            objFiltroBE.CodLog = intCodLog
                                            objLogBL = New LogBL
                                            objResDetSal = objLogBL.CargarDetalleSalidaExportar(objFiltroBE)
                                            If Not objResDetSal Is Nothing Then
                                                If objResDetSal.Count > 0 Then
                                                    strTxtCelda = objResDetSal(0).Cadena
                                                Else
                                                    strTxtCelda = ""
                                                End If
                                            Else
                                                strTxtCelda = ""
                                            End If

                                        End If
                                    Else
                                        strTxtCelda = row.Cells(intCuentaE).Text.Trim()
                                    End If

                                Else
                                    If (intCuentaE = 8) Then   'Celda - Salida (Boton)

                                        ltVerDetalle = CType(row.Cells(intCuentaE).FindControl("ltVerDetalle"), Literal)
                                        strTxtCelda = ltVerDetalle.Text.Trim()

                                    Else
                                        strTxtCelda = row.Cells(intCuentaE).Text.Trim()
                                    End If
                                End If

                                dr(intCuentaTmp) = HttpUtility.HtmlDecode(strTxtCelda)
                            End If
                        Next
                        dtConsulta.Rows.Add(dr)
                    Next

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
                    strNombreReporte = String.Format("Consulta_Logs_{0}{1}{2}{3}.xls", strDia, strMes, Now.Year.ToString(), Now.Hour.ToString() & Now.Minute.ToString() & Now.Second.ToString())

                    Me.ExportarExcel_Formato(strNombreReporte, dtConsulta)
                Else
                    ScriptManager.RegisterStartupScript(Me, Me.[GetType](), "alerta", "alert('No existen datos para exportar.');", True)
                End If
            Else
                ScriptManager.RegisterStartupScript(Me, Me.[GetType](), "alerta", "alert('No existen datos para exportar.');", True)
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ExportarExcel_Formato(ByVal strNombreExcel As String, ByVal dtConsulta As DataTable)
        Try
            Dim objList As List(Of LogBE) = Nothing
            Dim sb As StringBuilder = New StringBuilder()
            Dim strNombreArchivo As String = strNombreExcel
            Dim strformatoAdjunto As String = "attachment;filename={0}"

            'Cambiamos el diseño de la grilla
            Me.gvExcel.GridLines = GridLines.Both
            Me.gvExcel.HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#333333")
            Me.gvExcel.RowStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#E1F1F1")
            Me.gvExcel.HeaderStyle.ForeColor = System.Drawing.Color.White
            Me.gvExcel.AlternatingRowStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff")
            Me.gvExcel.AlternatingRowStyle.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000")
            Me.gvExcel.AllowSorting = False
            Me.gvExcel.EditIndex = -1
            Me.gvExcel.EnableViewState = False

            objList = DataTableToList(Of LogBE)(dtConsulta)
            Me.gvExcel.DataSource = objList
            Me.gvExcel.DataBind()

            Using htw As New HtmlTextWriter(New StringWriter(sb))

                htw.Write("<h1>" & Title & "</h1>")
                htw.Write(String.Format(Globales.Exportacion.K_FORMATO_TITULO_EXCEL, Me.gvExcel.Columns.Count, "Consulta de Logs"))
                htw.WriteBreak()

                Using form As New HtmlForm
                    form.Controls.Add(Me.gvExcel)
                    Using page As New Page
                        page.EnableEventValidation = False
                        page.DesignerInitialize()
                        page.Controls.Add(form)
                        page.RenderControl(htw)
                    End Using
                End Using
            End Using

            'Generamos la respuesta del reporte
            Response.ContentType = "application/vnd.ms-excel"
            Response.AddHeader("Content-Disposition", String.Format(Globales.Exportacion.K_FORMATO_CONTENIDO_ADJUNTO, strNombreArchivo))
            Response.Charset = String.Empty
            Response.ContentEncoding = Encoding.Default
            Response.Clear()
            Response.Buffer = True
            Response.Write(sb.ToString())
            Response.End()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

End Class
