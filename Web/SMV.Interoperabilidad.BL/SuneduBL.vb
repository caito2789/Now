Imports SMV.Interoperabilidad.DA
Imports SMV.Interoperabilidad.BE

Public Class SuneduBL
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

    Private _objSuneduDA As SuneduDA

    Sub New()
        _objSuneduDA = New SuneduDA()
    End Sub

#End Region

#Region "Métodos"

    Public Function RegistrarLog(ByVal objFiltro As SuneduBE) As Integer
        Dim intResultado As Integer = 0
        Try
            Dim objLista As List(Of SuneduBE) = Nothing
            Dim intCantidad As Integer = 0, intCuenta As Integer = 0

            objLista = objFiltro.objLista
            If Not objLista Is Nothing Then

                intCantidad = objLista.Count

                Dim arr_ApePaterno As [Object]() = New [Object](intCantidad - 1) {}
                Dim arr_ApeMaterno As [Object]() = New [Object](intCantidad - 1) {}
                Dim arr_Nombres As [Object]() = New [Object](intCantidad - 1) {}
                Dim arr_TipoDocumento As [Object]() = New [Object](intCantidad - 1) {}
                Dim arr_NroDocumento As [Object]() = New [Object](intCantidad - 1) {}
                Dim arr_Pais As [Object]() = New [Object](intCantidad - 1) {}
                Dim arr_Universidad As [Object]() = New [Object](intCantidad - 1) {}
                Dim arr_TitProfesional As [Object]() = New [Object](intCantidad - 1) {}
                Dim arr_AbrTitulo As [Object]() = New [Object](intCantidad - 1) {}
                Dim arr_Especialidad As [Object]() = New [Object](intCantidad - 1) {}
                intCuenta = 0  'Inicializo

                For Each Item In objLista
                    arr_ApePaterno.SetValue(Item.ApePaterno, intCuenta)
                    arr_ApeMaterno.SetValue(Item.ApeMaterno, intCuenta)
                    arr_Nombres.SetValue(Item.Nombres, intCuenta)
                    arr_TipoDocumento.SetValue(Item.TipoDocumento, intCuenta)
                    arr_NroDocumento.SetValue(Item.NroDocumento, intCuenta)
                    arr_Pais.SetValue(Item.Pais, intCuenta)
                    arr_Universidad.SetValue(Item.Universidad, intCuenta)
                    arr_TitProfesional.SetValue(Item.TitProfesional, intCuenta)
                    arr_AbrTitulo.SetValue(Item.AbrTitulo, intCuenta)
                    arr_Especialidad.SetValue(Item.Especialidad, intCuenta)

                    intCuenta = intCuenta + 1
                Next

                objFiltro.NCANTIDAD_ARR = intCantidad
                objFiltro.Arr_ApePaterno = arr_ApePaterno
                objFiltro.Arr_ApeMaterno = arr_ApeMaterno
                objFiltro.Arr_Nombres = arr_Nombres
                objFiltro.Arr_TipoDocumento = arr_TipoDocumento
                objFiltro.Arr_NroDocumento = arr_NroDocumento
                objFiltro.Arr_Pais = arr_Pais
                objFiltro.Arr_Universidad = arr_Universidad
                objFiltro.Arr_TitProfesional = arr_TitProfesional
                objFiltro.Arr_AbrTitulo = arr_AbrTitulo
                objFiltro.Arr_Especialidad = arr_Especialidad

            Else
                objFiltro.NCANTIDAD_ARR = 0
            End If

            intResultado = _objSuneduDA.RegistrarLog(objFiltro)
        Catch ex As Exception
            Throw ex
        End Try
        Return intResultado
    End Function

    Public Function ObtenerListaGradosYTitulos_PDF(ByVal objFiltroL As List(Of SuneduBE)) As List(Of SuneduBE)
        Dim objLista As List(Of SuneduBE) = Nothing
        objLista = objFiltroL
        Return objLista
    End Function

#End Region

End Class
