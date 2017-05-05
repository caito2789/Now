Imports System.ServiceModel
Imports SMV.ServicioInteroper.BE

<ServiceContract()>
Public Interface IServiceIO

    <OperationContract()>
    Function ReniecConsultaDNI(ByVal request As ReniecConsultaDNIRequest) As ReniecConsultaDNIResponse

    <OperationContract()>
    Function InpeAntecedentesPenales(ByVal request As InpeAntecedentesPenalesRequest) As InpeAntecedentesPenalesResponse

    <OperationContract()>
    Function PJAntecedentesJudiciales(ByVal request As PJAntecedentesJudicialesRequest) As PJAntecedentesJudicialesResponse

    <OperationContract()>
    Function MininterAntecedentesPoliciales(ByVal request As MininterAntecedentesPolicialesRequest) As MininterAntecedentesPolicialesResponse

    <OperationContract()>
    Function SunarpTitularidadBienes(ByVal request As SunarpTitularidadBienesRequest) As SunarpTitularidadBienesResponse

    <OperationContract()>
    Function SunarpVigenciaPoder(ByVal request As SunarpVigenciaPoderRequest) As SunarpVigenciaPoderResponse

    <OperationContract()>
    Function SunarpListaAsiento(ByVal request As SunarpListaAsientoRequest) As SunarpListaAsientoResponse

    <OperationContract()>
    Function SunarpVerImagenAsiento(ByVal request As SunarpVerImagenAsientoRequest) As SunarpVerImagenAsientoResponse

    <OperationContract()>
    Function SuneduGradosTitulos(ByVal request As SuneduGradosTitulosRequest) As SuneduGradosTitulosResponse

    <OperationContract()>
    Function MigracionesCarnetExtranjeria(ByVal request As MigracionesCarnetExtranjeriaRequest) As MigracionesCarnetExtranjeriaResponse
End Interface

' 1.	RENIEC: Identidad – Consulta DNI:
<DataContract()>
Public Class ReniecConsultaDNIRequest
    <DataMember(EmitDefaultValue:=False)> Public Property DNI() As String
    <DataMember(EmitDefaultValue:=False)> Public Property CODUSER() As String
    <DataMember(EmitDefaultValue:=False)> Public Property CODAPP() As String
End Class
<DataContract()>
Public Class ReniecConsultaDNIResponse
    <DataMember(EmitDefaultValue:=False)> Public Property Correcto() As Boolean
    <DataMember(EmitDefaultValue:=False)> Public Property MensajeError() As String
    <DataMember(EmitDefaultValue:=False)> Public Property Resultado() As ReniecConsultaDNIBE
End Class

'2.	INPE: Antecedentes Penales:
<DataContract()>
Public Class InpeAntecedentesPenalesRequest
    <DataMember(EmitDefaultValue:=False)> Public Property DNI() As String
    <DataMember(EmitDefaultValue:=False)> Public Property ApellidoPaterno() As String
    <DataMember(EmitDefaultValue:=False)> Public Property ApellidoMaterno() As String
    <DataMember(EmitDefaultValue:=False)> Public Property Nombre1() As String
    <DataMember(EmitDefaultValue:=False)> Public Property Nombre2() As String
    <DataMember(EmitDefaultValue:=False)> Public Property Nombre3() As String
    <DataMember(EmitDefaultValue:=False)> Public Property CODUSER() As String
    <DataMember(EmitDefaultValue:=False)> Public Property CODAPP() As String
End Class
<DataContract()>
Public Class InpeAntecedentesPenalesResponse
    <DataMember(EmitDefaultValue:=False)> Public Property Correcto() As Boolean
    <DataMember(EmitDefaultValue:=False)> Public Property MensajeError() As String
    <DataMember(EmitDefaultValue:=False)> Public Property Resultado() As String
End Class

