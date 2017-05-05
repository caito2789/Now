Imports System.ComponentModel
Imports System.Reflection

''' <summary>
''' Módulo que contiene métodos y constantes de apoyo.
''' </summary>
''' <remarks></remarks>
Public Module Globales

    <System.Runtime.CompilerServices.Extension()> _
    Public Sub DropDownlistBinding(drop As DropDownList, datasource As Object, dataTextField As String, dataValueField As String)
        DropDownlistBinding(drop, datasource, dataTextField, dataValueField, Nothing)
    End Sub

    <System.Runtime.CompilerServices.Extension()> _
    Public Sub DropDownlistBinding(drop As DropDownList, datasource As Object, dataTextField As String, dataValueField As String, firstItem As String)
        If Not drop Is Nothing Then
            drop.DataSource = datasource
            drop.DataTextField = dataTextField
            drop.DataValueField = dataValueField
            drop.DataBind()

            If Not String.IsNullOrEmpty(firstItem) Then
                drop.Items.Insert(0, New ListItem(firstItem, "0"))
                drop.SelectedIndex = 0
            End If

        End If
    End Sub

    Public Sub OrdenarGrilla(Of T)(grid As GridView, lista As List(Of T), campo As String, asc As Boolean)
        Try
            If Not lista Is Nothing Then
                If Not String.IsNullOrWhiteSpace(campo) Then
                    If asc Then
                        lista = lista.OrderBy(Function(l) l.GetType().GetProperty(campo).GetValue(l, Nothing)).ToList()
                    Else
                        lista = lista.OrderByDescending(Function(l) l.GetType().GetProperty(campo).GetValue(l, Nothing)).ToList()
                    End If
                End If
            End If

            grid.DataSource = lista
            grid.DataBind()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function ConvertToDataTable(Of T)(ByVal data As IList(Of T)) As DataTable
        Dim table As DataTable = Nothing
        Try
            If data IsNot Nothing Then
                Dim properties As PropertyDescriptorCollection = TypeDescriptor.GetProperties(GetType(T))
                table = New DataTable()
                Dim tblFinal As New DataTable()

                For Each prop As PropertyDescriptor In properties
                    table.Columns.Add(prop.Name, If(Nullable.GetUnderlyingType(prop.PropertyType), prop.PropertyType))
                Next

                For Each item As T In data
                    Dim row As DataRow = table.NewRow()
                    For Each prop As PropertyDescriptor In properties
                        row(prop.Name) = If(prop.GetValue(item), DBNull.Value)
                    Next
                    table.Rows.Add(row)
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
        Return table
    End Function

    Public Function DataTableToList(Of T As {Class, New})(table As DataTable) As List(Of T)
        Try
            Dim list As New List(Of T)

            For Each row In table.AsEnumerable()
                Dim obj As New T

                For Each prop In obj.[GetType].GetProperties()
                    Try
                        Dim propertyInfo As PropertyInfo = obj.[GetType].GetProperty(prop.Name)
                        propertyInfo.SetValue(obj, Convert.ChangeType(row(prop.Name), propertyInfo.PropertyType), Nothing)
                    Catch
                        Continue For
                    End Try
                Next

                list.Add(obj)
            Next

            Return list
        Catch
            Return Nothing
        End Try
    End Function

    Public Structure Exportacion
        Public Const K_FORMATO_TITULO_EXCEL = "<table border=0 cellpadding=0 cellspacing=0><tr><td colspan='{0}' align='center'><h1>{1}</h1></td></tr></table>"
        Public Const K_FORMATO_CONTENIDO_ADJUNTO = "attachment;filename={0}"
        Public Const K_NOMBRE_EXCEL_ = "Reporte.xls"
    End Structure

    Public Structure UrlPagina
        Public Const K_FORMULARIO_LOGIN = "~/Views/Security/frmLogin.aspx"
        Public Const K_FORMULARIO_LOGINJQ = "../../Views/Security/frmLogin.aspx"
        Public Const K_FORMULARIO_DEFAULT = "~/Views/Security/frmInicio.aspx"
    End Structure

    Public Function ListaEstado() As List(Of Object)
        Dim lista As New List(Of Object)
        lista.Add(New With {.Codigo = BE.Constantes.Estado.K_COD_ACTIVO, .Descripcion = BE.Constantes.Estado.K_DES_ACTIVO})
        lista.Add(New With {.Codigo = BE.Constantes.Estado.K_COD_INACTIVO, .Descripcion = BE.Constantes.Estado.K_DES_INACTIVO})
        Return lista
    End Function

    Public Sub LogErrores(Message, StackTrace)
        'Crear Carpeta Log:
        Dim Directorio = IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Log")
        If Not IO.Directory.Exists(Directorio) Then
            IO.Directory.CreateDirectory(Directorio)
        End If

        Dim texto As String = "(" & Date.Now & ") Message:" & vbCrLf & Message & vbCrLf &
                              "(" & Date.Now & ") StackTrace:" & vbCrLf & StackTrace & vbCrLf &
                              "----------------------------------------------------------------------------"
        Dim ArchivoLog As String = String.Format("{0}\LogError{1}.txt", Directorio, DateTime.Now.ToString("yyyyMMdd"))
        Dim tw As IO.TextWriter = New IO.StreamWriter(ArchivoLog, True)
        tw.WriteLine(texto)
        tw.Close()
    End Sub

    'Public Function FirmarArchivo(strFilessignature As String, strnameTag As String, strfileSignnet As String) As Boolean
    '    Dim myObject As SignnetSolution.SignnetSignature = New SignnetSolution.SignnetSignature
    '    Dim result As Boolean = False
    '    Dim Executable = BE.Constantes.ConfiguracionFirmaSignnet.Executable
    '    Dim Filessignature = strFilessignature '"ruta y archivo a firmar"
    '    Dim nameTag = strnameTag  '"Nombre del Firmante"
    '    Dim reason = BE.Constantes.ConfiguracionFirmaSignnet.reason
    '    Dim location = BE.Constantes.ConfiguracionFirmaSignnet.location
    '    Dim comment = BE.Constantes.ConfiguracionFirmaSignnet.comment
    '    Dim boolSignnet = BE.Constantes.ConfiguracionFirmaSignnet.boolSignnet
    '    Dim fileSignnet = strfileSignnet
    '    result = myObject.signature(Executable, Filessignature, nameTag, reason, location, comment, boolSignnet, fileSignnet)
    '    Return result
    'End Function

End Module
