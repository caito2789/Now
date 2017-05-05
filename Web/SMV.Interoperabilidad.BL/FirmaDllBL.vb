Imports SignnetSolution_v2
Imports System.Runtime.InteropServices
Imports SMV.Interoperabilidad.BE

Public Class FirmaDllBL
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

#Region "Atributos"

    Public blError As Boolean = False
    Public strMensajeError As String = String.Empty

#End Region

#Region "Métodos"

    Public Sub FirmarDocumento(ByVal strRutaArchivoFirmar As String, ByVal strRutaDestino As String, ByVal strNombreFirmante As String)
        Try
            Dim strNomArchivoPDF As String = strRutaArchivoFirmar.Substring(strRutaArchivoFirmar.LastIndexOf("\") + 1)
            Dim KEYSTORE As String = "C:\Interoperabilidad\TRAMITEDOCUMENTARIO.pfx"
            Dim objSigner As New SMV.Interop.SignerPDF.Signer
            objSigner.Sign(KEYSTORE, "identidad", strRutaArchivoFirmar, String.Format(strRutaDestino & "\" & strNomArchivoPDF, 1), "SHA-256", Constantes.ConfiguracionFirmaSignnet.reason, Constantes.ConfiguracionFirmaSignnet.location)
            objSigner = Nothing
            'System.IO.File.Copy(strRutaArchivoFirmar, String.Format(strRutaDestino & "\" & strNomArchivoPDF, 1))
            'Dim reason As String = String.Empty, location As String = String.Empty, comment As String = String.Empty, _
            '    strNameTag As String = String.Empty, strRutaArchivo As String = String.Empty, strRutaSignNet As String = String.Empty, _
            '    boolSignnet As String = String.Empty, strRutaDestinoFinal As String = String.Empty
            'Dim resultado As Boolean = False
            'strNameTag = strNombreFirmante      'Nombre del Firmante
            'strRutaSignNet = Constantes.ConfiguracionFirmaSignnet.Executable.Replace("\", "\\")
            'strRutaArchivo = strRutaArchivoFirmar.Replace("\", "\\")    'Ruta y archivo a firmar
            'strRutaDestinoFinal = strRutaDestino.Replace("\", "\\")
            'reason = Constantes.ConfiguracionFirmaSignnet.reason
            'location = Constantes.ConfiguracionFirmaSignnet.location
            'comment = Constantes.ConfiguracionFirmaSignnet.comment
            'boolSignnet = Constantes.ConfiguracionFirmaSignnet.boolSignnet
            'resultado = SignnetSolution_v2.SignnetSignature.signature(strRutaSignNet, strRutaArchivo, strNameTag, reason, location, comment, boolSignnet, strRutaDestinoFinal, "CO", 1, "69,460,152,488", True)

        Catch ex As Exception
            Me.blError = True
            Me.strMensajeError = "Error: " & ex.Message
        End Try
    End Sub

#End Region

End Class
