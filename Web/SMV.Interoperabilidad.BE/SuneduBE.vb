Imports System.Runtime.Serialization

<Serializable()>
<DataContract()>
Public Class SuneduBE

    <DataMember(EmitDefaultValue:=False)> Public Property DNI As String    
    <DataMember(EmitDefaultValue:=False)> Public Property ApePaterno As String
    <DataMember(EmitDefaultValue:=False)> Public Property ApeMaterno As String
    <DataMember(EmitDefaultValue:=False)> Public Property Nombres As String
    <DataMember(EmitDefaultValue:=False)> Public Property TipoDocumento As String
    <DataMember(EmitDefaultValue:=False)> Public Property NroDocumento As String
    <DataMember(EmitDefaultValue:=False)> Public Property Pais As String
    <DataMember(EmitDefaultValue:=False)> Public Property Universidad As String
    <DataMember(EmitDefaultValue:=False)> Public Property TitProfesional As String
    <DataMember(EmitDefaultValue:=False)> Public Property AbrTitulo As String
    <DataMember(EmitDefaultValue:=False)> Public Property Especialidad As String

    <DataMember(EmitDefaultValue:=False)> Public Property objLista As List(Of SuneduBE)

    <DataMember(EmitDefaultValue:=False)> Public Property NCANTIDAD_ARR As Integer
    <DataMember(EmitDefaultValue:=False)> Public Property Arr_ApePaterno As [Object]()
    <DataMember(EmitDefaultValue:=False)> Public Property Arr_ApeMaterno As [Object]()
    <DataMember(EmitDefaultValue:=False)> Public Property Arr_Nombres As [Object]()
    <DataMember(EmitDefaultValue:=False)> Public Property Arr_TipoDocumento As [Object]()
    <DataMember(EmitDefaultValue:=False)> Public Property Arr_NroDocumento As [Object]()
    <DataMember(EmitDefaultValue:=False)> Public Property Arr_Pais As [Object]()
    <DataMember(EmitDefaultValue:=False)> Public Property Arr_Universidad As [Object]()
    <DataMember(EmitDefaultValue:=False)> Public Property Arr_TitProfesional As [Object]()
    <DataMember(EmitDefaultValue:=False)> Public Property Arr_AbrTitulo As [Object]()
    <DataMember(EmitDefaultValue:=False)> Public Property Arr_Especialidad As [Object]()

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
