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
    /// A page used to display the contact details of a selected user.
    /// Once the user selects a contact, they will be directed to this page.
    /// </summary>
    public sealed partial class ContactDetailsPage : Page
    {
        public ContactDetailsPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// When this page is first opened, the paramater holding the Result( or user data)
        /// is passed in and read.
        /// </summary>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var result = e.Parameter as Result;
            if(result != null)
            {
                PopulateContactDetails(result);
            }
        }

        /// <summary>
        /// This function will populate all the contact details into each field 
        /// created in the corresponding ContactDetailsPage.xaml file.
        /// </summary>
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

        /// <summary>
        /// When the back button is clicked, this will navigate the user to the previous page.
        /// </summary>
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
