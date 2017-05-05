Public Class ConfiguracionBase
    Public Shared K_CADENA_CONEXION As String = Configuration.ConfigurationManager.ConnectionStrings("BdOracle").ConnectionString

    Public Shared K_ESQUEMA_DEFAULT As String = Configuration.ConfigurationManager.AppSettings("EsquemaDefault")
End Class
