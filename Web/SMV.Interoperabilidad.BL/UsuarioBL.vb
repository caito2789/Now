Imports System.Text
Imports System.Security.Cryptography
Imports SMV.Interoperabilidad.DA
Imports SMV.Interoperabilidad.BE

Public Class UsuarioBL
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

    Private _objUsuarioDA As UsuarioDA

    Sub New()
        _objUsuarioDA = New UsuarioDA()
    End Sub

#End Region

#Region "Métodos"

    Public Function ValidarUsuario(pobjUsuarioBE As UsuarioBE, pstrCodigoAplicacion As String) As UsuarioBE
        'Logica De Validacion del Control SMV
        pobjUsuarioBE.UsuarioPassword = GeneraPasswordHash(pobjUsuarioBE.UsuarioPassword)
        pobjUsuarioBE.UsuarioLogon = pobjUsuarioBE.UsuarioLogon.ToUpper()

        'Recuperamos la información del usuario SMV
        Dim lobjUsuarioBE = _objUsuarioDA.ValidarUsuario(pobjUsuarioBE)

        If IsNothing(lobjUsuarioBE) Then
            Throw New Exception("Contraseña Incorrecta. Verificar para continuar.")
        End If

        lobjUsuarioBE.ListaUsuarioPerfil = _objUsuarioDA.ListarPerfilesPorUsuarioAplicacion(New UsuarioPerfilBE With {.CodigoUsuario = lobjUsuarioBE.CodigoUsuario, .CodigoAplicacion = pstrCodigoAplicacion})
        If (lobjUsuarioBE.ListaUsuarioPerfil.Count = 0) Then
            Throw New Exception("El usuario no tiene perfil asignado en el sistema. Comunicarse con el Administrador.")
        End If

        'Recuperamos los accesos a los menus
        lobjUsuarioBE.ListaUsuarioPerfilOpcion = _objUsuarioDA.ListarOpcionPorUsuarioAplicacion(New UsuarioPerfilOpcionBE With {.CodigoUsuario = lobjUsuarioBE.CodigoUsuario, .CodigoAplicacion = pstrCodigoAplicacion})
        'lobjUsuarioBE.ListaUsuarioPerfilOpcion = ListaMenuPrueba(lobjUsuarioBE.CodigoUsuario, pstrCodigoAplicacion)

        Return lobjUsuarioBE
    End Function

    Public Function ValidarUsuarioNT(pobjUsuarioBE As UsuarioBE, pstrCodigoAplicacion As String) As UsuarioBE
        'Recuperamos la información del usuario SMV
        Dim lobjUsuarioBE = _objUsuarioDA.ValidarUsuarioNT(pobjUsuarioBE)

        If IsNothing(lobjUsuarioBE) Then
            Throw New Exception("Usuario Incorrecto.")
        End If

        lobjUsuarioBE.ListaUsuarioPerfil = _objUsuarioDA.ListarPerfilesPorUsuarioAplicacion(New UsuarioPerfilBE With {.CodigoUsuario = lobjUsuarioBE.CodigoUsuario, .CodigoAplicacion = pstrCodigoAplicacion})

        If (lobjUsuarioBE.ListaUsuarioPerfil.Count = 0) Then
            Throw New Exception("El usuario no tiene perfil asignado en el sistema. Comunicarse con el Administrador.")
        End If

        'Recuperamos los accesos a los menus
        lobjUsuarioBE.ListaUsuarioPerfilOpcion = _objUsuarioDA.ListarOpcionPorUsuarioAplicacion(New UsuarioPerfilOpcionBE With {.CodigoUsuario = lobjUsuarioBE.CodigoUsuario, .CodigoAplicacion = pstrCodigoAplicacion})

        Return lobjUsuarioBE
    End Function

    ''' <summary>
    ''' Nombre de Metodo:       GeneraPasswordHash
    ''' Autor:                  Herbert Sandoval Pacheco
    ''' Fecha de Creacion:      22/05/2013 
    ''' Descripcion:            Metodo que retorna el HASH de una cadena
    ''' Parametros de Entrada:  pClave  --> Cadena, clave a convertir a HASH SH256
    ''' Parametros de Salida:           --> Cadena, clave ya formateado al tipo de HASH
    ''' </summary>
    ''' <param name="pClave"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GeneraPasswordHash(ByVal pClave As String) As String
        Dim ue As New UnicodeEncoding
        Dim bHash As Byte()

        'Almacenamos la cadena ingresada en una matriz de bytes
        Dim bCadena() As Byte = ue.GetBytes(pClave)
        Dim slService As New SHA256Managed()

        'Creamos el hash
        bHash = slService.ComputeHash(bCadena)

        'Retornamos el Hash en texto codificado en base64
        Dim claveHash As String
        claveHash = Convert.ToBase64String(bHash)

        Return claveHash
    End Function

    Public Function ObtenerListaUsuarios() As List(Of UsuarioBE)
        Dim objLista As List(Of UsuarioBE) = Nothing
        Try
            objLista = _objUsuarioDA.ObtenerListaUsuarios()
        Catch ex As Exception
            Throw ex
        End Try
        Return objLista
    End Function

#End Region

End Class
