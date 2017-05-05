Imports Oracle.DataAccess.Client

Public Class UtilDA

    Shared Function Entero(ByVal dr As IDataReader, ByVal campo As String, Optional ByVal valor As Integer? = 0) As Integer?
        Return If(IsDBNull(dr(campo)), valor, Convert.ToInt32(dr(campo)))
    End Function

    Shared Function Entero(ByVal parameter As OracleParameter, Optional ByVal valor As Integer? = Nothing) As Integer?
        Return If(IsDBNull(parameter.Value), valor, Convert.ToInt32(parameter.Value.ToString))
    End Function

    Shared Function Cadena(ByVal dr As IDataReader, ByVal campo As String, Optional ByVal valor As String = "") As String
        Return If(IsDBNull(dr(campo)), valor, dr(campo).ToString().Trim())
    End Function

    Shared Function Cadena(ByVal parameter As OracleParameter, Optional ByVal valor As String = Nothing) As Integer?
        Return If(IsDBNull(parameter.Value), valor, parameter.Value.ToString)
    End Function

    Shared Function Fecha(ByVal dr As IDataReader, ByVal campo As String, Optional ByVal valor As Date? = Nothing) As Date?
        Return If(IsDBNull(dr(campo)), valor, Convert.ToDateTime(dr(campo)))
    End Function

    Shared Function Fecha(ByVal parameter As OracleParameter, Optional ByVal valor As Date = Nothing) As Date?
        Return If(IsDBNull(parameter.Value), valor, Convert.ToDateTime(parameter.Value.ToString))
    End Function

    Shared Function [Decimal](ByVal dr As IDataReader, ByVal campo As String, Optional ByVal valor As Decimal? = 0) As Decimal?
        Return If(IsDBNull(dr(campo)), valor, Convert.ToDecimal(dr(campo)))
    End Function

    Shared Function [Decimal](ByVal parameter As OracleParameter, Optional ByVal valor As Decimal? = Nothing) As Decimal?
        Return If(IsDBNull(parameter.Value), valor, Convert.ToDecimal(parameter.Value.ToString))
    End Function

End Class
