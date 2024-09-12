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
            InitializeComponent();
        }

        // Event handler for button click
        private async void FetchLocation_Click(object sender, RoutedEventArgs e)
        {
            // Get API key and IP address from TextBoxes
            string apiKey = ApiKeyTextBox.Text;
            string ipAddress = IpAddressTextBox.Text;

            // Check if the input fields are empty or have placeholder text
            if (string.IsNullOrEmpty(apiKey) || apiKey == "Enter API Key" ||
                string.IsNullOrEmpty(ipAddress) || ipAddress == "Enter IP Address")
            {
                MessageBox.Show("Please enter both API key and IP address.");
                return;
            }

            // Call the API to fetch location data
            string locationData = await FetchLocationData(apiKey, ipAddress);

            // Display the result or error message
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
                    // Make the API request
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    // Get response data as a string
                    string responseData = await response.Content.ReadAsStringAsync();
                    return responseData;
                }
                catch (HttpRequestException e)
                {
                    // Handle any errors with the request
                    MessageBox.Show("Request error: " + e.Message);
                    return null;
                }
            }
        }

        // Parse and display location data from the API response
        private void DisplayLocationData(string jsonData)
        {
            // Parse the JSON response
            JObject data = JObject.Parse(jsonData);

            // Access and display relevant data
            string ip = data["ip"].ToString();
            string country = data["country_name"].ToString();
            string city = data["city"].ToString();
            string latitude = data["latitude"].ToString();
            string longitude = data["longitude"].ToString();

            // Display the data in a MessageBox (you can change this to update the UI directly)
            MessageBox.Show($"IP: {ip}\nCountry: {country}\nCity: {city}\nLatitude: {latitude}\nLongitude: {longitude}");
        }

        // Placeholder text logic for when the TextBox gains focus
        private void RemoveText(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && (textBox.Text == "Enter API Key" || textBox.Text == "Enter IP Address"))
            {
                textBox.Text = "";
            }
        }

        // Placeholder text logic for when the TextBox loses focus
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
