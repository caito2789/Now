﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.34209
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On


Namespace MIGRACION_Consulta
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ServiceModel.ServiceContractAttribute([Namespace]:="http://ws.consulta.usuarios.migraciones.gob.pe/", ConfigurationName:="MIGRACION_Consulta.EjecTransaccionCarExtraPcm")>  _
    Public Interface EjecTransaccionCarExtraPcm
        
        'CODEGEN: Parameter 'return' requires additional schema information that cannot be captured using the parameter mode. The specific attribute is 'System.Xml.Serialization.XmlElementAttribute'.
        <System.ServiceModel.OperationContractAttribute(Action:="consultarDocumento", ReplyAction:="http://ws.consulta.usuarios.migraciones.gob.pe/EjecTransaccionCarExtraPcm/consult"& _ 
            "arDocumentoResponse"),  _
         System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults:=true),  _
         System.ServiceModel.ServiceKnownTypeAttribute(GetType(consulta))>  _
        Function consultarDocumento(ByVal request As MIGRACION_Consulta.consultarDocumentoRequest) As <System.ServiceModel.MessageParameterAttribute(Name:="return")> MIGRACION_Consulta.consultarDocumentoResponse
        
        <System.ServiceModel.OperationContractAttribute(Action:="consultarDocumento", ReplyAction:="http://ws.consulta.usuarios.migraciones.gob.pe/EjecTransaccionCarExtraPcm/consult"& _ 
            "arDocumentoResponse")>  _
        Function consultarDocumentoAsync(ByVal request As MIGRACION_Consulta.consultarDocumentoRequest) As System.Threading.Tasks.Task(Of MIGRACION_Consulta.consultarDocumentoResponse)
    End Interface
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234"),  _
     System.SerializableAttribute(),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://ws.consulta.usuarios.migraciones.gob.pe/")>  _
    Partial Public Class solicitudBean
        Inherits consulta
    End Class
    
    '''<remarks/>
    <System.Xml.Serialization.XmlIncludeAttribute(GetType(solicitudBean)),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234"),  _
     System.SerializableAttribute(),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://ws.consulta.usuarios.migraciones.gob.pe/")>  _
    Partial Public MustInherit Class consulta
        Inherits Object
        Implements System.ComponentModel.INotifyPropertyChanged
        
        Private strCodInstitucionField As String
        
        Private strMacField As String
        
        Private strNroIpField As String
        
        Private strNumDocumentoField As String
        
        Private strTipoDocumentoField As String
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified, Order:=0)>  _
        Public Property strCodInstitucion() As String
            Get
                Return Me.strCodInstitucionField
            End Get
            Set
                Me.strCodInstitucionField = value
                Me.RaisePropertyChanged("strCodInstitucion")
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified, Order:=1)>  _
        Public Property strMac() As String
            Get
                Return Me.strMacField
            End Get
            Set
                Me.strMacField = value
                Me.RaisePropertyChanged("strMac")
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified, Order:=2)>  _
        Public Property strNroIp() As String
            Get
                Return Me.strNroIpField
            End Get
            Set
                Me.strNroIpField = value
                Me.RaisePropertyChanged("strNroIp")
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified, Order:=3)>  _
        Public Property strNumDocumento() As String
            Get
                Return Me.strNumDocumentoField
            End Get
            Set
                Me.strNumDocumentoField = value
                Me.RaisePropertyChanged("strNumDocumento")
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified, Order:=4)>  _
        Public Property strTipoDocumento() As String
            Get
                Return Me.strTipoDocumentoField
            End Get
            Set
                Me.strTipoDocumentoField = value
                Me.RaisePropertyChanged("strTipoDocumento")
            End Set
        End Property
        
        Public Event PropertyChanged As System.ComponentModel.PropertyChangedEventHandler Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged
        
        Protected Sub RaisePropertyChanged(ByVal propertyName As String)
            Dim propertyChanged As System.ComponentModel.PropertyChangedEventHandler = Me.PropertyChangedEvent
            If (Not (propertyChanged) Is Nothing) Then
                propertyChanged(Me, New System.ComponentModel.PropertyChangedEventArgs(propertyName))
            End If
        End Sub
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234"),  _
     System.SerializableAttribute(),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://ws.consulta.usuarios.migraciones.gob.pe/")>  _
    Partial Public Class respuestaBean
        Inherits Object
        Implements System.ComponentModel.INotifyPropertyChanged
        
        Private strCalidadMigratoriaField As String
        
        Private strNombresField As String
        
        Private strNumRespuestaField As String
        
        Private strPrimerApellidoField As String
        
        Private strSegundoApellidoField As String
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified, Order:=0)>  _
        Public Property strCalidadMigratoria() As String
            Get
                Return Me.strCalidadMigratoriaField
            End Get
            Set
                Me.strCalidadMigratoriaField = value
                Me.RaisePropertyChanged("strCalidadMigratoria")
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified, Order:=1)>  _
        Public Property strNombres() As String
            Get
                Return Me.strNombresField
            End Get
            Set
                Me.strNombresField = value
                Me.RaisePropertyChanged("strNombres")
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified, Order:=2)>  _
        Public Property strNumRespuesta() As String
            Get
                Return Me.strNumRespuestaField
            End Get
            Set
                Me.strNumRespuestaField = value
                Me.RaisePropertyChanged("strNumRespuesta")
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified, Order:=3)>  _
        Public Property strPrimerApellido() As String
            Get
                Return Me.strPrimerApellidoField
            End Get
            Set
                Me.strPrimerApellidoField = value
                Me.RaisePropertyChanged("strPrimerApellido")
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified, Order:=4)>  _
        Public Property strSegundoApellido() As String
            Get
                Return Me.strSegundoApellidoField
            End Get
            Set
                Me.strSegundoApellidoField = value
                Me.RaisePropertyChanged("strSegundoApellido")
            End Set
        End Property
        
        Public Event PropertyChanged As System.ComponentModel.PropertyChangedEventHandler Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged
        
        Protected Sub RaisePropertyChanged(ByVal propertyName As String)
            Dim propertyChanged As System.ComponentModel.PropertyChangedEventHandler = Me.PropertyChangedEvent
            If (Not (propertyChanged) Is Nothing) Then
                propertyChanged(Me, New System.ComponentModel.PropertyChangedEventArgs(propertyName))
            End If
        End Sub
    End Class
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced),  _
     System.ServiceModel.MessageContractAttribute(WrapperName:="consultarDocumento", WrapperNamespace:="http://ws.consulta.usuarios.migraciones.gob.pe/", IsWrapped:=true)>  _
    Partial Public Class consultarDocumentoRequest
        
        <System.ServiceModel.MessageBodyMemberAttribute([Namespace]:="http://ws.consulta.usuarios.migraciones.gob.pe/", Order:=0),  _
         System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)>  _
        Public solicitud As MIGRACION_Consulta.solicitudBean
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal solicitud As MIGRACION_Consulta.solicitudBean)
            MyBase.New
            Me.solicitud = solicitud
        End Sub
    End Class
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced),  _
     System.ServiceModel.MessageContractAttribute(WrapperName:="consultarDocumentoResponse", WrapperNamespace:="http://ws.consulta.usuarios.migraciones.gob.pe/", IsWrapped:=true)>  _
    Partial Public Class consultarDocumentoResponse
        
        <System.ServiceModel.MessageBodyMemberAttribute([Namespace]:="http://ws.consulta.usuarios.migraciones.gob.pe/", Order:=0),  _
         System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)>  _
        Public [return] As MIGRACION_Consulta.respuestaBean
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal [return] As MIGRACION_Consulta.respuestaBean)
            MyBase.New
            Me.[return] = [return]
        End Sub
    End Class
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")>  _
    Public Interface EjecTransaccionCarExtraPcmChannel
        Inherits MIGRACION_Consulta.EjecTransaccionCarExtraPcm, System.ServiceModel.IClientChannel
    End Interface
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")>  _
    Partial Public Class EjecTransaccionCarExtraPcmClient
        Inherits System.ServiceModel.ClientBase(Of MIGRACION_Consulta.EjecTransaccionCarExtraPcm)
        Implements MIGRACION_Consulta.EjecTransaccionCarExtraPcm
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal endpointConfigurationName As String)
            MyBase.New(endpointConfigurationName)
        End Sub
        
        Public Sub New(ByVal endpointConfigurationName As String, ByVal remoteAddress As String)
            MyBase.New(endpointConfigurationName, remoteAddress)
        End Sub
        
        Public Sub New(ByVal endpointConfigurationName As String, ByVal remoteAddress As System.ServiceModel.EndpointAddress)
            MyBase.New(endpointConfigurationName, remoteAddress)
        End Sub
        
        Public Sub New(ByVal binding As System.ServiceModel.Channels.Binding, ByVal remoteAddress As System.ServiceModel.EndpointAddress)
            MyBase.New(binding, remoteAddress)
        End Sub
        
        <System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Function MIGRACION_Consulta_EjecTransaccionCarExtraPcm_consultarDocumento(ByVal request As MIGRACION_Consulta.consultarDocumentoRequest) As MIGRACION_Consulta.consultarDocumentoResponse Implements MIGRACION_Consulta.EjecTransaccionCarExtraPcm.consultarDocumento
            Return MyBase.Channel.consultarDocumento(request)
        End Function
        
        Public Function consultarDocumento(ByVal solicitud As MIGRACION_Consulta.solicitudBean) As MIGRACION_Consulta.respuestaBean
            Dim inValue As MIGRACION_Consulta.consultarDocumentoRequest = New MIGRACION_Consulta.consultarDocumentoRequest()
            inValue.solicitud = solicitud
            Dim retVal As MIGRACION_Consulta.consultarDocumentoResponse = CType(Me,MIGRACION_Consulta.EjecTransaccionCarExtraPcm).consultarDocumento(inValue)
            Return retVal.[return]
        End Function
        
        <System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Function MIGRACION_Consulta_EjecTransaccionCarExtraPcm_consultarDocumentoAsync(ByVal request As MIGRACION_Consulta.consultarDocumentoRequest) As System.Threading.Tasks.Task(Of MIGRACION_Consulta.consultarDocumentoResponse) Implements MIGRACION_Consulta.EjecTransaccionCarExtraPcm.consultarDocumentoAsync
            Return MyBase.Channel.consultarDocumentoAsync(request)
        End Function
        
        Public Function consultarDocumentoAsync(ByVal solicitud As MIGRACION_Consulta.solicitudBean) As System.Threading.Tasks.Task(Of MIGRACION_Consulta.consultarDocumentoResponse)
            Dim inValue As MIGRACION_Consulta.consultarDocumentoRequest = New MIGRACION_Consulta.consultarDocumentoRequest()
            inValue.solicitud = solicitud
            Return CType(Me,MIGRACION_Consulta.EjecTransaccionCarExtraPcm).consultarDocumentoAsync(inValue)
        End Function
    End Class
End Namespace
