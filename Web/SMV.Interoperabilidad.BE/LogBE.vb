Imports System.Runtime.Serialization

<Serializable()>
<DataContract()>
Public Class LogBE

    <DataMember(EmitDefaultValue:=False)> Public Property NombreTabla As String
    <DataMember(EmitDefaultValue:=False)> Public Property CodigoCampo1 As String
    <DataMember(EmitDefaultValue:=False)> Public Property CodigoCampo2 As String
    <DataMember(EmitDefaultValue:=False)> Public Property DescripcionValor As String
    <DataMember(EmitDefaultValue:=False)> Public Property Valor1 As String
    <DataMember(EmitDefaultValue:=False)> Public Property Valor2 As String

    <DataMember(EmitDefaultValue:=False)> Public Property TipoServicio As String
    <DataMember(EmitDefaultValue:=False)> Public Property TipoEvento As String
    <DataMember(EmitDefaultValue:=False)> Public Property Usuario As String
    <DataMember(EmitDefaultValue:=False)> Public Property TipoResultado As String
    <DataMember(EmitDefaultValue:=False)> Public Property FechaRegistro As String
    <DataMember(EmitDefaultValue:=False)> Public Property Entrada As String
    <DataMember(EmitDefaultValue:=False)> Public Property Salida As String

    <DataMember(EmitDefaultValue:=False)> Public Property CodLog As Integer

    <DataMember(EmitDefaultValue:=False)> Public Property CodTipoServicio As Integer
    <DataMember(EmitDefaultValue:=False)> Public Property FechaInicio As String
    <DataMember(EmitDefaultValue:=False)> Public Property FechaFin As String
    <DataMember(EmitDefaultValue:=False)> Public Property CodUsuario As String
    <DataMember(EmitDefaultValue:=False)> Public Property CodTipoEvento As Integer
    <DataMember(EmitDefaultValue:=False)> Public Property CodTipoResultado As Integer

    <DataMember(EmitDefaultValue:=False)> Public Property ApePaterno As String
    <DataMember(EmitDefaultValue:=False)> Public Property ApeMaterno As String
    <DataMember(EmitDefaultValue:=False)> Public Property Nombres As String
    <DataMember(EmitDefaultValue:=False)> Public Property RazonSocial As String
    <DataMember(EmitDefaultValue:=False)> Public Property TipoDocumento As String
    <DataMember(EmitDefaultValue:=False)> Public Property NroDocumento As String
    <DataMember(EmitDefaultValue:=False)> Public Property NumPartida As String
    <DataMember(EmitDefaultValue:=False)> Public Property Registro As String
    <DataMember(EmitDefaultValue:=False)> Public Property NumPlaca As String
    <DataMember(EmitDefaultValue:=False)> Public Property Zona As String
    <DataMember(EmitDefaultValue:=False)> Public Property Oficina As String
    <DataMember(EmitDefaultValue:=False)> Public Property Estado As String
    <DataMember(EmitDefaultValue:=False)> Public Property Transaccion As String
    <DataMember(EmitDefaultValue:=False)> Public Property NroTotalPag As String
    <DataMember(EmitDefaultValue:=False)> Public Property IDImgAsiento As String
    <DataMember(EmitDefaultValue:=False)> Public Property Tipo As String
    <DataMember(EmitDefaultValue:=False)> Public Property CantidadPag As String
    <DataMember(EmitDefaultValue:=False)> Public Property NroPagRef As String
    <DataMember(EmitDefaultValue:=False)> Public Property NroPag As String
    <DataMember(EmitDefaultValue:=False)> Public Property Pais As String
    <DataMember(EmitDefaultValue:=False)> Public Property Universidad As String
    <DataMember(EmitDefaultValue:=False)> Public Property TitProfesional As String
    <DataMember(EmitDefaultValue:=False)> Public Property AbrTitulo As String
    <DataMember(EmitDefaultValue:=False)> Public Property Especialidad As String

    <DataMember(EmitDefaultValue:=False)> Public Property Cadena As String

    <DataMember(EmitDefaultValue:=False)> Public Property Fuente As String
    <DataMember(EmitDefaultValue:=False)> Public Property Entidad As String

    <DataMember(EmitDefaultValue:=False)> Public Property DatosReniecBE As ReniecBE

    <DataMember(EmitDefaultValue:=False)> Public Property URLTipoServicio As String

End Class
