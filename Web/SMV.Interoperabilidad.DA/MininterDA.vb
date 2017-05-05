Imports Oracle.DataAccess.Client
Imports SMV.Interoperabilidad.BE

Public Class MininterDA

    Public Function RegistrarLog(ByVal objFiltro As MininterBE) As Integer
        Dim intResultado As Integer = 0
        Try
            Dim inCodLog As Integer = 0
            objFiltro.CodLog = inCodLog

            Using conexion = New OracleConnection(ConfiguracionBase.K_CADENA_CONEXION)
                Dim cmd As OracleCommand = conexion.CreateCommand()
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = String.Format("{0}.{1}.{2}", ConfiguracionBase.K_ESQUEMA_DEFAULT, "PKG_GESTION_INTEROPERABILIDAD", "SPI_SI_INSERT_LOG_MININTER")
                cmd.Parameters.Add(New OracleParameter("PN_TIPOSERVICIO", OracleDbType.Int32)).Value = objFiltro.TipoServicio
                cmd.Parameters.Add(New OracleParameter("PV_USER", OracleDbType.Varchar2, 3)).Value = objFiltro.VUSER
                cmd.Parameters.Add(New OracleParameter("PV_CCOAPL", OracleDbType.Varchar2, 3)).Value = objFiltro.VCODAPLI
                cmd.Parameters.Add(New OracleParameter("PN_CODRESULTADO", OracleDbType.Int32)).Value = objFiltro.NCODRESULTADO
                cmd.Parameters.Add(New OracleParameter("PV_MENSAJE_RES", OracleDbType.Varchar2, 4000)).Value = objFiltro.VMENSAJE_RESULTADO
                cmd.Parameters.Add(New OracleParameter("PN_ACCION", OracleDbType.Int32)).Value = objFiltro.NCODACCION
                cmd.Parameters.Add(New OracleParameter("PD_FECHA", OracleDbType.Date)).Value = objFiltro.DFECHA
                cmd.Parameters.Add(New OracleParameter("PN_TIPO_CONS", OracleDbType.Int32)).Value = objFiltro.TipoConsulta
                cmd.Parameters.Add(New OracleParameter("PV_DNI", OracleDbType.Varchar2, 11)).Value = objFiltro.DNI
                cmd.Parameters.Add(New OracleParameter("PV_APE_PAT", OracleDbType.Varchar2, 500)).Value = objFiltro.ApePaterno
                cmd.Parameters.Add(New OracleParameter("PV_APE_MAT", OracleDbType.Varchar2, 500)).Value = objFiltro.ApeMaterno
                cmd.Parameters.Add(New OracleParameter("PV_NOMBRES", OracleDbType.Varchar2, 500)).Value = objFiltro.Nombres
                cmd.Parameters.Add(New OracleParameter("PV_MSJRESPOK", OracleDbType.Varchar2, 500)).Value = objFiltro.MensajeRespOK
                cmd.Parameters.Add(New OracleParameter("PN_CODLOG", OracleDbType.Int32, ParameterDirection.InputOutput))

                conexion.Open()

                intResultado = cmd.ExecuteNonQuery()

                Integer.TryParse(cmd.Parameters("PN_CODLOG").Value.ToString(), inCodLog)
                objFiltro.CodLog = inCodLog

                conexion.Close()
            End Using
        Catch ex As Exception
            Throw ex
        End Try
        Return intResultado
    End Function

End Class
