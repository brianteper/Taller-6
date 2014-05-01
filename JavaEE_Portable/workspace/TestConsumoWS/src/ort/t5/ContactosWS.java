/**
 * ContactosWS.java
 *
 * This file was auto-generated from WSDL
 * by the Apache Axis 1.4 Apr 22, 2006 (06:55:48 PDT) WSDL2Java emitter.
 */

package ort.t5;

public interface ContactosWS extends java.rmi.Remote {
    public boolean agregar(java.lang.String nombre) throws java.rmi.RemoteException;
    public java.lang.String[] listar() throws java.rmi.RemoteException;
}
