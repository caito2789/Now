Imports System.Runtime.Serialization

<DataContract()>
Public Class MigracionesCarnetExtranjeriaBE
    <DataMember(EmitDefaultValue:=False)> Public Property CalidadMigratoria As String
    <DataMember(EmitDefaultValue:=False)> Public Property Nombres As String
    <DataMember(EmitDefaultValue:=False)> Public Property NumRespuesta As String
    <DataMember(EmitDefaultValue:=False)> Public Property PrimerApellido As String
    <DataMember(EmitDefaultValue:=False)> Public Property SegundoApellido As String
End Class
