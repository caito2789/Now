Public Class Constantes
    Public Structure Estado
        Public Const K_COD_ACTIVO As String = "A"
        Public Const K_COD_INACTIVO As String = "I"
        Public Const K_DES_ACTIVO As String = "ACTIVO"
        Public Const K_DES_INACTIVO As String = "INACTIVO"
    End Structure

    Public Structure Flag
        Public Const K_NO As String = "N"
        Public Const K_SI As String = "S"
    End Structure

    Public Structure MININTER_TipoConsulta
        Public Const K_NOMBRE As String = "1"
        Public Const K_DNI As String = "2"
    End Structure

    Public Structure TablaPadre
        Public Const K_CONS_RENIEC As Integer = 5
        Public Const K_CONS_INPE_ANTPENAL As Integer = 6
        Public Const K_CONS_MININTER_ANTPOLI As Integer = 7
        Public Const K_CONS_SUNEDU_GRADOS As Integer = 8
        Public Const K_CONS_MIGRA_CAREXT As Integer = 9
        Public Const K_ERROR_RENIEC As Integer = 10
        Public Const K_RESULTADO_PJ As Integer = 11
        Public Const K_RESULTADO_MinInter As Integer = 12
        Public Const K_ERROR_MinInter As Integer = 13
        Public Const K_ERROR_MIGRACIONES As Integer = 14
    End Structure

    Public Structure TH_CONS_RENIEC
        Public Const K_USER As Integer = 87
        Public Const K_PASS As Integer = 88
        Public Const K_COD_USER As Integer = 89
        Public Const K_COD_TRANSAC As Integer = 90
        Public Const K_COD_ENTIDAD As Integer = 91
    End Structure

    Public Structure TH_CONS_INPE
        Public Const K_MOTIVO_CONS As Integer = 92
        Public Const K_PROC_ENTIDAD_CONS As Integer = 93
        Public Const K_RUC_ENTIDAD_CONS As Integer = 94
        Public Const K_DNI_PERSONA_CONS As Integer = 95
        Public Const K_AUD_NOMBRE_PC As Integer = 96
        Public Const K_AUD_IP As Integer = 97
        Public Const K_AUD_NOMBRE_USER As Integer = 98
        Public Const K_AUD_DIREC_MAC As Integer = 99
    End Structure

    Public Structure TH_CONS_MININTER
        Public Const K_USUARIO As Integer = 100
        Public Const K_CLAVE As Integer = 101
        Public Const K_ENTIDAD_CONS As Integer = 102
        Public Const K_DNI_CONS As Integer = 103
    End Structure

    Public Structure TH_CONS_SUNEDU
        Public Const K_USUARIO As Integer = 104
        Public Const K_CLAVE As Integer = 105
        Public Const K_ID_ENTIDAD As Integer = 106
        Public Const K_FECHA As Integer = 107
        Public Const K_HORA As Integer = 108
        Public Const K_MAC_WSSERVER As Integer = 109
        Public Const K_IP_WSSERVER As Integer = 110
        Public Const K_IP_WSUSER As Integer = 111
    End Structure

    Public Structure TH_CONS_MIGRACION
        Public Const K_COD_INSTITUCION As Integer = 112
        Public Const K_MAC As Integer = 113
        Public Const K_NRO_IP As Integer = 114
        Public Const K_TIPO_DOC As Integer = 115
    End Structure


    Public Structure TH_RESULTADO_PJ
        Public Const K_TIENE_ANTEC As Integer = 126
        Public Const K_NO_TIENE_ANTEC As Integer = 127
    End Structure

    Public Structure TH_RESULTADO_MININTER
        Public Const K_TIENE_ANTEC As Integer = 128
        Public Const K_NO_TIENE_ANTEC As Integer = 129
    End Structure



    Public Structure TH_SERVICIOS_VALOR1
        Public Const K_RENIEC_VAL_DNI As String = "1"
        Public Const K_MIGRACIONES_CARNET_EXT As String = "2"
        Public Const K_INPE_ANT_PENALES As String = "3"
        Public Const K_PODERJUD_ANT_JUDICIALES As String = "4"
        Public Const K_MININTER_ANT_POLICIALES As String = "5"
        Public Const K_SUNARP_TITU_BIENES As String = "6"
        Public Const K_SUNARP_VIG_PODER As String = "7"
        Public Const K_SUNARP_LIST_ASIENTOS As String = "8"
        Public Const K_SUNARP_VER_ASIENTO As String = "9"
        Public Const K_SUNEDU_GRADOSYTIT As String = "10"
    End Structure

    Public Structure IndAccion
        Public Const K_CONSULTA As Integer = 1
        Public Const K_EXPORTAR As Integer = 2
        Public Const K_VER_IMAGEN As Integer = 3
    End Structure

    Public Structure TipoResultado
        Public Const K_OK As Integer = 1
        Public Const K_ERROR As Integer = 2
    End Structure

End Class
