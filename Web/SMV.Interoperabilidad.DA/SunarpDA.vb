Imports Oracle.DataAccess.Client
Imports SMV.Interoperabilidad.BE

Public Class SunarpDA

    Public Function ObtenerListaRegistros(ByVal objFiltro As SunarpBE) As List(Of SunarpBE)
        Dim objListRegistrosBE As List(Of SunarpBE) = Nothing
        Try
            objListRegistrosBE = New List(Of SunarpBE)

            Using conexion = New OracleConnection(ConfiguracionBase.K_CADENA_CONEXION)
                Dim cmd As OracleCommand = conexion.CreateCommand()
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = String.Format("{0}.{1}.{2}", ConfiguracionBase.K_ESQUEMA_DEFAULT, "PKG_GESTION_INTEROPERABILIDAD", "SPU_SI_LIST_MAESTRAXNOM")

                cmd.Parameters.Add(New OracleParameter("PV_NOMTABLA", OracleDbType.Varchar2, 300)).Value = objFiltro.NombreTabla
                cmd.Parameters.Add("RS_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output

                conexion.Open()
                Using dr As IDataReader = cmd.ExecuteReader()
                    While dr.Read
                        objListRegistrosBE.Add(New SunarpBE With { _
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

    Public Function ObtenerListaZonas(ByVal objFiltro As SunarpBE) As List(Of SunarpBE)
        Dim objListRegistrosBE As List(Of SunarpBE) = Nothing
        Try
            objListRegistrosBE = New List(Of SunarpBE)

            Using conexion = New OracleConnection(ConfiguracionBase.K_CADENA_CONEXION)
                Dim cmd As OracleCommand = conexion.CreateCommand()
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = String.Format("{0}.{1}.{2}", ConfiguracionBase.K_ESQUEMA_DEFAULT, "PKG_GESTION_INTEROPERABILIDAD", "SPU_SI_LIST_MAESTRAXNOM")

                cmd.Parameters.Add(New OracleParameter("PV_NOMTABLA", OracleDbType.Varchar2, 300)).Value = objFiltro.NombreTabla
                cmd.Parameters.Add("RS_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output

                conexion.Open()
                Using dr As IDataReader = cmd.ExecuteReader()
                    While dr.Read
                        objListRegistrosBE.Add(New SunarpBE With { _
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

    Public Function ObtenerListaOficinas(ByVal objFiltro As SunarpBE) As List(Of SunarpBE)
        Dim objListRegistrosBE As List(Of SunarpBE) = Nothing
        Try
            objListRegistrosBE = New List(Of SunarpBE)

            Using conexion = New OracleConnection(ConfiguracionBase.K_CADENA_CONEXION)
                Dim cmd As OracleCommand = conexion.CreateCommand()
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = String.Format("{0}.{1}.{2}", ConfiguracionBase.K_ESQUEMA_DEFAULT, "PKG_GESTION_INTEROPERABILIDAD", "SPU_SI_LIST_MAESTRAXCAMPO")

                cmd.Parameters.Add(New OracleParameter("PV_NOMTABLA", OracleDbType.Varchar2, 300)).Value = objFiltro.NombreTabla
                cmd.Parameters.Add(New OracleParameter("PV_VALOR1", OracleDbType.Varchar2, 300)).Value = objFiltro.CodigoCampo1
                cmd.Parameters.Add(New OracleParameter("PV_VALOR2", OracleDbType.Varchar2, 300)).Value = objFiltro.CodigoCampo2
                cmd.Parameters.Add("RS_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output

                conexion.Open()
                Using dr As IDataReader = cmd.ExecuteReader()
                    While dr.Read
                        objListRegistrosBE.Add(New SunarpBE With { _
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

    ''' <summary>
    ''' SUNARP - Vigencia de Poder.
    ''' </summary>
    ''' <param name="objFiltro"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function RegistrarLog_VP(ByVal objFiltro As SunarpBE) As Integer
        Dim intResultado As Integer = 0
        Try
            Dim inCodLog As Integer = 0
            objFiltro.CodLog = inCodLog

            Using conexion = New OracleConnection(ConfiguracionBase.K_CADENA_CONEXION)
                Dim cmd As OracleCommand = conexion.CreateCommand()
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = String.Format("{0}.{1}.{2}", ConfiguracionBase.K_ESQUEMA_DEFAULT, "PKG_GESTION_INTEROPERABILIDAD", "SPI_SI_INSERT_LOG_SUNARP_VP")
                cmd.Parameters.Add(New OracleParameter("PN_TIPOSERVICIO", OracleDbType.Int32)).Value = objFiltro.TipoServicio
                cmd.Parameters.Add(New OracleParameter("PV_USER", OracleDbType.Varchar2, 3)).Value = objFiltro.VUSER
                cmd.Parameters.Add(New OracleParameter("PV_CCOAPL", OracleDbType.Varchar2, 3)).Value = objFiltro.VCODAPLI
                cmd.Parameters.Add(New OracleParameter("PN_CODRESULTADO", OracleDbType.Int32)).Value = objFiltro.NCODRESULTADO
                cmd.Parameters.Add(New OracleParameter("PV_MENSAJE_RES", OracleDbType.Varchar2, 4000)).Value = objFiltro.VMENSAJE_RESULTADO
                cmd.Parameters.Add(New OracleParameter("PN_ACCION", OracleDbType.Int32)).Value = objFiltro.NCODACCION
                cmd.Parameters.Add(New OracleParameter("PD_FECHA", OracleDbType.Date)).Value = objFiltro.DFECHA
                cmd.Parameters.Add(New OracleParameter("PV_ZONA", OracleDbType.Varchar2, 50)).Value = objFiltro.Zona
                cmd.Parameters.Add(New OracleParameter("PV_OFICINA", OracleDbType.Varchar2, 50)).Value = objFiltro.Oficina
                cmd.Parameters.Add(New OracleParameter("PV_PARTIDA", OracleDbType.Varchar2, 300)).Value = objFiltro.NumPartida
                cmd.Parameters.Add(New OracleParameter("PV_ASIENTO", OracleDbType.Varchar2, 300)).Value = objFiltro.NumAsiento
                cmd.Parameters.Add(New OracleParameter("PV_APE_PAT", OracleDbType.Varchar2, 500)).Value = objFiltro.ApePaterno
                cmd.Parameters.Add(New OracleParameter("PV_APE_MAT", OracleDbType.Varchar2, 500)).Value = objFiltro.ApeMaterno
                cmd.Parameters.Add(New OracleParameter("PV_NOMBRES", OracleDbType.Varchar2, 500)).Value = objFiltro.Nombres
                cmd.Parameters.Add(New OracleParameter("PV_CARGO", OracleDbType.Varchar2, 300)).Value = objFiltro.Cargo
                cmd.Parameters.Add(New OracleParameter("PV_EMAIL", OracleDbType.Varchar2, 300)).Value = objFiltro.Email
                cmd.Parameters.Add(New OracleParameter("PV_ESTADO", OracleDbType.Varchar2, 300)).Value = objFiltro.Estado
                cmd.Parameters.Add(New OracleParameter("PV_SOLICITUD", OracleDbType.Varchar2, 200)).Value = objFiltro.Solicitud
                cmd.Parameters.Add(New OracleParameter("PV_FECHA", OracleDbType.Varchar2, 100)).Value = objFiltro.Fecha
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

    ''' <summary>
    ''' SUNARP - Titularidad de Bienes de consulta.
    ''' </summary>
    ''' <param name="objFiltro"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function RegistrarLog_TB(ByVal objFiltro As SunarpBE) As Integer
        Dim intResultado As Integer = 0
        Try
            Dim inCodLog As Integer = 0
            objFiltro.CodLog = inCodLog

            Dim op_ApePaterno As New OracleParameter()
            Dim op_ApeMaterno As New OracleParameter()
            Dim op_Nombres As New OracleParameter()
            Dim op_RazonS As New OracleParameter()
            Dim op_TipoDocumento As New OracleParameter()
            Dim op_NroDocumento As New OracleParameter()
            Dim op_NumPartida As New OracleParameter()
            Dim op_Registro As New OracleParameter()
            Dim op_NumPlaca As New OracleParameter()
            Dim op_Zona As New OracleParameter()
            Dim op_Oficina As New OracleParameter()
            Dim op_Estado As New OracleParameter()

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

                op_RazonS.OracleDbType = OracleDbType.Varchar2
                op_RazonS.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                op_RazonS.Value = objFiltro.Arr_RazonS
                op_RazonS.Size = objFiltro.Arr_RazonS.Length

                op_TipoDocumento.OracleDbType = OracleDbType.Varchar2
                op_TipoDocumento.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                op_TipoDocumento.Value = objFiltro.Arr_TipoDocumento
                op_TipoDocumento.Size = objFiltro.Arr_TipoDocumento.Length

                op_NroDocumento.OracleDbType = OracleDbType.Varchar2
                op_NroDocumento.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                op_NroDocumento.Value = objFiltro.Arr_NroDocumento
                op_NroDocumento.Size = objFiltro.Arr_NroDocumento.Length

                op_NumPartida.OracleDbType = OracleDbType.Varchar2
                op_NumPartida.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                op_NumPartida.Value = objFiltro.Arr_NumPartida
                op_NumPartida.Size = objFiltro.Arr_NumPartida.Length

                op_Registro.OracleDbType = OracleDbType.Varchar2
                op_Registro.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                op_Registro.Value = objFiltro.Arr_Registro
                op_Registro.Size = objFiltro.Arr_Registro.Length

                op_NumPlaca.OracleDbType = OracleDbType.Varchar2
                op_NumPlaca.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                op_NumPlaca.Value = objFiltro.Arr_NumPlaca
                op_NumPlaca.Size = objFiltro.Arr_NumPlaca.Length

                op_Zona.OracleDbType = OracleDbType.Varchar2
                op_Zona.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                op_Zona.Value = objFiltro.Arr_Zona
                op_Zona.Size = objFiltro.Arr_Zona.Length

                op_Oficina.OracleDbType = OracleDbType.Varchar2
                op_Oficina.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                op_Oficina.Value = objFiltro.Arr_Oficina
                op_Oficina.Size = objFiltro.Arr_Oficina.Length

                op_Estado.OracleDbType = OracleDbType.Varchar2
                op_Estado.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                op_Estado.Value = objFiltro.Arr_Estado
                op_Estado.Size = objFiltro.Arr_Estado.Length

            End If

            Using conexion = New OracleConnection(ConfiguracionBase.K_CADENA_CONEXION)
                Dim cmd As OracleCommand = conexion.CreateCommand()
                cmd.CommandType = CommandType.StoredProcedure

                If (objFiltro.NCANTIDAD_ARR > 0) Then
                    cmd.CommandText = String.Format("{0}.{1}.{2}", ConfiguracionBase.K_ESQUEMA_DEFAULT, "PKG_GESTION_INTEROPERABILIDAD", "SPI_SI_INSERT_LOG_SUNARP_TB")
                Else
                    cmd.CommandText = String.Format("{0}.{1}.{2}", ConfiguracionBase.K_ESQUEMA_DEFAULT, "PKG_GESTION_INTEROPERABILIDAD", "SPI_SI_INS_LOG_SUNARP_TB_ERR")
                End If

                cmd.Parameters.Add(New OracleParameter("PN_TIPOSERVICIO", OracleDbType.Int32)).Value = objFiltro.TipoServicio
                cmd.Parameters.Add(New OracleParameter("PV_USER", OracleDbType.Varchar2, 3)).Value = objFiltro.VUSER
                cmd.Parameters.Add(New OracleParameter("PV_CCOAPL", OracleDbType.Varchar2, 3)).Value = objFiltro.VCODAPLI
                cmd.Parameters.Add(New OracleParameter("PN_CODRESULTADO", OracleDbType.Int32)).Value = objFiltro.NCODRESULTADO
                cmd.Parameters.Add(New OracleParameter("PV_MENSAJE_RES", OracleDbType.Varchar2, 4000)).Value = objFiltro.VMENSAJE_RESULTADO
                cmd.Parameters.Add(New OracleParameter("PN_ACCION", OracleDbType.Int32)).Value = objFiltro.NCODACCION
                cmd.Parameters.Add(New OracleParameter("PD_FECHA", OracleDbType.Date)).Value = objFiltro.DFECHA
                cmd.Parameters.Add(New OracleParameter("PN_TIPO_CONS", OracleDbType.Int32)).Value = objFiltro.TipoConsulta
                cmd.Parameters.Add(New OracleParameter("PV_TIPO_PART", OracleDbType.Varchar2, 10)).Value = objFiltro.TipoParticipante
                cmd.Parameters.Add(New OracleParameter("PV_APE_PAT", OracleDbType.Varchar2, 500)).Value = objFiltro.ApePaterno
                cmd.Parameters.Add(New OracleParameter("PV_APE_MAT", OracleDbType.Varchar2, 500)).Value = objFiltro.ApeMaterno
                cmd.Parameters.Add(New OracleParameter("PV_NOMBRES", OracleDbType.Varchar2, 500)).Value = objFiltro.Nombres
                cmd.Parameters.Add(New OracleParameter("PV_RAZONS", OracleDbType.Varchar2, 500)).Value = objFiltro.RazonSocial

                If (objFiltro.NCANTIDAD_ARR > 0) Then

                    cmd.Parameters.Add(New OracleParameter("PN_CANTIDAD_ARR", OracleDbType.Int32)).Value = objFiltro.NCANTIDAD_ARR
                    cmd.Parameters.Add(op_ApePaterno)
                    cmd.Parameters.Add(op_ApeMaterno)
                    cmd.Parameters.Add(op_Nombres)
                    cmd.Parameters.Add(op_RazonS)
                    cmd.Parameters.Add(op_TipoDocumento)
                    cmd.Parameters.Add(op_NroDocumento)
                    cmd.Parameters.Add(op_NumPartida)
                    cmd.Parameters.Add(op_Registro)
                    cmd.Parameters.Add(op_NumPlaca)
                    cmd.Parameters.Add(op_Zona)
                    cmd.Parameters.Add(op_Oficina)
                    cmd.Parameters.Add(op_Estado)

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

    ''' <summary>
    ''' SUNARP - Lista de Asientos.
    ''' </summary>
    ''' <param name="objFiltro"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function RegistrarLog_LA(ByVal objFiltro As SunarpBE) As Integer
        Dim intResultado As Integer = 0
        Try
            Dim inCodLog As Integer = 0
            objFiltro.CodLog = inCodLog

            Dim op_IDImgAsiento As New OracleParameter()
            Dim op_Tipo As New OracleParameter()
            Dim op_CantidadPag As New OracleParameter()
            Dim op_NroPagRef As New OracleParameter()
            Dim op_NroPag As New OracleParameter()

            If (objFiltro.NCANTIDAD_ARR > 0) Then

                op_IDImgAsiento.OracleDbType = OracleDbType.Varchar2
                op_IDImgAsiento.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                op_IDImgAsiento.Value = objFiltro.Arr_IDImgAsiento
                op_IDImgAsiento.Size = objFiltro.Arr_IDImgAsiento.Length

                op_Tipo.OracleDbType = OracleDbType.Varchar2
                op_Tipo.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                op_Tipo.Value = objFiltro.Arr_Tipo
                op_Tipo.Size = objFiltro.Arr_Tipo.Length

                op_CantidadPag.OracleDbType = OracleDbType.Varchar2
                op_CantidadPag.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                op_CantidadPag.Value = objFiltro.Arr_CantidadPag
                op_CantidadPag.Size = objFiltro.Arr_CantidadPag.Length

                op_NroPagRef.OracleDbType = OracleDbType.Varchar2
                op_NroPagRef.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                op_NroPagRef.Value = objFiltro.Arr_NroPagRef
                op_NroPagRef.Size = objFiltro.Arr_NroPagRef.Length

                op_NroPag.OracleDbType = OracleDbType.Varchar2
                op_NroPag.CollectionType = OracleCollectionType.PLSQLAssociativeArray
                op_NroPag.Value = objFiltro.Arr_NroPag
                op_NroPag.Size = objFiltro.Arr_NroPag.Length

            End If

            Using conexion = New OracleConnection(ConfiguracionBase.K_CADENA_CONEXION)
                Dim cmd As OracleCommand = conexion.CreateCommand()
                cmd.CommandType = CommandType.StoredProcedure

                If (objFiltro.NCANTIDAD_ARR > 0) Then
                    cmd.CommandText = String.Format("{0}.{1}.{2}", ConfiguracionBase.K_ESQUEMA_DEFAULT, "PKG_GESTION_INTEROPERABILIDAD", "SPI_SI_INSERT_LOG_SUNARP_LA")
                Else
                    cmd.CommandText = String.Format("{0}.{1}.{2}", ConfiguracionBase.K_ESQUEMA_DEFAULT, "PKG_GESTION_INTEROPERABILIDAD", "SPI_SI_INS_LOG_SUNARP_LA_ERR")
                End If

                cmd.Parameters.Add(New OracleParameter("PN_TIPOSERVICIO", OracleDbType.Int32)).Value = objFiltro.TipoServicio
                cmd.Parameters.Add(New OracleParameter("PV_USER", OracleDbType.Varchar2, 3)).Value = objFiltro.VUSER
                cmd.Parameters.Add(New OracleParameter("PV_CCOAPL", OracleDbType.Varchar2, 3)).Value = objFiltro.VCODAPLI
                cmd.Parameters.Add(New OracleParameter("PN_CODRESULTADO", OracleDbType.Int32)).Value = objFiltro.NCODRESULTADO
                cmd.Parameters.Add(New OracleParameter("PV_MENSAJE_RES", OracleDbType.Varchar2, 4000)).Value = objFiltro.VMENSAJE_RESULTADO
                cmd.Parameters.Add(New OracleParameter("PN_ACCION", OracleDbType.Int32)).Value = objFiltro.NCODACCION
                cmd.Parameters.Add(New OracleParameter("PD_FECHA", OracleDbType.Date)).Value = objFiltro.DFECHA
                cmd.Parameters.Add(New OracleParameter("PV_ZONA", OracleDbType.Varchar2, 300)).Value = objFiltro.Zona
                cmd.Parameters.Add(New OracleParameter("PV_OFICINA", OracleDbType.Varchar2, 300)).Value = objFiltro.Oficina
                cmd.Parameters.Add(New OracleParameter("PV_REGISTRO", OracleDbType.Varchar2, 100)).Value = objFiltro.Registro
                cmd.Parameters.Add(New OracleParameter("PV_PARTIDA", OracleDbType.Varchar2, 100)).Value = objFiltro.NumPartida

                If (objFiltro.NCANTIDAD_ARR > 0) Then

                    cmd.Parameters.Add(New OracleParameter("PV_TRANSACCION", OracleDbType.Varchar2, 100)).Value = objFiltro.Transaccion
                    cmd.Parameters.Add(New OracleParameter("PV_NROTOTALREG", OracleDbType.Varchar2, 100)).Value = objFiltro.NroTotalPag
                    cmd.Parameters.Add(New OracleParameter("PN_CANTIDAD_ARR", OracleDbType.Int32)).Value = objFiltro.NCANTIDAD_ARR
                    cmd.Parameters.Add(op_IDImgAsiento)
                    cmd.Parameters.Add(op_Tipo)
                    cmd.Parameters.Add(op_CantidadPag)
                    cmd.Parameters.Add(op_NroPagRef)
                    cmd.Parameters.Add(op_NroPag)

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

    ''' <summary>
    ''' SUNARP - Ver Asiento.
    ''' </summary>
    ''' <param name="objFiltro"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function RegistrarLog_VImg(ByVal objFiltro As SunarpBE) As Integer
        Dim intResultado As Integer = 0
        Try
            Dim inCodLog As Integer = 0
            objFiltro.CodLog = inCodLog

            Using conexion = New OracleConnection(ConfiguracionBase.K_CADENA_CONEXION)
                Dim cmd As OracleCommand = conexion.CreateCommand()
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = String.Format("{0}.{1}.{2}", ConfiguracionBase.K_ESQUEMA_DEFAULT, "PKG_GESTION_INTEROPERABILIDAD", "SPI_SI_INSERT_LOG_SUNARP_VA")
                cmd.Parameters.Add(New OracleParameter("PN_TIPOSERVICIO", OracleDbType.Int32)).Value = objFiltro.TipoServicio
                cmd.Parameters.Add(New OracleParameter("PV_USER", OracleDbType.Varchar2, 3)).Value = objFiltro.VUSER
                cmd.Parameters.Add(New OracleParameter("PV_CCOAPL", OracleDbType.Varchar2, 3)).Value = objFiltro.VCODAPLI
                cmd.Parameters.Add(New OracleParameter("PN_CODRESULTADO", OracleDbType.Int32)).Value = objFiltro.NCODRESULTADO
                cmd.Parameters.Add(New OracleParameter("PV_MENSAJE_RES", OracleDbType.Varchar2, 4000)).Value = objFiltro.VMENSAJE_RESULTADO
                cmd.Parameters.Add(New OracleParameter("PN_ACCION", OracleDbType.Int32)).Value = objFiltro.NCODACCION
                cmd.Parameters.Add(New OracleParameter("PD_FECHA", OracleDbType.Date)).Value = objFiltro.DFECHA
                cmd.Parameters.Add(New OracleParameter("PV_TRANSACCION", OracleDbType.Varchar2, 50)).Value = objFiltro.Transaccion
                cmd.Parameters.Add(New OracleParameter("PV_NROTOTALPAG", OracleDbType.Varchar2, 50)).Value = objFiltro.NroTotalPag
                cmd.Parameters.Add(New OracleParameter("PV_IDIMGASIENTO", OracleDbType.Varchar2, 50)).Value = objFiltro.IDImgAsiento
                cmd.Parameters.Add(New OracleParameter("PV_TIPO", OracleDbType.Varchar2, 200)).Value = objFiltro.Tipo
                cmd.Parameters.Add(New OracleParameter("PV_NROPAGREF", OracleDbType.Varchar2, 50)).Value = objFiltro.NroPagRef
                cmd.Parameters.Add(New OracleParameter("PV_PAG", OracleDbType.Varchar2, 50)).Value = objFiltro.NroPag
                cmd.Parameters.Add(New OracleParameter("PV_RUTA_IMG", OracleDbType.Varchar2, 500)).Value = objFiltro.RutaImagen
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
