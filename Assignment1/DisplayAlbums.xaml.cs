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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Assignment1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DisplayAlbums : Page
    {
        public DisplayAlbums()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
           // Frame.Content = null;
            //navigate to this page based on buttons (button1 and button2) click 

            var albumList = e.Parameter as IList<Album>;
            this.DataContext = albumList;
        }

        private void Albums_ItemClick(object sender, ItemClickEventArgs e)
        {
            //get the clicked item (ie is album) and convert that into item of type Album
            //album is the instance of type album that was clicked on in a gridview
            var album = (Album)e.ClickedItem;

            this.Frame.Navigate(typeof(DisPlaySongs), album.SongsCollection);
        }

        private bool startedHolding = false;
        private void Grid_Holding(object sender, HoldingRoutedEventArgs e)
        {
            // simple checkup for holding release for this sample, though this probalby need better handling
            startedHolding = !startedHolding;
            if (startedHolding)
            {
                MenuFlyout myFlyout = new MenuFlyout();
                MenuFlyoutItem firstItem = new MenuFlyoutItem { Text = "Play" };
                MenuFlyoutSubItem subItem = new MenuFlyoutSubItem { Text = "Add To" };

                MenuFlyoutItem item1 = new MenuFlyoutItem { Text = "Test" };
                MenuFlyoutItem item2 = new MenuFlyoutItem { Text = "Sri" };
                subItem.Items.Add(item1);
                subItem.Items.Add(item2);
                myFlyout.Items.Add(firstItem);
                myFlyout.Items.Add(subItem);
                FrameworkElement senderElement = sender as FrameworkElement;
                myFlyout.ShowAt(senderElement);
            }
        }
    }
}
