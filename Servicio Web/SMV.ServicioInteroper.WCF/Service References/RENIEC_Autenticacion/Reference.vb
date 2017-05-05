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


Namespace RENIEC_Autenticacion
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ServiceModel.ServiceContractAttribute([Namespace]:="http://WSAuthentication_wsdl.wsauth.reniec.gob.pe/", ConfigurationName:="RENIEC_Autenticacion.WSAuthentication")>  _
    Public Interface WSAuthentication
        
        'CODEGEN: Generating message contract since element name user from namespace  is not marked nillable
        <System.ServiceModel.OperationContractAttribute(Action:="", ReplyAction:="*")>  _
        Function getTicket(ByVal request As RENIEC_Autenticacion.getTicket) As RENIEC_Autenticacion.getTicketResponse
        
        <System.ServiceModel.OperationContractAttribute(Action:="", ReplyAction:="*")>  _
        Function getTicketAsync(ByVal request As RENIEC_Autenticacion.getTicket) As System.Threading.Tasks.Task(Of RENIEC_Autenticacion.getTicketResponse)
    End Interface
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced),  _
     System.ServiceModel.MessageContractAttribute(IsWrapped:=false)>  _
    Partial Public Class getTicket
        
        <System.ServiceModel.MessageBodyMemberAttribute(Name:="getTicket", [Namespace]:="http://WSAuthentication_wsdl.wsauth.reniec.gob.pe/", Order:=0)>  _
        Public Body As RENIEC_Autenticacion.getTicketBody
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal Body As RENIEC_Autenticacion.getTicketBody)
            MyBase.New
            Me.Body = Body
        End Sub
    End Class
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced),  _
     System.Runtime.Serialization.DataContractAttribute([Namespace]:="")>  _
    Partial Public Class getTicketBody
        
        <System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue:=false, Order:=0)>  _
        Public user As String
        
        <System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue:=false, Order:=1)>  _
        Public password As String
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal user As String, ByVal password As String)
            MyBase.New
            Me.user = user
            Me.password = password
        End Sub
    End Class
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced),  _
     System.ServiceModel.MessageContractAttribute(IsWrapped:=false)>  _
    Partial Public Class getTicketResponse
        
        <System.ServiceModel.MessageBodyMemberAttribute(Name:="getTicketResponse", [Namespace]:="http://WSAuthentication_wsdl.wsauth.reniec.gob.pe/", Order:=0)>  _
        Public Body As RENIEC_Autenticacion.getTicketResponseBody
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal Body As RENIEC_Autenticacion.getTicketResponseBody)
            MyBase.New
            Me.Body = Body
        End Sub
    End Class
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced),  _
     System.Runtime.Serialization.DataContractAttribute([Namespace]:="")>  _
    Partial Public Class getTicketResponseBody
        
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
    Public Interface WSAuthenticationChannel
        Inherits RENIEC_Autenticacion.WSAuthentication, System.ServiceModel.IClientChannel
    End Interface
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")>  _
    Partial Public Class WSAuthenticationClient
        Inherits System.ServiceModel.ClientBase(Of RENIEC_Autenticacion.WSAuthentication)
        Implements RENIEC_Autenticacion.WSAuthentication
        
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
        Function RENIEC_Autenticacion_WSAuthentication_getTicket(ByVal request As RENIEC_Autenticacion.getTicket) As RENIEC_Autenticacion.getTicketResponse Implements RENIEC_Autenticacion.WSAuthentication.getTicket
            Return MyBase.Channel.getTicket(request)
        End Function
        
        Public Function getTicket(ByVal user As String, ByVal password As String) As String
            Dim inValue As RENIEC_Autenticacion.getTicket = New RENIEC_Autenticacion.getTicket()
            inValue.Body = New RENIEC_Autenticacion.getTicketBody()
            inValue.Body.user = user
            inValue.Body.password = password
            Dim retVal As RENIEC_Autenticacion.getTicketResponse = CType(Me,RENIEC_Autenticacion.WSAuthentication).getTicket(inValue)
            Return retVal.Body.[return]
        End Function
        
        <System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Function RENIEC_Autenticacion_WSAuthentication_getTicketAsync(ByVal request As RENIEC_Autenticacion.getTicket) As System.Threading.Tasks.Task(Of RENIEC_Autenticacion.getTicketResponse) Implements RENIEC_Autenticacion.WSAuthentication.getTicketAsync
            Return MyBase.Channel.getTicketAsync(request)
        End Function
        
        Public Function getTicketAsync(ByVal user As String, ByVal password As String) As System.Threading.Tasks.Task(Of RENIEC_Autenticacion.getTicketResponse)
            Dim inValue As RENIEC_Autenticacion.getTicket = New RENIEC_Autenticacion.getTicket()
            inValue.Body = New RENIEC_Autenticacion.getTicketBody()
            inValue.Body.user = user
            inValue.Body.password = password
            Return CType(Me,RENIEC_Autenticacion.WSAuthentication).getTicketAsync(inValue)
        End Function
    End Class
End Namespace