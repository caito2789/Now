Imports Oracle.DataAccess.Client
Imports SMV.Interoperabilidad.BE

Public Class LogDA

    Public Function ObtenerListaTipoServicio(ByVal objFiltro As LogBE) As List(Of LogBE)
        Dim objListRegistrosBE As List(Of LogBE) = Nothing
        Try
            objListRegistrosBE = New List(Of LogBE)

            Using conexion = New OracleConnection(ConfiguracionBase.K_CADENA_CONEXION)
                Dim cmd As OracleCommand = conexion.CreateCommand()
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = String.Format("{0}.{1}.{2}", ConfiguracionBase.K_ESQUEMA_DEFAULT, "PKG_GESTION_INTEROPERABILIDAD", "SPU_SI_LIST_MAESTRAXNOM")

                cmd.Parameters.Add(New OracleParameter("PV_NOMTABLA", OracleDbType.Varchar2, 300)).Value = objFiltro.NombreTabla
                cmd.Parameters.Add("RS_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output

                conexion.Open()
                Using dr As IDataReader = cmd.ExecuteReader()
                    While dr.Read
                        objListRegistrosBE.Add(New LogBE With { _
                                               .DescripcionValor = dr("VDESCRIPCION").ToString().Trim(), _
                                               .Valor1 = dr("VVALOR1").ToString().Trim(), _
                                               .Valor2 = dr("VVALOR2").ToString().Trim() _
                                               })
                    End While
                End Using
                conexion.Close()
            End Using
        Catch ex As Exception
            Throw ex
        End Try
        Return objListRegistrosBE
    End Function

    Public Function ObtenerListaTipoEvento(ByVal objFiltro As LogBE) As List(Of LogBE)
        Dim objListRegistrosBE As List(Of LogBE) = Nothing
        Try
            objListRegistrosBE = New List(Of LogBE)

            Using conexion = New OracleConnection(ConfiguracionBase.K_CADENA_CONEXION)
                Dim cmd As OracleCommand = conexion.CreateCommand()
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = String.Format("{0}.{1}.{2}", ConfiguracionBase.K_ESQUEMA_DEFAULT, "PKG_GESTION_INTEROPERABILIDAD", "SPU_SI_LIST_MAESTRAXNOM")

                cmd.Parameters.Add(New OracleParameter("PV_NOMTABLA", OracleDbType.Varchar2, 300)).Value = objFiltro.NombreTabla
                cmd.Parameters.Add("RS_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output

                conexion.Open()
                Using dr As IDataReader = cmd.ExecuteReader()
                    While dr.Read
                        objListRegistrosBE.Add(New LogBE With { _
                                               .DescripcionValor = dr("VDESCRIPCION").ToString().Trim(), _
                                               .Valor1 = dr("VVALOR1").ToString().Trim(), _
                                               .Valor2 = dr("VVALOR2").ToString().Trim() _
                                               })
                    End While
                End Using
                conexion.Close()
            End Using
        Catch ex As Exception
            Throw ex
        End Try
        Return objListRegistrosBE
    End Function

    Public Function ObtenerListaLogs(ByVal objFiltro As LogBE) As List(Of LogBE)
        Dim objListRegistrosBE As List(Of LogBE) = Nothing
        Try
            objListRegistrosBE = New List(Of LogBE)

            Using conexion = New OracleConnection(ConfiguracionBase.K_CADENA_CONEXION)
                Dim cmd As OracleCommand = conexion.CreateCommand()
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = String.Format("{0}.{1}.{2}", ConfiguracionBase.K_ESQUEMA_DEFAULT, "PKG_GESTION_INTEROPERABILIDAD", "SPU_SI_LIST_LOGS")

                cmd.Parameters.Add(New OracleParameter("PN_CODTIPOSERVICIO", OracleDbType.Int32)).Value = objFiltro.CodTipoServicio
                cmd.Parameters.Add(New OracleParameter("PV_FECHAINI", OracleDbType.Varchar2, 10)).Value = objFiltro.FechaInicio
                cmd.Parameters.Add(New OracleParameter("PV_FECHAFIN", OracleDbType.Varchar2, 10)).Value = objFiltro.FechaFin
                cmd.Parameters.Add(New OracleParameter("PV_CODUSUARIO", OracleDbType.Varchar2, 3)).Value = objFiltro.CodUsuario
                cmd.Parameters.Add(New OracleParameter("PN_CODTIPOEVENTO", OracleDbType.Int32)).Value = objFiltro.CodTipoEvento
                cmd.Parameters.Add("RS_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output

                conexion.Open()

                Using dr As IDataReader = cmd.ExecuteReader()
                    While dr.Read
                        objListRegistrosBE.Add(New LogBE With { _
                                                .CodLog = Integer.Parse(dr("NCODLOG").ToString().Trim()), _
                                                .CodTipoServicio = Integer.Parse(dr("NCODTIPOSERVICIO").ToString().Trim()), _
                                                .TipoServicio = dr("VTIPOSERVICIO").ToString().Trim(), _
                                                .URLTipoServicio = dr("VURLTIPOSERVICIO").ToString().Trim(), _
                                                .TipoEvento = dr("VTIPOEVENTO").ToString().Trim(), _
                                                .Usuario = dr("VNOMBREUSUARIO").ToString().Trim(), _
                                                .CodTipoResultado = Integer.Parse(dr("NCODTIPORESULTADO").ToString().Trim()), _
                                                .TipoResultado = dr("VTIPORESULTADO").ToString().Trim(), _
                                                .FechaRegistro = dr("VFECHAREGISTRO").ToString().Trim(), _
                                                .Entrada = dr("VENTRADA").ToString().Trim(), _
                                                .Salida = dr("VSALIDA").ToString().Trim() _
                                                })
                    End While
                End Using

                conexion.Close()
            End Using
        Catch ex As Exception
            Throw ex
        End Try
        Return objListRegistrosBE
    End Function

    Public Function CargarDetalleSalida(ByVal objFiltro As LogBE) As List(Of LogBE)
        Dim objListRegistrosBE As List(Of LogBE) = Nothing
        Try
            objListRegistrosBE = New List(Of LogBE)

            Using conexion = New OracleConnection(ConfiguracionBase.K_CADENA_CONEXION)
                Dim cmd As OracleCommand = conexion.CreateCommand()
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = String.Format("{0}.{1}.{2}", ConfiguracionBase.K_ESQUEMA_DEFAULT, "PKG_GESTION_INTEROPERABILIDAD", "SPU_SI_OBTENERLOG_DETSALIDA")

                cmd.Parameters.Add(New OracleParameter("PN_CODLOG", OracleDbType.Int32)).Value = objFiltro.CodLog
                cmd.Parameters.Add("RS_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output

                conexion.Open()

                If (objFiltro.CodTipoServicio = Constantes.TipoServicio.K_SUNARP_TITU_BIENES) Then

                    Using dr As IDataReader = cmd.ExecuteReader()
                        While dr.Read
                            objListRegistrosBE.Add(New LogBE With { _
                                                    .ApePaterno = dr("VAPE_PAT").ToString().Trim(), _
                                                    .ApeMaterno = dr("VAPE_MAT").ToString().Trim(), _
                                                    .Nombres = dr("VNOMBRES").ToString().Trim(), _
                                                    .RazonSocial = dr("VRAZ_SOC").ToString().Trim(), _
                                                    .TipoDocumento = dr("VTIP_DOC").ToString().Trim(), _
                                                    .NroDocumento = dr("VNRO_DOC").ToString().Trim(), _
                                                    .NumPartida = dr("VPARTIDA").ToString().Trim(), _
                                                    .Registro = dr("VREGISTRO").ToString().Trim(), _
                                                    .NumPlaca = dr("VNUM_PLACA").ToString().Trim(), _
                                                    .Zona = dr("VZONA").ToString().Trim(), _
                                                    .Oficina = dr("VOFICINA").ToString().Trim(), _
                                                    .Estado = dr("VESTADO").ToString().Trim()
                                                    })
                        End While
                    End Using

                ElseIf (objFiltro.CodTipoServicio = Constantes.TipoServicio.K_SUNARP_LIST_ASIENTOS) Then

                    Using dr As IDataReader = cmd.ExecuteReader()
                        While dr.Read
                            objListRegistrosBE.Add(New LogBE With { _
                                                    .Transaccion = dr("VTRANSACCION").ToString().Trim(), _
                                                    .NroTotalPag = dr("VNRO_TOTAL_PAG").ToString().Trim(), _
                                                    .IDImgAsiento = dr("VID_IMG_ASIENTO").ToString().Trim(), _
                                                    .CantidadPag = dr("VNUM_PAGINAS").ToString().Trim(), _
                                                    .Tipo = dr("VTIPO").ToString().Trim(), _
                                                    .NroPagRef = dr("VNRO_PAG_REF").ToString().Trim(), _
                                                    .NroPag = dr("VPAGINA").ToString().Trim()
                                                    })
                        End While
                    End Using

                ElseIf (objFiltro.CodTipoServicio = Constantes.TipoServicio.K_SUNEDU_GRADOSYTIT) Then

                    Using dr As IDataReader = cmd.ExecuteReader()
                        While dr.Read
                            objListRegistrosBE.Add(New LogBE With { _
                                                    .ApePaterno = dr("VAPE_PAT").ToString().Trim(), _
                                                    .ApeMaterno = dr("VAPE_MAT").ToString().Trim(), _
                                                    .Nombres = dr("VNOMBRES").ToString().Trim(), _
                                                    .TipoDocumento = dr("VTIP_DOC").ToString().Trim(), _
                                                    .NroDocumento = dr("VNRO_DOC").ToString().Trim(), _
                                                    .Pais = dr("VPAIS").ToString().Trim(), _
                                                    .Universidad = dr("VUNIVERSIDAD").ToString().Trim(), _
                                                    .TitProfesional = dr("VTIT_PROF").ToString().Trim(), _
                                                    .AbrTitulo = dr("VABREV_TIT").ToString().Trim(), _
                                                    .Especialidad = dr("VESPECIALIDAD").ToString().Trim()
                                                    })
                        End While
                    End Using

                End If

                conexion.Close()
            End Using
        Catch ex As Exception
            Throw ex
        End Try
        Return objListRegistrosBE
    End Function

    Public Function CargarDetalleSalidaExportar(ByVal objFiltro As LogBE) As List(Of LogBE)
        Dim objListRegistrosBE As List(Of LogBE) = Nothing
        Try
            objListRegistrosBE = New List(Of LogBE)

            Using conexion = New OracleConnection(ConfiguracionBase.K_CADENA_CONEXION)
                Dim cmd As OracleCommand = conexion.CreateCommand()
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = String.Format("{0}.{1}.{2}", ConfiguracionBase.K_ESQUEMA_DEFAULT, "PKG_GESTION_INTEROPERABILIDAD", "SPU_SI_OBTENERLOG_DETSAL_EXP")

                cmd.Parameters.Add(New OracleParameter("PN_CODLOG", OracleDbType.Int32)).Value = objFiltro.CodLog
                cmd.Parameters.Add("RS_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output

                conexion.Open()

                Using dr As IDataReader = cmd.ExecuteReader()
                    While dr.Read
                        objListRegistrosBE.Add(New LogBE With { _
                                                .Cadena = dr("VCADENA").ToString().Trim()
                                                })
                    End While
                End Using

                conexion.Close()
            End Using
        Catch ex As Exception
            Throw ex
        End Try
        Return objListRegistrosBE
    End Function

    Public Function ObtenerCabeceraPDF(ByVal objFiltro As LogBE) As List(Of LogBE)
        Dim objListRegistrosBE As List(Of LogBE) = Nothing
        Try
            objListRegistrosBE = New List(Of LogBE)

            Using conexion = New OracleConnection(ConfiguracionBase.K_CADENA_CONEXION)
                Dim cmd As OracleCommand = conexion.CreateCommand()
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = String.Format("{0}.{1}.{2}", ConfiguracionBase.K_ESQUEMA_DEFAULT, "PKG_GESTION_INTEROPERABILIDAD", "SPU_SI_OBTLOG_CAB_PDF")

                cmd.Parameters.Add(New OracleParameter("PN_CODLOG", OracleDbType.Int32)).Value = objFiltro.CodLog
                cmd.Parameters.Add("RS_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output

                conexion.Open()

                Using dr As IDataReader = cmd.ExecuteReader()
                    While dr.Read
                        objListRegistrosBE.Add(New LogBE With { _
                                                .TipoServicio = dr("VTIPOSERVICIO").ToString().Trim(), _
                                                .Fuente = dr("VFUENTE").ToString().Trim(), _
                                                .Entidad = dr("VENTIDAD").ToString().Trim(), _
                                                .FechaRegistro = dr("VFECHAREGISTRO").ToString().Trim()
                                                })
                    End While
                End Using

                conexion.Close()
            End Using
        Catch ex As Exception
            Throw ex
        End Try
        Return objListRegistrosBE
    End Function

End Class
