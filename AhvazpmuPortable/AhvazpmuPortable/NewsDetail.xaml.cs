using AhvazpmuPortable.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AhvazpmuPortable
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewsDetail : ContentPage
    {
        private News _news;
        private const string Url = "http://api.ahvazpmu.ir/newsdetails";
        private HttpClient _client = new HttpClient();

        public NewsDetail(News news)
        {
            _news = news;            
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            activityIndicator.IsVisible = true;
            activityIndicator.IsRunning = true;

            var content = await _client.GetStringAsync(Url + "/"+_news.tbNewsID.ToString());
            var newsDetails = JsonConvert.DeserializeObject<NewsDetails>(content);

            this.BindingContext = newsDetails;
            

            activityIndicator.IsVisible = false;
            activityIndicator.IsRunning = false;

        }
    }
}