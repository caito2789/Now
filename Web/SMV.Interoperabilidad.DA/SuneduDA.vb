Imports Oracle.DataAccess.Client
Imports SMV.Interoperabilidad.BE

Public Class SuneduDA

    Public Function RegistrarLog(ByVal objFiltro As SuneduBE) As Integer
        Dim intResultado As Integer = 0
        Try
            Dim inCodLog As Integer = 0
            objFiltro.CodLog = inCodLog

            Dim op_ApePaterno As New OracleParameter()
            Dim op_ApeMaterno As New OracleParameter()
            Dim op_Nombres As New OracleParameter()
            Dim op_TipoDocumento As New OracleParameter()
            Dim op_NroDocumento As New OracleParameter()
            Dim op_Pais As New OracleParameter()
            Dim op_Universidad As New OracleParameter()
            Dim op_TitProfesional As New OracleParameter()
            Dim op_AbrTitulo As New OracleParameter()
            Dim op_Especialidad As New OracleParameter()

            If (objFiltro.NCANTIDAD_ARR > 0) Then

                op_ApePaterno.OracleDbType = OracleDbType.Varchar2
                op_ApePaterno.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                op_ApePaterno.Value = objFiltro.Arr_ApePaterno
                op_ApePaterno.Size = objFiltro.Arr_ApePaterno.Length

                op_ApeMaterno.OracleDbType = OracleDbType.Varchar2
                op_ApeMaterno.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                op_ApeMaterno.Value = objFiltro.Arr_ApeMaterno
                op_ApeMaterno.Size = objFiltro.Arr_ApeMaterno.Length

                op_Nombres.OracleDbType = OracleDbType.Varchar2
                op_Nombres.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                op_Nombres.Value = objFiltro.Arr_Nombres
                op_Nombres.Size = objFiltro.Arr_Nombres.Length

                op_TipoDocumento.OracleDbType = OracleDbType.Varchar2
                op_TipoDocumento.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                op_TipoDocumento.Value = objFiltro.Arr_TipoDocumento
                op_TipoDocumento.Size = objFiltro.Arr_TipoDocumento.Length

                op_NroDocumento.OracleDbType = OracleDbType.Varchar2
                op_NroDocumento.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                op_NroDocumento.Value = objFiltro.Arr_NroDocumento
                op_NroDocumento.Size = objFiltro.Arr_NroDocumento.Length

                op_Pais.OracleDbType = OracleDbType.Varchar2
                op_Pais.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                op_Pais.Value = objFiltro.Arr_Pais
                op_Pais.Size = objFiltro.Arr_Pais.Length

                op_Universidad.OracleDbType = OracleDbType.Varchar2
                op_Universidad.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                op_Universidad.Value = objFiltro.Arr_Universidad
                op_Universidad.Size = objFiltro.Arr_Universidad.Length

                op_TitProfesional.OracleDbType = OracleDbType.Varchar2
                op_TitProfesional.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                op_TitProfesional.Value = objFiltro.Arr_TitProfesional
                op_TitProfesional.Size = objFiltro.Arr_TitProfesional.Length

                op_AbrTitulo.OracleDbType = OracleDbType.Varchar2
                op_AbrTitulo.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                op_AbrTitulo.Value = objFiltro.Arr_AbrTitulo
                op_AbrTitulo.Size = objFiltro.Arr_AbrTitulo.Length

                op_Especialidad.OracleDbType = OracleDbType.Varchar2
                op_Especialidad.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                op_Especialidad.Value = objFiltro.Arr_Especialidad
                op_Especialidad.Size = objFiltro.Arr_Especialidad.Length

            End If

            Using conexion = New OracleConnection(ConfiguracionBase.K_CADENA_CONEXION)
                Dim cmd As OracleCommand = conexion.CreateCommand()
                cmd.CommandType = CommandType.StoredProcedure

                If (objFiltro.NCANTIDAD_ARR > 0) Then
                    cmd.CommandText = String.Format("{0}.{1}.{2}", ConfiguracionBase.K_ESQUEMA_DEFAULT, "PKG_GESTION_INTEROPERABILIDAD", "SPI_SI_INSERT_LOG_SUNEDU")
                Else
                    cmd.CommandText = String.Format("{0}.{1}.{2}", ConfiguracionBase.K_ESQUEMA_DEFAULT, "PKG_GESTION_INTEROPERABILIDAD", "SPI_SI_INSERT_LOG_SUNEDU_ERR")
                End If

                cmd.Parameters.Add(New OracleParameter("PN_TIPOSERVICIO", OracleDbType.Int32)).Value = objFiltro.TipoServicio
                cmd.Parameters.Add(New OracleParameter("PV_USER", OracleDbType.Varchar2, 3)).Value = objFiltro.VUSER
                cmd.Parameters.Add(New OracleParameter("PV_CCOAPL", OracleDbType.Varchar2, 3)).Value = objFiltro.VCODAPLI
                cmd.Parameters.Add(New OracleParameter("PN_CODRESULTADO", OracleDbType.Int32)).Value = objFiltro.NCODRESULTADO
                cmd.Parameters.Add(New OracleParameter("PV_MENSAJE_RES", OracleDbType.Varchar2, 4000)).Value = objFiltro.VMENSAJE_RESULTADO
                cmd.Parameters.Add(New OracleParameter("PN_ACCION", OracleDbType.Int32)).Value = objFiltro.NCODACCION
                cmd.Parameters.Add(New OracleParameter("PD_FECHA", OracleDbType.Date)).Value = objFiltro.DFECHA
                cmd.Parameters.Add(New OracleParameter("PV_DNI", OracleDbType.Varchar2, 11)).Value = objFiltro.DNI

                If (objFiltro.NCANTIDAD_ARR > 0) Then

                    cmd.Parameters.Add(New OracleParameter("PN_CANTIDAD_ARR", OracleDbType.Int32)).Value = objFiltro.NCANTIDAD_ARR                    
                    cmd.Parameters.Add(op_ApePaterno)
                    cmd.Parameters.Add(op_ApeMaterno)
                    cmd.Parameters.Add(op_Nombres)
                    cmd.Parameters.Add(op_TipoDocumento)
                    cmd.Parameters.Add(op_NroDocumento)
                    cmd.Parameters.Add(op_Pais)
                    cmd.Parameters.Add(op_Universidad)
                    cmd.Parameters.Add(op_TitProfesional)
                    cmd.Parameters.Add(op_AbrTitulo)
                    cmd.Parameters.Add(op_Especialidad)
                End If

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
