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
        private ObservableCollection<News> _Selectednews;
        private int PageIndex = 1;
        private int itemCount = 6;
        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            activityIndicator.IsVisible = true;
            activityIndicator.IsRunning = true;

            var content = await _client.GetStringAsync(Url + "?Page="+PageIndex.ToString()+"&PageCount="+itemCount.ToString());
            var news = JsonConvert.DeserializeObject<List<News>>(content);
            _Selectednews = new ObservableCollection<News>(news.OrderByDescending(q=>Convert.ToDateTime(q.fldRegisterDate)));

            lvMain.ItemsSource = _Selectednews;

            Device.StartTimer(TimeSpan.FromSeconds(4), () =>
            {
                Task.Factory.StartNew(() =>
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Random rand = new Random();
                        int toSkip = rand.Next(0, 6);
                        imgCarousel.Source = _Selectednews.Skip(toSkip).Take(1).FirstOrDefault().ImageUrl;
                    });
                });
                return true;
            });

            activityIndicator.IsVisible = false;
            activityIndicator.IsRunning = false;
        }

        private async void lvMain_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            if (e.Item!=null && (e.Item as News).tbNewsID.ToString()== _Selectednews[_Selectednews.Count-1].tbNewsID.ToString())
            {
                activityIndicator.IsVisible = true;
                activityIndicator.IsRunning = true;

                PageIndex++;
                var content = await _client.GetStringAsync(Url + "?Page=" + PageIndex.ToString() + "&PageCount=" + itemCount.ToString());
                var _newItems = JsonConvert.DeserializeObject<List<News>>(content);
                foreach(News item in _newItems)
                {
                    _Selectednews.Add(item);
                }
                activityIndicator.IsVisible = false;
                activityIndicator.IsRunning = false;
            }
        }
    }
}
