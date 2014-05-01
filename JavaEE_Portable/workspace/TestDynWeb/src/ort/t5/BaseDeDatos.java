package ort.t5;

import java.sql.Connection;
import java.sql.DriverManager;

public final class BaseDeDatos {
	public static Connection getConnection() {
		Connection conn = null;
		try {
			 Class.forName("org.sqlite.JDBC");
			 conn = DriverManager.getConnection("jdbc:sqlite:C:\\JavaEE_Portable\\TestDynWeb.db");
		} catch (Exception e) {
		}
		return conn;
	}
}
