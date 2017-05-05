Imports SMV.ServicioInteroper.BE
Imports SMV.ServicioInteroper.BL

' NOTE: You can use the "Rename" command on the context menu to change the class name "Service1" in both code and config file together.
Public Class ServiceIO
    Implements IServiceIO

    Public Function ReniecConsultaDNI(ByVal request As ReniecConsultaDNIRequest) As ReniecConsultaDNIResponse Implements IServiceIO.ReniecConsultaDNI
        Dim response As ReniecConsultaDNIResponse
        Dim oReniecConsultaDNIBE As ReniecConsultaDNIBE = Nothing
        Dim oPerson = New PersonaBE() With {.DNI = If(IsNothing(request.DNI), "", request.DNI), _
                                            .CODUSER = If(IsNothing(request.CODUSER), "", request.CODUSER), _
                                            .CODAPP = If(IsNothing(request.CODAPP), "", request.CODAPP)}

        Try
            Using oMapeoDatos As New MapeoDatos
                oReniecConsultaDNIBE = oMapeoDatos.ReniecConsultaDNI(oPerson)
            End Using
            response = New ReniecConsultaDNIResponse With {.Correcto = True, .MensajeError = String.Empty, .Resultado = oReniecConsultaDNIBE}

        Catch ex As Exception
            Dim oResultadoBL As New ResultadoBL
            oResultadoBL.RegistrarLog_ReniecConsultaDNI(False, ex.Message, oPerson, oReniecConsultaDNIBE)
            response = New ReniecConsultaDNIResponse With {.Correcto = False, .MensajeError = ex.Message}
        End Try

        Return response
    End Function

    Public Function InpeAntecedentesPenales(ByVal request As InpeAntecedentesPenalesRequest) As InpeAntecedentesPenalesResponse Implements IServiceIO.InpeAntecedentesPenales

        Dim response As InpeAntecedentesPenalesResponse
        Dim strCodMensaje As String = ""
        Dim oPerson = New PersonaBE() With {.DNI = If(IsNothing(request.DNI), "", request.DNI), _
                                            .ApePaterno = If(IsNothing(request.ApellidoPaterno), "", request.ApellidoPaterno), _
                                            .ApeMaterno = If(IsNothing(request.ApellidoMaterno), "", request.ApellidoMaterno), _
                                            .Nombre1 = If(IsNothing(request.Nombre1), "", request.Nombre1), _
                                            .Nombre2 = If(IsNothing(request.Nombre2), "", request.Nombre2), _
                                            .Nombre3 = If(IsNothing(request.Nombre3), "", request.Nombre3), _
                                            .CODUSER = If(IsNothing(request.CODUSER), "", request.CODUSER), _
                                            .CODAPP = If(IsNothing(request.CODAPP), "", request.CODAPP)}

        Try
            Using oServicioConsultaBL As New MapeoDatos
                strCodMensaje = oServicioConsultaBL.InpeAntecedentesPenales(oPerson)
            End Using
            response = New InpeAntecedentesPenalesResponse With {.Correcto = True, .MensajeError = String.Empty, .Resultado = strCodMensaje}

        Catch ex As Exception
            Dim oResultadoBL As New ResultadoBL
            oResultadoBL.RegistrarLog_InpeAntecedentesPenales(False, ex.Message, oPerson, strCodMensaje)
            response = New InpeAntecedentesPenalesResponse With {.Correcto = False, .MensajeError = ex.Message}
        End Try

        Return response
    End Function

    Public Function PJAntecedentesJudiciales(ByVal request As PJAntecedentesJudicialesRequest) As PJAntecedentesJudicialesResponse Implements IServiceIO.PJAntecedentesJudiciales

        Dim response As PJAntecedentesJudicialesResponse
        Dim strCodMensaje As String = ""
        Dim oPerson = New PersonaBE() With {.ApePaterno = If(IsNothing(request.ApellidoPaterno), "", request.ApellidoPaterno), _
                                            .ApeMaterno = If(IsNothing(request.ApellidoMaterno), "", request.ApellidoMaterno), _
                                            .Nombres = If(IsNothing(request.Nombres), "", request.Nombres), _
                                            .CODUSER = If(IsNothing(request.CODUSER), "", request.CODUSER), _
                                            .CODAPP = If(IsNothing(request.CODAPP), "", request.CODAPP)}

        Try
            Using oServicioConsultaBL As New MapeoDatos
                strCodMensaje = oServicioConsultaBL.PJAntecedentesJudiciales(oPerson)
            End Using
            response = New PJAntecedentesJudicialesResponse With {.Correcto = True, .MensajeError = String.Empty, .Resultado = strCodMensaje}

        Catch ex As Exception
            Dim oResultadoBL As New ResultadoBL
            oResultadoBL.RegistrarLog_PJAntecedentesJudiciales(False, ex.Message, oPerson, strCodMensaje)
            response = New PJAntecedentesJudicialesResponse With {.Correcto = False, .MensajeError = ex.Message}
        End Try

        Return response
    End Function

    Public Function MininterAntecedentesPoliciales(ByVal request As MininterAntecedentesPolicialesRequest) As MininterAntecedentesPolicialesResponse Implements IServiceIO.MininterAntecedentesPoliciales

        Dim response As MininterAntecedentesPolicialesResponse
        Dim strCodMensaje As String = ""
        Dim oPerson = New PersonaBE() With {.ApePaterno = If(IsNothing(request.ApellidoPaterno), "", request.ApellidoPaterno), _
                                            .ApeMaterno = If(IsNothing(request.ApellidoMaterno), "", request.ApellidoMaterno), _
                                            .Nombres = If(IsNothing(request.Nombres), "", request.Nombres), _
                                            .DNI = If(IsNothing(request.DNI), "", request.DNI), _
                                            .TipoConsulta = If(IsNothing(request.TipoConsulta), "", request.TipoConsulta), _
                                            .CODUSER = If(IsNothing(request.CODUSER), "", request.CODUSER), _
                                            .CODAPP = If(IsNothing(request.CODAPP), "", request.CODAPP)}

        Try
            Using oServicioConsultaBL As New MapeoDatos
                strCodMensaje = oServicioConsultaBL.MininterAntecedentesPoliciales(oPerson)
            End Using
            response = New MininterAntecedentesPolicialesResponse With {.Correcto = True, .MensajeError = String.Empty, .Resultado = strCodMensaje}

        Catch ex As Exception
            Dim oResultadoBL As New ResultadoBL
            oResultadoBL.RegistrarLog_MininterAntecedentesPoliciales(False, ex.Message, oPerson, strCodMensaje)
            response = New MininterAntecedentesPolicialesResponse With {.Correcto = False, .MensajeError = ex.Message}
        End Try

        Return response
    End Function

    Public Function SunarpTitularidadBienes(ByVal request As SunarpTitularidadBienesRequest) As SunarpTitularidadBienesResponse Implements IServiceIO.SunarpTitularidadBienes
        Dim response As SunarpTitularidadBienesResponse
        Dim olista = New List(Of SunarpTitularidadBienesBE)()
        Dim oPerson = New PersonaBE() With {.TipoParticipante = If(IsNothing(request.TipoParticipante), "", request.TipoParticipante), _
                                            .ApePaterno = If(IsNothing(request.ApellidoPaterno), "", request.ApellidoPaterno), _
                                            .ApeMaterno = If(IsNothing(request.ApellidoMaterno), "", request.ApellidoMaterno), _
                                            .Nombres = If(IsNothing(request.Nombres), "", request.Nombres), _
                                            .RazonSocial = If(IsNothing(request.RazonSocial), "", request.RazonSocial), _
                                            .CODUSER = If(IsNothing(request.CODUSER), "", request.CODUSER), _
                                            .CODAPP = If(IsNothing(request.CODAPP), "", request.CODAPP)}

        Try
            Using oServicioConsultaBL As New MapeoDatos
                olista = oServicioConsultaBL.SunarpTitularidadBienes(oPerson)
            End Using
            response = New SunarpTitularidadBienesResponse With {.Correcto = True, .MensajeError = String.Empty, .Resultado = olista}

        Catch ex As Exception
            Dim oResultadoBL As New ResultadoBL
            oResultadoBL.RegistrarLog_SunarpTitularidadBienes(False, ex.Message, oPerson, olista)
            response = New SunarpTitularidadBienesResponse With {.Correcto = False, .MensajeError = ex.Message}
        End Try

        Return response
    End Function

    Public Function SunarpVigenciaPoder(ByVal request As SunarpVigenciaPoderRequest) As SunarpVigenciaPoderResponse Implements IServiceIO.SunarpVigenciaPoder
        Dim response As SunarpVigenciaPoderResponse
        Dim oSunarpVigenciaPoderBE As SunarpVigenciaPoderBE = Nothing
        Dim oPerson = New PersonaBE() With {.Zona = If(IsNothing(request.Zona), "", request.Zona), _
                                            .Oficina = If(IsNothing(request.Oficina), "", request.Oficina), _
                                            .Partida = If(IsNothing(request.Partida), "", request.Partida), _
                                            .Asiento = If(IsNothing(request.Asiento), "", request.Asiento), _
                                            .ApePaterno = If(IsNothing(request.ApellidoPaterno), "", request.ApellidoPaterno), _
                                            .ApeMaterno = If(IsNothing(request.ApellidoMaterno), "", request.ApellidoMaterno), _
                                            .Nombres = If(IsNothing(request.Nombre), "", request.Nombre), _
                                            .Cargo = If(IsNothing(request.Cargo), "", request.Cargo), _
                                            .Email = If(IsNothing(request.Email), "", request.Email), _
                                            .CODUSER = If(IsNothing(request.CODUSER), "", request.CODUSER), _
                                            .CODAPP = If(IsNothing(request.CODAPP), "", request.CODAPP)}

        Try
            Using oServicioConsultaBL As New MapeoDatos
                oSunarpVigenciaPoderBE = oServicioConsultaBL.SunarpVigenciaPoder(oPerson)
            End Using
            response = New SunarpVigenciaPoderResponse With {.Correcto = True, .MensajeError = String.Empty, .Resultado = oSunarpVigenciaPoderBE}

        Catch ex As Exception
            Dim oResultadoBL As New ResultadoBL
            oResultadoBL.RegistrarLog_SunarpVigenciaPoder(False, ex.Message, oPerson, oSunarpVigenciaPoderBE)
            response = New SunarpVigenciaPoderResponse With {.Correcto = False, .MensajeError = ex.Message}
        End Try

        Return response
    End Function

    Public Function SunarpListaAsiento(ByVal request As SunarpListaAsientoRequest) As SunarpListaAsientoResponse Implements IServiceIO.SunarpListaAsiento
        Dim response As SunarpListaAsientoResponse
        Dim olista = New List(Of SunarpAsientoBE)()
        Dim oPerson = New PersonaBE() With {.Zona = If(IsNothing(request.Zona), "", request.Zona), _
                                            .Oficina = If(IsNothing(request.Oficina), "", request.Oficina), _
                                            .Partida = If(IsNothing(request.Partida), "", request.Partida), _
                                            .Registro = If(IsNothing(request.Registro), "", request.Registro), _
                                            .CODUSER = If(IsNothing(request.CODUSER), "", request.CODUSER), _
                                            .CODAPP = If(IsNothing(request.CODAPP), "", request.CODAPP)}

        Try
            Using oServicioConsultaBL As New MapeoDatos
                olista = oServicioConsultaBL.SunarpListaAsiento(oPerson)
            End Using
            response = New SunarpListaAsientoResponse With {.Correcto = True, .MensajeError = String.Empty, .Resultado = olista}

        Catch ex As Exception
            Dim oResultadoBL As New ResultadoBL
            oResultadoBL.RegistrarLog_SunarpListaAsiento(False, ex.Message, oPerson, olista)
            response = New SunarpListaAsientoResponse With {.Correcto = False, .MensajeError = ex.Message}
        End Try

        Return response
    End Function

    Public Function SunarpVerImagenAsiento(ByVal request As SunarpVerImagenAsientoRequest) As SunarpVerImagenAsientoResponse Implements IServiceIO.SunarpVerImagenAsiento
        Dim response As SunarpVerImagenAsientoResponse
        Dim strImagen As String = ""
        Dim oPerson = New AsientoBE() With {.Transaccion = If(IsNothing(request.Transaccion), "", request.Transaccion), _
                                            .IdImg = If(IsNothing(request.IdImg), "", request.IdImg), _
                                            .Tipo = If(IsNothing(request.Tipo), "", request.Tipo), _
                                            .NroTotalPag = If(IsNothing(request.NroTotalPag), "", request.NroTotalPag), _
                                            .NroPagRef = If(IsNothing(request.NroPagRef), "", request.NroPagRef), _
                                            .Pagina = If(IsNothing(request.Pagina), "", request.Pagina), _
                                            .CODUSER = If(IsNothing(request.CODUSER), "", request.CODUSER), _
                                            .CODAPP = If(IsNothing(request.CODAPP), "", request.CODAPP)}

        Try
            Using oServicioConsultaBL As New MapeoDatos
                strImagen = oServicioConsultaBL.SunarpVerImagenAsiento(oPerson)
            End Using
            response = New SunarpVerImagenAsientoResponse With {.Correcto = True, .MensajeError = String.Empty, .Resultado = strImagen}

        Catch ex As Exception
            Dim oResultadoBL As New ResultadoBL
            oResultadoBL.RegistrarLog_SunarpVerImagenAsiento(False, ex.Message, oPerson, strImagen)
            response = New SunarpVerImagenAsientoResponse With {.Correcto = False, .MensajeError = ex.Message}
        End Try

        Return response
    End Function

    Public Function SuneduGradosTitulos(ByVal request As SuneduGradosTitulosRequest) As SuneduGradosTitulosResponse Implements IServiceIO.SuneduGradosTitulos
        Dim response As SuneduGradosTitulosResponse
        Dim olista = New List(Of SuneduGradoAcademicoBE)
        Dim oPerson = New PersonaBE() With {.DNI = If(IsNothing(request.DNI), "", request.DNI), _
                                            .CODUSER = If(IsNothing(request.CODUSER), "", request.CODUSER), _
                                            .CODAPP = If(IsNothing(request.CODAPP), "", request.CODAPP)}

        Try
            Using oServicioConsultaBL As New MapeoDatos
                olista = oServicioConsultaBL.SuneduGradosTitulos(oPerson)
            End Using
            response = New SuneduGradosTitulosResponse With {.Correcto = True, .MensajeError = String.Empty, .Resultado = olista}

        Catch ex As Exception
            Dim oResultadoBL As New ResultadoBL
            oResultadoBL.RegistrarLog_SuneduGradosTitulos(False, ex.Message, oPerson, olista)
            response = New SuneduGradosTitulosResponse With {.Correcto = False, .MensajeError = ex.Message}
        End Try

        Return response
    End Function

    Public Function MigracionesCarnetExtranjeria(ByVal request As MigracionesCarnetExtranjeriaRequest) As MigracionesCarnetExtranjeriaResponse Implements IServiceIO.MigracionesCarnetExtranjeria
        Dim response As MigracionesCarnetExtranjeriaResponse
        Dim oMigracionesCarnetExtranjeriaBE As MigracionesCarnetExtranjeriaBE = Nothing
        Dim oPerson = New PersonaBE() With {.NumeroDoc = If(IsNothing(request.NumeroDocumento), "", request.NumeroDocumento), _
                                            .CODUSER = If(IsNothing(request.CODUSER), "", request.CODUSER), _
                                            .CODAPP = If(IsNothing(request.CODAPP), "", request.CODAPP)}

        Try
            Using oServicioConsultaBL As New MapeoDatos
                oMigracionesCarnetExtranjeriaBE = oServicioConsultaBL.MigracionesCarnetExtranjeria(oPerson)
            End Using
            response = New MigracionesCarnetExtranjeriaResponse With {.Correcto = True, .MensajeError = String.Empty, .Resultado = oMigracionesCarnetExtranjeriaBE}

        Catch ex As Exception
            Dim oResultadoBL As New ResultadoBL
            oResultadoBL.RegistrarLog_MigracionesCarnetExtranjeria(False, ex.Message, oPerson, oMigracionesCarnetExtranjeriaBE)
            response = New MigracionesCarnetExtranjeriaResponse With {.Correcto = False, .MensajeError = ex.Message}
        End Try

        Return response
    End Function

End Class
