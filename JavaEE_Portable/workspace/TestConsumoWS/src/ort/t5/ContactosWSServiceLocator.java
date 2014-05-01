/**
 * ContactosWSServiceLocator.java
 *
 * This file was auto-generated from WSDL
 * by the Apache Axis 1.4 Apr 22, 2006 (06:55:48 PDT) WSDL2Java emitter.
 */

package ort.t5;

public class ContactosWSServiceLocator extends org.apache.axis.client.Service implements ort.t5.ContactosWSService {

    public ContactosWSServiceLocator() {
    }


    public ContactosWSServiceLocator(org.apache.axis.EngineConfiguration config) {
        super(config);
    }

    public ContactosWSServiceLocator(java.lang.String wsdlLoc, javax.xml.namespace.QName sName) throws javax.xml.rpc.ServiceException {
        super(wsdlLoc, sName);
    }

    // Use to get a proxy class for ContactosWS
    private java.lang.String ContactosWS_address = "http://localhost:8080/TestDynWeb/services/ContactosWS";

    public java.lang.String getContactosWSAddress() {
        return ContactosWS_address;
    }

    // The WSDD service name defaults to the port name.
    private java.lang.String ContactosWSWSDDServiceName = "ContactosWS";

    public java.lang.String getContactosWSWSDDServiceName() {
        return ContactosWSWSDDServiceName;
    }

    public void setContactosWSWSDDServiceName(java.lang.String name) {
        ContactosWSWSDDServiceName = name;
    }

    public ort.t5.ContactosWS getContactosWS() throws javax.xml.rpc.ServiceException {
       java.net.URL endpoint;
        try {
            endpoint = new java.net.URL(ContactosWS_address);
        }
        catch (java.net.MalformedURLException e) {
            throw new javax.xml.rpc.ServiceException(e);
        }
        return getContactosWS(endpoint);
    }

    public ort.t5.ContactosWS getContactosWS(java.net.URL portAddress) throws javax.xml.rpc.ServiceException {
        try {
            ort.t5.ContactosWSSoapBindingStub _stub = new ort.t5.ContactosWSSoapBindingStub(portAddress, this);
            _stub.setPortName(getContactosWSWSDDServiceName());
            return _stub;
        }
        catch (org.apache.axis.AxisFault e) {
            return null;
        }
    }

    public void setContactosWSEndpointAddress(java.lang.String address) {
        ContactosWS_address = address;
    }

    /**
     * For the given interface, get the stub implementation.
     * If this service has no port for the given interface,
     * then ServiceException is thrown.
     */
    public java.rmi.Remote getPort(Class serviceEndpointInterface) throws javax.xml.rpc.ServiceException {
        try {
            if (ort.t5.ContactosWS.class.isAssignableFrom(serviceEndpointInterface)) {
                ort.t5.ContactosWSSoapBindingStub _stub = new ort.t5.ContactosWSSoapBindingStub(new java.net.URL(ContactosWS_address), this);
                _stub.setPortName(getContactosWSWSDDServiceName());
                return _stub;
            }
        }
        catch (java.lang.Throwable t) {
            throw new javax.xml.rpc.ServiceException(t);
        }
        throw new javax.xml.rpc.ServiceException("There is no stub implementation for the interface:  " + (serviceEndpointInterface == null ? "null" : serviceEndpointInterface.getName()));
    }

    /**
     * For the given interface, get the stub implementation.
     * If this service has no port for the given interface,
     * then ServiceException is thrown.
     */
    public java.rmi.Remote getPort(javax.xml.namespace.QName portName, Class serviceEndpointInterface) throws javax.xml.rpc.ServiceException {
        if (portName == null) {
            return getPort(serviceEndpointInterface);
        }
        java.lang.String inputPortName = portName.getLocalPart();
        if ("ContactosWS".equals(inputPortName)) {
            return getContactosWS();
        }
        else  {
            java.rmi.Remote _stub = getPort(serviceEndpointInterface);
            ((org.apache.axis.client.Stub) _stub).setPortName(portName);
            return _stub;
        }
    }

    public javax.xml.namespace.QName getServiceName() {
        return new javax.xml.namespace.QName("http://t5.ort", "ContactosWSService");
    }

    private java.util.HashSet ports = null;

    public java.util.Iterator getPorts() {
        if (ports == null) {
            ports = new java.util.HashSet();
            ports.add(new javax.xml.namespace.QName("http://t5.ort", "ContactosWS"));
        }
        return ports.iterator();
    }

    /**
    * Set the endpoint address for the specified port name.
    */
    public void setEndpointAddress(java.lang.String portName, java.lang.String address) throws javax.xml.rpc.ServiceException {
        
if ("ContactosWS".equals(portName)) {
            setContactosWSEndpointAddress(address);
        }
        else 
{ // Unknown Port Name
            throw new javax.xml.rpc.ServiceException(" Cannot set Endpoint Address for Unknown Port" + portName);
        }
    }

    /**
    * Set the endpoint address for the specified port name.
    */
    public void setEndpointAddress(javax.xml.namespace.QName portName, java.lang.String address) throws javax.xml.rpc.ServiceException {
        setEndpointAddress(portName.getLocalPart(), address);
    }

}