'3.	Poder Judicial: Antecedentes Judiciales
<DataContract()>
Public Class PJAntecedentesJudicialesRequest
    <DataMember(EmitDefaultValue:=False)> Public Property ApellidoPaterno() As String
    <DataMember(EmitDefaultValue:=False)> Public Property ApellidoMaterno() As String
    <DataMember(EmitDefaultValue:=False)> Public Property Nombres() As String
    <DataMember(EmitDefaultValue:=False)> Public Property CODUSER() As String
    <DataMember(EmitDefaultValue:=False)> Public Property CODAPP() As String
End Class
<DataContract()>
Public Class PJAntecedentesJudicialesResponse
    <DataMember(EmitDefaultValue:=False)> Public Property Correcto() As Boolean
    <DataMember(EmitDefaultValue:=False)> Public Property MensajeError() As String
    <DataMember(EmitDefaultValue:=False)> Public Property Resultado() As String
End Class

'4.	Ministerio del Interior: Antecedentes Policiales
<DataContract()>
Public Class MininterAntecedentesPolicialesRequest
    <DataMember(EmitDefaultValue:=False)> Public Property TipoConsulta() As String
    <DataMember(EmitDefaultValue:=False)> Public Property DNI() As String
    <DataMember(EmitDefaultValue:=False)> Public Property ApellidoPaterno() As String
    <DataMember(EmitDefaultValue:=False)> Public Property ApellidoMaterno() As String
    <DataMember(EmitDefaultValue:=False)> Public Property Nombres() As String
    <DataMember(EmitDefaultValue:=False)> Public Property CODUSER() As String
    <DataMember(EmitDefaultValue:=False)> Public Property CODAPP() As String
End Class
<DataContract()>
Public Class MininterAntecedentesPolicialesResponse
    <DataMember(EmitDefaultValue:=False)> Public Property Correcto() As Boolean
    <DataMember(EmitDefaultValue:=False)> Public Property MensajeError() As String
    <DataMember(EmitDefaultValue:=False)> Public Property Resultado() As String
End Class

'5.1 SUNARP Consulta Titularidad de Bienes de Consulta 
<DataContract()>
Public Class SunarpTitularidadBienesRequest
    <DataMember(EmitDefaultValue:=False)> Public Property TipoParticipante() As String
    <DataMember(EmitDefaultValue:=False)> Public Property ApellidoPaterno() As String
    <DataMember(EmitDefaultValue:=False)> Public Property ApellidoMaterno() As String
    <DataMember(EmitDefaultValue:=False)> Public Property Nombres() As String
    <DataMember(EmitDefaultValue:=False)> Public Property RazonSocial() As String
    <DataMember(EmitDefaultValue:=False)> Public Property CODUSER() As String
    <DataMember(EmitDefaultValue:=False)> Public Property CODAPP() As String
End Class
<DataContract()>
Public Class SunarpTitularidadBienesResponse
    <DataMember(EmitDefaultValue:=False)> Public Property Correcto() As Boolean
    <DataMember(EmitDefaultValue:=False)> Public Property MensajeError() As String
    <DataMember(EmitDefaultValue:=False)> Public Property Resultado() As List(Of SunarpTitularidadBienesBE)
End Class

'5.2 SUNARP Consulta Vigencia de Poder (Natural/Jurídica)
<DataContract()>
Public Class SunarpVigenciaPoderRequest
    <DataMember(EmitDefaultValue:=False)> Public Property Zona() As String
    <DataMember(EmitDefaultValue:=False)> Public Property Oficina() As String
    <DataMember(EmitDefaultValue:=False)> Public Property Partida() As String
    <DataMember(EmitDefaultValue:=False)> Public Property Asiento() As String
    <DataMember(EmitDefaultValue:=False)> Public Property ApellidoPaterno() As String
    <DataMember(EmitDefaultValue:=False)> Public Property ApellidoMaterno() As String
    <DataMember(EmitDefaultValue:=False)> Public Property Nombre() As String
    <DataMember(EmitDefaultValue:=False)> Public Property Cargo() As String
    <DataMember(EmitDefaultValue:=False)> Public Property Email() As String
    <DataMember(EmitDefaultValue:=False)> Public Property CODUSER() As String
    <DataMember(EmitDefaultValue:=False)> Public Property CODAPP() As String
