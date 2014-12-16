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


namespace Address_Book
{
    /// <summary>
    /// This page is responsible for displaying the list of contacts when the page loads.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private HttpClient httpClient;
        private HttpResponseMessage response;
        private int numberOfResults = 25; //Set the number of contacts to load.
        RootObject root = new RootObject(); //For now, we will use one instance of RootObject.

        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;

            httpClient = new HttpClient(); //Create a new HttpClient which is the mechanism used to connect to the web.

            // Add a user-agent header
            var headers = httpClient.DefaultRequestHeaders;
            headers.UserAgent.ParseAdd("ie");
            headers.UserAgent.ParseAdd("Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)");

        }

        /// <summary>
        /// When this page is navigated to, pull the user data from the web.
        /// </summary>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            LoadContacts();
        }

        private async void LoadContacts()
        {
            response = new HttpResponseMessage();

            string jsonResult;
            //Add the number of results selected above, and convert to a url
            string numResults = numberOfResults.ToString();
            string randomUserUrl = "http://api.randomuser.me/?results={0}";
            string randomuserUrlLink = string.Format(randomUserUrl, numberOfResults);

            Uri url = new Uri(randomuserUrlLink, UriKind.Absolute);

            //Send the request asynchronously to fetch the user data, and catch an exception if it fails.
            try
            {
                response = await httpClient.GetAsync(url);

                response.EnsureSuccessStatusCode();

                jsonResult = await response.Content.ReadAsStringAsync();

            }
            catch (Exception ex)
            {
                //TODO: Implement a better solution to ensure the program does not exit. Warn the user the data fetch failed.
                jsonResult = "";
            }

            PopulateList(jsonResult); 

        }

        /// <summary>
        /// Bind the list named in MainPage.xaml to the data retrieved and saved in the ParseData.cs model.
        /// </summary>
        private void PopulateList(string json)
        {
            root = JsonConvert.DeserializeObject<RootObject>(json);

            List<Result> contactsList = root.results;
            contacts.DataContext = contactsList;
        }


        /// <summary>
        /// If the user selects a contact, open the contact details page, and send the "Result"(contact) selected to the page.
        /// </summary>
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
