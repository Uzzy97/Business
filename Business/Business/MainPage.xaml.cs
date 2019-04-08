using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Business
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            button.Clicked += Button_Clicked;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            string name = entry.Text;
            await DisplayAlert("Hi there", $"Hello {name}", "OK");
        }
    }
}
