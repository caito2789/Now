Imports System.IO
Imports Microsoft.Reporting.WebForms
Imports iTextSharp.text.pdf
Imports SMV.Interoperabilidad.BE
Imports SMV.Interoperabilidad.BL

Public Class frmConsTituBienes
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
            Dim blPorNombres As Boolean = False, blPorRazonS As Boolean = False

            blPorNombres = Me.rbNombres.Checked
            blPorRazonS = Me.rbRazonS.Checked

            If blPorNombres = True Then

                Me.dvTipoNombres.Style("display") = "inline"
                Me.dvTipoRazonS.Style("display") = "none"

                Me.txtRazonSocial.Text = String.Empty

            ElseIf blPorRazonS = True Then

                Me.dvTipoNombres.Style("display") = "none"
                Me.dvTipoRazonS.Style("display") = "inline"

                Me.txtApePaterno.Text = String.Empty
                Me.txtApeMaterno.Text = String.Empty
                Me.txtNombres.Text = String.Empty

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
            Dim blPorNombres As Boolean = False, blPorRazonS As Boolean = False
            Dim objFilaBE As SunarpBE = Nothing

            blPorNombres = Me.rbNombres.Checked
            blPorRazonS = Me.rbRazonS.Checked

            If (e.Row.RowType = DataControlRowType.Header) Then

                If blPorNombres = True Then

                    e.Row.Cells(4).Visible = False         'RazonSocial

                ElseIf blPorRazonS = True Then

                    e.Row.Cells(1).Visible = False         'ApePaterno
                    e.Row.Cells(2).Visible = False         'ApeMaterno
                    e.Row.Cells(3).Visible = False         'Nombres

                End If

            End If

            If (e.Row.RowType = DataControlRowType.DataRow) Then

                If blPorNombres = True Then

                    e.Row.Cells(4).Visible = False         'RazonSocial

                ElseIf blPorRazonS = True Then

                    e.Row.Cells(1).Visible = False         'ApePaterno
                    e.Row.Cells(2).Visible = False         'ApeMaterno
                    e.Row.Cells(3).Visible = False         'Nombres

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
            Dim objResultado As List(Of SunarpBE) = Nothing

            Me.rbNombres.Checked = True
            Me.rbRazonS.Checked = False
            Me.dvTipoNombres.Style("display") = "inline"
            Me.dvTipoRazonS.Style("display") = "none"

            Me.txtApePaterno.Text = String.Empty
            Me.txtApeMaterno.Text = String.Empty
            Me.txtNombres.Text = String.Empty
            Me.txtRazonSocial.Text = String.Empty

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

            'Grilla
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
            Dim objRespCons As ServicioIntermedio.SunarpTitularidadBienesResponse = Nothing
            Dim objListResultadoBE As List(Of ServicioIntermedio.SunarpTitularidadBienesBE) = Nothing
            Dim proxy As ServicioIntermedio.ServiceIOClient = Nothing
            Dim strRazonSocial As String = String.Empty, strApePaterno As String = String.Empty, strApeMaterno As String = String.Empty, _
                strNombres As String = String.Empty, strTipoParticipante As String = String.Empty, strTipoDocumento As String = String.Empty, _
                strNroDocumento As String = String.Empty, strNumPartida As String = String.Empty, strRegistro As String = String.Empty, _
                strNumPlaca As String = String.Empty, strZona As String = String.Empty, strOficina As String = String.Empty, _
                strEstado As String = String.Empty
            Dim objListResultadoFinal As List(Of SunarpBE) = Nothing
            Dim objItemBE As SunarpBE = Nothing
            Dim blPorNombres As Boolean = False, blPorRazonS As Boolean = False
            Dim intTipoConsulta As Integer = 0

            Me.LimpiarConsulta()

            blPorNombres = Me.rbNombres.Checked
            blPorRazonS = Me.rbRazonS.Checked
            strApePaterno = Me.txtApePaterno.Text.Trim().ToUpper
            strApeMaterno = Me.txtApeMaterno.Text.Trim().ToUpper
            strNombres = Me.txtNombres.Text.Trim().ToUpper
            strRazonSocial = Me.txtRazonSocial.Text.Trim().ToUpper

            If blPorNombres = True Then
                intTipoConsulta = Constantes.TipoConsulta_SUNARP_TITULARIDADB_COD.K_POR_NOMBRES
                strTipoParticipante = Constantes.TipoConsulta_SUNARP_TITULARIDADB_TXT.K_POR_NOMBRES
            ElseIf blPorRazonS = True Then
                intTipoConsulta = Constantes.TipoConsulta_SUNARP_TITULARIDADB_COD.K_POR_RAZONSOCIAL
                strTipoParticipante = Constantes.TipoConsulta_SUNARP_TITULARIDADB_TXT.K_POR_RAZONSOCIAL
            End If

            'Memoria
            objDatosMemoriaBE.TipoConsulta = intTipoConsulta
            objDatosMemoriaBE.TipoParticipante = strTipoParticipante
            objDatosMemoriaBE.ApePaterno = strApePaterno
            objDatosMemoriaBE.ApeMaterno = strApeMaterno
            objDatosMemoriaBE.Nombres = strNombres
            objDatosMemoriaBE.RazonSocial = strRazonSocial

            If blPorNombres = True Then

                Try
                    proxy = New ServicioIntermedio.ServiceIOClient
                    objRespCons = proxy.SunarpTitularidadBienes(New ServicioIntermedio.SunarpTitularidadBienesRequest With {.CODAPP = Constantes.K_COD_APPLI, _
                                                                                                                            .CODUSER = FachadaSesion.Usuario.CodigoUsuario.Trim(), _
                                                                                                                            .TipoParticipante = strTipoParticipante, _
                                                                                                                            .ApellidoPaterno = strApePaterno, _
                                                                                                                            .ApellidoMaterno = strApeMaterno, _
                                                                                                                            .Nombres = strNombres})
                    If Not objRespCons Is Nothing Then
                        If objRespCons.Correcto And objRespCons.MensajeError = String.Empty Then
                            objListResultadoBE = objRespCons.Resultado
                            If Not objListResultadoBE Is Nothing Then

                                objListResultadoFinal = New List(Of SunarpBE)
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

                                    If itemBE.RazonSocial Is Nothing Then
                                        strRazonSocial = ""
                                    Else
                                        strRazonSocial = itemBE.RazonSocial.Trim()
                                    End If

                                    If itemBE.TipoDocumento Is Nothing Then
                                        strTipoDocumento = ""
                                    Else
                                        strTipoDocumento = itemBE.TipoDocumento.Trim()
                                    End If

                                    If itemBE.NumeroDocumento Is Nothing Then
                                        strNroDocumento = ""
                                    Else
                                        strNroDocumento = itemBE.NumeroDocumento.Trim()
                                    End If

                                    If itemBE.NumeroPartida Is Nothing Then
                                        strNumPartida = ""
                                    Else
                                        strNumPartida = itemBE.NumeroPartida.Trim()
                                    End If

                                    If itemBE.Registro Is Nothing Then
                                        strRegistro = ""
                                    Else
                                        strRegistro = itemBE.Registro.Trim()
                                    End If

                                    If itemBE.NumeroPlaca Is Nothing Then
                                        strNumPlaca = ""
                                    Else
                                        strNumPlaca = itemBE.NumeroPlaca.Trim()
                                    End If

                                    If itemBE.Zona Is Nothing Then
                                        strZona = ""
                                    Else
                                        strZona = itemBE.Zona.Trim()
                                    End If

                                    If itemBE.Oficina Is Nothing Then
                                        strOficina = ""
                                    Else
                                        strOficina = itemBE.Oficina.Trim()
                                    End If

                                    If itemBE.Estado Is Nothing Then
                                        strEstado = ""
                                    Else
                                        strEstado = itemBE.Estado.Trim()
                                    End If

                                    objItemBE = New SunarpBE
                                    objItemBE.ApePaterno = strApePaterno
                                    objItemBE.ApeMaterno = strApeMaterno
                                    objItemBE.Nombres = strNombres
                                    objItemBE.RazonSocial = strRazonSocial
                                    objItemBE.TipoDocumento = strTipoDocumento
                                    objItemBE.NroDocumento = strNroDocumento
                                    objItemBE.NumPartida = strNumPartida
                                    objItemBE.Registro = strRegistro
                                    objItemBE.NumPlaca = strNumPlaca
                                    objItemBE.Zona = strZona
                                    objItemBE.Oficina = strOficina
                                    objItemBE.Estado = strEstado
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

            ElseIf blPorRazonS = True Then

                Try
                    proxy = New ServicioIntermedio.ServiceIOClient
                    objRespCons = proxy.SunarpTitularidadBienes(New ServicioIntermedio.SunarpTitularidadBienesRequest With {.CODAPP = Constantes.K_COD_APPLI, _
                                                                                                                            .CODUSER = FachadaSesion.Usuario.CodigoUsuario.Trim(), _
                                                                                                                            .TipoParticipante = strTipoParticipante, _
                                                                                                                            .RazonSocial = strRazonSocial})
                    If Not objRespCons Is Nothing Then
                        If objRespCons.Correcto And objRespCons.MensajeError = String.Empty Then
                            objListResultadoBE = objRespCons.Resultado
                            If Not objListResultadoBE Is Nothing Then

                                objListResultadoFinal = New List(Of SunarpBE)
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

                                    If itemBE.RazonSocial Is Nothing Then
                                        strRazonSocial = ""
                                    Else
                                        strRazonSocial = itemBE.RazonSocial.Trim()
                                    End If

                                    If itemBE.TipoDocumento Is Nothing Then
                                        strTipoDocumento = ""
                                    Else
                                        strTipoDocumento = itemBE.TipoDocumento.Trim()
                                    End If

                                    If itemBE.NumeroDocumento Is Nothing Then
                                        strNroDocumento = ""
                                    Else
                                        strNroDocumento = itemBE.NumeroDocumento.Trim()
                                    End If

                                    If itemBE.NumeroPartida Is Nothing Then
                                        strNumPartida = ""
                                    Else
                                        strNumPartida = itemBE.NumeroPartida.Trim()
                                    End If

                                    If itemBE.Registro Is Nothing Then
                                        strRegistro = ""
                                    Else
                                        strRegistro = itemBE.Registro.Trim()
                                    End If

                                    If itemBE.NumeroPlaca Is Nothing Then
                                        strNumPlaca = ""
                                    Else
                                        strNumPlaca = itemBE.NumeroPlaca.Trim()
                                    End If

                                    If itemBE.Zona Is Nothing Then
                                        strZona = ""
                                    Else
                                        strZona = itemBE.Zona.Trim()
                                    End If

                                    If itemBE.Oficina Is Nothing Then
                                        strOficina = ""
                                    Else
                                        strOficina = itemBE.Oficina.Trim()
                                    End If

                                    If itemBE.Estado Is Nothing Then
                                        strEstado = ""
                                    Else
                                        strEstado = itemBE.Estado.Trim()
                                    End If

                                    objItemBE = New SunarpBE
                                    objItemBE.ApePaterno = strApePaterno
                                    objItemBE.ApeMaterno = strApeMaterno
                                    objItemBE.Nombres = strNombres
                                    objItemBE.RazonSocial = strRazonSocial
                                    objItemBE.TipoDocumento = strTipoDocumento
                                    objItemBE.NroDocumento = strNroDocumento
                                    objItemBE.NumPartida = strNumPartida
                                    objItemBE.Registro = strRegistro
                                    objItemBE.NumPlaca = strNumPlaca
                                    objItemBE.Zona = strZona
                                    objItemBE.Oficina = strOficina
                                    objItemBE.Estado = strEstado
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

            End If

            Me.DatosMemoria = objDatosMemoriaBE
            Me.hdnConsulta.Value = Constantes.ConsultoSW.K_SI

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Sub RegistrarLog(ByRef intCodLog As Integer)
        Try
            Dim strMsjResultado As String = String.Empty, strRazonSocial As String = String.Empty, strApePaterno As String = String.Empty, _
                strApeMaterno As String = String.Empty, strNombres As String = String.Empty, strTipoParticipante As String = String.Empty
            Dim objLista As List(Of SunarpBE) = Nothing
            Dim objRegistroBE As SunarpBE = Nothing, objDatosMemoria As SunarpBE = Nothing
            Dim intResultado As Integer = 0, intCodResultado As Integer = 0, intTipoConsulta As Integer = 0
            Dim objSunarpBL As SunarpBL = Nothing

            objDatosMemoria = Me.DatosMemoria
            If Not objDatosMemoria Is Nothing Then

                intCodResultado = objDatosMemoria.NCODRESULTADO

                If objDatosMemoria.VMENSAJE_RESULTADO Is Nothing Then
                    strMsjResultado = ""
                Else
                    strMsjResultado = objDatosMemoria.VMENSAJE_RESULTADO
                End If

                intTipoConsulta = objDatosMemoria.TipoConsulta

                If objDatosMemoria.TipoParticipante Is Nothing Then
                    strTipoParticipante = ""
                Else
                    strTipoParticipante = objDatosMemoria.TipoParticipante
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

                If objDatosMemoria.RazonSocial Is Nothing Then
                    strRazonSocial = ""
                Else
                    strRazonSocial = objDatosMemoria.RazonSocial
                End If

                If objDatosMemoria.objLista Is Nothing Then
                    objLista = Nothing
                Else
                    objLista = objDatosMemoria.objLista
                End If
            End If

            objRegistroBE = New SunarpBE
            objRegistroBE.TipoServicio = Constantes.TipoServicio.K_SUNARP_TITU_BIENES
            objRegistroBE.VUSER = FachadaSesion.Usuario.CodigoUsuario.Trim()
            objRegistroBE.VCODAPLI = Constantes.K_COD_APPLI
            objRegistroBE.NCODRESULTADO = intCodResultado
            objRegistroBE.VMENSAJE_RESULTADO = strMsjResultado
            objRegistroBE.NCODACCION = Constantes.Accion.K_EXPORTAR
            objRegistroBE.DFECHA = Now
            objRegistroBE.TipoConsulta = intTipoConsulta
            objRegistroBE.TipoParticipante = strTipoParticipante
            objRegistroBE.ApePaterno = strApePaterno
            objRegistroBE.ApeMaterno = strApeMaterno
            objRegistroBE.Nombres = strNombres
            objRegistroBE.RazonSocial = strRazonSocial
            objRegistroBE.objLista = objLista
            objSunarpBL = New SunarpBL()
            intResultado = objSunarpBL.RegistrarLog_TB(objRegistroBE)

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

            Me.DatosMemoria.RutaArchivoPDF = System.IO.Path.Combine(strDirectorio, "Reporte_SUNARP_TitularidadBienes_" & strDia & strMes & Now.Year.ToString() & Now.Hour.ToString() & Now.Minute.ToString() & Now.Second.ToString() & Constantes.K_EXTENSION_PDF)

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
                strTipoParticipante As String = String.Empty, strRazonSocial As String = String.Empty, strTipoParticipanteF As String = String.Empty
            Dim rptReporte As Microsoft.Reporting.WebForms.ReportViewer
            Dim lstParametros As List(Of ReportParameter)
            Dim warnings() As Warning = Nothing
            Dim streams() As String = Nothing
            Dim renderedBytes() As Byte = Nothing
            Dim objLista As List(Of SunarpBE) = Nothing

            strFormat = Constantes.K_PDF
            strRutaReporte = Constantes.ConfiguracionReportePDF.K_URL_CARPETA_REPORTE

            If Me.DatosMemoria.NCODRESULTADO = Constantes.Cod_Resultado.K_OK Then
                strPath = System.IO.Path.Combine(Server.MapPath(strRutaReporte), Constantes.ConfiguracionReportePDF.K_RDLC_REPORTE_SUNARP_TB_OK)
            ElseIf Me.DatosMemoria.NCODRESULTADO = Constantes.Cod_Resultado.K_Error Then
                strPath = System.IO.Path.Combine(Server.MapPath(strRutaReporte), Constantes.ConfiguracionReportePDF.K_RDLC_REPORTE_SUNARP_TB_ERROR)
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
            strTipoParticipante = Me.DatosMemoria.TipoParticipante
            If strTipoParticipante = Constantes.TipoConsulta_SUNARP_TITULARIDADB_TXT.K_POR_NOMBRES Then
                strTipoParticipanteF = "Por Nombres (" & strTipoParticipante & ")"
            ElseIf strTipoParticipante = Constantes.TipoConsulta_SUNARP_TITULARIDADB_TXT.K_POR_RAZONSOCIAL Then
                strTipoParticipanteF = "Por Razón Social (" & strTipoParticipante & ")"
            End If
            strApePaterno = Me.DatosMemoria.ApePaterno
            strApeMaterno = Me.DatosMemoria.ApeMaterno
            strNombres = Me.DatosMemoria.Nombres
            strRazonSocial = Me.DatosMemoria.RazonSocial

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
            lstParametros.Add(New ReportParameter("pTipoParticipante", strTipoParticipanteF))
            lstParametros.Add(New ReportParameter("pApePaterno", strApePaterno))
            lstParametros.Add(New ReportParameter("pApeMaterno", strApeMaterno))
            lstParametros.Add(New ReportParameter("pNombres", strNombres))
            lstParametros.Add(New ReportParameter("pRazonSocial", strRazonSocial))

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
