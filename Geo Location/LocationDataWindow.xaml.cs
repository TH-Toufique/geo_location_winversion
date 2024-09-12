using System.Windows;

namespace GeoLocApp
{
    public partial class LocationDataWindow : Window
    {
        public LocationDataWindow(string ip, string hostname, string continent, string country, string state, string district,
                                  string city, string zipcode, string latitude, string longitude, string isEU,
                                  string callingCode, string countryTLD, string languages, string isp,
                                  string organization, string asn, string currency, string timeZone,
                                  string currentTime, string isDST, string dstSavings)
        {
            InitializeComponent();

            IpTextBlock.Text = $"IP: {ip}";
            HostnameTextBlock.Text = $"Hostname: {hostname}";
            ContinentTextBlock.Text = $"Continent: {continent}";
            CountryTextBlock.Text = $"Country: {country}";
            StateTextBlock.Text = $"State/Province: {state}";
            DistrictTextBlock.Text = $"District: {district}";
            CityTextBlock.Text = $"City: {city}";
            ZipcodeTextBlock.Text = $"Zipcode: {zipcode}";
            LatitudeTextBlock.Text = $"Latitude: {latitude}";
            LongitudeTextBlock.Text = $"Longitude: {longitude}";
            IsEUTextBlock.Text = $"Is EU: {isEU}";
            CallingCodeTextBlock.Text = $"Calling Code: {callingCode}";
            CountryTLDTextBlock.Text = $"Country TLD: {countryTLD}";
            LanguagesTextBlock.Text = $"Languages: {languages}";
            ISPTextBlock.Text = $"ISP: {isp}";
            OrganizationTextBlock.Text = $"Organization: {organization}";
            ASNTextBlock.Text = $"ASN: {asn}";
            CurrencyTextBlock.Text = $"Currency: {currency}";
            TimeZoneTextBlock.Text = $"Time Zone: {timeZone}";
            CurrentTimeTextBlock.Text = $"Current Time: {currentTime}";
            IsDSTTextBlock.Text = $"Is DST: {isDST}";
            DSTSavingsTextBlock.Text = $"DST Savings: {dstSavings}";
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
