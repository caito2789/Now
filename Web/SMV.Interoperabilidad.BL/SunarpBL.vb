Imports SMV.Interoperabilidad.DA
Imports SMV.Interoperabilidad.BE

Public Class SunarpBL
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

#Region "Constructor"

    Private _objSunarpDA As SunarpDA

    Sub New()
        _objSunarpDA = New SunarpDA()
    End Sub

#End Region

#Region "Métodos"

    Public Function ObtenerListaRegistros(ByVal objFiltro As SunarpBE) As List(Of SunarpBE)
        Dim objResultado As List(Of SunarpBE) = Nothing
        Try
            objResultado = _objSunarpDA.ObtenerListaRegistros(objFiltro)
        Catch ex As Exception
            Throw ex
        End Try
        Return objResultado
    End Function

    Public Function ObtenerListaZonas(ByVal objFiltro As SunarpBE) As List(Of SunarpBE)
        Dim objResultado As List(Of SunarpBE) = Nothing
        Try
            objResultado = _objSunarpDA.ObtenerListaZonas(objFiltro)
        Catch ex As Exception
            Throw ex
        End Try
        Return objResultado
    End Function

    Public Function ObtenerListaOficinas(ByVal objFiltro As SunarpBE) As List(Of SunarpBE)
        Dim objResultado As List(Of SunarpBE) = Nothing
        Try
            objResultado = _objSunarpDA.ObtenerListaOficinas(objFiltro)
        Catch ex As Exception
            Throw ex
        End Try
        Return objResultado
    End Function

    ''' <summary>
    ''' SUNARP - Vigencia de Poder.
    ''' </summary>
    ''' <param name="objFiltro"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function RegistrarLog_VP(ByVal objFiltro As SunarpBE) As Integer
        Dim intResultado As Integer = 0
        Try
            intResultado = _objSunarpDA.RegistrarLog_VP(objFiltro)
        Catch ex As Exception
            Throw ex
        End Try
        Return intResultado
    End Function

    ''' <summary>
    ''' SUNARP - Titularidad de Bienes de consulta.
    ''' </summary>
    ''' <param name="objFiltro"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function RegistrarLog_TB(ByVal objFiltro As SunarpBE) As Integer
        Dim intResultado As Integer = 0
        Try
            Dim objLista As List(Of SunarpBE) = Nothing
            Dim intCantidad As Integer = 0, intCuenta As Integer = 0

            objLista = objFiltro.objLista
            If Not objLista Is Nothing Then
                intCantidad = objLista.Count

                Dim arr_ApePaterno As [Object]() = New [Object](intCantidad - 1) {}
                Dim arr_ApeMaterno As [Object]() = New [Object](intCantidad - 1) {}
                Dim arr_Nombres As [Object]() = New [Object](intCantidad - 1) {}
                Dim arr_RazonSocial As [Object]() = New [Object](intCantidad - 1) {}
                Dim arr_TipoDocumento As [Object]() = New [Object](intCantidad - 1) {}
                Dim arr_NroDocumento As [Object]() = New [Object](intCantidad - 1) {}
                Dim arr_NumPartida As [Object]() = New [Object](intCantidad - 1) {}
                Dim arr_Registro As [Object]() = New [Object](intCantidad - 1) {}
                Dim arr_NumPlaca As [Object]() = New [Object](intCantidad - 1) {}
                Dim arr_Zona As [Object]() = New [Object](intCantidad - 1) {}
                Dim arr_Oficina As [Object]() = New [Object](intCantidad - 1) {}
                Dim arr_Estado As [Object]() = New [Object](intCantidad - 1) {}
                intCuenta = 0  'Inicializo

                For Each Item In objLista
                    arr_ApePaterno.SetValue(Item.ApePaterno, intCuenta)
                    arr_ApeMaterno.SetValue(Item.ApeMaterno, intCuenta)
                    arr_Nombres.SetValue(Item.Nombres, intCuenta)
                    arr_RazonSocial.SetValue(Item.RazonSocial, intCuenta)
                    arr_TipoDocumento.SetValue(Item.TipoDocumento, intCuenta)
                    arr_NroDocumento.SetValue(Item.NroDocumento, intCuenta)
                    arr_NumPartida.SetValue(Item.NumPartida, intCuenta)
                    arr_Registro.SetValue(Item.Registro, intCuenta)
                    arr_NumPlaca.SetValue(Item.NumPlaca, intCuenta)
                    arr_Zona.SetValue(Item.Zona, intCuenta)
                    arr_Oficina.SetValue(Item.Oficina, intCuenta)
                    arr_Estado.SetValue(Item.Estado, intCuenta)

                    intCuenta = intCuenta + 1
                Next

                objFiltro.NCANTIDAD_ARR = intCantidad
                objFiltro.Arr_ApePaterno = arr_ApePaterno
                objFiltro.Arr_ApeMaterno = arr_ApeMaterno
                objFiltro.Arr_Nombres = arr_Nombres
                objFiltro.Arr_RazonS = arr_RazonSocial
                objFiltro.Arr_TipoDocumento = arr_TipoDocumento
                objFiltro.Arr_NroDocumento = arr_NroDocumento
                objFiltro.Arr_NumPartida = arr_NumPartida
                objFiltro.Arr_Registro = arr_Registro
                objFiltro.Arr_NumPlaca = arr_NumPlaca
                objFiltro.Arr_Zona = arr_Zona
                objFiltro.Arr_Oficina = arr_Oficina
                objFiltro.Arr_Estado = arr_Estado

            Else
                objFiltro.NCANTIDAD_ARR = 0
            End If

            intResultado = _objSunarpDA.RegistrarLog_TB(objFiltro)
        Catch ex As Exception
            Throw ex
        End Try
        Return intResultado
    End Function

    ''' <summary>
    ''' SUNARP - Lista de Asientos.
    ''' </summary>
    ''' <param name="objFiltro"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function RegistrarLog_LA(ByVal objFiltro As SunarpBE) As Integer
        Dim intResultado As Integer = 0
        Try
            Dim objLista As List(Of SunarpBE) = Nothing
            Dim intCantidad As Integer = 0, intCuenta As Integer = 0

            objLista = objFiltro.objLista
            If Not objLista Is Nothing Then
                intCantidad = objLista.Count

                Dim arr_IDImgAsiento As [Object]() = New [Object](intCantidad - 1) {}
                Dim arr_Tipo As [Object]() = New [Object](intCantidad - 1) {}
                Dim arr_CantidadPag As [Object]() = New [Object](intCantidad - 1) {}
                Dim arr_NroPagRef As [Object]() = New [Object](intCantidad - 1) {}
                Dim arr_NroPag As [Object]() = New [Object](intCantidad - 1) {}                
                intCuenta = 0  'Inicializo

                For Each Item In objLista
                    arr_IDImgAsiento.SetValue(Item.IDImgAsiento, intCuenta)
                    arr_Tipo.SetValue(Item.Tipo, intCuenta)
                    arr_CantidadPag.SetValue(Item.CantidadPag, intCuenta)
                    arr_NroPagRef.SetValue(Item.NroPagRef, intCuenta)
                    arr_NroPag.SetValue(Item.NroPag, intCuenta)

                    intCuenta = intCuenta + 1
                Next

                objFiltro.NCANTIDAD_ARR = intCantidad
                objFiltro.Arr_IDImgAsiento = arr_IDImgAsiento
                objFiltro.Arr_Tipo = arr_Tipo
                objFiltro.Arr_CantidadPag = arr_CantidadPag
                objFiltro.Arr_NroPagRef = arr_NroPagRef
                objFiltro.Arr_NroPag = arr_NroPag

            Else
                objFiltro.NCANTIDAD_ARR = 0
            End If

            intResultado = _objSunarpDA.RegistrarLog_LA(objFiltro)
        Catch ex As Exception
            Throw ex
        End Try
        Return intResultado
    End Function

    ''' <summary>
    ''' SUNARP - Ver Asiento.
    ''' </summary>
    ''' <param name="objFiltro"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function RegistrarLog_VImg(ByVal objFiltro As SunarpBE) As Integer
        Dim intResultado As Integer = 0
        Try
            intResultado = _objSunarpDA.RegistrarLog_VImg(objFiltro)
        Catch ex As Exception
            Throw ex
        End Try
        Return intResultado
    End Function

    Public Function ObtenerListaTitularidadB_PDF(ByVal objFiltroL As List(Of SunarpBE)) As List(Of SunarpBE)
        Dim objLista As List(Of SunarpBE) = Nothing
        objLista = objFiltroL
        Return objLista
    End Function

    Public Function ObtenerListaAsientos_PDF(ByVal objFiltroL As List(Of SunarpBE)) As List(Of SunarpBE)
        Dim objLista As List(Of SunarpBE) = Nothing
        objLista = objFiltroL
        Return objLista
    End Function

#End Region

End Class
