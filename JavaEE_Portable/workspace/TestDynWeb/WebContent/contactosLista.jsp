<%@page import="java.util.ArrayList"%>
<%@page import="ort.t5.ContactosWS"%>
<%@ page language="java" contentType="text/html; charset=ISO-8859-1"
	pageEncoding="ISO-8859-1"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<jsp:include page="page_header.jsp" />
</head>
<body>
<div class="content">
  <jsp:include page="page_menu.jsp" />
	<h1>Lista de Contactos</h1>
	<%
		ContactosWS contactos = new ContactosWS();
		String[] lista = contactos.listar();
		if (lista.length == 0)
			out.println("No hay contactos cargados");
		else
			for (String nombre : lista)
				out.println(nombre + "<br />");
	%>
</div>
<jsp:include page="page_footer.jsp" />
</body>
</html>