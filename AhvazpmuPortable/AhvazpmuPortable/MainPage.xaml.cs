using AhvazpmuPortable.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AhvazpmuPortable
{
    public partial class MainPage : ContentPage
    {
        private const string Url = "http://api.ahvazpmu.ir/news";
        private HttpClient _client = new HttpClient();
        private ObservableCollection<News> _news;
        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var content = await _client.GetStringAsync(Url);
            var news = JsonConvert.DeserializeObject<List<News>>(content);
            _news = new ObservableCollection<News>(news);

            lvMain.ItemsSource = _news.Skip(1).Take(10);

            Device.StartTimer(TimeSpan.FromSeconds(4), () =>
            {
                Task.Factory.StartNew(() =>
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Random rand = new Random();
                        int toSkip = rand.Next(0, 10);
                        imgCarousel.Source = _news.Skip(toSkip).Take(1).FirstOrDefault().ImageUrl;
                    });
                });
                return true;
            });
        }
    }
}
