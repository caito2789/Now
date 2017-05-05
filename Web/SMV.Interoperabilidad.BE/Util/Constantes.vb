Public Class Constantes

#Region "Constantes"

    Public Const K_TITULO_APP As String = "SISTEMA DE INTEROPERABILIDAD"
    Public Const K_OPCION_SELECCIONE = "-- SELECCIONE --"
    Public Const K_OPCION_TODOS = "-- TODOS --"

    Public Shared K_MSJ_INTRO As String = Configuration.ConfigurationManager.AppSettings("MsjIntro_Sistema").ToString()
    Public Shared K_COD_APPLI As String = Configuration.ConfigurationManager.AppSettings("CodigoAplicacion").ToString()

    Public Const K_NOM_TABLA_REGISTRO As String = "REGISTROS"
    Public Const K_NOM_TABLA_ZONA As String = "ZONA"
    Public Const K_NOM_TABLA_OFICINA As String = "OFICINA"
    Public Const K_NOM_TABLA_TIPO_SERVICIO As String = "TIPO_SERVICIO"
    Public Const K_NOM_TABLA_TIPO_EVENTO As String = "TIPO_EVENTO"

    Public Const K_RUTA_CARPETA_ARCHIVO_IMG As String = "~/Temporal/Descargas_Tmp"
    Public Const K_RUTA_CARPETA_GENERAR_PDF As String = "~/Temporal/Generados_PDF"
    Public Const K_RUTA_CARPETA_FIRMADOS_PDF As String = "~/Temporal/Firmados_PDF"

    Public Shared K_RUTAWEB_IMGASIENTO As String = Configuration.ConfigurationManager.AppSettings("RutaWebImgAsiento").ToString()
    Public Shared K_RUTAWEB_FIRMA_PDF As String = Configuration.ConfigurationManager.AppSettings("RutaWebFirmaPDF").ToString()

    Public Const K_EXTENSION_PDF As String = ".pdf"
    Public Const K_PDF As String = "PDF"

#End Region

#Region "Estructuras"

    Public Structure ConfiguracionReportePDF
        Public Const K_URL_CARPETA_REPORTE = "~/Views/Reportes"
        Public Const K_RDLC_REPORTE_RENIEC_OK = "rptReniecValDNI.rdlc"
        Public Const K_RDLC_REPORTE_RENIEC_ERROR = "rptReniecValDNI_Err.rdlc"
        Public Const K_RDLC_REPORTE_MIGRACIONES_OK = "rptMigraCarnetExt.rdlc"
        Public Const K_RDLC_REPORTE_MIGRACIONES_ERROR = "rptMigraCarnetExt_Err.rdlc"
        Public Const K_RDLC_REPORTE_INPE = "rptInpeAntPenales.rdlc"
        Public Const K_RDLC_REPORTE_PODERJUDICIAL = "rptPJAntJudicial.rdlc"
        Public Const K_RDLC_REPORTE_MININTER = "rptMininterAntPol.rdlc"
        Public Const K_RDLC_REPORTE_SUNARP_VP_OK = "rptSunarpVigPod.rdlc"
        Public Const K_RDLC_REPORTE_SUNARP_VP_ERROR = "rptSunarpVigPod_Err.rdlc"
        Public Const K_RDLC_REPORTE_SUNARP_TB_OK = "rptSunarpTitBien.rdlc"
        Public Const K_RDLC_REPORTE_SUNARP_TB_ERROR = "rptSunarpTitBien_Err.rdlc"
        Public Const K_RDLC_REPORTE_SUNARP_LA_OK = "rptSunarpListAsiento.rdlc"
        Public Const K_RDLC_REPORTE_SUNARP_LA_ERROR = "rptSunarpListAsiento_Err.rdlc"
        Public Const K_RDLC_REPORTE_SUNEDU_OK = "rptSuneduGradYTit.rdlc"
        Public Const K_RDLC_REPORTE_SUNEDU_ERROR = "rptSuneduGradYTit_Err.rdlc"
    End Structure

    Public Structure ConfiguracionFirmaSignnet
        Public Shared Executable As String = Configuration.ConfigurationManager.AppSettings("Executable").ToString()
        Public Shared reason As String = Configuration.ConfigurationManager.AppSettings("reason").ToString()
        Public Shared location As String = Configuration.ConfigurationManager.AppSettings("location").ToString()
        Public Shared comment As String = Configuration.ConfigurationManager.AppSettings("comment").ToString()
        Public Shared boolSignnet As String = "0"
    End Structure

    Public Structure ConfiguracionCorreo
        'Public Shared CorreoRemitente As String = Configuration.ConfigurationManager.AppSettings("CorreoRemitente")
        'Public Shared NombreRemitente As String = Configuration.ConfigurationManager.AppSettings("NombreRemitente")
        'Public Shared Host As String = Configuration.ConfigurationManager.AppSettings("Host")
    End Structure

    Public Structure TipoConsulta_INPE
        Public Const K_POR_NOMBRES As Integer = 1
        Public Const K_POR_DNI As Integer = 2
    End Structure

    Public Structure TipoConsulta_MININTER
        Public Const K_POR_NOMBRES As Integer = 1
        Public Const K_POR_DNI As Integer = 2
    End Structure

    Public Structure TipoConsulta_SUNARP_TITULARIDADB_COD
        Public Const K_POR_NOMBRES As Integer = 1
        Public Const K_POR_RAZONSOCIAL As Integer = 2
    End Structure

    Public Structure TipoConsulta_SUNARP_TITULARIDADB_TXT
        Public Const K_POR_NOMBRES As String = "N"
        Public Const K_POR_RAZONSOCIAL As String = "J"
    End Structure

    Public Structure ConsultoSW
        Public Const K_NO = 0
        Public Const K_SI = 1
    End Structure

    Public Structure TipoServicio
        Public Const K_RENIEC_VAL_DNI As Integer = 1
        Public Const K_MIGRACIONES_CARNET_EXT As Integer = 2
        Public Const K_INPE_ANT_PENALES As Integer = 3
        Public Const K_PODERJUD_ANT_JUDICIALES As Integer = 4
        Public Const K_MININTER_ANT_POLICIALES As Integer = 5
        Public Const K_SUNARP_TITU_BIENES As Integer = 6
        Public Const K_SUNARP_VIG_PODER As Integer = 7
        Public Const K_SUNARP_LIST_ASIENTOS As Integer = 8
        Public Const K_SUNARP_VER_ASIENTO As Integer = 9
        Public Const K_SUNEDU_GRADOSYTIT As Integer = 10
    End Structure

    Public Structure Accion
        Public Const K_CONSULTA As Integer = 1
        Public Const K_EXPORTAR As Integer = 2
        Public Const K_VER_IMAGEN As Integer = 3
    End Structure

    Public Structure Cod_Resultado
        Public Const K_OK As Integer = 1
        Public Const K_Error As Integer = 2
    End Structure

    Public Structure Estado
        Public Const K_COD_ACTIVO As String = "A"
        Public Const K_COD_INACTIVO As String = "I"
        Public Const K_DES_ACTIVO As String = "ACTIVO"
        Public Const K_DES_INACTIVO As String = "INACTIVO"
    End Structure

#End Region

End Class
