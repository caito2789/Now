Imports System.Runtime.Serialization

<DataContract()>
Public Class SuneduGradoAcademicoBE
    <DataMember(EmitDefaultValue:=False)> Public Property AbreviaturaTitulo As String
    <DataMember(EmitDefaultValue:=False)> Public Property ApePaterno As String
    <DataMember(EmitDefaultValue:=False)> Public Property ApeMaterno As String
    <DataMember(EmitDefaultValue:=False)> Public Property Nombres As String
    <DataMember(EmitDefaultValue:=False)> Public Property NumeroDocumento As String
    <DataMember(EmitDefaultValue:=False)> Public Property Pais As String
    <DataMember(EmitDefaultValue:=False)> Public Property Tipodocumento As String
    <DataMember(EmitDefaultValue:=False)> Public Property TituloProfesional As String
    <DataMember(EmitDefaultValue:=False)> Public Property Universidad As String
    <DataMember(EmitDefaultValue:=False)> Public Property Especialidad As String
End Class
