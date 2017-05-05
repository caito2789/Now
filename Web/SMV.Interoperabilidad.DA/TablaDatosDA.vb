Imports SMV.Interoperabilidad.BE
Imports Oracle.DataAccess.Client

Public Class TablaDatosDA
    Public Function ListarTablaPadre() As List(Of TablaDatosBE)
        Dim lstTablaHijaBE As List(Of TablaDatosBE) = New List(Of TablaDatosBE)
        Try
            Using conexion = New OracleConnection(ConfiguracionBase.K_CADENA_CONEXION)
                Dim cmd As OracleCommand = conexion.CreateCommand()
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = String.Format("{0}.{1}.{2}", ConfiguracionBase.K_ESQUEMA_DEFAULT, "PKG_GESTION_INTEROPERABILIDAD", "SPI_SI_LIST_TABLAPADRE")

                cmd.Parameters.Add("RS_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output

                conexion.Open()
                Using dr As IDataReader = cmd.ExecuteReader()
                    While dr.Read
                        Dim obj As New TablaDatosBE()
                        obj.IdTablaPadre = Convert.ToInt32(dr("IID_TABLA_PADRE"))
                        obj.Nombre = dr("VNOM_TABLA").ToString()
                        obj.Descripcion = dr("VDESCRIPCION_TABLA").ToString()
                        obj.Estado = dr("CESTADO").ToString()
                        obj.DesEstado = If(obj.Estado = Constantes.Estado.K_COD_ACTIVO, Constantes.Estado.K_DES_ACTIVO, Constantes.Estado.K_DES_INACTIVO)

                        lstTablaHijaBE.Add(obj)
                    End While
                End Using
            End Using

            Return lstTablaHijaBE
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ListarTablaHija(ByVal idTablaPadre As Integer) As List(Of TablaDatosBE)
        Dim lstTablaHijaBE As List(Of TablaDatosBE) = New List(Of TablaDatosBE)
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
                        Dim obj As New TablaDatosBE()
                        obj.IdTablaHija = Convert.ToInt32(dr("IID_TABLA_HIJA"))
                        obj.Descripcion = dr("VDESCRIPCION").ToString()
                        obj.Valor1 = dr("VVALOR1").ToString()
                        obj.Valor2 = dr("VVALOR2").ToString()
                        obj.Estado = dr("CESTADO").ToString()
                        obj.DesEstado = If(obj.Estado = Constantes.Estado.K_COD_ACTIVO, Constantes.Estado.K_DES_ACTIVO, Constantes.Estado.K_DES_INACTIVO)

                        lstTablaHijaBE.Add(obj)
                    End While
                End Using
            End Using

            Return lstTablaHijaBE
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ActualizarTablaHija(ByVal oTablaDatosBE As TablaDatosBE) As Integer
        Dim intResultado As Integer = 0
        Try
            Using conexion = New OracleConnection(ConfiguracionBase.K_CADENA_CONEXION)
                Dim cmd As OracleCommand = conexion.CreateCommand()
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = String.Format("{0}.{1}.{2}", ConfiguracionBase.K_ESQUEMA_DEFAULT, "PKG_GESTION_INTEROPERABILIDAD", "SPI_SI_UPDATE_TABLAHIJA")
                cmd.Parameters.Add(New OracleParameter("PIID_TABLA_HIJA", OracleDbType.Int32)).Value = oTablaDatosBE.IdTablaHija
                cmd.Parameters.Add(New OracleParameter("PVDESCRIPCION", OracleDbType.Varchar2)).Value = oTablaDatosBE.Descripcion
                cmd.Parameters.Add(New OracleParameter("PVVALOR1", OracleDbType.Varchar2)).Value = oTablaDatosBE.Valor1
                cmd.Parameters.Add(New OracleParameter("PVVALOR2", OracleDbType.Varchar2)).Value = oTablaDatosBE.Valor2
                cmd.Parameters.Add(New OracleParameter("PCESTADO", OracleDbType.Varchar2)).Value = oTablaDatosBE.Estado

                conexion.Open()

                intResultado = cmd.ExecuteNonQuery()

                conexion.Close()
            End Using
        Catch ex As Exception
            Throw ex
        End Try
        Return intResultado
    End Function
End Class
