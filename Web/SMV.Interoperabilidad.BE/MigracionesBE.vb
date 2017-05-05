Imports System.Runtime.Serialization

<Serializable()>
<DataContract()>
Public Class MigracionesBE

    <DataMember(EmitDefaultValue:=False)> Public Property CE As String
    <DataMember(EmitDefaultValue:=False)> Public Property ApePrimer As String
    <DataMember(EmitDefaultValue:=False)> Public Property ApeSegundo As String
    <DataMember(EmitDefaultValue:=False)> Public Property Nombres As String
    <DataMember(EmitDefaultValue:=False)> Public Property CalidadMigratoria As String

    <DataMember(EmitDefaultValue:=False)> Public Property TipoServicio As Integer
    <DataMember(EmitDefaultValue:=False)> Public Property VUSER As String
    <DataMember(EmitDefaultValue:=False)> Public Property VCODAPLI As String
    <DataMember(EmitDefaultValue:=False)> Public Property NCODRESULTADO As Integer
    <DataMember(EmitDefaultValue:=False)> Public Property VMENSAJE_RESULTADO As String
    <DataMember(EmitDefaultValue:=False)> Public Property NCODACCION As Integer
    <DataMember(EmitDefaultValue:=False)> Public Property DFECHA As Date

    <DataMember(EmitDefaultValue:=False)> Public Property CodLog As Integer
    <DataMember(EmitDefaultValue:=False)> Public Property RutaArchivoPDF As String

End Class
