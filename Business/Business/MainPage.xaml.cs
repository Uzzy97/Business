using Business.Data;
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

            entries.ItemTapped += OnItemTapped;
            newEntry.Completed += OnAddNewEntry;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            entries.ItemsSource = await App.Entries.GetAllAsync();
        }

        private async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = e.Item as NoteTake;
            await Navigation.PushAsync(new NoteEntryEditPage(item));
        }


        private async void OnAddNewEntry(object sender, EventArgs e)
        {
            string text = newEntry.Text;
            if (!string.IsNullOrWhiteSpace(text))
            {
                var item = new NoteTake { Title = text };
                await App.Entries.AddAsync(item);
                await Navigation.PushAsync(new NoteEntryEditPage(item));
                newEntry.Text = string.Empty;
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Calculator());

        }

    }
}

        

       
