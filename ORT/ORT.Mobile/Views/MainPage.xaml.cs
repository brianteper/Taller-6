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
using System.Windows.Media.Imaging;
using System.IO;
using Microsoft.Phone.Tasks;

namespace ORT.Mobile.Views
{
    public partial class MainPage : PhoneApplicationPage
    {
        private string ServiceBaseUrl = "http://futurasoft.com.ar/ORTWebApi/api/";

        public class Evento
        {
            public int ID { get; set; }
            public string Fecha { get; set; }
            public string Titulo { get; set; }
            public string Descripcion { get; set; }
        }

        public class Foto
        {
            public BitmapImage Imagen { get; set; }
        }
        
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            //GetRequest("photos", photos_DownloadStringCompleted);
            GetRequest("agenda", agenda_DownloadStringCompleted);
        }

        private void GetRequest(string path, DownloadStringCompletedEventHandler method)
        {
            try
            {
                Uri uri = new Uri(ServiceBaseUrl + path, UriKind.Absolute);

                WebClient webClient = new WebClient();
                webClient.DownloadStringCompleted += method;
                webClient.DownloadStringAsync(uri);
            }
            catch (Exception ex)
            {
                
            }
        }

        //private void photos_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        //{
        //    if (e.Error == null)
        //    {
        //        dynamic photos = JsonConvert.DeserializeObject(e.Result);

        //        var list = new List<Foto>();
        //        foreach (var photo in photos)
        //        {
        //            list.Add(new Foto() { Imagen = Base64Image(Convert.ToString(photo.Imagen)) });
        //        }

        //        Photos.ItemsSource = list;
        //    }
        //}

        private void agenda_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                var agenda = JsonConvert.DeserializeObject<List<Evento>>(e.Result);

                Agenda.ItemsSource = agenda;
            }
        }

        public static BitmapImage Base64Image(string base64string)
        {
            byte[] fileBytes = Convert.FromBase64String(base64string);

            using (MemoryStream ms = new MemoryStream(fileBytes, 0, fileBytes.Length))
            {
                ms.Write(fileBytes, 0, fileBytes.Length);
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.SetSource(ms);
                return bitmapImage;
            }
        }

        private void Map_Loaded(object sender, RoutedEventArgs e)
        {
            MapsSettings.ApplicationContext.ApplicationId = "73727035-2266-4818-bbc7-c59aaa4a1f94";
            MapsSettings.ApplicationContext.AuthenticationToken = "pY5Q7xjtgWEYcCcelb3ZIA";
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

                HideTray();

                MessageBox.Show("El formulario ha sido enviado con éxito!");
            }
            else
            {
                HideTray();

                MessageBox.Show("No se ha podido enviar su consulta. Por favor vuelva a intentarlo.");
            }
        }

        private void Carrera_Click(object sender, RoutedEventArgs e)
        {
            if (((Button)sender).Content is System.Windows.Controls.TextBlock)
            {
                NavigationService.Navigate(new Uri("/Views/Carrera.xaml?buttonName=" + ((Button)sender).Name + "&description=" + ((TextBlock)((Button)sender).Content).Text, UriKind.Relative));
            }
            else
            {
                NavigationService.Navigate(new Uri("/Views/Carrera.xaml?buttonName=" + ((Button)sender).Name + "&description=" + ((Button)sender).Content, UriKind.Relative));
            }
        }

        private void FacebookLink_Click(object sender, RoutedEventArgs e)
        {
            WebBrowserTask webBrowserTask = new WebBrowserTask();
            webBrowserTask.Uri = new Uri("http://m.facebook.com/isedeyatay");
            webBrowserTask.Show();
        }
    }
}