using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Maps.Toolkit;
using System.Device.Location;
using Microsoft.Phone.Maps;
using System.Windows.Media;
using System.Text.RegularExpressions;

namespace ORT.Mobile
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;
        }

        private void Map_Loaded(object sender, RoutedEventArgs e)
        {
            MapsSettings.ApplicationContext.ApplicationId = "<applicationid>";
            MapsSettings.ApplicationContext.AuthenticationToken = "<authenticationtoken>";
        }

        // Load data for the ViewModel Items
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            dynamic form = new 
            {
                Nombre = this.Nombre.Text,
                Apellido = this.Apellido.Text,
                Mail = this.Mail.Text,
                UstedEs = this.UstedEs.SelectedItem.ToString(),
                TieneInteres = this.TieneInteres.SelectedItem.ToString(),
                Mensaje = this.Mensaje.Text
            };

            if (form.Nombre == String.Empty)
            {
                GeneralTransform transform = this.Nombre.TransformToVisual(this.ConsultasSV);
                Point textBoxPosition = transform.Transform(new Point(0, 0));
                this.ConsultasSV.ScrollToVerticalOffset(textBoxPosition.Y);
                this.Nombre.Focus();

                MessageBox.Show("Por favor, ingrese su nombre.");
                return;
            }

            if (form.Apellido == String.Empty)
            {
                GeneralTransform transform = this.Apellido.TransformToVisual(this.ConsultasSV);
                Point textBoxPosition = transform.Transform(new Point(0, 0));
                this.ConsultasSV.ScrollToVerticalOffset(textBoxPosition.Y);
                this.Apellido.Focus();

                MessageBox.Show("Por favor, ingrese su apellido.");
                return;
            }

            if (form.Mail == String.Empty)
            {
                GeneralTransform transform = this.Mail.TransformToVisual(this.ConsultasSV);
                Point textBoxPosition = transform.Transform(new Point(0, 0));
                this.ConsultasSV.ScrollToVerticalOffset(textBoxPosition.Y);
                this.Mail.Focus();

                MessageBox.Show("Por favor, ingrese su mail.");
                return;
            }

            bool isEmail = Regex.IsMatch(form.Mail, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z");

            if (!isEmail)
            {
                GeneralTransform transform = this.Mail.TransformToVisual(this.ConsultasSV);
                Point textBoxPosition = transform.Transform(new Point(0, 0));
                this.ConsultasSV.ScrollToVerticalOffset(textBoxPosition.Y);
                this.Mail.Focus();

                MessageBox.Show("Por favor, ingrese un mail válido.");
                return;

            }

            if (form.Mensaje == String.Empty)
            {
                GeneralTransform transform = this.Mensaje.TransformToVisual(this.ConsultasSV);
                Point textBoxPosition = transform.Transform(new Point(0, 0));
                this.ConsultasSV.ScrollToVerticalOffset(textBoxPosition.Y);
                this.Mensaje.Focus();

                MessageBox.Show("Por favor, ingrese un mensaje.");
                return;
            }

            //Send form to service

            MessageBox.Show("El formulario ha sido enviado con éxito!");

            this.Nombre.Text = String.Empty;
            this.Apellido.Text = String.Empty;
            this.Mail.Text = String.Empty;
            this.UstedEs.SelectedIndex = 0;
            this.TieneInteres.SelectedIndex = 0;
            this.Mensaje.Text = String.Empty;

            this.ConsultasSV.ScrollToVerticalOffset(0);
        }

        private void Carrera_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/Carrera.xaml?buttonName=" + ((Button)sender).Name + "&description=" + ((Button)sender).Content, UriKind.Relative));
        }
    }
}