End Class
<DataContract()>
Public Class SunarpVigenciaPoderResponse
    <DataMember(EmitDefaultValue:=False)> Public Property Correcto() As Boolean
    <DataMember(EmitDefaultValue:=False)> Public Property MensajeError() As String
    <DataMember(EmitDefaultValue:=False)> Public Property Resultado() As SunarpVigenciaPoderBE
End Class

'5.3 SUNARP Consulta Lista de Asientos
<DataContract()>
Public Class SunarpListaAsientoRequest
    <DataMember(EmitDefaultValue:=False)> Public Property Zona() As String
    <DataMember(EmitDefaultValue:=False)> Public Property Oficina() As String
    <DataMember(EmitDefaultValue:=False)> Public Property Partida() As String
    <DataMember(EmitDefaultValue:=False)> Public Property Registro() As String
    <DataMember(EmitDefaultValue:=False)> Public Property CODUSER() As String
    <DataMember(EmitDefaultValue:=False)> Public Property CODAPP() As String
End Class
<DataContract()>
Public Class SunarpListaAsientoResponse
    <DataMember(EmitDefaultValue:=False)> Public Property Correcto() As Boolean
    <DataMember(EmitDefaultValue:=False)> Public Property MensajeError() As String
    <DataMember(EmitDefaultValue:=False)> Public Property Resultado() As List(Of SunarpAsientoBE)
End Class

'5.3.1 SUNARP Ver Imagen Asiento
<DataContract()>
Public Class SunarpVerImagenAsientoRequest
    <DataMember(EmitDefaultValue:=False)> Public Property Transaccion() As String
    <DataMember(EmitDefaultValue:=False)> Public Property IdImg() As String
    <DataMember(EmitDefaultValue:=False)> Public Property Tipo() As String
    <DataMember(EmitDefaultValue:=False)> Public Property NroTotalPag() As String
    <DataMember(EmitDefaultValue:=False)> Public Property NroPagRef() As String
    <DataMember(EmitDefaultValue:=False)> Public Property Pagina() As String
    <DataMember(EmitDefaultValue:=False)> Public Property CODUSER() As String
    <DataMember(EmitDefaultValue:=False)> Public Property CODAPP() As String
End Class
<DataContract()>
Public Class SunarpVerImagenAsientoResponse
    <DataMember(EmitDefaultValue:=False)> Public Property Correcto() As Boolean
    <DataMember(EmitDefaultValue:=False)> Public Property MensajeError() As String
    <DataMember(EmitDefaultValue:=False)> Public Property Resultado() As String
End Class

'6. SUNEDU: Consulta de Grados y Títulos
<DataContract()>
Public Class SuneduGradosTitulosRequest
    <DataMember(EmitDefaultValue:=False)> Public Property DNI() As String
    <DataMember(EmitDefaultValue:=False)> Public Property CODUSER() As String
    <DataMember(EmitDefaultValue:=False)> Public Property CODAPP() As String
End Class
<DataContract()>
Public Class SuneduGradosTitulosResponse
    <DataMember(EmitDefaultValue:=False)> Public Property Correcto() As Boolean
    <DataMember(EmitDefaultValue:=False)> Public Property MensajeError() As String
    <DataMember(EmitDefaultValue:=False)> Public Property Resultado() As List(Of SuneduGradoAcademicoBE)
End Class

'7. MIGRACIONES: Carnet Extranjeria
<DataContract()>
Public Class MigracionesCarnetExtranjeriaRequest
    <DataMember(EmitDefaultValue:=False)> Public Property NumeroDocumento() As String
    <DataMember(EmitDefaultValue:=False)> Public Property CODUSER() As String
    <DataMember(EmitDefaultValue:=False)> Public Property CODAPP() As String
End Class
<DataContract()>
Public Class MigracionesCarnetExtranjeriaResponse
    <DataMember(EmitDefaultValue:=False)> Public Property Correcto() As Boolean
    <DataMember(EmitDefaultValue:=False)> Public Property MensajeError() As String
    <DataMember(EmitDefaultValue:=False)> Public Property Resultado() As MigracionesCarnetExtranjeriaBE
End Class

