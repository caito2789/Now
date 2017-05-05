Imports SMV.Interoperabilidad.BE

Public NotInheritable Class FachadaSesion

    Private Sub New()
    End Sub

#Region "Propiedades Compartidas"

    Public Shared Property Usuario() As UsuarioBE
        Get
            Return Obtener(Of UsuarioBE)("_Usuario_")
        End Get
        Set(value As UsuarioBE)
            Asignar("_Usuario_", value)
        End Set
    End Property

    Public Shared Property Filtro() As Object
        Get
            Return Obtener(Of Object)("_Filtro_")
        End Get
        Set(value As Object)
            Asignar("_Filtro_", value)
        End Set
    End Property

#End Region

#Region "Métodos Compartidos"

    Public Shared Sub Asignar(Of T)(key As String, value As T)
        If HttpContext.Current.Session(key) IsNot Nothing Then
            HttpContext.Current.Session.Add(key, value)
        Else
            HttpContext.Current.Session(key) = value
        End If
    End Sub

    Public Shared Function Obtener(Of T)(key As String) As T
        If IsNothing(HttpContext.Current.Session(key)) Then
            Return Nothing
        End If
        Return DirectCast(HttpContext.Current.Session(key), T)
    End Function

    Public Shared Function Existe(key As String) As Boolean
        Return HttpContext.Current.Session(key) IsNot Nothing
    End Function

    Public Shared Sub Eliminar(key As String)
        HttpContext.Current.Session.Remove(key)
    End Sub

#End Region

End Class
