package ort.t5;

import java.awt.BorderLayout;
import java.awt.Color;
import java.awt.Dimension;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.WindowAdapter;
import java.awt.event.WindowEvent;

import javax.swing.JButton;
import javax.swing.JFrame;
import javax.swing.JList;
import javax.swing.JOptionPane;
import javax.swing.JPanel;
import javax.swing.JTextField;
import javax.swing.border.LineBorder;

public class Ventana extends JFrame {
	private static final long serialVersionUID = 1L;
	private JList<String> listado;
	private JTextField nombre;
	
	public Ventana() {
		super("Consumidor de WS");
		setSize(400, 240);
		setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		getContentPane().setLayout(new BorderLayout());
		
		listado = new JList<String>();
		getContentPane().add(listado, BorderLayout.CENTER);
		
		JPanel panel = new JPanel();
		panel.setPreferredSize(new Dimension(0, 40));
		panel.setBackground(Color.DARK_GRAY);
		panel.setBorder(new LineBorder(Color.DARK_GRAY, 8));
		panel.setLayout(new BorderLayout());
		getContentPane().add(panel, BorderLayout.SOUTH);
	
		nombre = new JTextField();
		panel.add(nombre, BorderLayout.CENTER);
		
		JButton boton = new JButton("Agregar");
		panel.add(boton, BorderLayout.EAST);
		boton.setPreferredSize(new Dimension(100, 0));
		boton.addActionListener(new ActionListener() {
			@Override
			public void actionPerformed(ActionEvent arg0) {
				agregarContacto();
			}
		});

		addWindowListener(new WindowAdapter() {
			@Override
			public void windowOpened(WindowEvent arg0) {
				actualizarContactos();
			}

		});
	}
	
	private void agregarContacto() {
		ContactosWSServiceLocator locator = new ContactosWSServiceLocator();
		try {
			ContactosWS ws = locator.getContactosWS();
			ws.agregar(nombre.getText());
			actualizarContactos();
		} catch (Exception e) {
			JOptionPane.showMessageDialog(this,  e.getMessage());
			e.printStackTrace();
		}
	}
	
	private void actualizarContactos() {
		ContactosWSServiceLocator locator = new ContactosWSServiceLocator();
		try {
			ContactosWS ws = locator.getContactosWS();
			listado.setListData(ws.listar());
		} catch (Exception e) {
			JOptionPane.showMessageDialog(this,  e.getMessage());
			e.printStackTrace();
		}
	}

	public static void main(String[] args) {
		Ventana v = new Ventana();
		v.setVisible(true);
	}

}
