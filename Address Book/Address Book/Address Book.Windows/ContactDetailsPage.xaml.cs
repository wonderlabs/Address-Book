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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace Address_Book
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ContactDetailsPage : Page
    {
        public ContactDetailsPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var result = e.Parameter as Result;
            if(result != null)
            {
                PopulateContactDetails(result);
            }
        }
        private void PopulateContactDetails(Result result)
        {
            //Populate the field data
            var usr = result.user;
            var usrName = usr.name;

            Title.Text = usrName.title;
            First.Text = usrName.first;
            Last.Text = usrName.last;

            var usrLocation = usr.location;

            Street.Text = usrLocation.street;
            City.Text = usrLocation.city;
            State.Text = usrLocation.state;
            Zip.Text = usrLocation.zip;

            //set profile image
            ProfilePic.Source = new BitmapImage(new Uri(usr.picture.medium));

            Email.Text = usr.email;
            Username.Text = usr.username;
            Password.Text = usr.password;

            Gender.Text = usr.gender;
            Dob.Text = usr.dob;
            Hphone.Text = usr.phone;
            Cphone.Text = usr.cell;
        }

        private void BackClicked(object sender, RoutedEventArgs e)
        {
            Frame frame = Window.Current.Content as Frame;
            if (frame == null)
            {
                return;
            }

            if (frame.CanGoBack)
            {
                frame.GoBack();
            }
        }

    }
}
