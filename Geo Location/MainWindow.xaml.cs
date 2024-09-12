using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json.Linq;

namespace GeoLocApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();  // Ensure this method is called to initialize the components in the XAML
        }

        // Event handler for button click
        private async void FetchLocation_Click(object sender, RoutedEventArgs e)
        {
            // Get API key and IP address from TextBoxes
            string apiKey = ApiKeyTextBox.Text;
            string ipAddress = IpAddressTextBox.Text;

            // Validate inputs
            if (string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(ipAddress) || apiKey == "Enter API Key" || ipAddress == "Enter IP Address")
            {
                MessageBox.Show("Please enter both API key and IP address.");
                return;
            }

            // Fetch location data
            string locationData = await FetchLocationData(apiKey, ipAddress);

            if (locationData != null)
            {
                DisplayLocationData(locationData);
            }
            else
            {
                MessageBox.Show("Failed to fetch location data.");
            }
        }

        // Fetch location data from ipgeolocation.io API
        private async Task<string> FetchLocationData(string apiKey, string ip)
        {
            string url = $"https://api.ipgeolocation.io/ipgeo?apiKey={apiKey}&ip={ip}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadAsStringAsync();
                }
                catch (HttpRequestException e)
                {
                    MessageBox.Show("Request error: " + e.Message);
                    return null;
                }
            }
        }

        // Display all the detailed location data in a message box
        private void DisplayLocationData(string jsonData)
        {
            JObject data = JObject.Parse(jsonData);

            // Access all fields
            string ip = data["ip"].ToString();
            string hostname = data["hostname"]?.ToString() ?? "N/A";
            string continent = data["continent_name"]?.ToString() ?? "N/A";
            string country = $"{data["country_name"]} ({data["country_code3"]})";
            string state = data["state_prov"]?.ToString() ?? "N/A";
            string district = data["district"]?.ToString() ?? "N/A";
            string city = data["city"]?.ToString() ?? "N/A";
            string zipcode = data["zipcode"]?.ToString() ?? "N/A";
            string latitude = data["latitude"]?.ToString() ?? "N/A";
            string longitude = data["longitude"]?.ToString() ?? "N/A";
            string isEU = data["is_eu"]?.ToString() == "true" ? "Yes" : "No";
            string callingCode = data["calling_code"]?.ToString() ?? "N/A";
            string countryTLD = data["country_tld"]?.ToString() ?? "N/A";
            string languages = data["languages"]?.ToString() ?? "N/A";
            string isp = data["isp"]?.ToString() ?? "N/A";
            string organization = data["organization"]?.ToString() ?? "N/A";
            string asn = data["asn"]?.ToString() ?? "N/A";

            // Timezone details
            var timeZone = data["time_zone"];
            string timeZoneName = timeZone?["name"]?.ToString() ?? "N/A";
            string currentTime = timeZone?["current_time"]?.ToString() ?? "N/A";
            string isDST = timeZone?["is_dst"]?.ToString() == "true" ? "Yes" : "No";
            string dstSavings = timeZone?["dst_savings"]?.ToString() ?? "N/A";

            // Currency details
            var currency = data["currency"];
            string currencyName = currency?["name"]?.ToString() ?? "N/A";
            string currencyCode = currency?["code"]?.ToString() ?? "N/A";
            string currencySymbol = currency?["symbol"]?.ToString() ?? "N/A";

            // Display all the data in a message box
            string message = $"IP: {ip}\n" +
                             $"Hostname: {hostname}\n" +
                             $"Continent: {continent}\n" +
                             $"Country: {country}\n" +
                             $"State/Province: {state}\n" +
                             $"District: {district}\n" +
                             $"City: {city}\n" +
                             $"Zipcode: {zipcode}\n" +
                             $"Latitude: {latitude}\n" +
                             $"Longitude: {longitude}\n" +
                             $"Is EU: {isEU}\n" +
                             $"Calling Code: {callingCode}\n" +
                             $"Country TLD: {countryTLD}\n" +
                             $"Languages: {languages}\n" +
                             $"ISP: {isp}\n" +
                             $"Organization: {organization}\n" +
                             $"ASN: {asn}\n" +
                             $"Currency: {currencyName} ({currencyCode}) {currencySymbol}\n" +
                             $"Time Zone: {timeZoneName}\n" +
                             $"Current Time: {currentTime}\n" +
                             $"Is DST: {isDST}\n" +
                             $"DST Savings: {dstSavings} hour(s)";

            MessageBox.Show(message, "Location Data", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // Placeholder logic for textboxes
        private void RemoveText(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && (textBox.Text == "Enter API Key" || textBox.Text == "Enter IP Address"))
            {
                textBox.Text = "";
            }
        }

        private void AddText(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && string.IsNullOrWhiteSpace(textBox.Text))
            {
                if (textBox.Name == "ApiKeyTextBox")
                {
                    textBox.Text = "Enter API Key";
                }
                else if (textBox.Name == "IpAddressTextBox")
                {
                    textBox.Text = "Enter IP Address";
                }
            }
        }
    }
}
