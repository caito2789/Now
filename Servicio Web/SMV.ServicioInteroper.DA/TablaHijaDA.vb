Imports SMV.ServicioInteroper.BE
Imports Oracle.DataAccess.Client

Public Class TablaHijaDA
    Public Function ListarTablaHija(ByVal idTablaPadre As Integer) As List(Of TablaHijaBE)
        Dim lstTablaHijaBE As List(Of TablaHijaBE) = New List(Of TablaHijaBE)
        Try
            Using conexion = New OracleConnection(ConfiguracionBase.K_CADENA_CONEXION)
                Dim cmd As OracleCommand = conexion.CreateCommand()
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = String.Format("{0}.{1}.{2}", ConfiguracionBase.K_ESQUEMA_DEFAULT, "PKG_GESTION_INTEROPERABILIDAD", "SPI_SI_LIST_TABLAHIJA")

                cmd.Parameters.Add(New OracleParameter("PIID_TABLA_PADRE", OracleDbType.Int32)).Value = idTablaPadre
                cmd.Parameters.Add("RS_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output

                conexion.Open()
                Using dr As IDataReader = cmd.ExecuteReader()
                    While dr.Read
                        Dim obj As New TablaHijaBE()
                        obj.IdTablaHija = Convert.ToInt32(dr("IID_TABLA_HIJA"))
                        obj.Descripcion = dr("VDESCRIPCION").ToString()
                        obj.Valor1 = dr("VVALOR1").ToString()
                        obj.Valor2 = dr("VVALOR2").ToString()
                        obj.Estado = dr("CESTADO").ToString()
                        
                        lstTablaHijaBE.Add(obj)
                    End While
                End Using
            End Using

            Return lstTablaHijaBE
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
