Imports SMV.Interoperabilidad.BL
Imports SMV.Interoperabilidad.BE
Imports System.IO

Public Class frmMantenedor
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

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Globales.DropDownlistBinding(ddlTipoDato, New TablaDatosBL().ListarTablaPadre(), "Descripcion", "IdTablaPadre", Constantes.K_OPCION_SELECCIONE)

                'listar estado:
                Globales.DropDownlistBinding(ddlEstado, Globales.ListaEstado(), "Descripcion", "Codigo")
            End If
        Catch ex As Exception
            LogErrores(ex.Message, ex.StackTrace)
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "Alerta", "alert('" & ex.Message & "');", True)
        End Try
    End Sub

    Protected Sub btnConsultar_Click(sender As Object, e As EventArgs) Handles btnConsultar.Click
        Try
            Me.ConsultarTablaHija()
        Catch ex As Exception
            LogErrores(ex.Message, ex.StackTrace)
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "Alerta", "alert('" & ex.Message & "');", True)
        End Try
    End Sub

    Protected Sub gvDatos_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gvDatos.Sorting
        Try
            Me.OrdenAsc1 = Not Me.OrdenAsc1
            OrdenarGrilla(DirectCast(sender, GridView), ViewState("GrillaResultados"), e.SortExpression, Me.OrdenAsc1)

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

    Protected Sub gvDatos_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvDatos.RowCommand
        Try
            Select Case e.CommandName
                Case "Editar"
                    Dim id = e.CommandArgument
                    Dim obj = DirectCast(ViewState("GrillaResultados"), List(Of TablaDatosBE)).Find(Function(o) o.IdTablaHija = id)

                    ViewState("IdTablaHija") = obj.IdTablaHija
                    Me.txtCodigo.Text = obj.IdTablaHija
                    Me.txtDescripcion.Text = obj.Descripcion
                    Me.txtValor1.Text = obj.Valor1
                    Me.txtValor2.Text = obj.Valor2
                    Me.ddlEstado.SelectedValue = obj.Estado

                    Me.upnlMant.Update()

                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Filtros", "jsMostrarMant();", True)
            End Select

        Catch ex As Exception
            LogErrores(ex.Message, ex.StackTrace)
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "Alerta", "alert('" & ex.Message & "');", True)
        End Try        
    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        Try
            Dim obj As New TablaDatosBE()
            obj.IdTablaHija = Convert.ToInt32(ViewState("IdTablaHija"))
            obj.Descripcion = txtDescripcion.Text.Trim
            obj.Valor1 = txtValor1.Text.Trim
            obj.Valor2 = txtValor2.Text.Trim
            obj.Estado = ddlEstado.SelectedValue

            Using oTablaDatosBL As New TablaDatosBL
                oTablaDatosBL.ActualizarTablaHija(obj)
            End Using

            Me.ConsultarTablaHija()

            upGrilla.Update()
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "Filtros", "alert('Los datos se registraron correctamente'); jsOcultarMant();", True)

        Catch ex As Exception
            LogErrores(ex.Message, ex.StackTrace)
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "Alerta", "alert('" & ex.Message & "');", True)
        End Try        
    End Sub

    Protected Sub btnExportarExcel_Click(sender As Object, e As EventArgs) Handles btnExportarExcel.Click
        Try
            If IsNothing(ViewState("GrillaResultados")) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "alerta", "alert('Realizar la búsqueda primero.');", True)
                Return
            End If

            Dim sb As StringBuilder = New StringBuilder()
            Dim strNombreArchivo As String = "MantenimientoDatos.xls"
            Dim strformatoAdjunto As String = "attachment;filename={0}"

            'Cambiamos el diseño de la grilla
            gvExcel.GridLines = GridLines.Both
            gvExcel.HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#333333")
            gvExcel.RowStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#E1F1F1")
            gvExcel.HeaderStyle.ForeColor = System.Drawing.Color.White
            gvExcel.AlternatingRowStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#fff")
            gvExcel.AlternatingRowStyle.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000")
            gvExcel.AllowSorting = False
            gvExcel.EditIndex = -1
            gvExcel.EnableViewState = False

            gvExcel.DataSource = DirectCast(ViewState("GrillaResultados"), List(Of TablaDatosBE))
            gvExcel.DataBind()

            Using htw As New HtmlTextWriter(New StringWriter(sb))

                htw.Write("<h1>" & Title & "</h1>")
                htw.Write(String.Format(Globales.Exportacion.K_FORMATO_TITULO_EXCEL, gvExcel.Columns.Count, "Configuración de Datos"))
                htw.WriteBreak()

                Using form As New HtmlForm
                    form.Controls.Add(gvExcel)
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

#Region "Metodos"

    Private Sub ConsultarTablaHija()
        Try
            Dim id = Me.ddlTipoDato.SelectedValue
            Dim listaHija = New TablaDatosBL().ListarTablaHija(id)

            ViewState("GrillaResultados") = listaHija
            Me.gvDatos.DataSource = listaHija
            Me.gvDatos.DataBind()

        Catch ex As Exception
            Throw ex
        End Try        
    End Sub

#End Region

End Class
