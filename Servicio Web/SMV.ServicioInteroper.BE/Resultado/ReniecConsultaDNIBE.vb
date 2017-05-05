Imports System.Runtime.Serialization

<DataContract()>
Public Class ReniecConsultaDNIBE
    <DataMember(EmitDefaultValue:=False)> Public Property Nombres As String
    <DataMember(EmitDefaultValue:=False)> Public Property ApePaterno As String
    <DataMember(EmitDefaultValue:=False)> Public Property ApeMaterno As String
    <DataMember(EmitDefaultValue:=False)> Public Property FechaNacimiento As String
    <DataMember(EmitDefaultValue:=False)> Public Property Sexo As String
End Class
