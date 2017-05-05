Public Class ConfiguracionBase

    Public Shared K_CADENA_CONEXION As String = Configuration.ConfigurationManager.ConnectionStrings("BdOracle").ConnectionString

    Public Shared K_ESQUEMA_DEFAULT As String = Configuration.ConfigurationManager.AppSettings("EsquemaDefault")
    Public Shared K_ESQUEMA_WORKFLOW As String = Configuration.ConfigurationManager.AppSettings("EsquemaWORKFLOW")
    Public Shared K_CODIGO_APLICACION As String = Configuration.ConfigurationManager.AppSettings("CodigoAplicacion")

End Class
