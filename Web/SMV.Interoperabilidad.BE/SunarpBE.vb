Imports System.Runtime.Serialization

<Serializable()>
<DataContract()>
Public Class SunarpBE

    <DataMember(EmitDefaultValue:=False)> Public Property ApePaterno As String
    <DataMember(EmitDefaultValue:=False)> Public Property ApeMaterno As String
    <DataMember(EmitDefaultValue:=False)> Public Property Nombres As String
    <DataMember(EmitDefaultValue:=False)> Public Property RazonSocial As String
    <DataMember(EmitDefaultValue:=False)> Public Property TipoDocumento As String
    <DataMember(EmitDefaultValue:=False)> Public Property NroDocumento As String
    <DataMember(EmitDefaultValue:=False)> Public Property NumPartida As String
    <DataMember(EmitDefaultValue:=False)> Public Property NumAsiento As String
    <DataMember(EmitDefaultValue:=False)> Public Property Registro As String
    <DataMember(EmitDefaultValue:=False)> Public Property NumPlaca As String
    <DataMember(EmitDefaultValue:=False)> Public Property Estado As String
    <DataMember(EmitDefaultValue:=False)> Public Property Solicitud As String
    <DataMember(EmitDefaultValue:=False)> Public Property Fecha As String
    <DataMember(EmitDefaultValue:=False)> Public Property Zona As String
    <DataMember(EmitDefaultValue:=False)> Public Property Oficina As String
    <DataMember(EmitDefaultValue:=False)> Public Property Cargo As String
    <DataMember(EmitDefaultValue:=False)> Public Property Email As String
    <DataMember(EmitDefaultValue:=False)> Public Property IDImgAsiento As String
    <DataMember(EmitDefaultValue:=False)> Public Property Tipo As String
    <DataMember(EmitDefaultValue:=False)> Public Property CantidadPag As String
    <DataMember(EmitDefaultValue:=False)> Public Property NroPagRef As String
    <DataMember(EmitDefaultValue:=False)> Public Property NroPag As String
    <DataMember(EmitDefaultValue:=False)> Public Property Transaccion As String
    <DataMember(EmitDefaultValue:=False)> Public Property NroTotalPag As String
    <DataMember(EmitDefaultValue:=False)> Public Property RutaImagen As String

    <DataMember(EmitDefaultValue:=False)> Public Property NombreTabla As String
    <DataMember(EmitDefaultValue:=False)> Public Property CodigoCampo1 As String
    <DataMember(EmitDefaultValue:=False)> Public Property CodigoCampo2 As String
    <DataMember(EmitDefaultValue:=False)> Public Property DescripcionValor As String
    <DataMember(EmitDefaultValue:=False)> Public Property Valor1 As String
    <DataMember(EmitDefaultValue:=False)> Public Property Valor2 As String

    <DataMember(EmitDefaultValue:=False)> Public Property TipoConsulta As Integer
    <DataMember(EmitDefaultValue:=False)> Public Property TipoParticipante As String
    <DataMember(EmitDefaultValue:=False)> Public Property objLista As List(Of SunarpBE)

    <DataMember(EmitDefaultValue:=False)> Public Property NCANTIDAD_ARR As Integer
    <DataMember(EmitDefaultValue:=False)> Public Property Arr_ApePaterno As [Object]()
    <DataMember(EmitDefaultValue:=False)> Public Property Arr_ApeMaterno As [Object]()
    <DataMember(EmitDefaultValue:=False)> Public Property Arr_Nombres As [Object]()
    <DataMember(EmitDefaultValue:=False)> Public Property Arr_RazonS As [Object]()
    <DataMember(EmitDefaultValue:=False)> Public Property Arr_TipoDocumento As [Object]()
    <DataMember(EmitDefaultValue:=False)> Public Property Arr_NroDocumento As [Object]()
    <DataMember(EmitDefaultValue:=False)> Public Property Arr_NumPartida As [Object]()
    <DataMember(EmitDefaultValue:=False)> Public Property Arr_Registro As [Object]()
    <DataMember(EmitDefaultValue:=False)> Public Property Arr_NumPlaca As [Object]()
    <DataMember(EmitDefaultValue:=False)> Public Property Arr_Zona As [Object]()
    <DataMember(EmitDefaultValue:=False)> Public Property Arr_Oficina As [Object]()
    <DataMember(EmitDefaultValue:=False)> Public Property Arr_Estado As [Object]()

    <DataMember(EmitDefaultValue:=False)> Public Property Arr_IDImgAsiento As [Object]()
    <DataMember(EmitDefaultValue:=False)> Public Property Arr_Tipo As [Object]()
    <DataMember(EmitDefaultValue:=False)> Public Property Arr_CantidadPag As [Object]()
    <DataMember(EmitDefaultValue:=False)> Public Property Arr_NroPagRef As [Object]()
    <DataMember(EmitDefaultValue:=False)> Public Property Arr_NroPag As [Object]()

    <DataMember(EmitDefaultValue:=False)> Public Property TipoServicio As Integer
    <DataMember(EmitDefaultValue:=False)> Public Property VUSER As String
    <DataMember(EmitDefaultValue:=False)> Public Property VCODAPLI As String
    <DataMember(EmitDefaultValue:=False)> Public Property NCODRESULTADO As Integer
    <DataMember(EmitDefaultValue:=False)> Public Property VMENSAJE_RESULTADO As String
    <DataMember(EmitDefaultValue:=False)> Public Property NCODACCION As Integer
    <DataMember(EmitDefaultValue:=False)> Public Property DFECHA As Date

    <DataMember(EmitDefaultValue:=False)> Public Property CodLog As Integer
    <DataMember(EmitDefaultValue:=False)> Public Property RutaArchivoPDF As String
    <DataMember(EmitDefaultValue:=False)> Public Property ZonaTXT As String
    <DataMember(EmitDefaultValue:=False)> Public Property OficinaTXT As String
    <DataMember(EmitDefaultValue:=False)> Public Property ResAsiento_WS As Integer

End Class
