using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
//My added libraries
using Windows.Web.Http;
using Newtonsoft.Json;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Address_Book
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private HttpClient httpClient;
        private HttpResponseMessage response;
        private int numberOfResults = 25; //Set the number of contacts to load.
        RootObject root = new RootObject();

        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;

            httpClient = new HttpClient();

            // Add a user-agent header
            var headers = httpClient.DefaultRequestHeaders;
            headers.UserAgent.ParseAdd("ie");
            headers.UserAgent.ParseAdd("Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)");

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            LoadContacts();
        }

        private async void LoadContacts()
        {
            response = new HttpResponseMessage();

            string jsonResult;
            string numResults = numberOfResults.ToString(); //set the number of results you want
            string randomUserUrl = "http://api.randomuser.me/?results={0}";
            string randomuserUrlLink = string.Format(randomUserUrl, numberOfResults);

            Uri url = new Uri(randomuserUrlLink, UriKind.Absolute);


            try
            {
                response = await httpClient.GetAsync(url);

                response.EnsureSuccessStatusCode();

                jsonResult = await response.Content.ReadAsStringAsync();

            }
            catch (Exception ex)
            {

                jsonResult = "";
            }

            PopulateList(jsonResult);

        }

        private void PopulateList(string json)
        {
            root = JsonConvert.DeserializeObject<RootObject>(json);

            List<Result> contactsList = root.results;
            contacts.DataContext = contactsList;
        }



        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Add code to perform some action here.

            ListBox selection = sender as ListBox;
            if (selection != null)
            {
                var result = selection.SelectedItem;
                var selectedResult = result as Result;
                if(selectedResult != null)
                {
                    //push new page displaying contact information, pass in Result
                    this.Frame.Navigate(typeof(ContactDetailsPage), selectedResult);
                }
                
            }

        }
    }
}
