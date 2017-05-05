Imports System.Runtime.Serialization

<Serializable()>
<DataContract()>
Public Class UsuarioPerfilBE

    <DataMember(EmitDefaultValue:=False)> Public Property CodigoUsuario As String
    <DataMember(EmitDefaultValue:=False)> Public Property CodigoAplicacion As String
    <DataMember(EmitDefaultValue:=False)> Public Property CodigoPerfil As String
    <DataMember(EmitDefaultValue:=False)> Public Property NombreUsuario As String
    <DataMember(EmitDefaultValue:=False)> Public Property DescripcionPerfil As String

End Class
