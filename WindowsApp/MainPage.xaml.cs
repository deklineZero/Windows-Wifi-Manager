using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace WindowsApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        protected void displayStatus()
        {

        }

        protected string getStatus()
        {
            var profiles = NetShWrapper.GetWifiProfiles();
            bool sawBadWifi = false;
            foreach (var a in profiles)
            {
                string warning = NetShWrapper.IsOpenAndAutoWifiProfile(a) ? "Warning: AUTO connect to OPEN WiFi" : String.Empty;
                Console.WriteLine(String.Format("{0,-20} {1,10} {2,10} {3,30} ", a.Name, a.ConnectionMode, a.Authentication, warning));
                if (!String.IsNullOrWhiteSpace(warning)) sawBadWifi = true;
            }
            if (sawBadWifi)
            {
                Console.WriteLine("\r\nDelete WiFi profiles that are OPEN *and* AUTO connect? [y/n]");
                if (args[0].ToUpperInvariant() == "/DELETEAUTOOPEN" || Console.ReadLine().Trim().ToUpperInvariant()[0] == 'Y')
                {
                    Console.WriteLine("in here");
                    foreach (var a in profiles.Where(a => NetShWrapper.IsOpenAndAutoWifiProfile(a)))
                    {
                        Console.WriteLine(NetShWrapper.DeleteWifiProfile(a));
                    }
                }
            }
            else
            {
                Console.WriteLine("\r\nNo WiFi profiles set to OPEN and AUTO connect were found. \r\nOption: Run with /deleteautoopen to auto delete.");
            }
        }
    }
}
