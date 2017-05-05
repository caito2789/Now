Imports System.Runtime.Serialization

<Serializable()>
<DataContract()>
Public Class UsuarioBE

    <DataMember(EmitDefaultValue:=False)> Public Property CodigoUsuario As String
    <DataMember(EmitDefaultValue:=False)> Public Property NombreUsuario As String
    <DataMember(EmitDefaultValue:=False)> Public Property ApellidoPaterno As String
    <DataMember(EmitDefaultValue:=False)> Public Property ApellidoMaterno As String
    <DataMember(EmitDefaultValue:=False)> Public Property UsuarioLogon As String
    <DataMember(EmitDefaultValue:=False)> Public Property UsuarioPassword As String
    <DataMember(EmitDefaultValue:=False)> Public Property CorreoElectronico As String
    <DataMember(EmitDefaultValue:=False)> Public Property TipoUsuario As String
    <DataMember(EmitDefaultValue:=False)> Public Property EstadoUsuario As String
    <DataMember(EmitDefaultValue:=False)> Public Property CodigoGerencia As String
    <DataMember(EmitDefaultValue:=False)> Public Property CodigoPerfil As String
    <DataMember(EmitDefaultValue:=False)> Public Property DescripcionPerfil As String
    <DataMember(EmitDefaultValue:=False)> Public Property ListaUsuarioPerfil As List(Of UsuarioPerfilBE)
    <DataMember(EmitDefaultValue:=False)> Public Property ListaUsuarioPerfilOpcion As List(Of UsuarioPerfilOpcionBE)
    <DataMember(EmitDefaultValue:=False)> Public Property EstacionUsuario As String
    <DataMember(EmitDefaultValue:=False)> Public Property UsuarioNT As String

    <DataMember(EmitDefaultValue:=False)> Public Property NomCompletoUsuario As String

    <DataMember(EmitDefaultValue:=False)> Public Property CCODPERSON As String
    <DataMember(EmitDefaultValue:=False)> Public Property EMPLEADO As String
    <DataMember(EmitDefaultValue:=False)> Public Property CCOARE As String

End Class
