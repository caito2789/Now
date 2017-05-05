Imports SMV.Interoperabilidad.DA
Imports SMV.Interoperabilidad.BE

Public Class LogBL
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

    Private _objLogDA As LogDA

    Sub New()
        _objLogDA = New LogDA()
    End Sub

#End Region

#Region "Métodos"

    Public Function ObtenerListaTipoServicio(ByVal objFiltro As LogBE) As List(Of LogBE)
        Dim objResultado As List(Of LogBE) = Nothing
        Try
            objResultado = _objLogDA.ObtenerListaTipoServicio(objFiltro)
        Catch ex As Exception
            Throw ex
        End Try
        Return objResultado
    End Function

    Public Function ObtenerListaTipoEvento(ByVal objFiltro As LogBE) As List(Of LogBE)
        Dim objResultado As List(Of LogBE) = Nothing
        Try
            objResultado = _objLogDA.ObtenerListaTipoEvento(objFiltro)
        Catch ex As Exception
            Throw ex
        End Try
        Return objResultado
    End Function

    Public Function ObtenerListaLogs(ByVal objFiltro As LogBE) As List(Of LogBE)
        Dim objResultado As List(Of LogBE) = Nothing
        Try
            objResultado = _objLogDA.ObtenerListaLogs(objFiltro)
        Catch ex As Exception
            Throw ex
        End Try
        Return objResultado
    End Function

    Public Function CargarDetalleSalida(ByVal objFiltro As LogBE) As List(Of LogBE)
        Dim objResultado As List(Of LogBE) = Nothing
        Try
            objResultado = _objLogDA.CargarDetalleSalida(objFiltro)
        Catch ex As Exception
            Throw ex
        End Try
        Return objResultado
    End Function

    Public Function CargarDetalleSalidaExportar(ByVal objFiltro As LogBE) As List(Of LogBE)
        Dim objResultado As List(Of LogBE) = Nothing
        Try
            objResultado = _objLogDA.CargarDetalleSalidaExportar(objFiltro)
        Catch ex As Exception
            Throw ex
        End Try
        Return objResultado
    End Function

    Public Function ObtenerCabeceraPDF(ByVal objFiltro As LogBE) As List(Of LogBE)
        Dim objResultado As List(Of LogBE) = Nothing
        Try
            objResultado = _objLogDA.ObtenerCabeceraPDF(objFiltro)
        Catch ex As Exception
            Throw ex
        End Try
        Return objResultado
    End Function

#End Region

End Class
