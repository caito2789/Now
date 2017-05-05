Imports System.Runtime.Serialization

<Serializable()>
<DataContract()>
Public Class UsuarioPerfilOpcionBE

    <DataMember(EmitDefaultValue:=False)> Public Property CodigoAplicacion As String
    <DataMember(EmitDefaultValue:=False)> Public Property CodigoUsuario As String
    <DataMember(EmitDefaultValue:=False)> Public Property CodigoPerfil As String
    <DataMember(EmitDefaultValue:=False)> Public Property CodigoOpcion As String
    <DataMember(EmitDefaultValue:=False)> Public Property CodigoSecuencial As String
    <DataMember(EmitDefaultValue:=False)> Public Property Denominacion As String
    <DataMember(EmitDefaultValue:=False)> Public Property NombrePagina As String
    <DataMember(EmitDefaultValue:=False)> Public Property TipoOpcion As String
    <DataMember(EmitDefaultValue:=False)> Public Property TipoPublico As String
    <DataMember(EmitDefaultValue:=False)> Public Property TipoAcceso As String

End Class
