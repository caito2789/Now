Imports SMV.ServicioInteroper.DA
Imports SMV.ServicioInteroper.BE

Public Class ResultadoBL
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

    Private _objResultadoDA As ResultadoDA
    Private _objTablaHijaDA As TablaHijaDA
    Sub New()
        _objResultadoDA = New ResultadoDA()
        _objTablaHijaDA = New TablaHijaDA()
    End Sub

    Public Function ListarTablaHija(ByVal idTablaPadre As Integer) As List(Of TablaHijaBE)
        Return _objTablaHijaDA.ListarTablaHija(idTablaPadre)
    End Function

    Public Function ListarAsientoDetalle(oEntrada As AsientoBE) As List(Of AsientoBE)
        Return _objResultadoDA.ListarAsientoDetalle(oEntrada)
    End Function


    Public Sub Registrar_ReniecConsultaDNI(entrada As PersonaBE, salida As ReniecConsultaDNIBE)
        _objResultadoDA.Registrar_ReniecConsultaDNI(entrada, salida)
        'Rergistrar Log:
        RegistrarLog_ReniecConsultaDNI(True, "", entrada, salida)
    End Sub
    Public Sub RegistrarLog_ReniecConsultaDNI(exito As Boolean, strMensajeError As String, entrada As PersonaBE, salida As ReniecConsultaDNIBE)
        Dim oLogBE As New LogBE()
        oLogBE.TipoServicio = Constantes.TH_SERVICIOS_VALOR1.K_RENIEC_VAL_DNI
        oLogBE.VUSER = If(IsNothing(entrada.CODUSER), "", entrada.CODUSER.Trim())
        oLogBE.VCODAPLI = If(IsNothing(entrada.CODAPP), "", entrada.CODAPP.Trim())
        oLogBE.NCODRESULTADO = If(exito = True, Constantes.TipoResultado.K_OK, Constantes.TipoResultado.K_ERROR)
        oLogBE.VMENSAJE_RESULTADO = strMensajeError
        oLogBE.NCODACCION = Constantes.IndAccion.K_CONSULTA
        oLogBE.DFECHA = Date.Now

        _objResultadoDA.RegistrarLog_ReniecConsultaDNI(oLogBE, entrada, salida)
    End Sub

    Public Sub Registrar_InpeAntecedentesPenales(entrada As PersonaBE, salida As String, indAp As String)
        _objResultadoDA.Registrar_InpeAntecedentesPenales(entrada, indAp)
        'Rergistrar Log:
        RegistrarLog_InpeAntecedentesPenales(True, "", entrada, salida)
    End Sub
    Public Sub RegistrarLog_InpeAntecedentesPenales(exito As Boolean, strMensajeError As String, entrada As PersonaBE, salida As String)
        Dim oLogBE As New LogBE()
        oLogBE.TipoServicio = Constantes.TH_SERVICIOS_VALOR1.K_INPE_ANT_PENALES
        oLogBE.VUSER = If(IsNothing(entrada.CODUSER), "", entrada.CODUSER.Trim())
        oLogBE.VCODAPLI = If(IsNothing(entrada.CODAPP), "", entrada.CODAPP.Trim())
        oLogBE.NCODRESULTADO = If(exito = True, Constantes.TipoResultado.K_OK, Constantes.TipoResultado.K_ERROR)
        oLogBE.VMENSAJE_RESULTADO = strMensajeError
        oLogBE.NCODACCION = Constantes.IndAccion.K_CONSULTA
        oLogBE.DFECHA = Date.Now

        _objResultadoDA.RegistrarLog_InpeAntecedentesPenales(oLogBE, entrada, salida)
    End Sub

    Public Sub Registrar_PJAntecedentesJudiciales(entrada As PersonaBE, salida As String, indAj As String)
        _objResultadoDA.Registrar_PJAntecedentesJudiciales(entrada, indAj)
        'Rergistrar Log:
        RegistrarLog_PJAntecedentesJudiciales(True, "", entrada, salida)
    End Sub
    Public Sub RegistrarLog_PJAntecedentesJudiciales(exito As Boolean, strMensajeError As String, entrada As PersonaBE, salida As String)
        Dim oLogBE As New LogBE()
        oLogBE.TipoServicio = Constantes.TH_SERVICIOS_VALOR1.K_PODERJUD_ANT_JUDICIALES
        oLogBE.VUSER = If(IsNothing(entrada.CODUSER), "", entrada.CODUSER.Trim())
        oLogBE.VCODAPLI = If(IsNothing(entrada.CODAPP), "", entrada.CODAPP.Trim())
        oLogBE.NCODRESULTADO = If(exito = True, Constantes.TipoResultado.K_OK, Constantes.TipoResultado.K_ERROR)
        oLogBE.VMENSAJE_RESULTADO = strMensajeError
        oLogBE.NCODACCION = Constantes.IndAccion.K_CONSULTA
        oLogBE.DFECHA = Date.Now

        _objResultadoDA.RegistrarLog_PJAntecedentesJudiciales(oLogBE, entrada, salida)
    End Sub

    Public Sub Registrar_MininterAntecedentesPoliciales(entrada As PersonaBE, salida As String, indAp As String)
        _objResultadoDA.Registrar_MininterAntecedentesPoliciales(entrada, indAp)
        'Rergistrar Log:
        RegistrarLog_MininterAntecedentesPoliciales(True, "", entrada, salida)
    End Sub
    Public Sub RegistrarLog_MininterAntecedentesPoliciales(exito As Boolean, strMensajeError As String, entrada As PersonaBE, salida As String)
        Dim oLogBE As New LogBE()
        oLogBE.TipoServicio = Constantes.TH_SERVICIOS_VALOR1.K_MININTER_ANT_POLICIALES
        oLogBE.VUSER = If(IsNothing(entrada.CODUSER), "", entrada.CODUSER.Trim())
        oLogBE.VCODAPLI = If(IsNothing(entrada.CODAPP), "", entrada.CODAPP.Trim())
        oLogBE.NCODRESULTADO = If(exito = True, Constantes.TipoResultado.K_OK, Constantes.TipoResultado.K_ERROR)
        oLogBE.VMENSAJE_RESULTADO = strMensajeError
        oLogBE.NCODACCION = Constantes.IndAccion.K_CONSULTA
        oLogBE.DFECHA = Date.Now

        _objResultadoDA.RegistrarLog_MininterAntecedentesPoliciales(oLogBE, entrada, salida)
    End Sub

    Public Sub Registrar_SunarpTitularidadBienes(entrada As PersonaBE, salida As List(Of SunarpTitularidadBienesBE))
        For Each itemSalida As SunarpTitularidadBienesBE In salida
            _objResultadoDA.Registrar_SunarpTitularidadBienes(entrada, itemSalida)
        Next
        'Rergistrar Log:
        RegistrarLog_SunarpTitularidadBienes(True, "", entrada, salida)
    End Sub
    Public Sub RegistrarLog_SunarpTitularidadBienes(exito As Boolean, strMensajeError As String, entrada As PersonaBE, salida As List(Of SunarpTitularidadBienesBE))
        Dim oLogBE As New LogBE()
        oLogBE.TipoServicio = Constantes.TH_SERVICIOS_VALOR1.K_SUNARP_TITU_BIENES
        oLogBE.VUSER = If(IsNothing(entrada.CODUSER), "", entrada.CODUSER.Trim())
        oLogBE.VCODAPLI = If(IsNothing(entrada.CODAPP), "", entrada.CODAPP.Trim())
        oLogBE.NCODRESULTADO = If(exito = True, Constantes.TipoResultado.K_OK, Constantes.TipoResultado.K_ERROR)
        oLogBE.VMENSAJE_RESULTADO = strMensajeError
        oLogBE.NCODACCION = Constantes.IndAccion.K_CONSULTA
        oLogBE.DFECHA = Date.Now

        _objResultadoDA.RegistrarLog_SunarpTitularidadBienes(oLogBE, entrada, salida)
    End Sub

    Public Sub Registrar_SunarpVigenciaPoder(entrada As PersonaBE, salida As SunarpVigenciaPoderBE)
        _objResultadoDA.Registrar_SunarpVigenciaPoder(entrada, salida)
        'Rergistrar Log:
        RegistrarLog_SunarpVigenciaPoder(True, "", entrada, salida)
    End Sub
    Public Sub RegistrarLog_SunarpVigenciaPoder(exito As Boolean, strMensajeError As String, entrada As PersonaBE, salida As SunarpVigenciaPoderBE)
        Dim oLogBE As New LogBE()
        oLogBE.TipoServicio = Constantes.TH_SERVICIOS_VALOR1.K_SUNARP_VIG_PODER
        oLogBE.VUSER = If(IsNothing(entrada.CODUSER), "", entrada.CODUSER.Trim())
        oLogBE.VCODAPLI = If(IsNothing(entrada.CODAPP), "", entrada.CODAPP.Trim())
        oLogBE.NCODRESULTADO = If(exito = True, Constantes.TipoResultado.K_OK, Constantes.TipoResultado.K_ERROR)
        oLogBE.VMENSAJE_RESULTADO = strMensajeError
        oLogBE.NCODACCION = Constantes.IndAccion.K_CONSULTA
        oLogBE.DFECHA = Date.Now

        _objResultadoDA.RegistrarLog_SunarpVigenciaPoder(oLogBE, entrada, salida)
    End Sub

    Public Sub Registrar_SunarpListaAsiento(entrada As PersonaBE, salida As List(Of SunarpAsientoBE))
        For Each itemSalida As SunarpAsientoBE In salida
            _objResultadoDA.Registrar_SunarpListaAsiento(entrada, itemSalida)
        Next
        'Rergistrar Log:
        RegistrarLog_SunarpListaAsiento(True, "", entrada, salida)
    End Sub
    Public Sub RegistrarLog_SunarpListaAsiento(exito As Boolean, strMensajeError As String, entrada As PersonaBE, salida As List(Of SunarpAsientoBE))
        Dim oLogBE As New LogBE()
        oLogBE.TipoServicio = Constantes.TH_SERVICIOS_VALOR1.K_SUNARP_LIST_ASIENTOS
        oLogBE.VUSER = If(IsNothing(entrada.CODUSER), "", entrada.CODUSER.Trim())
        oLogBE.VCODAPLI = If(IsNothing(entrada.CODAPP), "", entrada.CODAPP.Trim())
        oLogBE.NCODRESULTADO = If(exito = True, Constantes.TipoResultado.K_OK, Constantes.TipoResultado.K_ERROR)
        oLogBE.VMENSAJE_RESULTADO = strMensajeError
        oLogBE.NCODACCION = Constantes.IndAccion.K_CONSULTA
        oLogBE.DFECHA = Date.Now

        _objResultadoDA.RegistrarLog_SunarpListaAsiento(oLogBE, entrada, salida)
    End Sub

    Public Sub Registrar_SuneduGradosTitulos(entrada As PersonaBE, salida As List(Of SuneduGradoAcademicoBE))
        For Each itemSalida As SuneduGradoAcademicoBE In salida
            _objResultadoDA.Registrar_SuneduGradosTitulos(entrada, itemSalida)
        Next
        'Rergistrar Log:
        RegistrarLog_SuneduGradosTitulos(True, "", entrada, salida)
    End Sub
    Public Sub RegistrarLog_SuneduGradosTitulos(exito As Boolean, strMensajeError As String, entrada As PersonaBE, salida As List(Of SuneduGradoAcademicoBE))
        Dim oLogBE As New LogBE()
        oLogBE.TipoServicio = Constantes.TH_SERVICIOS_VALOR1.K_SUNEDU_GRADOSYTIT
        oLogBE.VUSER = If(IsNothing(entrada.CODUSER), "", entrada.CODUSER.Trim())
        oLogBE.VCODAPLI = If(IsNothing(entrada.CODAPP), "", entrada.CODAPP.Trim())
        oLogBE.NCODRESULTADO = If(exito = True, Constantes.TipoResultado.K_OK, Constantes.TipoResultado.K_ERROR)
        oLogBE.VMENSAJE_RESULTADO = strMensajeError
        oLogBE.NCODACCION = Constantes.IndAccion.K_CONSULTA
        oLogBE.DFECHA = Date.Now

        _objResultadoDA.RegistrarLog_SuneduGradosTitulos(oLogBE, entrada, salida)
    End Sub

    Public Sub Registrar_MigracionesCarnetExtranjeria(entrada As PersonaBE, salida As MigracionesCarnetExtranjeriaBE)
        _objResultadoDA.Registrar_MigracionesCarnetExtranjeria(entrada, salida)
        'Rergistrar Log:
        RegistrarLog_MigracionesCarnetExtranjeria(True, "", entrada, salida)
    End Sub
    Public Sub RegistrarLog_MigracionesCarnetExtranjeria(exito As Boolean, strMensajeError As String, entrada As PersonaBE, salida As MigracionesCarnetExtranjeriaBE)
        Dim oLogBE As New LogBE()
        oLogBE.TipoServicio = Constantes.TH_SERVICIOS_VALOR1.K_MIGRACIONES_CARNET_EXT
        oLogBE.VUSER = If(IsNothing(entrada.CODUSER), "", entrada.CODUSER.Trim())
        oLogBE.VCODAPLI = If(IsNothing(entrada.CODAPP), "", entrada.CODAPP.Trim())
        oLogBE.NCODRESULTADO = If(exito = True, Constantes.TipoResultado.K_OK, Constantes.TipoResultado.K_ERROR)
        oLogBE.VMENSAJE_RESULTADO = strMensajeError
        oLogBE.NCODACCION = Constantes.IndAccion.K_CONSULTA
        oLogBE.DFECHA = Date.Now

        _objResultadoDA.RegistrarLog_MigracionesCarnetExtranjeria(oLogBE, entrada, salida)
    End Sub

    Public Sub Registrar_SunarpVerImagenAsiento(entrada As AsientoBE, rutaImagen As String)
        _objResultadoDA.Registrar_SunarpVerImagenAsiento(entrada, rutaImagen)
    End Sub
    Public Sub RegistrarLog_SunarpVerImagenAsiento(exito As Boolean, strMensajeError As String, entrada As AsientoBE, salida As String)
        Dim oLogBE As New LogBE()
        oLogBE.TipoServicio = Constantes.TH_SERVICIOS_VALOR1.K_SUNARP_VER_ASIENTO
        oLogBE.VUSER = If(IsNothing(entrada.CODUSER), "", entrada.CODUSER.Trim())
        oLogBE.VCODAPLI = If(IsNothing(entrada.CODAPP), "", entrada.CODAPP.Trim())
        oLogBE.NCODRESULTADO = If(exito = True, Constantes.TipoResultado.K_OK, Constantes.TipoResultado.K_ERROR)
        oLogBE.VMENSAJE_RESULTADO = strMensajeError
        oLogBE.NCODACCION = Constantes.IndAccion.K_CONSULTA
        oLogBE.DFECHA = Date.Now

        _objResultadoDA.RegistrarLog_SunarpVerImagenAsiento(oLogBE, entrada, salida)
    End Sub

End Class
