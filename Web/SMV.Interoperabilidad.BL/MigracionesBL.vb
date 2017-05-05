Imports SMV.Interoperabilidad.DA
Imports SMV.Interoperabilidad.BE

Public Class MigracionesBL
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

    Private _objMigracionesDA As MigracionesDA

    Sub New()
        _objMigracionesDA = New MigracionesDA()
    End Sub

#End Region

#Region "Métodos"

    Public Function RegistrarLog(ByVal objFiltro As MigracionesBE) As Integer
        Dim intResultado As Integer = 0
        Try
            intResultado = _objMigracionesDA.RegistrarLog(objFiltro)
        Catch ex As Exception
            Throw ex
        End Try
        Return intResultado
    End Function

#End Region

End Class
