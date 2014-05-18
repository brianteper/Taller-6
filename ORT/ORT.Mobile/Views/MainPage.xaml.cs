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
using Venetasoft.WP.Net;
using System.Text;
using Newtonsoft.Json;

namespace ORT.Mobile
{
    public partial class MainPage : PhoneApplicationPage
    {
        private string ServiceBaseUrl = "http://10.0.1.20:1297/api/";
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
            SendMail(form);
        }

        private void SendMail(dynamic form)
        {
            try
            {
                Microsoft.Phone.Shell.SystemTray.IsVisible = true;
                
                var progressIndicator = Microsoft.Phone.Shell.SystemTray.ProgressIndicator;
                progressIndicator.Text = "Enviando consulta...";
                progressIndicator.IsVisible = true;

                string jsonData = JsonConvert.SerializeObject(form);

                WebClient webClient = new WebClient();
                webClient.Headers["Content-type"] = "application/json";
                webClient.Encoding = Encoding.UTF8;

                Uri uri = new Uri(ServiceBaseUrl + "mail/sendmail", UriKind.Absolute);
                webClient.UploadStringCompleted += new UploadStringCompletedEventHandler(webClient_UploadStringCompleted);
                webClient.UploadStringAsync(uri, "POST", jsonData);
            }
            catch (Exception ex)
            {
                HideTray();

                MessageBox.Show("No se ha podido enviar su consulta. Por favor vuelva a intentarlo.");
            }
        }

        private static void HideTray()
        {
            var progressIndicator = Microsoft.Phone.Shell.SystemTray.ProgressIndicator;
            progressIndicator.IsVisible = false;

            Microsoft.Phone.Shell.SystemTray.IsVisible = false;
        }

        private void webClient_UploadStringCompleted(object sender, UploadStringCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                this.Nombre.Text = String.Empty;
                this.Apellido.Text = String.Empty;
                this.Mail.Text = String.Empty;
                this.UstedEs.SelectedIndex = 0;
                this.TieneInteres.SelectedIndex = 0;
                this.Mensaje.Text = String.Empty;

                this.ConsultasSV.ScrollToVerticalOffset(0);

                MessageBox.Show("El formulario ha sido enviado con éxito!");
            }
            else
            {
                MessageBox.Show("No se ha podido enviar su consulta. Por favor vuelva a intentarlo.");
            }

            HideTray();
        }

        private void Carrera_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/Carrera.xaml?buttonName=" + ((Button)sender).Name + "&description=" + ((Button)sender).Content, UriKind.Relative));
        }
    }
}