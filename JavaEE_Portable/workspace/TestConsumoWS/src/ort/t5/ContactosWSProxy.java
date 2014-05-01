package ort.t5;

public class ContactosWSProxy implements ort.t5.ContactosWS {
  private String _endpoint = null;
  private ort.t5.ContactosWS contactosWS = null;
  
  public ContactosWSProxy() {
    _initContactosWSProxy();
  }
  
  public ContactosWSProxy(String endpoint) {
    _endpoint = endpoint;
    _initContactosWSProxy();
  }
  
  private void _initContactosWSProxy() {
    try {
      contactosWS = (new ort.t5.ContactosWSServiceLocator()).getContactosWS();
      if (contactosWS != null) {
        if (_endpoint != null)
          ((javax.xml.rpc.Stub)contactosWS)._setProperty("javax.xml.rpc.service.endpoint.address", _endpoint);
        else
          _endpoint = (String)((javax.xml.rpc.Stub)contactosWS)._getProperty("javax.xml.rpc.service.endpoint.address");
      }
      
    }
    catch (javax.xml.rpc.ServiceException serviceException) {}
  }
  
  public String getEndpoint() {
    return _endpoint;
  }
  
  public void setEndpoint(String endpoint) {
    _endpoint = endpoint;
    if (contactosWS != null)
      ((javax.xml.rpc.Stub)contactosWS)._setProperty("javax.xml.rpc.service.endpoint.address", _endpoint);
    
  }
  
  public ort.t5.ContactosWS getContactosWS() {
    if (contactosWS == null)
      _initContactosWSProxy();
    return contactosWS;
  }
  
  public boolean agregar(java.lang.String nombre) throws java.rmi.RemoteException{
    if (contactosWS == null)
      _initContactosWSProxy();
    return contactosWS.agregar(nombre);
  }
  
  public java.lang.String[] listar() throws java.rmi.RemoteException{
    if (contactosWS == null)
      _initContactosWSProxy();
    return contactosWS.listar();
  }
  
  
}