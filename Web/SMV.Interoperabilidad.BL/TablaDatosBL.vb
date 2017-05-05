Imports SMV.Interoperabilidad.BE
Imports SMV.Interoperabilidad.DA

Public Class TablaDatosBL
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

    Private _objTablaDatosDA As TablaDatosDA
    Sub New()
        _objTablaDatosDA = New TablaDatosDA()
    End Sub

    Public Function ListarTablaPadre() As List(Of TablaDatosBE)
        Return _objTablaDatosDA.ListarTablaPadre()
    End Function

    Public Function ListarTablaHija(ByVal idTablaPadre As Integer) As List(Of TablaDatosBE)
        Return _objTablaDatosDA.ListarTablaHija(idTablaPadre)
    End Function

    Public Function ActualizarTablaHija(ByVal oTablaDatosBE As TablaDatosBE)
        _objTablaDatosDA.ActualizarTablaHija(oTablaDatosBE)
    End Function


End Class
