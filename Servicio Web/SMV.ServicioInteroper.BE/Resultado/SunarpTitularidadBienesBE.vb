Imports System.Runtime.Serialization

<DataContract()>
Public Class SunarpTitularidadBienesBE
    <DataMember(EmitDefaultValue:=False)> Public Property Registro As String
    <DataMember(EmitDefaultValue:=False)> Public Property ApePaterno As String
    <DataMember(EmitDefaultValue:=False)> Public Property ApeMaterno As String
    <DataMember(EmitDefaultValue:=False)> Public Property Nombres As String
    <DataMember(EmitDefaultValue:=False)> Public Property RazonSocial As String
    <DataMember(EmitDefaultValue:=False)> Public Property TipoDocumento As String
    <DataMember(EmitDefaultValue:=False)> Public Property NumeroDocumento As String
    <DataMember(EmitDefaultValue:=False)> Public Property NumeroPartida As String
    <DataMember(EmitDefaultValue:=False)> Public Property NumeroPlaca As String
    <DataMember(EmitDefaultValue:=False)> Public Property Estado As String
    <DataMember(EmitDefaultValue:=False)> Public Property Zona As String
    <DataMember(EmitDefaultValue:=False)> Public Property Oficina As String
End Class
