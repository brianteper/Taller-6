using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace ORT.Mobile.Views
{
    public partial class Carrera : PhoneApplicationPage
    {
        public Carrera()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string description = "";
            if (NavigationContext.QueryString.TryGetValue("description", out description))
            {
                PageName.Text = description;
            }

            string buttonName = "";
            if (NavigationContext.QueryString.TryGetValue("buttonName", out buttonName))
            {
                var textBox = FindName(buttonName) as RichTextBox;

                if (textBox != null)
                {
                    textBox.Visibility = System.Windows.Visibility.Visible;
                }
            }
        }
    }
}