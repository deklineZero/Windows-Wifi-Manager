using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using NetSh;

namespace WpfApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        WiFiSecurityStatus wifiStatus;

        public MainWindow()
        {
            InitializeComponent();
            wifiStatus = new WiFiSecurityStatus();

            checkWifiStatus();
            displayWifiStatus();
        }

        private void checkWifiStatus()
        {
            lblStatus.Content = "Checking WiFi status";
            wifiStatus.CheckSecurityStatus();
        }

        private void displayWifiStatus()
        {          
            string status;

            if (wifiStatus.isSecure)
            {
                status = "Your saved WiFi connections are safe";
            }
            else
            {
                status = "These Wifi connections are unsafe\n";
                status += String.Format("\n{0,-20} {1,20} {2,20} ", "Name", "Connection", "Auth");
                foreach (var p in wifiStatus.openWifiList)
                    status += String.Format("\n{0,-20} {1,20} {2,20} ", p.Name, p.ConnectionMode, p.Authentication);
            }

            lblStatus.Content = status;
        }
    }
}
