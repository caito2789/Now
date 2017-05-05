Imports System.Runtime.Serialization

<Serializable()>
<DataContract()>
Public Class MininterBE

    <DataMember(EmitDefaultValue:=False)> Public Property TipoConsulta As Integer
    <DataMember(EmitDefaultValue:=False)> Public Property DNI As String
    <DataMember(EmitDefaultValue:=False)> Public Property ApePaterno As String
    <DataMember(EmitDefaultValue:=False)> Public Property ApeMaterno As String
    <DataMember(EmitDefaultValue:=False)> Public Property Nombres As String
    <DataMember(EmitDefaultValue:=False)> Public Property MensajeRespOK As String

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
