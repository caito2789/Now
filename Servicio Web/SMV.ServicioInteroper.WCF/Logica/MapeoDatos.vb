Imports SMV.ServicioInteroper.BE
Imports SMV.ServicioInteroper.BL
Imports System.IO
Imports System.Configuration

Public Class MapeoDatos
    Implements IDisposable

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

    Public Function ReniecConsultaDNI(oEntrada As PersonaBE) As ReniecConsultaDNIBE
        Dim oResultadoBL As New ResultadoBL()
        Dim listaParamDefecto = oResultadoBL.ListarTablaHija(Constantes.TablaPadre.K_CONS_RENIEC)
        Dim listaError = oResultadoBL.ListarTablaHija(Constantes.TablaPadre.K_ERROR_RENIEC)
        Dim strSesion As String = ""

        '1. Autenticacion al Servicio RENIEC:
        Dim user = listaParamDefecto.Find(Function(a) a.IdTablaHija = Constantes.TH_CONS_RENIEC.K_USER).Valor1
        Dim pass = listaParamDefecto.Find(Function(a) a.IdTablaHija = Constantes.TH_CONS_RENIEC.K_PASS).Valor1
        Dim ServReniec As New RENIEC_Autenticacion.WSAuthenticationClient
        strSesion = ServReniec.getTicket(user, pass)

        'Identificar Errores del Servicio - Autenticación
        Dim listaA = listaError.FindAll(Function(e) e.Valor1.Trim = strSesion.Trim)
        If listaA.Count > 0 Then
            Dim strMensajeErrorA = String.Format("({0}) AUTENTICACION: {1}", listaA(0).Valor1, listaA(0).Descripcion)
            Throw New Exception(strMensajeErrorA)
        End If


        '2. Consultar servicio:
        Dim CodUser = listaParamDefecto.Find(Function(a) a.IdTablaHija = Constantes.TH_CONS_RENIEC.K_COD_USER).Valor1
        Dim CodTransac = listaParamDefecto.Find(Function(a) a.IdTablaHija = Constantes.TH_CONS_RENIEC.K_COD_TRANSAC).Valor1
        Dim CodEntidad = listaParamDefecto.Find(Function(a) a.IdTablaHija = Constantes.TH_CONS_RENIEC.K_COD_ENTIDAD).Valor1
        Dim xmlDocumento = "<IN>" & _
                            "<CONSULTA>" & _
                                String.Format("<DNI>{0}</DNI>", oEntrada.DNI) & _
                            "</CONSULTA>" & _
                            "<IDENTIFICACION>" & _
                                String.Format("<CODUSER>{0}</CODUSER>", CodUser) & _
                                String.Format("<CODTRANSAC>{0}</CODTRANSAC>", CodTransac) & _
                                String.Format("<CODENTIDAD>{0}</CODENTIDAD>", CodEntidad) & _
                                String.Format("<SESION>{0}</SESION>", strSesion) & _
                            "</IDENTIFICACION>" & _
                           "</IN>"

        Dim ServReniecCon As New RENIEC_Consulta.WSDataVerificationClient
        Dim salida = ServReniecCon.getDatavalidate(xmlDocumento)

        'Identificar Errores del Servicio - Consulta
        Dim lista = listaError.FindAll(Function(e) e.Valor1.Trim = salida.Trim)
        If lista.Count > 0 Then
            Dim strMensajeError = String.Format("({0}) CONSULTA: {1}", lista(0).Valor1, lista(0).Descripcion)
            Throw New Exception(strMensajeError)
        End If


        '3. Registrar Resultado
        Dim objResultado As New ReniecConsultaDNIBE
        Dim xml As XDocument = XDocument.Parse(salida)
        Dim resultadoXML = xml.Descendants("RESPUESTA")
        If resultadoXML.Count > 0 Then
            For Each nodo As XElement In resultadoXML
                Dim Nnombre = nodo.Descendants("NOMBRES").FirstOrDefault
                objResultado.Nombres = IIf(IsNothing(Nnombre), "", Nnombre.Value.Trim)

                Dim NApePaterno = nodo.Descendants("APPAT").FirstOrDefault
                objResultado.ApePaterno = IIf(IsNothing(NApePaterno), "", NApePaterno.Value.Trim)

                Dim NApeMaterno = nodo.Descendants("APMAT").FirstOrDefault
                objResultado.ApeMaterno = IIf(IsNothing(NApeMaterno), "", NApeMaterno.Value.Trim)

                Dim NFechaNacimiento = nodo.Descendants("FENAC").FirstOrDefault
                objResultado.FechaNacimiento = IIf(IsNothing(NFechaNacimiento), "", NFechaNacimiento.Value.Trim)

                Dim NSexo = nodo.Descendants("SEXO").FirstOrDefault
                objResultado.Sexo = IIf(IsNothing(NSexo), "", NSexo.Value.Trim)
            Next

            oResultadoBL.Registrar_ReniecConsultaDNI(oEntrada, objResultado)
        Else
            Throw New Exception("SERVICIO INTERNO: No se encontraron Resultados para la consulta")
        End If

        Return objResultado
    End Function

    Public Function InpeAntecedentesPenales(oEntrada As PersonaBE) As String
        Dim oResultadoBL As New ResultadoBL()
        Dim listaParamDefecto = oResultadoBL.ListarTablaHija(Constantes.TablaPadre.K_CONS_INPE_ANTPENAL)

        '1. Consultar servicio:
        Dim MotivoConsulta = listaParamDefecto.Find(Function(a) a.IdTablaHija = Constantes.TH_CONS_INPE.K_MOTIVO_CONS).Valor1
        Dim ProcesoEntidadConsultante = listaParamDefecto.Find(Function(a) a.IdTablaHija = Constantes.TH_CONS_INPE.K_PROC_ENTIDAD_CONS).Valor1
        Dim RucEntidadConsultante = listaParamDefecto.Find(Function(a) a.IdTablaHija = Constantes.TH_CONS_INPE.K_RUC_ENTIDAD_CONS).Valor1
        Dim DniPersonaConsultante = listaParamDefecto.Find(Function(a) a.IdTablaHija = Constantes.TH_CONS_INPE.K_DNI_PERSONA_CONS).Valor1
        Dim AudNombrePC = listaParamDefecto.Find(Function(a) a.IdTablaHija = Constantes.TH_CONS_INPE.K_AUD_NOMBRE_PC).Valor1
        Dim AudIP = listaParamDefecto.Find(Function(a) a.IdTablaHija = Constantes.TH_CONS_INPE.K_AUD_IP).Valor1
        Dim AudNombreUsuario = listaParamDefecto.Find(Function(a) a.IdTablaHija = Constantes.TH_CONS_INPE.K_AUD_NOMBRE_USER).Valor1
        Dim AudDireccionMAC = listaParamDefecto.Find(Function(a) a.IdTablaHija = Constantes.TH_CONS_INPE.K_AUD_DIREC_MAC).Valor1

        Dim oServEntrada As New INPE_Consulta.verificarAntecedentesPenales
        oServEntrada.xApellidoPaterno = oEntrada.ApePaterno
        oServEntrada.xApellidoMaterno = oEntrada.ApeMaterno
        oServEntrada.xNombre1 = oEntrada.Nombre1
        oServEntrada.xNombre2 = oEntrada.Nombre2
        oServEntrada.xNombre3 = oEntrada.Nombre3
        oServEntrada.xDni = oEntrada.DNI
        oServEntrada.xMotivoConsulta = MotivoConsulta
        oServEntrada.xProcesoEntidadConsultante = ProcesoEntidadConsultante
        oServEntrada.xRucEntidadConsultante = RucEntidadConsultante
        oServEntrada.xDniPersonaConsultante = DniPersonaConsultante
        oServEntrada.xAudNombrePC = AudNombrePC
        oServEntrada.xAudIP = AudIP
        oServEntrada.xAudNombreUsuario = AudNombreUsuario
        oServEntrada.xAudDireccionMAC = AudDireccionMAC

        Dim ServInpeCon As New INPE_Consulta.verificacionAntecedentesPenalesServicioClient
        Dim salida = ServInpeCon.verificarAntecedentesPenales(oServEntrada)


        '2. Registrar Resultado
        Dim objResultado = ""
        Dim strFlag As String
        'Caso: respuesta exitosa (siempre?)
        objResultado = String.Format("({0}) {1}", salida.xCodigoRespuesta, salida.xMensajeRespuesta)
        Select Case salida.xCodigoRespuesta
            Case "0000"
                strFlag = Constantes.Flag.K_NO
            Case Else
                strFlag = Constantes.Flag.K_SI
        End Select
        oResultadoBL.Registrar_InpeAntecedentesPenales(oEntrada, objResultado, strFlag)

        Return objResultado
    End Function

    Public Function PJAntecedentesJudiciales(oEntrada As PersonaBE) As String
        Dim oResultadoBL As New ResultadoBL()
        Dim listaResultado = oResultadoBL.ListarTablaHija(Constantes.TablaPadre.K_RESULTADO_PJ)

        '1. Consultar servicio:
        Dim apepat = oEntrada.ApePaterno
        Dim apemat = oEntrada.ApeMaterno
        Dim nombres = oEntrada.Nombres

        Dim ServPJCon As New PJ_Consulta.ServiceAntecedenteJudicialClient
        Dim salida = ServPJCon.getAntecedenteJudicial(apepat, apemat, nombres)


        '2. Registrar Resultado
        Dim objResultado = ""
        Dim strFlag As String
        Dim listaA = listaResultado.FindAll(Function(e) e.Valor1.ToUpper.Trim = salida.ToUpper.Trim)
        If listaA.Count > 0 Then
            'Caso: respuesta exitosa
            objResultado = String.Format("({0}) {1}", listaA(0).Valor1, listaA(0).Descripcion)
            Select Case listaA(0).IdTablaHija
                Case Constantes.TH_RESULTADO_PJ.K_TIENE_ANTEC
                    strFlag = Constantes.Flag.K_SI
                Case Else
                    strFlag = Constantes.Flag.K_NO
            End Select

            oResultadoBL.Registrar_PJAntecedentesJudiciales(oEntrada, objResultado, strFlag)
        Else
            'Caso: error:
            Throw New Exception(salida)
        End If

        Return objResultado
    End Function

    Public Function MininterAntecedentesPoliciales(oEntrada As PersonaBE) As String
        Dim oResultadoBL As New ResultadoBL()
        Dim listaParamDefecto = oResultadoBL.ListarTablaHija(Constantes.TablaPadre.K_CONS_MININTER_ANTPOLI)
        Dim listaResultado = oResultadoBL.ListarTablaHija(Constantes.TablaPadre.K_RESULTADO_MinInter)
        Dim listaError = oResultadoBL.ListarTablaHija(Constantes.TablaPadre.K_ERROR_MinInter)

        '1. Consultar servicio:
        Dim vUsuario = listaParamDefecto.Find(Function(a) a.IdTablaHija = Constantes.TH_CONS_MININTER.K_USUARIO).Valor1
        Dim vClave = listaParamDefecto.Find(Function(a) a.IdTablaHija = Constantes.TH_CONS_MININTER.K_CLAVE).Valor1
        Dim vEntidadconsulta = listaParamDefecto.Find(Function(a) a.IdTablaHija = Constantes.TH_CONS_MININTER.K_ENTIDAD_CONS).Valor1
        Dim vDNIconsulta = listaParamDefecto.Find(Function(a) a.IdTablaHija = Constantes.TH_CONS_MININTER.K_DNI_CONS).Valor1

        Dim ServMICon As New MININTER_Consulta.WS_persona_rq_antClient
        Dim salida As String = ""
        If oEntrada.TipoConsulta = Constantes.MININTER_TipoConsulta.K_DNI Then
            salida = ServMICon.consultaDniGeneral(vUsuario, vClave, oEntrada.DNI, vEntidadconsulta, vDNIconsulta)
        Else
            salida = ServMICon.consultaNombreGeneral(vUsuario, vClave, oEntrada.Nombres, oEntrada.ApePaterno, oEntrada.ApeMaterno, vEntidadconsulta, vDNIconsulta)
        End If


        '2. Registrar Resultado
        Dim objResultado = ""
        Dim strFlag As String
        Dim listaA = listaResultado.FindAll(Function(e) e.Valor1.ToUpper.Trim = salida.ToUpper.Trim)
        If listaA.Count > 0 Then
            'Caso: respuesta exitosa
            objResultado = String.Format("({0}) {1}", listaA(0).Valor1, listaA(0).Descripcion)
            Select Case listaA(0).IdTablaHija
                Case Constantes.TH_RESULTADO_MININTER.K_TIENE_ANTEC
                    strFlag = Constantes.Flag.K_SI
                Case Else
                    strFlag = Constantes.Flag.K_NO
            End Select

            oResultadoBL.Registrar_MininterAntecedentesPoliciales(oEntrada, objResultado, strFlag)
        Else
            'Caso: error:
            Dim listaE = listaError.FindAll(Function(e) e.Valor1.Trim = salida.Trim)
            If listaE.Count > 0 Then
                Dim strMensajeError = String.Format("({0}) {1}", listaE(0).Valor1, listaE(0).Descripcion)
                Throw New Exception(strMensajeError)
            Else
                Throw New Exception(salida)
            End If
        End If

        Return objResultado
    End Function

    Public Function SunarpTitularidadBienes(oEntrada As PersonaBE) As List(Of SunarpTitularidadBienesBE)
        Dim oResultadoBL As New ResultadoBL()

        '1. Consultar servicio:
        Dim ServSUNARPTit As New SUNARP_Consulta.PIDEWSServiceClient
        Dim salida = ServSUNARPTit.buscarTitularidad(oEntrada.TipoParticipante, oEntrada.ApePaterno, oEntrada.ApeMaterno, oEntrada.Nombres, oEntrada.RazonSocial).ToList

        '2. Registrar Resultado
        Dim listaResultado As New List(Of SunarpTitularidadBienesBE)
        If salida.Count > 0 Then
            Dim objResultado As SunarpTitularidadBienesBE
            For Each item As SUNARP_Consulta.respuestaTitularidadBean In salida
                objResultado = New SunarpTitularidadBienesBE

                objResultado.Registro = If(IsNothing(item.registro), "", item.registro.Trim)
                objResultado.ApePaterno = If(IsNothing(item.apPaterno), "", item.apPaterno.Trim)
                objResultado.ApeMaterno = If(IsNothing(item.apMaterno), "", item.apMaterno.Trim)
                objResultado.Nombres = If(IsNothing(item.nombre), "", item.nombre.Trim)
                objResultado.TipoDocumento = If(IsNothing(item.tipoDocumento), "", item.tipoDocumento.Trim)
                objResultado.NumeroDocumento = If(IsNothing(item.numeroDocumento), "", item.numeroDocumento.Trim)
                objResultado.NumeroPartida = If(IsNothing(item.numeroPartida), "", item.numeroPartida.Trim)
                objResultado.NumeroPlaca = If(IsNothing(item.numeroPlaca), "", item.numeroPlaca.Trim)
                objResultado.Estado = If(IsNothing(item.estado), "", item.estado.Trim)
                objResultado.Zona = If(IsNothing(item.zona), "", item.zona.Trim)
                objResultado.Oficina = If(IsNothing(item.oficina), "", item.oficina.Trim)
                objResultado.RazonSocial = If(IsNothing(item.razonSocial), "", item.razonSocial.Trim)
                listaResultado.Add(objResultado)
            Next
            oResultadoBL.Registrar_SunarpTitularidadBienes(oEntrada, listaResultado)
        Else
            Throw New Exception("SERVICIO INTERNO: No se encontraron Resultados para la consulta")
        End If

        Return listaResultado
    End Function

    Public Function SunarpVigenciaPoder(oEntrada As PersonaBE) As SunarpVigenciaPoderBE
        Dim oResultadoBL As New ResultadoBL()

        '1. Consultar servicio:
        Dim ServSUNARPVig As New SUNARP_Consulta.PIDEWSServiceClient
        Dim salida = ServSUNARPVig.generarVigencia(oEntrada.Zona, oEntrada.Oficina, oEntrada.Partida, oEntrada.Asiento, oEntrada.ApePaterno, _
                                                   oEntrada.ApeMaterno, oEntrada.Nombres, oEntrada.Cargo, oEntrada.Email)

        '2. Registrar Resultado
        Dim objResultado As New SunarpVigenciaPoderBE
        If Not IsNothing(salida) Then
            objResultado.Estado = If(IsNothing(salida.estado), "", salida.estado.Trim)
            objResultado.Solicitud = If(IsNothing(salida.solicitud), "", salida.solicitud)
            objResultado.Fecha = If(IsNothing(salida.fecha), "", salida.fecha)

            oResultadoBL.Registrar_SunarpVigenciaPoder(oEntrada, objResultado)
        Else
            Throw New Exception("SERVICIO INTERNO: No se encontraron Resultados para la consulta")
        End If

        Return objResultado
    End Function

    Public Function SunarpListaAsiento(oEntrada As PersonaBE) As List(Of SunarpAsientoBE)
        Dim oResultadoBL As New ResultadoBL()

        '1. Consultar servicio:
        Dim ServSUNARPAsi As New SUNARP_Consulta.PIDEWSServiceClient
        Dim salida = ServSUNARPAsi.listarAsientos(oEntrada.Zona, oEntrada.Oficina, oEntrada.Partida, oEntrada.Registro)

        '2. Registrar Resultado
        Dim listaResultado As New List(Of SunarpAsientoBE)
        If Not IsNothing(salida) Then
            Dim ind As Boolean = False

            'ASIENTOS:
            If Not IsNothing(salida.listAsientos) AndAlso salida.listAsientos.Count > 0 Then
                Dim listaTemp As New List(Of SunarpAsientoBE)
                Dim objResultado As SunarpAsientoBE
                For Each itemAsiento As SUNARP_Consulta.asientosBean In salida.listAsientos
                    For Each itemAsientoPag As SUNARP_Consulta.numeroPaginasBean In itemAsiento.listPag
                        objResultado = New SunarpAsientoBE
                        objResultado.Transaccion = salida.transaccion
                        objResultado.TotalPag = salida.nroTotalPag

                        objResultado.IdImgAsiento = itemAsiento.idImgAsiento
                        objResultado.CantPaginas = itemAsiento.numPag
                        objResultado.Tipo = itemAsiento.tipo.Trim

                        objResultado.NroPagRef = itemAsientoPag.nroPagRef
                        objResultado.NroPagina = itemAsientoPag.pagina

                        listaTemp.Add(objResultado)
                    Next
                Next
                ind = True
                listaResultado.AddRange(listaTemp)
                oResultadoBL.Registrar_SunarpListaAsiento(oEntrada, listaTemp)
            End If

            'FICHAS:
            If Not IsNothing(salida.listFichas) AndAlso salida.listFichas.Count > 0 Then
                Dim listaTemp As New List(Of SunarpAsientoBE)
                Dim objResultado As SunarpAsientoBE
                For Each itemAsiento As SUNARP_Consulta.fichaBean In salida.listFichas
                    For Each itemAsientoPag As SUNARP_Consulta.numeroPaginasBean In itemAsiento.listPag
                        objResultado = New SunarpAsientoBE
                        objResultado.Transaccion = salida.transaccion
                        objResultado.TotalPag = salida.nroTotalPag

                        objResultado.IdImgAsiento = itemAsiento.idImgFicha
                        objResultado.CantPaginas = itemAsiento.numPag
                        objResultado.Tipo = itemAsiento.tipo.Trim

                        objResultado.NroPagRef = itemAsientoPag.nroPagRef
                        objResultado.NroPagina = itemAsientoPag.pagina

                        listaTemp.Add(objResultado)
                    Next
                Next
                ind = True
                listaResultado.AddRange(listaTemp)
                oResultadoBL.Registrar_SunarpListaAsiento(oEntrada, listaTemp)
            End If

            'FOLIOS
            If Not IsNothing(salida.listFolios) AndAlso salida.listFolios.Count > 0 Then
                Dim listaTemp As New List(Of SunarpAsientoBE)
                Dim objResultado As SunarpAsientoBE
                For Each itemAsiento As SUNARP_Consulta.tomoFolioBean In salida.listFolios
                    'For Each itemAsientoPag As SUNARP_Consulta.numeroPaginasBean In itemAsiento.
                    objResultado = New SunarpAsientoBE
                    objResultado.Transaccion = salida.transaccion
                    objResultado.TotalPag = salida.nroTotalPag

                    objResultado.IdImgAsiento = itemAsiento.idImgFolio
                    objResultado.CantPaginas = 1 'itemAsiento.numPag
                    objResultado.Tipo = itemAsiento.tipo.Trim

                    objResultado.NroPagRef = itemAsiento.nroPagRef
                    objResultado.NroPagina = itemAsiento.pagina

                    listaTemp.Add(objResultado)
                    'Next
                Next
                ind = True
                listaResultado.AddRange(listaTemp)
                oResultadoBL.Registrar_SunarpListaAsiento(oEntrada, listaTemp)
            End If

            If ind = False Then
                Throw New Exception("SERVICIO INTERNO: No se encontraron Asientos para la consulta")
            End If

        Else
            Throw New Exception("SERVICIO INTERNO: No se encontraron Resultados para la consulta")
        End If

        Return listaResultado.OrderBy(Function(l) Convert.ToInt32(l.NroPagRef)).ThenBy(Function(l) Convert.ToInt32(l.NroPagina)).ToList()
    End Function

    Public Function SunarpVerImagenAsiento(oEntrada As AsientoBE) As String
        Dim oResultadoBL As New ResultadoBL()
        Dim rutaImagen As String = ""
        'JPG

        'EXISTE ASIENTO:
        Dim listaAsientoImagen = oResultadoBL.ListarAsientoDetalle(oEntrada)
        If listaAsientoImagen.Count > 0 Then
            rutaImagen = listaAsientoImagen(0).RutaImagen.Trim

            'No Existe imagen de asiento: -->GENERAR:
            If rutaImagen = "" Then

                '1. Consultar servicio:
                Dim ServSUNARPver As New SUNARP_Consulta.PIDEWSServiceClient
                Dim salida = ServSUNARPver.verAsiento(oEntrada.Transaccion, oEntrada.IdImg, oEntrada.Tipo, oEntrada.NroTotalPag, oEntrada.NroPagRef, oEntrada.Pagina)

                '2. Registrar Resultado
                Dim ms As MemoryStream = New MemoryStream(salida)
                Dim imagen = New System.Drawing.Bitmap(ms)

                Dim directorio = ConfigurationManager.AppSettings("DirectorioImgAsiento").ToString
                Dim rutaI = String.Format("{0}\{1}_{2}_{3}.jpg", directorio, oEntrada.Transaccion, oEntrada.NroPagRef, Date.Now.ToString("HHmmss"))

                imagen.Save(rutaI, System.Drawing.Imaging.ImageFormat.Jpeg)
                rutaImagen = rutaI

                oResultadoBL.Registrar_SunarpVerImagenAsiento(oEntrada, rutaImagen)
            End If

            oResultadoBL.RegistrarLog_SunarpVerImagenAsiento(True, "", oEntrada, rutaImagen)
        Else
            Throw New Exception("SERVICIO INTERNO: El asiento no se encuentra registrado en la BD")
        End If

        Return rutaImagen
    End Function

    Public Function SuneduGradosTitulos(oEntrada As PersonaBE) As List(Of SuneduGradoAcademicoBE)
        Dim oResultadoBL As New ResultadoBL()
        Dim listaParamDefecto = oResultadoBL.ListarTablaHija(Constantes.TablaPadre.K_CONS_SUNEDU_GRADOS)

        '1. Consultar servicio:
        Dim usuario = listaParamDefecto.Find(Function(a) a.IdTablaHija = Constantes.TH_CONS_SUNEDU.K_USUARIO).Valor1
        Dim clave = listaParamDefecto.Find(Function(a) a.IdTablaHija = Constantes.TH_CONS_SUNEDU.K_CLAVE).Valor1
        Dim idEntidad = listaParamDefecto.Find(Function(a) a.IdTablaHija = Constantes.TH_CONS_SUNEDU.K_ID_ENTIDAD).Valor1
        Dim fecha = listaParamDefecto.Find(Function(a) a.IdTablaHija = Constantes.TH_CONS_SUNEDU.K_FECHA).Valor1
        Dim hora = listaParamDefecto.Find(Function(a) a.IdTablaHija = Constantes.TH_CONS_SUNEDU.K_HORA).Valor1
        Dim mac_wsServer = listaParamDefecto.Find(Function(a) a.IdTablaHija = Constantes.TH_CONS_SUNEDU.K_MAC_WSSERVER).Valor1
        Dim ip_wsServer = listaParamDefecto.Find(Function(a) a.IdTablaHija = Constantes.TH_CONS_SUNEDU.K_IP_WSSERVER).Valor1
        Dim ip_wsUser = listaParamDefecto.Find(Function(a) a.IdTablaHija = Constantes.TH_CONS_SUNEDU.K_IP_WSUSER).Valor1

        Dim _usuarioType As New SUNEDU_Consulta.UsuarioType()
        _usuarioType.usuario = usuario
        _usuarioType.clave = clave
        _usuarioType.idEntidad = idEntidad

        Dim _operacionType As New SUNEDU_Consulta.OperacionType()
        _operacionType.fecha = fecha
        _operacionType.hora = hora
        _operacionType.mac_wsServer = mac_wsServer
        _operacionType.ip_wsServer = ip_wsServer
        _operacionType.ip_wsUser = ip_wsUser

        Dim ServSUNEDUGra As New SUNEDU_Consulta.ServiceGradoAcademicoClient
        Dim salida = ServSUNEDUGra.getGradoAcademico(_usuarioType, _operacionType, oEntrada.DNI)


        '2. Registrar Resultado
        Dim listaResultado As New List(Of SuneduGradoAcademicoBE)
        If Not IsNothing(salida) Then
            If Not IsNothing(salida.listaGradoAcademico) AndAlso salida.listaGradoAcademico.Count > 0 Then
                Dim objResultado As SuneduGradoAcademicoBE
                For Each itemAsiento As SUNEDU_Consulta.gradoAcademico In salida.listaGradoAcademico
                    objResultado = New SuneduGradoAcademicoBE()
                    objResultado.AbreviaturaTitulo = If(IsNothing(itemAsiento.abreviaturaTitulo), "", itemAsiento.abreviaturaTitulo.Trim)
                    objResultado.ApeMaterno = If(IsNothing(itemAsiento.apellidoMaterno), "", itemAsiento.apellidoMaterno)
                    objResultado.ApePaterno = If(IsNothing(itemAsiento.apellidoPaterno), "", itemAsiento.apellidoPaterno)
                    objResultado.Nombres = If(IsNothing(itemAsiento.nombres), "", itemAsiento.nombres)
                    objResultado.NumeroDocumento = If(IsNothing(itemAsiento.nroDocumento), "", itemAsiento.nroDocumento)
                    objResultado.Pais = If(IsNothing(itemAsiento.pais), "", itemAsiento.pais)
                    objResultado.Tipodocumento = If(IsNothing(itemAsiento.tipoDocumento), "", itemAsiento.tipoDocumento)
                    objResultado.TituloProfesional = If(IsNothing(itemAsiento.tituloProfesional), "", itemAsiento.tituloProfesional)
                    objResultado.Universidad = If(IsNothing(itemAsiento.universidad), "", itemAsiento.universidad)
                    objResultado.Especialidad = If(IsNothing(itemAsiento.especialidad), "", itemAsiento.especialidad)

                    listaResultado.Add(objResultado)
                Next
                oResultadoBL.Registrar_SuneduGradosTitulos(oEntrada, listaResultado)
            Else
                If Not IsNothing(salida.dGenerica) Then
                    Throw New Exception(String.Format("({0}) {1}", salida.cGenerico, salida.dGenerica))
                Else
                    Throw New Exception("SERVICIO INTERNO: No se encontraron Grados o Títulos para la consulta")
                End If
            End If
        Else
            Throw New Exception("SERVICIO INTERNO: No se encontraron Resultados para la consulta")
        End If

        Return listaResultado
    End Function

    Public Function MigracionesCarnetExtranjeria(oEntrada As PersonaBE) As MigracionesCarnetExtranjeriaBE
        Dim oResultadoBL As New ResultadoBL()
        Dim listaParamDefecto = oResultadoBL.ListarTablaHija(Constantes.TablaPadre.K_CONS_MIGRA_CAREXT)
        Dim listaError = oResultadoBL.ListarTablaHija(Constantes.TablaPadre.K_ERROR_MIGRACIONES)

        '1. Consultar servicio:
        Dim strCodInstitucion = listaParamDefecto.Find(Function(a) a.IdTablaHija = Constantes.TH_CONS_MIGRACION.K_COD_INSTITUCION).Valor1
        Dim strMac = listaParamDefecto.Find(Function(a) a.IdTablaHija = Constantes.TH_CONS_MIGRACION.K_MAC).Valor1
        Dim strNroIp = listaParamDefecto.Find(Function(a) a.IdTablaHija = Constantes.TH_CONS_MIGRACION.K_NRO_IP).Valor1
        Dim strTipoDocumento = listaParamDefecto.Find(Function(a) a.IdTablaHija = Constantes.TH_CONS_MIGRACION.K_TIPO_DOC).Valor1

        Dim _solicitud As New MIGRACION_Consulta.solicitudBean()
        _solicitud.strCodInstitucion = strCodInstitucion
        _solicitud.strMac = strMac
        _solicitud.strNroIp = strNroIp
        _solicitud.strNumDocumento = oEntrada.NumeroDoc
        _solicitud.strTipoDocumento = strTipoDocumento

        Dim ServMIGRACIONCar As New MIGRACION_Consulta.EjecTransaccionCarExtraPcmClient
        Dim salida = ServMIGRACIONCar.consultarDocumento(_solicitud)


        '2. Registrar Resultado
        Dim objResultado As New MigracionesCarnetExtranjeriaBE
        If Not IsNothing(salida) Then
            If salida.strNumRespuesta = "0000" Then
                'Caso: respuesta exitosa
                objResultado.CalidadMigratoria = If(IsNothing(salida.strCalidadMigratoria), "", salida.strCalidadMigratoria.Trim)
                objResultado.Nombres = If(IsNothing(salida.strNombres), "", salida.strNombres)
                objResultado.PrimerApellido = If(IsNothing(salida.strPrimerApellido), "", salida.strPrimerApellido)
                objResultado.SegundoApellido = If(IsNothing(salida.strSegundoApellido), "", salida.strSegundoApellido)
                objResultado.NumRespuesta = If(IsNothing(salida.strNumRespuesta), "", salida.strNumRespuesta)

                oResultadoBL.Registrar_MigracionesCarnetExtranjeria(oEntrada, objResultado)
            Else
                'Caso: error:
                Dim listaE = listaError.FindAll(Function(e) e.Valor1.Trim = salida.strNumRespuesta.Trim)
                If listaE.Count > 0 Then
                    Dim strMensajeError = String.Format("({0}) {1}", listaE(0).Valor1, listaE(0).Descripcion)
                    Throw New Exception(strMensajeError)
                Else
                    Throw New Exception(salida.strNumRespuesta)
                End If
            End If
        Else
            Throw New Exception("SERVICIO INTERNO: No se encontraron Resultados para la consulta")
        End If

        Return objResultado
    End Function

    Public Sub LogErrores(Message, StackTrace)
        'Crear Carpeta Log:
        Dim Directorio = IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Log")
        If Not IO.Directory.Exists(Directorio) Then
            IO.Directory.CreateDirectory(Directorio)
        End If

        Dim texto As String = "(" & Date.Now & ") Message:" & vbCrLf & Message & vbCrLf &
                              "(" & Date.Now & ") StackTrace:" & vbCrLf & StackTrace & vbCrLf &
                              "----------------------------------------------------------------------------"
        Dim ArchivoLog As String = String.Format("{0}\LogError{1}.txt", Directorio, DateTime.Now.ToString("yyyyMMdd"))
        Dim tw As IO.TextWriter = New IO.StreamWriter(ArchivoLog, True)
        tw.WriteLine(texto)
        tw.Close()
    End Sub

    'Public Function ReniecConsultaDNI(oPersonaBE As PersonaBE) As ReniecConsultaDNIBE
    '    Dim resultado As New ReniecConsultaDNIBE
    '    resultado.Nombres = "JOSE LUIS"
    '    resultado.ApePaterno = "BUGARIN"
    '    resultado.ApeMaterno = "PECHE"
    '    resultado.FechaNacimiento = "19820424"
    '    resultado.Sexo = "M"
    '    Return resultado
    'End Function

    'Public Function InpeAntecedentesPenales(oPersonaBE As PersonaBE) As String
    '    Dim o As String = "(0000) El registro indicado no cuenta con antecedentes penales"
    '    Return o
    'End Function

    'Public Function PJAntecedentesJudiciales(oPersonaBE As PersonaBE) As String
    '    Dim o As String = "(Observado) Existen coincidencias con nombres de internos"
    '    Return o
    'End Function

    'Public Function MininterAntecedentesPoliciales(oPersonaBE As PersonaBE) As String
    '    Dim o As String = "(1) El dato consultado se encuentra registrado con antecedentes policiales vigentes"
    '    Return o
    'End Function

    'Public Function SunarpTitularidadBienes(oPersonaBE As PersonaBE) As List(Of SunarpTitularidadBienesBE)
    '    Dim o = New List(Of SunarpTitularidadBienesBE)
    '    o.Add(New SunarpTitularidadBienesBE With {.Registro = "PROPIEDAD INMUEBLE", .ApePaterno = "SANCHEZ", .ApeMaterno = "CHAVEZ", .Nombres = "JORGE", .TipoDocumento = "DNI", .NumeroDocumento = "1984274", .NumeroPartida = "11072986", .NumeroPlaca = "", .Estado = "ACTIVA", .Zona = "ZONA REGISTRAL XII - SEDE AREQUIPA", .Oficina = "AREQUIPA", .RazonSocial = ""})
    '    o.Add(New SunarpTitularidadBienesBE With {.Registro = "PROPIEDAD VEHICULAR", .ApePaterno = "SANCHEZ", .ApeMaterno = "CHAVEZ", .Nombres = "JORGE", .TipoDocumento = "DNI", .NumeroDocumento = "24960844", .NumeroPartida = "60591689", .NumeroPlaca = "12233X", .Estado = "ACTIVA", .Zona = "ZONA REGISTRAL X - SEDE CUSCO", .Oficina = "CUSCO", .RazonSocial = ""})
    '    o.Add(New SunarpTitularidadBienesBE With {.Registro = "PROPIEDAD VEHICULAR", .ApePaterno = "SANCHEZ", .ApeMaterno = "CHAVEZ", .Nombres = "JORGE", .TipoDocumento = "DNI", .NumeroDocumento = "45070667", .NumeroPartida = "50770824", .NumeroPlaca = "AAN358", .Estado = "ACTIVA", .Zona = "ZONA REGISTRAL IX - SEDE LIMA", .Oficina = "LIMA", .RazonSocial = ""})
    '    o.Add(New SunarpTitularidadBienesBE With {.Registro = "PROPIEDAD INMUEBLE", .ApePaterno = "SANCHEZ", .ApeMaterno = "CHAVEZ", .Nombres = "JORGE", .TipoDocumento = "", .NumeroDocumento = "", .NumeroPartida = "46129776", .NumeroPlaca = "", .Estado = "CERRADA", .Zona = "ZONA REGISTRAL IX - SEDE LIMA", .Oficina = "LIMA", .RazonSocial = ""})
    '    Return o
    'End Function

    'Public Function SunarpVigenciaPoder(oPersonaBE As PersonaBE) As SunarpVigenciaPoderBE
    '    Dim o As New SunarpVigenciaPoderBE
    '    o.Estado = "1"
    '    o.Solicitud = "77949"
    '    o.Fecha = "07/12/2016 12:08:31"
    '    Return o
    'End Function

    'Public Function SunarpListaAsiento(oPersonaBE As PersonaBE) As List(Of SunarpAsientoBE)
    '    Dim o = New List(Of SunarpAsientoBE)
    '    o.Add(New SunarpAsientoBE With {.Transaccion = "27040", .TotalPag = "23", .IdImgAsiento = "52056879", .CantPaginas = "1", .Tipo = "ASIENTO", .NroPagRef = "23", .NroPagina = "1"})
    '    o.Add(New SunarpAsientoBE With {.Transaccion = "27040", .TotalPag = "23", .IdImgAsiento = "43982671", .CantPaginas = "2", .Tipo = "ASIENTO", .NroPagRef = "21", .NroPagina = "1"})
    '    o.Add(New SunarpAsientoBE With {.Transaccion = "27040", .TotalPag = "23", .IdImgAsiento = "43982671", .CantPaginas = "2", .Tipo = "ASIENTO", .NroPagRef = "22", .NroPagina = "2"})
    '    o.Add(New SunarpAsientoBE With {.Transaccion = "27040", .TotalPag = "23", .IdImgAsiento = "42850678", .CantPaginas = "1", .Tipo = "ASIENTO", .NroPagRef = "20", .NroPagina = "1"})
    '    o.Add(New SunarpAsientoBE With {.Transaccion = "27040", .TotalPag = "23", .IdImgAsiento = "42850677", .CantPaginas = "1", .Tipo = "ASIENTO", .NroPagRef = "19", .NroPagina = "1"})
    '    o.Add(New SunarpAsientoBE With {.Transaccion = "27040", .TotalPag = "23", .IdImgAsiento = "42850676", .CantPaginas = "1", .Tipo = "ASIENTO", .NroPagRef = "18", .NroPagina = "1"})
    '    o.Add(New SunarpAsientoBE With {.Transaccion = "27040", .TotalPag = "23", .IdImgAsiento = "42850674", .CantPaginas = "3", .Tipo = "ASIENTO", .NroPagRef = "17", .NroPagina = "1"})
    '    o.Add(New SunarpAsientoBE With {.Transaccion = "27040", .TotalPag = "23", .IdImgAsiento = "42850674", .CantPaginas = "3", .Tipo = "ASIENTO", .NroPagRef = "16", .NroPagina = "2"})
    '    o.Add(New SunarpAsientoBE With {.Transaccion = "27040", .TotalPag = "23", .IdImgAsiento = "42850674", .CantPaginas = "3", .Tipo = "ASIENTO", .NroPagRef = "15", .NroPagina = "3"})
    '    o.Add(New SunarpAsientoBE With {.Transaccion = "27040", .TotalPag = "23", .IdImgAsiento = "42850673", .CantPaginas = "1", .Tipo = "ASIENTO", .NroPagRef = "14", .NroPagina = "1"})
    '    Return o
    'End Function

    'Public Function SunarpVerImagenAsiento(oAsientoBE As AsientoBE) As String
    '    Dim o As String = "\\10.0.100.34\Documentos\ImagenAsiento\s1.jpg"
    '    Return o
    'End Function

    'Public Function SuneduGradosTitulos(oPersonaBE As PersonaBE) As List(Of SuneduGradoAcademicoBE)
    '    Dim o = New List(Of SuneduGradoAcademicoBE)
    '    o.Add(New SuneduGradoAcademicoBE With {.AbreviaturaTitulo = "T", .ApeMaterno = "ESPINOZA", .ApePaterno = "LUCIANO", .Nombres = "VERONICA SILVERIA", .NumeroDocumento = "15738846", .Pais = "PERU", .Tipodocumento = "DNI", .TituloProfesional = "INGENIERO DE SISTEMAS", .Universidad = "UNIVERSIDAD DE LIMA", .Especialidad = ""})
    '    o.Add(New SuneduGradoAcademicoBE With {.AbreviaturaTitulo = "B", .ApeMaterno = "ESPINOZA", .ApePaterno = "LUCIANO", .Nombres = "VERONICA SILVERIA", .NumeroDocumento = "15738846", .Pais = "PERU", .Tipodocumento = "DNI", .TituloProfesional = "BACHILLER EN INGENIERIA DE SISTEMAS", .Universidad = "UNIVERSIDAD DE LIMA", .Especialidad = ""})
    '    o.Add(New SuneduGradoAcademicoBE With {.AbreviaturaTitulo = "M", .ApeMaterno = "ESPINOZA", .ApePaterno = "LUCIANO", .Nombres = "VERONICA SILVERIA", .NumeroDocumento = "15738846", .Pais = "PERU", .Tipodocumento = "DNI", .TituloProfesional = "MAGÍSTER EN ADMINISTRACIÓN, ESPECIALIDAD: ADMINISTRACIÓN", .Universidad = "UNIVERSIDAD ESAN", .Especialidad = "ADMINISTRACIÓN"})
    '    Return o
    'End Function

    'Public Function MigracionesCarnetExtranjeria(oPersonaBE As PersonaBE) As MigracionesCarnetExtranjeriaBE
    '    Dim o As New MigracionesCarnetExtranjeriaBE
    '    'Data Prueba:
    '    o.CalidadMigratoria = "INMIGRANTE"
    '    o.Nombres = "HELENA EIKO"
    '    o.NumRespuesta = "0000"
    '    o.PrimerApellido = "HOUMA BUSTAMANTE CRUZ"
    '    o.SegundoApellido = ""
    '    Return o
    'End Function

End Class
