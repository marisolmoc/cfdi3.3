﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SiscomCFDI.cfdinova_produccion {
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://integradores.cfdi.mx.konesh.com")]
    public partial class TimbradorIntegradoresException : object, System.ComponentModel.INotifyPropertyChanged {
        
        private Exception timbradorIntegradoresException1Field;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("TimbradorIntegradoresException", IsNullable=true, Order=0)]
        public Exception TimbradorIntegradoresException1 {
            get {
                return this.timbradorIntegradoresException1Field;
            }
            set {
                this.timbradorIntegradoresException1Field = value;
                this.RaisePropertyChanged("TimbradorIntegradoresException1");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://integradores.cfdi.mx.konesh.com")]
    public partial class Exception : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string messageField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=0)]
        public string Message {
            get {
                return this.messageField;
            }
            set {
                this.messageField = value;
                this.RaisePropertyChanged("Message");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://integradores.cfdi.mx.konesh.com", ConfigurationName="cfdinova_produccion.TimbradorIntegradoresPortType")]
    public interface TimbradorIntegradoresPortType {
        
        // CODEGEN: Parameter 'return' requires additional schema information that cannot be captured using the parameter mode. The specific attribute is 'System.Xml.Serialization.XmlElementAttribute'.
        [System.ServiceModel.OperationContractAttribute(Action="urn:get", ReplyAction="urn:getResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(SiscomCFDI.cfdinova_produccion.TimbradorIntegradoresException), Action="urn:getTimbradorIntegradoresException", Name="TimbradorIntegradoresException")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="return")]
        SiscomCFDI.cfdinova_produccion.getResponse get(SiscomCFDI.cfdinova_produccion.getRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="get", WrapperNamespace="http://integradores.cfdi.mx.konesh.com", IsWrapped=true)]
    public partial class getRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://integradores.cfdi.mx.konesh.com", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string cad;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://integradores.cfdi.mx.konesh.com", Order=1)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string tk;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://integradores.cfdi.mx.konesh.com", Order=2)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string user;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://integradores.cfdi.mx.konesh.com", Order=3)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string pass;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://integradores.cfdi.mx.konesh.com", Order=4)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string cuenta;
        
        public getRequest() {
        }
        
        public getRequest(string cad, string tk, string user, string pass, string cuenta) {
            this.cad = cad;
            this.tk = tk;
            this.user = user;
            this.pass = pass;
            this.cuenta = cuenta;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="getResponse", WrapperNamespace="http://integradores.cfdi.mx.konesh.com", IsWrapped=true)]
    public partial class getResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://integradores.cfdi.mx.konesh.com", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string @return;
        
        public getResponse() {
        }
        
        public getResponse(string @return) {
            this.@return = @return;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface TimbradorIntegradoresPortTypeChannel : SiscomCFDI.cfdinova_produccion.TimbradorIntegradoresPortType, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class TimbradorIntegradoresPortTypeClient : System.ServiceModel.ClientBase<SiscomCFDI.cfdinova_produccion.TimbradorIntegradoresPortType>, SiscomCFDI.cfdinova_produccion.TimbradorIntegradoresPortType {
        
        public TimbradorIntegradoresPortTypeClient() {
        }
        
        public TimbradorIntegradoresPortTypeClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public TimbradorIntegradoresPortTypeClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TimbradorIntegradoresPortTypeClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TimbradorIntegradoresPortTypeClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        SiscomCFDI.cfdinova_produccion.getResponse SiscomCFDI.cfdinova_produccion.TimbradorIntegradoresPortType.get(SiscomCFDI.cfdinova_produccion.getRequest request) {
            return base.Channel.get(request);
        }
        
        public string get(string cad, string tk, string user, string pass, string cuenta) {
            SiscomCFDI.cfdinova_produccion.getRequest inValue = new SiscomCFDI.cfdinova_produccion.getRequest();
            inValue.cad = cad;
            inValue.tk = tk;
            inValue.user = user;
            inValue.pass = pass;
            inValue.cuenta = cuenta;
            SiscomCFDI.cfdinova_produccion.getResponse retVal = ((SiscomCFDI.cfdinova_produccion.TimbradorIntegradoresPortType)(this)).get(inValue);
            return retVal.@return;
        }
    }
}
