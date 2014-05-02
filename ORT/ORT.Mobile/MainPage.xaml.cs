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

namespace ORT
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;

            SetPushPinsLocation();
        }

        private void SetPushPinsLocation()
        {
            Pushpin almagro = (Pushpin)this.FindName("Almagro");
            almagro.GeoCoordinate = new GeoCoordinate(-34.6099167, -58.4291801);

            Pushpin belgrano = (Pushpin)this.FindName("Belgrano");
            belgrano.GeoCoordinate = new GeoCoordinate(-34.5496559, -58.4540443);
        }

        // Load data for the ViewModel Items
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
        }
    }
}