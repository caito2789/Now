Imports System.Runtime.Serialization

<DataContract()>
Public Class SunarpAsientoBE
    <DataMember(EmitDefaultValue:=False)> Public Property Transaccion As String
    <DataMember(EmitDefaultValue:=False)> Public Property TotalPag As String
    <DataMember(EmitDefaultValue:=False)> Public Property IdImgAsiento As String
    <DataMember(EmitDefaultValue:=False)> Public Property CantPaginas As String
    <DataMember(EmitDefaultValue:=False)> Public Property Tipo As String
    <DataMember(EmitDefaultValue:=False)> Public Property NroPagRef As String
    <DataMember(EmitDefaultValue:=False)> Public Property NroPagina As String
    <DataMember(EmitDefaultValue:=False)> Public Property RutaImagen As String
End Class
