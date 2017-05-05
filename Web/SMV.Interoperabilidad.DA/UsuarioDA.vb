Imports Oracle.DataAccess.Client
Imports SMV.Interoperabilidad.BE

Public Class UsuarioDA

    Public Function ValidarUsuario(ByVal pobjUsuarioBE As UsuarioBE) As UsuarioBE
        Dim objBEUsuario As UsuarioBE = Nothing
        Try
            Using conexion = New OracleConnection(ConfiguracionBase.K_CADENA_CONEXION)
                Dim cmd As OracleCommand = conexion.CreateCommand()
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = String.Format("{0}.{1}.{2}", "SEGURIDAD", "PKG_MODSEGURIDAD", "SP_SEL_VALIDAUSUARIO")

                cmd.Parameters.Add(New OracleParameter("p_Usuario", OracleDbType.Varchar2)).Value = pobjUsuarioBE.UsuarioLogon
                cmd.Parameters.Add(New OracleParameter("p_Clave", OracleDbType.Varchar2)).Value = pobjUsuarioBE.UsuarioPassword
                cmd.Parameters.Add(New OracleParameter("p_TipoUsuario", OracleDbType.Varchar2)).Value = pobjUsuarioBE.TipoUsuario
                cmd.Parameters.Add("p_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output

                conexion.Open()
                Using dr As IDataReader = cmd.ExecuteReader()
                    While (dr.Read)

                        objBEUsuario = New UsuarioBE With {.CodigoUsuario = dr.GetValue(dr.GetOrdinal("CODIGO")).ToString().Trim(), _
                                                           .NombreUsuario = dr.GetString(dr.GetOrdinal("NOMBRE")).Trim(), _
                                                           .ApellidoPaterno = dr.GetString(dr.GetOrdinal("APEPATERNO")).Trim(), _
                                                           .ApellidoMaterno = dr.GetString(dr.GetOrdinal("APEMATERNO")).Trim(), _
                                                           .UsuarioLogon = dr.GetString(dr.GetOrdinal("LOGIN")).Trim(), _
                                                           .CorreoElectronico = dr.GetString(dr.GetOrdinal("EMAIL")).Trim(), _
                                                           .CodigoPerfil = dr.GetString(dr.GetOrdinal("PERFIL")).Trim(), _
                                                           .EstadoUsuario = dr.GetString(dr.GetOrdinal("FLAGBLOQUEADO")).Trim() _
                                                          }

                    End While
                End Using
                conexion.Close()
            End Using
        Catch ex As Exception
            Throw ex
        End Try
        Return objBEUsuario
    End Function

    Public Function ListarPerfilesPorUsuarioAplicacion(ByVal pobjUsuarioPerfilBE As UsuarioPerfilBE) As IEnumerable(Of UsuarioPerfilBE)
        Dim lstUsuarioPerfilBE As List(Of UsuarioPerfilBE) = Nothing
        Try
            lstUsuarioPerfilBE = New List(Of UsuarioPerfilBE)

            Using conexion = New OracleConnection(ConfiguracionBase.K_CADENA_CONEXION)
                Dim cmd As OracleCommand = conexion.CreateCommand()
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = String.Format("{0}.{1}", "WORKFLOW", "SPU_WFN_USUPRF_LISTAR")

                cmd.Parameters.Add(New OracleParameter("PV_CODIGO_USUARIO", OracleDbType.Varchar2)).Value = pobjUsuarioPerfilBE.CodigoUsuario
                cmd.Parameters.Add(New OracleParameter("PV_CODIGO_APLICACION", OracleDbType.Varchar2)).Value = pobjUsuarioPerfilBE.CodigoAplicacion
                cmd.Parameters.Add("RS_CURSOR_USUARIO_PERFIL", OracleDbType.RefCursor).Direction = ParameterDirection.Output

                conexion.Open()
                Using dr As IDataReader = cmd.ExecuteReader()
                    While dr.Read

                        lstUsuarioPerfilBE.Add(New UsuarioPerfilBE With { _
                                               .CodigoUsuario = dr("USPFC_VCODIGO_USUARIO").ToString().Trim(), _
                                               .CodigoAplicacion = dr("USPFC_VCODIGO_APLICACION").ToString().Trim(), _
                                               .CodigoPerfil = dr("USPFC_VCODIGO_PERFIL").ToString().Trim() _
                                               })

                    End While
                End Using
                conexion.Close()
            End Using
        Catch ex As Exception
            Throw ex
        End Try
        Return lstUsuarioPerfilBE
    End Function

    Public Function ListarOpcionPorUsuarioAplicacion(ByVal pbojUsuarioPerfilOpcionBE As BE.UsuarioPerfilOpcionBE) As IEnumerable(Of UsuarioPerfilOpcionBE)
        Dim lstUsuarioPerfilOpcionBE As List(Of UsuarioPerfilOpcionBE) = Nothing
        Try
            lstUsuarioPerfilOpcionBE = New List(Of UsuarioPerfilOpcionBE)

            Using conexion = New OracleConnection(ConfiguracionBase.K_CADENA_CONEXION)
                Dim cmd As OracleCommand = conexion.CreateCommand()
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = String.Format("{0}.{1}.{2}", "SIME", "PKG_SIME_SEGURIDAD", "SPU_SIME_LISTA_OPCIONES_MENU")

                cmd.Parameters.Add(New OracleParameter("pc_CCOUSR", OracleDbType.Char, 3)).Value = pbojUsuarioPerfilOpcionBE.CodigoUsuario
                cmd.Parameters.Add(New OracleParameter("pc_CCOAPL", OracleDbType.Char, 3)).Value = pbojUsuarioPerfilOpcionBE.CodigoAplicacion
                cmd.Parameters.Add("pcur_CURUSUARIO", OracleDbType.RefCursor).Direction = ParameterDirection.Output

                conexion.Open()
                Using dr As IDataReader = cmd.ExecuteReader()
                    While dr.Read

                        lstUsuarioPerfilOpcionBE.Add(New UsuarioPerfilOpcionBE With { _
                                                   .CodigoUsuario = dr(0).ToString().Trim(), _
                                                   .CodigoAplicacion = dr(1).ToString().Trim(), _
                                                   .CodigoOpcion = dr(2).ToString().Trim(), _
                                                   .CodigoSecuencial = dr(3).ToString().Trim(), _
                                                   .Denominacion = dr(4).ToString().Trim(), _
                                                   .NombrePagina = dr(5).ToString().Trim(), _
                                                   .TipoOpcion = dr(6).ToString().Trim(), _
                                                   .TipoPublico = dr(7).ToString().Trim(), _
                                                   .TipoAcceso = dr(8).ToString().Trim() _
                                                   })

                    End While
                End Using
                conexion.Close()
            End Using
        Catch ex As Exception
            Throw ex
        End Try
        Return lstUsuarioPerfilOpcionBE
    End Function

    Public Function ValidarUsuarioNT(ByVal pobjUsuarioBE As BE.UsuarioBE) As UsuarioBE
        Dim objBEUsuario As UsuarioBE = Nothing
        Try
            Using conexion = New OracleConnection(ConfiguracionBase.K_CADENA_CONEXION)
                Dim cmd As OracleCommand = conexion.CreateCommand()
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = String.Format("{0}.{1}", "WORKFLOW", "SPU_WFN_USUARIO_OBTENER_NT")

                cmd.Parameters.Add(New OracleParameter("PV_USUARIO_NT", OracleDbType.Varchar2)).Value = pobjUsuarioBE.UsuarioNT
                cmd.Parameters.Add("RS_CURSOR_USUARIO", OracleDbType.RefCursor).Direction = ParameterDirection.Output

                conexion.Open()
                Using dr As IDataReader = cmd.ExecuteReader()
                    While (dr.Read)

                        objBEUsuario = New UsuarioBE With {.CodigoUsuario = dr("CODIGO").ToString().Trim(), _
                                                           .NombreUsuario = dr("NOMBRE").ToString().Trim(), _
                                                           .ApellidoPaterno = dr("APEPATERNO").ToString().Trim(), _
                                                           .ApellidoMaterno = dr("APEMATERNO").ToString().Trim(), _
                                                           .UsuarioLogon = dr("LOGIN").ToString().Trim(), _
                                                           .CorreoElectronico = dr("EMAIL").ToString().Trim(), _
                                                           .EstadoUsuario = dr("FLAGBLOQUEADO").ToString().Trim() _
                                                          }

                    End While
                End Using
                conexion.Close()
            End Using
        Catch ex As Exception
            Throw ex
        End Try
        Return objBEUsuario
    End Function

    Public Function ObtenerListaUsuarios() As List(Of UsuarioBE)
        Dim objListUsuariosBE As List(Of UsuarioBE) = Nothing
        Try
            objListUsuariosBE = New List(Of UsuarioBE)

            Using conexion = New OracleConnection(ConfiguracionBase.K_CADENA_CONEXION)
                Dim cmd As OracleCommand = conexion.CreateCommand()
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = String.Format("{0}.{1}.{2}", ConfiguracionBase.K_ESQUEMA_DEFAULT, "PKG_GESTION_INTEROPERABILIDAD", "SPU_SI_LIST_USUARIOS")

                cmd.Parameters.Add("RS_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output

                conexion.Open()
                Using dr As IDataReader = cmd.ExecuteReader()
                    While dr.Read
                        objListUsuariosBE.Add(New UsuarioBE With { _
                                               .CodigoUsuario = dr("VCOD_USR").ToString().Trim(), _
                                               .NomCompletoUsuario = dr("VDESC_USU").ToString().Trim() _
                                               })
                    End While
                End Using
                conexion.Close()
            End Using
        Catch ex As Exception
            Throw ex
        End Try
        Return objListUsuariosBE
    End Function

End Class
