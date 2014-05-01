<%@page import="java.sql.ResultSet"%>
<%@page import="java.sql.Statement"%>
<% /* @page import="java.util.ArrayList" */%>
<%@ page language="java" contentType="text/html; charset=ISO-8859-1"
    pageEncoding="ISO-8859-1"%>
<%@ page import="java.sql.Connection, java.sql.DriverManager" %>
<%
Connection conn = null;
try {
	 Class.forName("org.sqlite.JDBC");
	 conn = DriverManager.getConnection("jdbc:sqlite:C:\\JavaEE_Portable\\agenda.db");
	 
	 String campo = request.getParameter("p");
	 String valor = request.getParameter("texto"); 
	 if ("t".equals(campo)) campo = "telefono";
	 else if ("e".equals(campo)) campo = "email";
	 else campo = "nombre";
	 if (valor == null) valor = "";
	 
	String json = "";
	Statement sql = conn.createStatement();
	ResultSet rs = sql.executeQuery("select * from agenda where " +
	                                 campo + " like '%" + valor + "%' " +
			                         "order by nombre");
	while (rs.next()) {
		if (json.length() > 0)
			json += ", ";
		json += "{\"nombre\" : \"" + rs.getString(1) + "\"," +
			    " \"telefono\" : \"" + rs.getString(2) + "\", " +
			    " \"email\" : \"" + rs.getString(3) + "\"}";
		
	}
	json = "{\"agenda\" : [" + json + "]}";
	out.print(json);
} catch (Exception e) {
	out.println(e.getMessage());
}
%>