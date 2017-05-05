Imports System.Runtime.Serialization

<DataContract()>
Public Class SunarpVigenciaPoderBE
    <DataMember(EmitDefaultValue:=False)> Public Property Estado As String
    <DataMember(EmitDefaultValue:=False)> Public Property Solicitud As String
    <DataMember(EmitDefaultValue:=False)> Public Property Fecha As String
End Class
