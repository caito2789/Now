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


Namespace RENIEC_Consulta
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ServiceModel.ServiceContractAttribute([Namespace]:="http://WSDataVerification_wsdl.wsauth.reniec.gob.pe/", ConfigurationName:="RENIEC_Consulta.WSDataVerification")>  _
    Public Interface WSDataVerification
        
        'CODEGEN: Generating message contract since element name xmlDocumento from namespace  is not marked nillable
        <System.ServiceModel.OperationContractAttribute(Action:="", ReplyAction:="*")>  _
        Function getDatavalidate(ByVal request As RENIEC_Consulta.getDatavalidate) As RENIEC_Consulta.getDatavalidateResponse
        
        <System.ServiceModel.OperationContractAttribute(Action:="", ReplyAction:="*")>  _
        Function getDatavalidateAsync(ByVal request As RENIEC_Consulta.getDatavalidate) As System.Threading.Tasks.Task(Of RENIEC_Consulta.getDatavalidateResponse)
    End Interface
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced),  _
     System.ServiceModel.MessageContractAttribute(IsWrapped:=false)>  _
    Partial Public Class getDatavalidate
        
        <System.ServiceModel.MessageBodyMemberAttribute(Name:="getDatavalidate", [Namespace]:="http://WSDataVerification_wsdl.wsauth.reniec.gob.pe/", Order:=0)>  _
        Public Body As RENIEC_Consulta.getDatavalidateBody
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal Body As RENIEC_Consulta.getDatavalidateBody)
            MyBase.New
            Me.Body = Body
        End Sub
    End Class
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced),  _
     System.Runtime.Serialization.DataContractAttribute([Namespace]:="")>  _
    Partial Public Class getDatavalidateBody
        
        <System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue:=false, Order:=0)>  _
        Public xmlDocumento As String
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal xmlDocumento As String)
            MyBase.New
            Me.xmlDocumento = xmlDocumento
        End Sub
    End Class
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced),  _
     System.ServiceModel.MessageContractAttribute(IsWrapped:=false)>  _
    Partial Public Class getDatavalidateResponse
        
        <System.ServiceModel.MessageBodyMemberAttribute(Name:="getDatavalidateResponse", [Namespace]:="http://WSDataVerification_wsdl.wsauth.reniec.gob.pe/", Order:=0)>  _
        Public Body As RENIEC_Consulta.getDatavalidateResponseBody
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal Body As RENIEC_Consulta.getDatavalidateResponseBody)
            MyBase.New
            Me.Body = Body
        End Sub
    End Class
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced),  _
     System.Runtime.Serialization.DataContractAttribute([Namespace]:="")>  _
    Partial Public Class getDatavalidateResponseBody
        
        <System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue:=false, Order:=0)>  _
        Public [return] As String
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal [return] As String)
            MyBase.New
            Me.[return] = [return]
        End Sub
    End Class
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")>  _
    Public Interface WSDataVerificationChannel
        Inherits RENIEC_Consulta.WSDataVerification, System.ServiceModel.IClientChannel
    End Interface
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")>  _
    Partial Public Class WSDataVerificationClient
        Inherits System.ServiceModel.ClientBase(Of RENIEC_Consulta.WSDataVerification)
        Implements RENIEC_Consulta.WSDataVerification
        
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
        Function RENIEC_Consulta_WSDataVerification_getDatavalidate(ByVal request As RENIEC_Consulta.getDatavalidate) As RENIEC_Consulta.getDatavalidateResponse Implements RENIEC_Consulta.WSDataVerification.getDatavalidate
            Return MyBase.Channel.getDatavalidate(request)
        End Function
        
        Public Function getDatavalidate(ByVal xmlDocumento As String) As String
            Dim inValue As RENIEC_Consulta.getDatavalidate = New RENIEC_Consulta.getDatavalidate()
            inValue.Body = New RENIEC_Consulta.getDatavalidateBody()
            inValue.Body.xmlDocumento = xmlDocumento
            Dim retVal As RENIEC_Consulta.getDatavalidateResponse = CType(Me,RENIEC_Consulta.WSDataVerification).getDatavalidate(inValue)
            Return retVal.Body.[return]
        End Function
        
        <System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Function RENIEC_Consulta_WSDataVerification_getDatavalidateAsync(ByVal request As RENIEC_Consulta.getDatavalidate) As System.Threading.Tasks.Task(Of RENIEC_Consulta.getDatavalidateResponse) Implements RENIEC_Consulta.WSDataVerification.getDatavalidateAsync
            Return MyBase.Channel.getDatavalidateAsync(request)
        End Function
        
        Public Function getDatavalidateAsync(ByVal xmlDocumento As String) As System.Threading.Tasks.Task(Of RENIEC_Consulta.getDatavalidateResponse)
            Dim inValue As RENIEC_Consulta.getDatavalidate = New RENIEC_Consulta.getDatavalidate()
            inValue.Body = New RENIEC_Consulta.getDatavalidateBody()
            inValue.Body.xmlDocumento = xmlDocumento
            Return CType(Me,RENIEC_Consulta.WSDataVerification).getDatavalidateAsync(inValue)
        End Function
    End Class
End Namespace