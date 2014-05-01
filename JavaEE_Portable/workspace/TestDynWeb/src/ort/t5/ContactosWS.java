package ort.t5;

import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.ArrayList;

public class ContactosWS implements Contactos {

	@Override
	public String[] listar() {
		ArrayList<String> lista = new ArrayList<String>();
		try {
			Statement sql = BaseDeDatos.getConnection().createStatement();
			ResultSet rs = sql.executeQuery("select * from t5");
			while (rs.next()) {
				String s = rs.getString(1);
				lista.add(s);
			}
		} catch (SQLException e) {
			e.printStackTrace();
		}
		return lista.toArray(new String[lista.size()]);
	}

	@Override
	public boolean agregar(String nombre) {
		try {
			PreparedStatement sql = BaseDeDatos.getConnection().prepareStatement("insert into t5 values (?)");
			sql.setString(1, nombre);
			sql.execute();
			return true;
		} catch (SQLException e) {
			e.printStackTrace();
			return false;
		}
	}

}
