package ort.t5;

import javax.jws.WebMethod;
import javax.jws.WebService;
import javax.jws.soap.SOAPBinding;
import javax.jws.soap.SOAPBinding.Style;

@WebService
@SOAPBinding(style = Style.RPC)
public interface Contactos {
	@WebMethod String[] listar();
	@WebMethod boolean agregar(String nombre);
}
