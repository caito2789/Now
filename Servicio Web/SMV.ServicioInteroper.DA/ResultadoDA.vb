Imports Oracle.DataAccess.Client
Imports SMV.ServicioInteroper.BE

Public Class ResultadoDA

    Public Function ListarAsientoDetalle(oEntrada As AsientoBE) As List(Of AsientoBE)
        Dim lstAsientoBE As List(Of AsientoBE) = New List(Of AsientoBE)
        Try
            Using conexion = New OracleConnection(ConfiguracionBase.K_CADENA_CONEXION)
                Dim cmd As OracleCommand = conexion.CreateCommand()
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = String.Format("{0}.{1}.{2}", ConfiguracionBase.K_ESQUEMA_DEFAULT, "PKG_GESTION_INTEROPERABILIDAD", "SPI_SI_LIST_ASIENTODET")

                cmd.Parameters.Add(New OracleParameter("P_VTRANSACCION", OracleDbType.Varchar2)).Value = oEntrada.Transaccion
                cmd.Parameters.Add(New OracleParameter("P_VID_IMGASIENTO", OracleDbType.Varchar2)).Value = oEntrada.IdImg
                cmd.Parameters.Add(New OracleParameter("P_VTIPO_DOC", OracleDbType.Varchar2)).Value = oEntrada.Tipo
                cmd.Parameters.Add(New OracleParameter("P_NTOTAL_PAG", OracleDbType.Int32)).Value = oEntrada.NroTotalPag
                cmd.Parameters.Add(New OracleParameter("P_NNUM_PAGINA_REF", OracleDbType.Int32)).Value = oEntrada.NroPagRef
                cmd.Parameters.Add(New OracleParameter("P_NPAGINA", OracleDbType.Int32)).Value = oEntrada.Pagina
                cmd.Parameters.Add("RS_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output

                conexion.Open()
                Using dr As IDataReader = cmd.ExecuteReader()
                    While dr.Read
                        Dim obj As New AsientoBE()
                        obj.iid_asiento_det = UtilDA.Entero(dr, "iid_asiento_det")
                        obj.iid_asiento = UtilDA.Entero(dr, "iid_asiento")
                        obj.Transaccion = UtilDA.Cadena(dr, "vtransaccion")
                        obj.NroTotalPag = UtilDA.Cadena(dr, "ntotal_pag")
                        obj.IdImg = UtilDA.Cadena(dr, "vid_imgasiento")
                        obj.CantPag = UtilDA.Entero(dr, "nnum_pagina")
                        obj.Tipo = UtilDA.Cadena(dr, "vtipo_doc")
                        obj.NroPagRef = UtilDA.Cadena(dr, "nnum_pagina_ref")
                        obj.Pagina = UtilDA.Cadena(dr, "npagina")
                        obj.RutaImagen = UtilDA.Cadena(dr, "vruta_imagen")
                        lstAsientoBE.Add(obj)
                    End While
                End Using
            End Using

            Return lstAsientoBE
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function Registrar_ReniecConsultaDNI(entrada As PersonaBE, salida As ReniecConsultaDNIBE) As Integer
        Dim intResultado As Integer = 0
        Try
            Using conexion = New OracleConnection(ConfiguracionBase.K_CADENA_CONEXION)
                Dim cmd As OracleCommand = conexion.CreateCommand()
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = String.Format("{0}.{1}.{2}", ConfiguracionBase.K_ESQUEMA_DEFAULT, "PKG_GESTION_INTEROPERABILIDAD", "SPI_SI_INSERT_RENIEC")
                cmd.Parameters.Add(New OracleParameter("PV_DNI", OracleDbType.Varchar2)).Value = entrada.DNI
                cmd.Parameters.Add(New OracleParameter("PV_APE_PAT", OracleDbType.Varchar2)).Value = salida.ApePaterno
                cmd.Parameters.Add(New OracleParameter("PV_APE_MAT", OracleDbType.Varchar2)).Value = salida.ApeMaterno
                cmd.Parameters.Add(New OracleParameter("PV_NOMBRES", OracleDbType.Varchar2)).Value = salida.Nombres

                Dim anio As Integer = salida.FechaNacimiento.Substring(0, 4)
                Dim mes As Integer = salida.FechaNacimiento.Substring(4, 2)
                Dim dia As Integer = salida.FechaNacimiento.Substring(6, 2)
                cmd.Parameters.Add(New OracleParameter("PV_FEC_NAC", OracleDbType.Date)).Value = New Date(anio, mes, dia)

                cmd.Parameters.Add(New OracleParameter("PV_SEXO", OracleDbType.Varchar2)).Value = salida.Sexo

                conexion.Open()

                intResultado = cmd.ExecuteNonQuery()

                conexion.Close()
            End Using
        Catch ex As Exception
            Throw ex
        End Try
        Return intResultado
    End Function

    Public Function Registrar_InpeAntecedentesPenales(entrada As PersonaBE, indAp As String) As Integer
        Dim intResultado As Integer = 0
        Try
            Using conexion = New OracleConnection(ConfiguracionBase.K_CADENA_CONEXION)
                Dim cmd As OracleCommand = conexion.CreateCommand()
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = String.Format("{0}.{1}.{2}", ConfiguracionBase.K_ESQUEMA_DEFAULT, "PKG_GESTION_INTEROPERABILIDAD", "SPI_SI_INSERT_ANTPENAL")
                cmd.Parameters.Add(New OracleParameter("P_VDNI", OracleDbType.Varchar2)).Value = entrada.DNI
                cmd.Parameters.Add(New OracleParameter("P_VAPE_PATERNO", OracleDbType.Varchar2)).Value = entrada.ApePaterno
                cmd.Parameters.Add(New OracleParameter("P_VAPE_MATERNO", OracleDbType.Varchar2)).Value = entrada.ApeMaterno
                cmd.Parameters.Add(New OracleParameter("P_VNOMBRE1", OracleDbType.Varchar2)).Value = entrada.Nombre1
                cmd.Parameters.Add(New OracleParameter("P_VNOMBRE2", OracleDbType.Varchar2)).Value = entrada.Nombre2
                cmd.Parameters.Add(New OracleParameter("P_VNOMBRE3", OracleDbType.Varchar2)).Value = entrada.Nombre3
                cmd.Parameters.Add(New OracleParameter("P_CIND_ANTPENAL", OracleDbType.Varchar2)).Value = indAp

                conexion.Open()

                intResultado = cmd.ExecuteNonQuery()

                conexion.Close()
            End Using
        Catch ex As Exception
            Throw ex
        End Try
        Return intResultado
    End Function

    Public Function Registrar_PJAntecedentesJudiciales(entrada As PersonaBE, indAj As String) As Integer
        Dim intResultado As Integer = 0
        Try
            Using conexion = New OracleConnection(ConfiguracionBase.K_CADENA_CONEXION)
                Dim cmd As OracleCommand = conexion.CreateCommand()
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = String.Format("{0}.{1}.{2}", ConfiguracionBase.K_ESQUEMA_DEFAULT, "PKG_GESTION_INTEROPERABILIDAD", "SPI_SI_INSERT_ANTJUDICAL")
                cmd.Parameters.Add(New OracleParameter("P_VNOMBRES", OracleDbType.Varchar2)).Value = entrada.Nombres
                cmd.Parameters.Add(New OracleParameter("P_VAPE_PATERNO", OracleDbType.Varchar2)).Value = entrada.ApePaterno
                cmd.Parameters.Add(New OracleParameter("P_VAPE_MATERNO", OracleDbType.Varchar2)).Value = entrada.ApeMaterno
                cmd.Parameters.Add(New OracleParameter("P_CIND_ANTJUDICIAL", OracleDbType.Varchar2)).Value = indAj

                conexion.Open()

                intResultado = cmd.ExecuteNonQuery()

                conexion.Close()
            End Using
        Catch ex As Exception
            Throw ex
        End Try
        Return intResultado
    End Function

    Public Function Registrar_MininterAntecedentesPoliciales(entrada As PersonaBE, indAp As String) As Integer
        Dim intResultado As Integer = 0
        Try
            Using conexion = New OracleConnection(ConfiguracionBase.K_CADENA_CONEXION)
                Dim cmd As OracleCommand = conexion.CreateCommand()
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = String.Format("{0}.{1}.{2}", ConfiguracionBase.K_ESQUEMA_DEFAULT, "PKG_GESTION_INTEROPERABILIDAD", "SPI_SI_INSERT_ANTPOLICIAL")
                cmd.Parameters.Add(New OracleParameter("P_VDNI", OracleDbType.Varchar2)).Value = entrada.DNI
                cmd.Parameters.Add(New OracleParameter("P_VNOMBRES", OracleDbType.Varchar2)).Value = entrada.Nombres
                cmd.Parameters.Add(New OracleParameter("P_VAPE_PATERNO", OracleDbType.Varchar2)).Value = entrada.ApePaterno
                cmd.Parameters.Add(New OracleParameter("P_VAPE_MATERNO", OracleDbType.Varchar2)).Value = entrada.ApeMaterno
                cmd.Parameters.Add(New OracleParameter("P_CIND_ANTPOLICIAL", OracleDbType.Varchar2)).Value = indAp

                conexion.Open()

                intResultado = cmd.ExecuteNonQuery()

                conexion.Close()
            End Using
        Catch ex As Exception
            Throw ex
        End Try
        Return intResultado
    End Function

    Public Function Registrar_SunarpTitularidadBienes(entrada As PersonaBE, salida As SunarpTitularidadBienesBE) As Integer
        Dim intResultado As Integer = 0
        Try
            Using conexion = New OracleConnection(ConfiguracionBase.K_CADENA_CONEXION)
                Dim cmd As OracleCommand = conexion.CreateCommand()
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = String.Format("{0}.{1}.{2}", ConfiguracionBase.K_ESQUEMA_DEFAULT, "PKG_GESTION_INTEROPERABILIDAD", "SPI_SI_INSERT_SUNARP_TIT")
                cmd.Parameters.Add(New OracleParameter("P_VREGISTRO", OracleDbType.Varchar2)).Value = salida.Registro
                cmd.Parameters.Add(New OracleParameter("P_VAPE_PATERNO", OracleDbType.Varchar2)).Value = salida.ApePaterno
                cmd.Parameters.Add(New OracleParameter("P_VAPE_MATERNO", OracleDbType.Varchar2)).Value = salida.ApeMaterno
                cmd.Parameters.Add(New OracleParameter("P_VNOMBRES", OracleDbType.Varchar2)).Value = salida.Nombres
                cmd.Parameters.Add(New OracleParameter("P_VRAZSOCIAL", OracleDbType.Varchar2)).Value = salida.RazonSocial
                cmd.Parameters.Add(New OracleParameter("P_VTIPO_DOC", OracleDbType.Varchar2)).Value = salida.TipoDocumento
                cmd.Parameters.Add(New OracleParameter("P_VNUM_DOC", OracleDbType.Varchar2)).Value = salida.NumeroDocumento
                cmd.Parameters.Add(New OracleParameter("P_VNUM_PARTIDA", OracleDbType.Varchar2)).Value = salida.NumeroPartida
                cmd.Parameters.Add(New OracleParameter("P_VNUM_PLACA", OracleDbType.Varchar2)).Value = salida.NumeroPlaca
                cmd.Parameters.Add(New OracleParameter("P_VESTADO", OracleDbType.Varchar2)).Value = salida.Estado
                cmd.Parameters.Add(New OracleParameter("P_VZONA", OracleDbType.Varchar2)).Value = salida.Zona
                cmd.Parameters.Add(New OracleParameter("P_VOFICINA", OracleDbType.Varchar2)).Value = salida.Oficina

                conexion.Open()

                intResultado = cmd.ExecuteNonQuery()

                conexion.Close()
            End Using
        Catch ex As Exception
            Throw ex
        End Try
        Return intResultado
    End Function

    Public Function Registrar_SunarpVigenciaPoder(entrada As PersonaBE, salida As SunarpVigenciaPoderBE) As Integer
        Dim intResultado As Integer = 0
        Try
            Using conexion = New OracleConnection(ConfiguracionBase.K_CADENA_CONEXION)
                Dim cmd As OracleCommand = conexion.CreateCommand()
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = String.Format("{0}.{1}.{2}", ConfiguracionBase.K_ESQUEMA_DEFAULT, "PKG_GESTION_INTEROPERABILIDAD", "SPI_SI_INSERT_SUNARP_VIGE")
                cmd.Parameters.Add(New OracleParameter("P_VCOD_ZONA", OracleDbType.Varchar2)).Value = entrada.Zona
                cmd.Parameters.Add(New OracleParameter("P_VCOD_OFICINA", OracleDbType.Varchar2)).Value = entrada.Oficina
                cmd.Parameters.Add(New OracleParameter("P_VPARTIDA", OracleDbType.Varchar2)).Value = entrada.Partida
                cmd.Parameters.Add(New OracleParameter("P_VASIENTO", OracleDbType.Varchar2)).Value = entrada.Asiento
                cmd.Parameters.Add(New OracleParameter("P_VAPE_PATERNO", OracleDbType.Varchar2)).Value = entrada.Partida
                cmd.Parameters.Add(New OracleParameter("P_VAPE_MATERNO", OracleDbType.Varchar2)).Value = entrada.ApeMaterno
                cmd.Parameters.Add(New OracleParameter("P_VNOMBRES", OracleDbType.Varchar2)).Value = entrada.Nombres
                cmd.Parameters.Add(New OracleParameter("P_VCARGO", OracleDbType.Varchar2)).Value = entrada.Cargo
                cmd.Parameters.Add(New OracleParameter("P_VEMAIL", OracleDbType.Varchar2)).Value = entrada.Email
                cmd.Parameters.Add(New OracleParameter("P_VESTADO", OracleDbType.Varchar2)).Value = salida.Estado
                cmd.Parameters.Add(New OracleParameter("P_VSOLICITUD", OracleDbType.Varchar2)).Value = salida.Solicitud
                cmd.Parameters.Add(New OracleParameter("P_DFECHA", OracleDbType.Varchar2)).Value = salida.Fecha

                conexion.Open()

                intResultado = cmd.ExecuteNonQuery()

                conexion.Close()
            End Using
        Catch ex As Exception
            Throw ex
        End Try
        Return intResultado
    End Function

    Public Function Registrar_SunarpListaAsiento(entrada As PersonaBE, salida As SunarpAsientoBE) As Integer
        Dim intResultado As Integer = 0
        Try
            Using conexion = New OracleConnection(ConfiguracionBase.K_CADENA_CONEXION)
                Dim cmd As OracleCommand = conexion.CreateCommand()
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = String.Format("{0}.{1}.{2}", ConfiguracionBase.K_ESQUEMA_DEFAULT, "PKG_GESTION_INTEROPERABILIDAD", "SPI_SI_INSERT_SUNARP_ASIE")
                cmd.Parameters.Add(New OracleParameter("P_VCOD_ZONA", OracleDbType.Varchar2)).Value = entrada.Zona
                cmd.Parameters.Add(New OracleParameter("P_VCOD_OFICINA", OracleDbType.Varchar2)).Value = entrada.Oficina
                cmd.Parameters.Add(New OracleParameter("P_VPARTIDA", OracleDbType.Varchar2)).Value = entrada.Partida
                cmd.Parameters.Add(New OracleParameter("P_VCOD_REGISTRO", OracleDbType.Varchar2)).Value = entrada.Registro
                cmd.Parameters.Add(New OracleParameter("P_VTRANSACCION", OracleDbType.Varchar2)).Value = salida.Transaccion
                cmd.Parameters.Add(New OracleParameter("P_NTOTAL_PAG", OracleDbType.Int32)).Value = salida.TotalPag
                cmd.Parameters.Add(New OracleParameter("P_VID_IMGASIENTO", OracleDbType.Varchar2)).Value = salida.IdImgAsiento
                cmd.Parameters.Add(New OracleParameter("P_NNUM_PAGINA", OracleDbType.Int32)).Value = salida.CantPaginas
                cmd.Parameters.Add(New OracleParameter("P_VTIPO_DOC", OracleDbType.Varchar2)).Value = salida.Tipo
                cmd.Parameters.Add(New OracleParameter("P_NNUM_PAGINA_REF", OracleDbType.Int32)).Value = salida.NroPagRef
                cmd.Parameters.Add(New OracleParameter("P_NPAGINA", OracleDbType.Int32)).Value = salida.NroPagina

                conexion.Open()

                intResultado = cmd.ExecuteNonQuery()

                conexion.Close()
            End Using
        Catch ex As Exception
            Throw ex
        End Try
        Return intResultado
    End Function

    Public Function Registrar_SuneduGradosTitulos(entrada As PersonaBE, salida As SuneduGradoAcademicoBE) As Integer
        Dim intResultado As Integer = 0
        Try
            Using conexion = New OracleConnection(ConfiguracionBase.K_CADENA_CONEXION)
                Dim cmd As OracleCommand = conexion.CreateCommand()
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = String.Format("{0}.{1}.{2}", ConfiguracionBase.K_ESQUEMA_DEFAULT, "PKG_GESTION_INTEROPERABILIDAD", "SPI_SI_INSERT_SUNEDU_GRAD")
                cmd.Parameters.Add(New OracleParameter("P_VDNI", OracleDbType.Varchar2)).Value = entrada.DNI
                cmd.Parameters.Add(New OracleParameter("P_VNOMBRES", OracleDbType.Varchar2)).Value = salida.Nombres
                cmd.Parameters.Add(New OracleParameter("P_VAPE_PATERNO", OracleDbType.Varchar2)).Value = salida.ApePaterno
                cmd.Parameters.Add(New OracleParameter("P_VAPE_MATERNO", OracleDbType.Varchar2)).Value = salida.ApeMaterno
                cmd.Parameters.Add(New OracleParameter("P_VPAIS", OracleDbType.Varchar2)).Value = salida.Pais
                cmd.Parameters.Add(New OracleParameter("P_VTIP_DOCUMENTO", OracleDbType.Varchar2)).Value = salida.Tipodocumento
                cmd.Parameters.Add(New OracleParameter("P_VNUMDOCUMENTO", OracleDbType.Varchar2)).Value = salida.NumeroDocumento
                cmd.Parameters.Add(New OracleParameter("P_VABREV_TITULO", OracleDbType.Varchar2)).Value = salida.AbreviaturaTitulo
                cmd.Parameters.Add(New OracleParameter("P_VTITULO", OracleDbType.Varchar2)).Value = salida.TituloProfesional
                cmd.Parameters.Add(New OracleParameter("P_VUNIVERSIDAD", OracleDbType.Varchar2)).Value = salida.Universidad
                cmd.Parameters.Add(New OracleParameter("P_VESPECIALIDAD", OracleDbType.Varchar2)).Value = salida.Especialidad

                conexion.Open()

                intResultado = cmd.ExecuteNonQuery()

                conexion.Close()
            End Using
        Catch ex As Exception
            Throw ex
        End Try
        Return intResultado
    End Function

    Public Function Registrar_MigracionesCarnetExtranjeria(entrada As PersonaBE, salida As MigracionesCarnetExtranjeriaBE) As Integer
        Dim intResultado As Integer = 0
        Try
            Using conexion = New OracleConnection(ConfiguracionBase.K_CADENA_CONEXION)
                Dim cmd As OracleCommand = conexion.CreateCommand()
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = String.Format("{0}.{1}.{2}", ConfiguracionBase.K_ESQUEMA_DEFAULT, "PKG_GESTION_INTEROPERABILIDAD", "SPI_SI_INSERT_MIGRACION_CARN")
                cmd.Parameters.Add(New OracleParameter("P_VCE", OracleDbType.Varchar2)).Value = entrada.NumeroDoc
                cmd.Parameters.Add(New OracleParameter("P_VNOMBRES", OracleDbType.Varchar2)).Value = salida.Nombres
                cmd.Parameters.Add(New OracleParameter("P_VAPE_PRIMER", OracleDbType.Varchar2)).Value = salida.PrimerApellido
                cmd.Parameters.Add(New OracleParameter("P_VAPE_SEGUNDO", OracleDbType.Varchar2)).Value = salida.SegundoApellido
                cmd.Parameters.Add(New OracleParameter("P_VCALIDAD_MIGRATORIA", OracleDbType.Varchar2)).Value = salida.CalidadMigratoria

                conexion.Open()

                intResultado = cmd.ExecuteNonQuery()

                conexion.Close()
            End Using
        Catch ex As Exception
            Throw ex
        End Try
        Return intResultado
    End Function

    Public Function Registrar_SunarpVerImagenAsiento(oEntrada As AsientoBE, rutaImagen As String) As Integer
        Dim intResultado As Integer = 0
        Try
            Using conexion = New OracleConnection(ConfiguracionBase.K_CADENA_CONEXION)
                Dim cmd As OracleCommand = conexion.CreateCommand()
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = String.Format("{0}.{1}.{2}", ConfiguracionBase.K_ESQUEMA_DEFAULT, "PKG_GESTION_INTEROPERABILIDAD", "SPI_SI_INSERT_SUNARP_IMG")
                cmd.Parameters.Add(New OracleParameter("P_VTRANSACCION", OracleDbType.Varchar2)).Value = oEntrada.Transaccion
                cmd.Parameters.Add(New OracleParameter("P_VID_IMGASIENTO", OracleDbType.Varchar2)).Value = oEntrada.IdImg
                cmd.Parameters.Add(New OracleParameter("P_VTIPO_DOC", OracleDbType.Varchar2)).Value = oEntrada.Tipo
                cmd.Parameters.Add(New OracleParameter("P_NTOTAL_PAG", OracleDbType.Int32)).Value = oEntrada.NroTotalPag
                cmd.Parameters.Add(New OracleParameter("P_NNUM_PAGINA_REF", OracleDbType.Int32)).Value = oEntrada.NroPagRef
                cmd.Parameters.Add(New OracleParameter("P_NPAGINA", OracleDbType.Int32)).Value = oEntrada.Pagina
                cmd.Parameters.Add(New OracleParameter("P_VRUTA_IMAGEN", OracleDbType.Varchar2)).Value = rutaImagen

                conexion.Open()

                intResultado = cmd.ExecuteNonQuery()

                conexion.Close()
            End Using
        Catch ex As Exception
            Throw ex
        End Try
        Return intResultado
    End Function

    ''REGISTRO DE LOG
    Public Function RegistrarLog_ReniecConsultaDNI(ByVal oLogBE As LogBE, entrada As PersonaBE, salida As ReniecConsultaDNIBE) As Integer
        Dim intResultado As Integer = 0
        Try
            Using conexion = New OracleConnection(ConfiguracionBase.K_CADENA_CONEXION)
                Dim cmd As OracleCommand = conexion.CreateCommand()
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = String.Format("{0}.{1}.{2}", ConfiguracionBase.K_ESQUEMA_DEFAULT, "PKG_GESTION_INTEROPERABILIDAD", "SPI_SI_INSERT_LOG_RENIEC")
                cmd.Parameters.Add(New OracleParameter("PN_TIPOSERVICIO", OracleDbType.Int32)).Value = oLogBE.TipoServicio
                cmd.Parameters.Add(New OracleParameter("PV_USER", OracleDbType.Varchar2)).Value = oLogBE.VUSER
                cmd.Parameters.Add(New OracleParameter("PV_CCOAPL", OracleDbType.Varchar2)).Value = oLogBE.VCODAPLI
                cmd.Parameters.Add(New OracleParameter("PN_CODRESULTADO", OracleDbType.Int32)).Value = oLogBE.NCODRESULTADO
                cmd.Parameters.Add(New OracleParameter("PV_MENSAJE_RES", OracleDbType.Varchar2)).Value = oLogBE.VMENSAJE_RESULTADO
                cmd.Parameters.Add(New OracleParameter("PN_ACCION", OracleDbType.Int32)).Value = oLogBE.NCODACCION
                cmd.Parameters.Add(New OracleParameter("PD_FECHA", OracleDbType.Date)).Value = oLogBE.DFECHA
                cmd.Parameters.Add(New OracleParameter("PV_DNI", OracleDbType.Varchar2)).Value = entrada.DNI
                cmd.Parameters.Add(New OracleParameter("PV_APE_PAT", OracleDbType.Varchar2)).Value = If(IsNothing(salida), "", salida.ApePaterno)
                cmd.Parameters.Add(New OracleParameter("PV_APE_MAT", OracleDbType.Varchar2)).Value = If(IsNothing(salida), "", salida.ApeMaterno)
                cmd.Parameters.Add(New OracleParameter("PV_NOMBRES", OracleDbType.Varchar2)).Value = If(IsNothing(salida), "", salida.Nombres)
                cmd.Parameters.Add(New OracleParameter("PV_FEC_NAC", OracleDbType.Varchar2)).Value = If(IsNothing(salida), "", salida.FechaNacimiento)
                cmd.Parameters.Add(New OracleParameter("PV_SEXO", OracleDbType.Varchar2)).Value = If(IsNothing(salida), "", salida.Sexo)
                cmd.Parameters.Add(New OracleParameter("PN_CODLOG", OracleDbType.Int32)).Direction = ParameterDirection.InputOutput

                conexion.Open()

                intResultado = cmd.ExecuteNonQuery()
                Dim Id = Convert.ToInt32(cmd.Parameters("PN_CODLOG").Value.ToString())

                conexion.Close()
            End Using
        Catch ex As Exception
            Throw ex
        End Try
        Return intResultado
    End Function

    Public Function RegistrarLog_InpeAntecedentesPenales(ByVal oLogBE As LogBE, entrada As PersonaBE, salida As String) As Integer
        Dim intResultado As Integer = 0
        Try
            Using conexion = New OracleConnection(ConfiguracionBase.K_CADENA_CONEXION)
                Dim cmd As OracleCommand = conexion.CreateCommand()
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = String.Format("{0}.{1}.{2}", ConfiguracionBase.K_ESQUEMA_DEFAULT, "PKG_GESTION_INTEROPERABILIDAD", "SPI_SI_INSERT_LOG_INPE")
                cmd.Parameters.Add(New OracleParameter("PN_TIPOSERVICIO", OracleDbType.Int32)).Value = oLogBE.TipoServicio
                cmd.Parameters.Add(New OracleParameter("PV_USER", OracleDbType.Varchar2)).Value = oLogBE.VUSER
                cmd.Parameters.Add(New OracleParameter("PV_CCOAPL", OracleDbType.Varchar2)).Value = oLogBE.VCODAPLI
                cmd.Parameters.Add(New OracleParameter("PN_CODRESULTADO", OracleDbType.Int32)).Value = oLogBE.NCODRESULTADO
                cmd.Parameters.Add(New OracleParameter("PV_MENSAJE_RES", OracleDbType.Varchar2)).Value = oLogBE.VMENSAJE_RESULTADO
                cmd.Parameters.Add(New OracleParameter("PN_ACCION", OracleDbType.Int32)).Value = oLogBE.NCODACCION
                cmd.Parameters.Add(New OracleParameter("PD_FECHA", OracleDbType.Date)).Value = oLogBE.DFECHA
                cmd.Parameters.Add(New OracleParameter("PV_TIPO_CONS", OracleDbType.Int32)).Value = 0 'objFiltro.TipoConsulta '
                cmd.Parameters.Add(New OracleParameter("PV_DNI", OracleDbType.Varchar2)).Value = entrada.DNI
                cmd.Parameters.Add(New OracleParameter("PV_APE_PAT", OracleDbType.Varchar2)).Value = entrada.ApePaterno
                cmd.Parameters.Add(New OracleParameter("PV_APE_MAT", OracleDbType.Varchar2)).Value = entrada.ApeMaterno
                cmd.Parameters.Add(New OracleParameter("PV_NOMBRES1", OracleDbType.Varchar2)).Value = entrada.Nombre1
                cmd.Parameters.Add(New OracleParameter("PV_NOMBRES2", OracleDbType.Varchar2)).Value = entrada.Nombre2
                cmd.Parameters.Add(New OracleParameter("PV_NOMBRES3", OracleDbType.Varchar2)).Value = entrada.Nombre3
                cmd.Parameters.Add(New OracleParameter("PV_MSJRESPOK", OracleDbType.Varchar2)).Value = salida
                cmd.Parameters.Add(New OracleParameter("PN_CODLOG", OracleDbType.Int32, ParameterDirection.InputOutput))

                conexion.Open()

                intResultado = cmd.ExecuteNonQuery()

                conexion.Close()
            End Using
        Catch ex As Exception
            Throw ex
        End Try
        Return intResultado
    End Function

    Public Function RegistrarLog_PJAntecedentesJudiciales(ByVal oLogBE As LogBE, entrada As PersonaBE, salida As String) As Integer
        Dim intResultado As Integer = 0
        Try
            Using conexion = New OracleConnection(ConfiguracionBase.K_CADENA_CONEXION)
                Dim cmd As OracleCommand = conexion.CreateCommand()
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = String.Format("{0}.{1}.{2}", ConfiguracionBase.K_ESQUEMA_DEFAULT, "PKG_GESTION_INTEROPERABILIDAD", "SPI_SI_INSERT_LOG_PODERJUD")
                cmd.Parameters.Add(New OracleParameter("PN_TIPOSERVICIO", OracleDbType.Int32)).Value = oLogBE.TipoServicio
                cmd.Parameters.Add(New OracleParameter("PV_USER", OracleDbType.Varchar2)).Value = oLogBE.VUSER
                cmd.Parameters.Add(New OracleParameter("PV_CCOAPL", OracleDbType.Varchar2)).Value = oLogBE.VCODAPLI
                cmd.Parameters.Add(New OracleParameter("PN_CODRESULTADO", OracleDbType.Int32)).Value = oLogBE.NCODRESULTADO
                cmd.Parameters.Add(New OracleParameter("PV_MENSAJE_RES", OracleDbType.Varchar2)).Value = oLogBE.VMENSAJE_RESULTADO
                cmd.Parameters.Add(New OracleParameter("PN_ACCION", OracleDbType.Int32)).Value = oLogBE.NCODACCION
                cmd.Parameters.Add(New OracleParameter("PD_FECHA", OracleDbType.Date)).Value = oLogBE.DFECHA
                cmd.Parameters.Add(New OracleParameter("PV_APE_PAT", OracleDbType.Varchar2)).Value = entrada.ApePaterno
                cmd.Parameters.Add(New OracleParameter("PV_APE_MAT", OracleDbType.Varchar2)).Value = entrada.ApeMaterno
                cmd.Parameters.Add(New OracleParameter("PV_NOMBRES", OracleDbType.Varchar2)).Value = entrada.Nombres
                cmd.Parameters.Add(New OracleParameter("PV_MSJRESPOK", OracleDbType.Varchar2)).Value = salida
                cmd.Parameters.Add(New OracleParameter("PN_CODLOG", OracleDbType.Int32, ParameterDirection.InputOutput))

                conexion.Open()

                intResultado = cmd.ExecuteNonQuery()

                conexion.Close()
            End Using
        Catch ex As Exception
            Throw ex
        End Try
        Return intResultado
    End Function

    Public Function RegistrarLog_MininterAntecedentesPoliciales(ByVal oLogBE As LogBE, entrada As PersonaBE, salida As String) As Integer
        Dim intResultado As Integer = 0
        Try
            Using conexion = New OracleConnection(ConfiguracionBase.K_CADENA_CONEXION)
                Dim cmd As OracleCommand = conexion.CreateCommand()
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = String.Format("{0}.{1}.{2}", ConfiguracionBase.K_ESQUEMA_DEFAULT, "PKG_GESTION_INTEROPERABILIDAD", "SPI_SI_INSERT_LOG_MININTER")
                cmd.Parameters.Add(New OracleParameter("PN_TIPOSERVICIO", OracleDbType.Int32)).Value = oLogBE.TipoServicio
                cmd.Parameters.Add(New OracleParameter("PV_USER", OracleDbType.Varchar2)).Value = oLogBE.VUSER
                cmd.Parameters.Add(New OracleParameter("PV_CCOAPL", OracleDbType.Varchar2)).Value = oLogBE.VCODAPLI
                cmd.Parameters.Add(New OracleParameter("PN_CODRESULTADO", OracleDbType.Int32)).Value = oLogBE.NCODRESULTADO
                cmd.Parameters.Add(New OracleParameter("PV_MENSAJE_RES", OracleDbType.Varchar2)).Value = oLogBE.VMENSAJE_RESULTADO
                cmd.Parameters.Add(New OracleParameter("PN_ACCION", OracleDbType.Int32)).Value = oLogBE.NCODACCION
                cmd.Parameters.Add(New OracleParameter("PD_FECHA", OracleDbType.Date)).Value = oLogBE.DFECHA
                cmd.Parameters.Add(New OracleParameter("PN_TIPO_CONS", OracleDbType.Int32)).Value = entrada.TipoConsulta
                cmd.Parameters.Add(New OracleParameter("PV_DNI", OracleDbType.Varchar2)).Value = entrada.DNI
                cmd.Parameters.Add(New OracleParameter("PV_APE_PAT", OracleDbType.Varchar2)).Value = entrada.ApePaterno
                cmd.Parameters.Add(New OracleParameter("PV_APE_MAT", OracleDbType.Varchar2)).Value = entrada.ApeMaterno
                cmd.Parameters.Add(New OracleParameter("PV_NOMBRES", OracleDbType.Varchar2)).Value = entrada.Nombres
                cmd.Parameters.Add(New OracleParameter("PV_MSJRESPOK", OracleDbType.Varchar2)).Value = salida
                cmd.Parameters.Add(New OracleParameter("PN_CODLOG", OracleDbType.Int32, ParameterDirection.InputOutput))

                conexion.Open()

                intResultado = cmd.ExecuteNonQuery()

                conexion.Close()
            End Using
        Catch ex As Exception
            Throw ex
        End Try
        Return intResultado
    End Function

    Public Function RegistrarLog_SunarpTitularidadBienes(ByVal oLogBE As LogBE, entrada As PersonaBE, salida As List(Of SunarpTitularidadBienesBE)) As Integer
        Dim intResultado As Integer = 0
        Dim CantidadResultado As Integer = salida.Count
        Try
            Using conexion = New OracleConnection(ConfiguracionBase.K_CADENA_CONEXION)
                Dim cmd As OracleCommand = conexion.CreateCommand()
                cmd.CommandType = CommandType.StoredProcedure
                If CantidadResultado > 0 Then
                    cmd.CommandText = String.Format("{0}.{1}.{2}", ConfiguracionBase.K_ESQUEMA_DEFAULT, "PKG_GESTION_INTEROPERABILIDAD", "SPI_SI_INSERT_LOG_SUNARP_TB")
                Else
                    cmd.CommandText = String.Format("{0}.{1}.{2}", ConfiguracionBase.K_ESQUEMA_DEFAULT, "PKG_GESTION_INTEROPERABILIDAD", "SPI_SI_INS_LOG_SUNARP_TB_ERR")
                End If

                cmd.Parameters.Add(New OracleParameter("PN_TIPOSERVICIO", OracleDbType.Int32)).Value = oLogBE.TipoServicio
                cmd.Parameters.Add(New OracleParameter("PV_USER", OracleDbType.Varchar2)).Value = oLogBE.VUSER
                cmd.Parameters.Add(New OracleParameter("PV_CCOAPL", OracleDbType.Varchar2)).Value = oLogBE.VCODAPLI
                cmd.Parameters.Add(New OracleParameter("PN_CODRESULTADO", OracleDbType.Int32)).Value = oLogBE.NCODRESULTADO
                cmd.Parameters.Add(New OracleParameter("PV_MENSAJE_RES", OracleDbType.Varchar2)).Value = oLogBE.VMENSAJE_RESULTADO
                cmd.Parameters.Add(New OracleParameter("PN_ACCION", OracleDbType.Int32)).Value = oLogBE.NCODACCION
                cmd.Parameters.Add(New OracleParameter("PD_FECHA", OracleDbType.Date)).Value = oLogBE.DFECHA
                cmd.Parameters.Add(New OracleParameter("PN_TIPO_CONS", OracleDbType.Int32)).Value = 0 'entrada.TipoConsulta
                cmd.Parameters.Add(New OracleParameter("PV_TIPO_PART", OracleDbType.Varchar2)).Value = entrada.TipoParticipante
                cmd.Parameters.Add(New OracleParameter("PV_APE_PAT", OracleDbType.Varchar2)).Value = entrada.ApePaterno
                cmd.Parameters.Add(New OracleParameter("PV_APE_MAT", OracleDbType.Varchar2)).Value = entrada.ApeMaterno
                cmd.Parameters.Add(New OracleParameter("PV_NOMBRES", OracleDbType.Varchar2)).Value = entrada.Nombres
                cmd.Parameters.Add(New OracleParameter("PV_RAZONS", OracleDbType.Varchar2)).Value = entrada.RazonSocial

                If CantidadResultado > 0 Then
                    Dim opApePaterno As OracleParameter = New OracleParameter("PARR_APEPATERNO", OracleDbType.Varchar2)
                    opApePaterno.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                    Dim opApeMaterno As OracleParameter = New OracleParameter("PARR_APEMATERNO", OracleDbType.Varchar2)
                    opApeMaterno.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                    Dim opNombres As OracleParameter = New OracleParameter("PARR_NOMBRES", OracleDbType.Varchar2)
                    opNombres.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                    Dim opRazonSocial As OracleParameter = New OracleParameter("PARR_RAZONS", OracleDbType.Varchar2)
                    opRazonSocial.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                    Dim opTipoDocumento As OracleParameter = New OracleParameter("PARR_TIPODOCUMENTO", OracleDbType.Varchar2)
                    opTipoDocumento.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                    Dim opNumeroDocumento As OracleParameter = New OracleParameter("PARR_NRODOCUMENTO", OracleDbType.Varchar2)
                    opNumeroDocumento.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                    Dim opNumeroPartida As OracleParameter = New OracleParameter("PARR_NUMPARTIDA", OracleDbType.Varchar2)
                    opNumeroPartida.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                    Dim opRegistro As OracleParameter = New OracleParameter("PARR_REGISTRO", OracleDbType.Varchar2)
                    opRegistro.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                    Dim opNumeroPlaca As OracleParameter = New OracleParameter("PARR_NUMPLACA", OracleDbType.Varchar2)
                    opNumeroPlaca.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                    Dim opZona As OracleParameter = New OracleParameter("PARR_ZONA", OracleDbType.Varchar2)
                    opZona.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                    Dim opOficina As OracleParameter = New OracleParameter("PARR_OFICINA", OracleDbType.Varchar2)
                    opOficina.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                    Dim opEstado As OracleParameter = New OracleParameter("PARR_ESTADO", OracleDbType.Varchar2)
                    opEstado.CollectionType = OracleCollectionType.PLSQLAssociativeArray

                    cmd.Parameters.Add(New OracleParameter("PN_CANTIDAD_ARR", OracleDbType.Int32)).Value = CantidadResultado
                    cmd.Parameters.Add(opApePaterno).Value = salida.Select(Function(o) o.ApePaterno).ToArray()
                    cmd.Parameters.Add(opApeMaterno).Value = salida.Select(Function(o) o.ApeMaterno).ToArray()
                    cmd.Parameters.Add(opNombres).Value = salida.Select(Function(o) o.Nombres).ToArray()
                    cmd.Parameters.Add(opRazonSocial).Value = salida.Select(Function(o) o.RazonSocial).ToArray()
                    cmd.Parameters.Add(opTipoDocumento).Value = salida.Select(Function(o) o.TipoDocumento).ToArray()
                    cmd.Parameters.Add(opNumeroDocumento).Value = salida.Select(Function(o) o.NumeroDocumento).ToArray()
                    cmd.Parameters.Add(opNumeroPartida).Value = salida.Select(Function(o) o.NumeroPartida).ToArray()
                    cmd.Parameters.Add(opRegistro).Value = salida.Select(Function(o) o.Registro).ToArray()
                    cmd.Parameters.Add(opNumeroPlaca).Value = salida.Select(Function(o) o.NumeroPlaca).ToArray()
                    cmd.Parameters.Add(opZona).Value = salida.Select(Function(o) o.Zona).ToArray()
                    cmd.Parameters.Add(opOficina).Value = salida.Select(Function(o) o.Oficina).ToArray()
                    cmd.Parameters.Add(opEstado).Value = salida.Select(Function(o) o.Estado).ToArray()
                End If
                cmd.Parameters.Add(New OracleParameter("PN_CODLOG", OracleDbType.Int32)).Direction = ParameterDirection.InputOutput

                conexion.Open()

                intResultado = cmd.ExecuteNonQuery()

                conexion.Close()
            End Using
        Catch ex As Exception
            Throw ex
        End Try
        Return intResultado
    End Function

    Public Function RegistrarLog_SunarpVigenciaPoder(ByVal oLogBE As LogBE, entrada As PersonaBE, salida As SunarpVigenciaPoderBE) As Integer
        Dim intResultado As Integer = 0
        Try
            Using conexion = New OracleConnection(ConfiguracionBase.K_CADENA_CONEXION)
                Dim cmd As OracleCommand = conexion.CreateCommand()
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = String.Format("{0}.{1}.{2}", ConfiguracionBase.K_ESQUEMA_DEFAULT, "PKG_GESTION_INTEROPERABILIDAD", "SPI_SI_INSERT_LOG_SUNARP_VP")
                cmd.Parameters.Add(New OracleParameter("PN_TIPOSERVICIO", OracleDbType.Int32)).Value = oLogBE.TipoServicio
                cmd.Parameters.Add(New OracleParameter("PV_USER", OracleDbType.Varchar2)).Value = oLogBE.VUSER
                cmd.Parameters.Add(New OracleParameter("PV_CCOAPL", OracleDbType.Varchar2)).Value = oLogBE.VCODAPLI
                cmd.Parameters.Add(New OracleParameter("PN_CODRESULTADO", OracleDbType.Int32)).Value = oLogBE.NCODRESULTADO
                cmd.Parameters.Add(New OracleParameter("PV_MENSAJE_RES", OracleDbType.Varchar2)).Value = oLogBE.VMENSAJE_RESULTADO
                cmd.Parameters.Add(New OracleParameter("PN_ACCION", OracleDbType.Int32)).Value = oLogBE.NCODACCION
                cmd.Parameters.Add(New OracleParameter("PD_FECHA", OracleDbType.Date)).Value = oLogBE.DFECHA
                cmd.Parameters.Add(New OracleParameter("PV_ZONA", OracleDbType.Varchar2)).Value = entrada.Zona
                cmd.Parameters.Add(New OracleParameter("PV_OFICINA", OracleDbType.Varchar2)).Value = entrada.Oficina
                cmd.Parameters.Add(New OracleParameter("PV_PARTIDA", OracleDbType.Varchar2)).Value = entrada.Partida
                cmd.Parameters.Add(New OracleParameter("PV_ASIENTO", OracleDbType.Varchar2)).Value = entrada.Asiento
                cmd.Parameters.Add(New OracleParameter("PV_APE_PAT", OracleDbType.Varchar2)).Value = entrada.ApePaterno
                cmd.Parameters.Add(New OracleParameter("PV_APE_MAT", OracleDbType.Varchar2)).Value = entrada.ApeMaterno
                cmd.Parameters.Add(New OracleParameter("PV_NOMBRES", OracleDbType.Varchar2)).Value = entrada.Nombres
                cmd.Parameters.Add(New OracleParameter("PV_CARGO", OracleDbType.Varchar2)).Value = entrada.Cargo
                cmd.Parameters.Add(New OracleParameter("PV_EMAIL", OracleDbType.Varchar2)).Value = entrada.Email
                cmd.Parameters.Add(New OracleParameter("PV_ESTADO", OracleDbType.Varchar2)).Value = If(IsNothing(salida), "", salida.Estado)
                cmd.Parameters.Add(New OracleParameter("PV_SOLICITUD", OracleDbType.Varchar2)).Value = If(IsNothing(salida), "", salida.Solicitud)
                cmd.Parameters.Add(New OracleParameter("PV_FECHA", OracleDbType.Varchar2)).Value = If(IsNothing(salida), "", salida.Fecha)
                cmd.Parameters.Add(New OracleParameter("PN_CODLOG", OracleDbType.Int32)).Direction = ParameterDirection.InputOutput

                conexion.Open()

                intResultado = cmd.ExecuteNonQuery()

                conexion.Close()
            End Using
        Catch ex As Exception
            Throw ex
        End Try
        Return intResultado
    End Function

    Public Function RegistrarLog_SunarpListaAsiento(ByVal oLogBE As LogBE, entrada As PersonaBE, salida As List(Of SunarpAsientoBE)) As Integer
        Dim intResultado As Integer = 0
        Dim CantidadResultado As Integer = salida.Count
        Try
            Using conexion = New OracleConnection(ConfiguracionBase.K_CADENA_CONEXION)
                Dim cmd As OracleCommand = conexion.CreateCommand()
                cmd.CommandType = CommandType.StoredProcedure
                If CantidadResultado > 0 Then
                    cmd.CommandText = String.Format("{0}.{1}.{2}", ConfiguracionBase.K_ESQUEMA_DEFAULT, "PKG_GESTION_INTEROPERABILIDAD", "SPI_SI_INSERT_LOG_SUNARP_LA")
                Else
                    cmd.CommandText = String.Format("{0}.{1}.{2}", ConfiguracionBase.K_ESQUEMA_DEFAULT, "PKG_GESTION_INTEROPERABILIDAD", "SPI_SI_INS_LOG_SUNARP_LA_ERR")
                End If

                cmd.Parameters.Add(New OracleParameter("PN_TIPOSERVICIO", OracleDbType.Int32)).Value = oLogBE.TipoServicio
                cmd.Parameters.Add(New OracleParameter("PV_USER", OracleDbType.Varchar2)).Value = oLogBE.VUSER
                cmd.Parameters.Add(New OracleParameter("PV_CCOAPL", OracleDbType.Varchar2)).Value = oLogBE.VCODAPLI
                cmd.Parameters.Add(New OracleParameter("PN_CODRESULTADO", OracleDbType.Int32)).Value = oLogBE.NCODRESULTADO
                cmd.Parameters.Add(New OracleParameter("PV_MENSAJE_RES", OracleDbType.Varchar2)).Value = oLogBE.VMENSAJE_RESULTADO
                cmd.Parameters.Add(New OracleParameter("PN_ACCION", OracleDbType.Int32)).Value = oLogBE.NCODACCION
                cmd.Parameters.Add(New OracleParameter("PD_FECHA", OracleDbType.Date)).Value = oLogBE.DFECHA
                cmd.Parameters.Add(New OracleParameter("PV_ZONA", OracleDbType.Varchar2)).Value = entrada.Zona
                cmd.Parameters.Add(New OracleParameter("PV_OFICINA", OracleDbType.Varchar2)).Value = entrada.Oficina
                cmd.Parameters.Add(New OracleParameter("PV_REGISTRO", OracleDbType.Varchar2)).Value = entrada.Registro
                cmd.Parameters.Add(New OracleParameter("PV_PARTIDA", OracleDbType.Varchar2)).Value = entrada.Partida

                If CantidadResultado > 0 Then
                    Dim opIdImgAsiento As OracleParameter = New OracleParameter("PARR_IDIMGASIENTO", OracleDbType.Varchar2, CantidadResultado)
                    opIdImgAsiento.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                    Dim opTipo As OracleParameter = New OracleParameter("PARR_TIPO", OracleDbType.Varchar2, CantidadResultado)
                    opTipo.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                    Dim opCantPaginas As OracleParameter = New OracleParameter("PARR_CANTIDADPAG", OracleDbType.Varchar2, CantidadResultado)
                    opCantPaginas.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                    Dim opNroPagRef As OracleParameter = New OracleParameter("PARR_NROPAGREF", OracleDbType.Varchar2, CantidadResultado)
                    opNroPagRef.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                    Dim opNroPagina As OracleParameter = New OracleParameter("PARR_NROPAG", OracleDbType.Varchar2, CantidadResultado)
                    opNroPagina.CollectionType = OracleCollectionType.PLSQLAssociativeArray

                    cmd.Parameters.Add(New OracleParameter("PV_TRANSACCION", OracleDbType.Varchar2)).Value = salida(0).Transaccion
                    cmd.Parameters.Add(New OracleParameter("PV_NROTOTALREG", OracleDbType.Varchar2)).Value = salida(0).TotalPag
                    cmd.Parameters.Add(New OracleParameter("PN_CANTIDAD_ARR", OracleDbType.Int32)).Value = CantidadResultado
                    cmd.Parameters.Add(opIdImgAsiento).Value = salida.Select(Function(o) o.IdImgAsiento).ToArray()
                    cmd.Parameters.Add(opTipo).Value = salida.Select(Function(o) o.Tipo).ToArray()
                    cmd.Parameters.Add(opCantPaginas).Value = salida.Select(Function(o) o.CantPaginas).ToArray()
                    cmd.Parameters.Add(opNroPagRef).Value = salida.Select(Function(o) o.NroPagRef).ToArray()
                    cmd.Parameters.Add(opNroPagina).Value = salida.Select(Function(o) o.NroPagina).ToArray()
                End If
                cmd.Parameters.Add(New OracleParameter("PN_CODLOG", OracleDbType.Int32)).Direction = ParameterDirection.InputOutput

                conexion.Open()

                intResultado = cmd.ExecuteNonQuery()

                conexion.Close()
            End Using
        Catch ex As Exception
            Throw ex
        End Try
        Return intResultado
    End Function

    Public Function RegistrarLog_SuneduGradosTitulos(ByVal oLogBE As LogBE, entrada As PersonaBE, salida As List(Of SuneduGradoAcademicoBE)) As Integer
        Dim intResultado As Integer = 0
        Dim CantidadResultado As Integer = salida.Count
        Try
            Using conexion = New OracleConnection(ConfiguracionBase.K_CADENA_CONEXION)
                Dim cmd As OracleCommand = conexion.CreateCommand()
                cmd.CommandType = CommandType.StoredProcedure
                If CantidadResultado > 0 Then
                    cmd.CommandText = String.Format("{0}.{1}.{2}", ConfiguracionBase.K_ESQUEMA_DEFAULT, "PKG_GESTION_INTEROPERABILIDAD", "SPI_SI_INSERT_LOG_SUNEDU")
                Else
                    cmd.CommandText = String.Format("{0}.{1}.{2}", ConfiguracionBase.K_ESQUEMA_DEFAULT, "PKG_GESTION_INTEROPERABILIDAD", "SPI_SI_INSERT_LOG_SUNEDU_ERR")
                End If

                cmd.Parameters.Add(New OracleParameter("PN_TIPOSERVICIO", OracleDbType.Int32)).Value = oLogBE.TipoServicio
                cmd.Parameters.Add(New OracleParameter("PV_USER", OracleDbType.Varchar2)).Value = oLogBE.VUSER
                cmd.Parameters.Add(New OracleParameter("PV_CCOAPL", OracleDbType.Varchar2)).Value = oLogBE.VCODAPLI
                cmd.Parameters.Add(New OracleParameter("PN_CODRESULTADO", OracleDbType.Int32)).Value = oLogBE.NCODRESULTADO
                cmd.Parameters.Add(New OracleParameter("PV_MENSAJE_RES", OracleDbType.Varchar2)).Value = oLogBE.VMENSAJE_RESULTADO
                cmd.Parameters.Add(New OracleParameter("PN_ACCION", OracleDbType.Int32)).Value = oLogBE.NCODACCION
                cmd.Parameters.Add(New OracleParameter("PD_FECHA", OracleDbType.Date)).Value = oLogBE.DFECHA
                cmd.Parameters.Add(New OracleParameter("PV_DNI", OracleDbType.Varchar2)).Value = entrada.DNI

                If CantidadResultado > 0 Then
                    Dim opApePaterno As OracleParameter = New OracleParameter("PARR_APEPATERNO", OracleDbType.Varchar2, CantidadResultado)
                    opApePaterno.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                    Dim opApeMaterno As OracleParameter = New OracleParameter("PARR_APEMATERNO", OracleDbType.Varchar2, CantidadResultado)
                    opApeMaterno.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                    Dim opNombres As OracleParameter = New OracleParameter("PARR_NOMBRES", OracleDbType.Varchar2, CantidadResultado)
                    opNombres.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                    Dim opTipodocumento As OracleParameter = New OracleParameter("PARR_TIPODOCUMENTO", OracleDbType.Varchar2, CantidadResultado)
                    opTipodocumento.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                    Dim opNumeroDocumento As OracleParameter = New OracleParameter("PARR_NRODOCUMENTO", OracleDbType.Varchar2, CantidadResultado)
                    opNumeroDocumento.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                    Dim opPais As OracleParameter = New OracleParameter("PARR_PAIS", OracleDbType.Varchar2, CantidadResultado)
                    opPais.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                    Dim opUniversidad As OracleParameter = New OracleParameter("PARR_UNIVERSIDAD", OracleDbType.Varchar2, CantidadResultado)
                    opUniversidad.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                    Dim opTituloProfesional As OracleParameter = New OracleParameter("PARR_TITPROFESIONAL", OracleDbType.Varchar2, CantidadResultado)
                    opTituloProfesional.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                    Dim opAbreviaturaTitulo As OracleParameter = New OracleParameter("PARR_ABRTITULO", OracleDbType.Varchar2, CantidadResultado)
                    opAbreviaturaTitulo.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                    Dim opEspecialidad As OracleParameter = New OracleParameter("PARR_ESPECIALIDAD", OracleDbType.Varchar2, CantidadResultado)
                    opEspecialidad.CollectionType = OracleCollectionType.PLSQLAssociativeArray

                    cmd.Parameters.Add(New OracleParameter("PN_CANTIDAD_ARR", OracleDbType.Int32)).Value = CantidadResultado
                    cmd.Parameters.Add(opApePaterno).Value = salida.Select(Function(o) o.ApePaterno).ToArray()
                    cmd.Parameters.Add(opApeMaterno).Value = salida.Select(Function(o) o.ApeMaterno).ToArray()
                    cmd.Parameters.Add(opNombres).Value = salida.Select(Function(o) o.Nombres).ToArray()
                    cmd.Parameters.Add(opTipodocumento).Value = salida.Select(Function(o) o.Tipodocumento).ToArray()
                    cmd.Parameters.Add(opNumeroDocumento).Value = salida.Select(Function(o) o.NumeroDocumento).ToArray()
                    cmd.Parameters.Add(opPais).Value = salida.Select(Function(o) o.Pais).ToArray()
                    cmd.Parameters.Add(opUniversidad).Value = salida.Select(Function(o) o.Universidad).ToArray()
                    cmd.Parameters.Add(opTituloProfesional).Value = salida.Select(Function(o) o.TituloProfesional).ToArray()
                    cmd.Parameters.Add(opAbreviaturaTitulo).Value = salida.Select(Function(o) o.AbreviaturaTitulo).ToArray()
                    cmd.Parameters.Add(opEspecialidad).Value = salida.Select(Function(o) o.Especialidad).ToArray()
                End If
                cmd.Parameters.Add(New OracleParameter("PN_CODLOG", OracleDbType.Int32)).Direction = ParameterDirection.InputOutput

                conexion.Open()

                intResultado = cmd.ExecuteNonQuery()

                conexion.Close()
            End Using
        Catch ex As Exception
            Throw ex
        End Try
        Return intResultado
    End Function

    Public Function RegistrarLog_MigracionesCarnetExtranjeria(ByVal oLogBE As LogBE, entrada As PersonaBE, salida As MigracionesCarnetExtranjeriaBE) As Integer
        Dim intResultado As Integer = 0
        Try
            Using conexion = New OracleConnection(ConfiguracionBase.K_CADENA_CONEXION)
                Dim cmd As OracleCommand = conexion.CreateCommand()
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = String.Format("{0}.{1}.{2}", ConfiguracionBase.K_ESQUEMA_DEFAULT, "PKG_GESTION_INTEROPERABILIDAD", "SPI_SI_INSERT_LOG_MIGRACIONES")
                cmd.Parameters.Add(New OracleParameter("PN_TIPOSERVICIO", OracleDbType.Int32)).Value = oLogBE.TipoServicio
                cmd.Parameters.Add(New OracleParameter("PV_USER", OracleDbType.Varchar2)).Value = oLogBE.VUSER
                cmd.Parameters.Add(New OracleParameter("PV_CCOAPL", OracleDbType.Varchar2)).Value = oLogBE.VCODAPLI
                cmd.Parameters.Add(New OracleParameter("PN_CODRESULTADO", OracleDbType.Int32)).Value = oLogBE.NCODRESULTADO
                cmd.Parameters.Add(New OracleParameter("PV_MENSAJE_RES", OracleDbType.Varchar2)).Value = oLogBE.VMENSAJE_RESULTADO
                cmd.Parameters.Add(New OracleParameter("PN_ACCION", OracleDbType.Int32)).Value = oLogBE.NCODACCION
                cmd.Parameters.Add(New OracleParameter("PD_FECHA", OracleDbType.Date)).Value = oLogBE.DFECHA
                cmd.Parameters.Add(New OracleParameter("PV_CE", OracleDbType.Varchar2)).Value = entrada.NumeroDoc
                cmd.Parameters.Add(New OracleParameter("PV_APE_PRI", OracleDbType.Varchar2)).Value = If(IsNothing(salida), "", salida.PrimerApellido)
                cmd.Parameters.Add(New OracleParameter("PV_APE_SEG", OracleDbType.Varchar2)).Value = If(IsNothing(salida), "", salida.SegundoApellido)
                cmd.Parameters.Add(New OracleParameter("PV_NOMBRES", OracleDbType.Varchar2)).Value = If(IsNothing(salida), "", salida.Nombres)
                cmd.Parameters.Add(New OracleParameter("PV_CALIDAD_MIGR", OracleDbType.Varchar2)).Value = If(IsNothing(salida), "", salida.CalidadMigratoria)
                cmd.Parameters.Add(New OracleParameter("PN_CODLOG", OracleDbType.Int32)).Direction = ParameterDirection.InputOutput

                conexion.Open()

                intResultado = cmd.ExecuteNonQuery()

                conexion.Close()
            End Using
        Catch ex As Exception
            Throw ex
        End Try
        Return intResultado
    End Function

    Public Function RegistrarLog_SunarpVerImagenAsiento(ByVal oLogBE As LogBE, entrada As AsientoBE, salida As String) As Integer
        Dim intResultado As Integer = 0
        Try
            Using conexion = New OracleConnection(ConfiguracionBase.K_CADENA_CONEXION)
                Dim cmd As OracleCommand = conexion.CreateCommand()
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = String.Format("{0}.{1}.{2}", ConfiguracionBase.K_ESQUEMA_DEFAULT, "PKG_GESTION_INTEROPERABILIDAD", "SPI_SI_INSERT_LOG_SUNARP_VA")
                cmd.Parameters.Add(New OracleParameter("PN_TIPOSERVICIO", OracleDbType.Int32)).Value = oLogBE.TipoServicio
                cmd.Parameters.Add(New OracleParameter("PV_USER", OracleDbType.Varchar2)).Value = oLogBE.VUSER
                cmd.Parameters.Add(New OracleParameter("PV_CCOAPL", OracleDbType.Varchar2)).Value = oLogBE.VCODAPLI
                cmd.Parameters.Add(New OracleParameter("PN_CODRESULTADO", OracleDbType.Int32)).Value = oLogBE.NCODRESULTADO
                cmd.Parameters.Add(New OracleParameter("PV_MENSAJE_RES", OracleDbType.Varchar2)).Value = oLogBE.VMENSAJE_RESULTADO
                cmd.Parameters.Add(New OracleParameter("PN_ACCION", OracleDbType.Int32)).Value = oLogBE.NCODACCION
                cmd.Parameters.Add(New OracleParameter("PD_FECHA", OracleDbType.Date)).Value = oLogBE.DFECHA
                cmd.Parameters.Add(New OracleParameter("PV_TRANSACCION", OracleDbType.Varchar2)).Value = entrada.Transaccion
                cmd.Parameters.Add(New OracleParameter("PV_NROTOTALPAG", OracleDbType.Varchar2)).Value = entrada.NroTotalPag
                cmd.Parameters.Add(New OracleParameter("PV_IDIMGASIENTO", OracleDbType.Varchar2)).Value = entrada.IdImg
                cmd.Parameters.Add(New OracleParameter("PV_TIPO", OracleDbType.Varchar2)).Value = entrada.Tipo
                cmd.Parameters.Add(New OracleParameter("PV_NROPAGREF", OracleDbType.Varchar2)).Value = entrada.NroPagRef
                cmd.Parameters.Add(New OracleParameter("PV_PAG", OracleDbType.Varchar2)).Value = entrada.Pagina
                cmd.Parameters.Add(New OracleParameter("PV_RUTA_IMG", OracleDbType.Varchar2)).Value = salida
                cmd.Parameters.Add(New OracleParameter("PN_CODLOG", OracleDbType.Int32, ParameterDirection.InputOutput))

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
