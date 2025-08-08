using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace WeatherClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private readonly Dictionary<int, string> weatherDescriptions = new Dictionary<int, string>
        {
            { 0, "Clear sky ☀️" },
            { 1, "Mainly clear 🌤️" },
            { 2, "Partly cloudy ⛅" },
            { 3, "Overcast ☁️" },
            { 45, "Fog 🌫️" },
            { 48, "Depositing rime fog 🌫️❄️" },
            { 51, "Light drizzle 🌦️" },
            { 53, "Moderate drizzle 🌧️" },
            { 55, "Dense drizzle 🌧️💧" },
            { 56, "Freezing light drizzle 🌧️❄️" },
            { 57, "Freezing dense drizzle 🌧️🧊" },
            { 61, "Slight rain 🌦️" },
            { 63, "Moderate rain 🌧️" },
            { 65, "Heavy rain 🌧️💦" },
            { 66, "Freezing light rain 🌧️❄️" },
            { 67, "Freezing heavy rain 🌧️🧊" },
            { 71, "Slight snow fall 🌨️" },
            { 73, "Moderate snow fall 🌨️❄️" },
            { 75, "Heavy snow fall ❄️🌨️" },
            { 77, "Snow grains ❄️🌾" },
            { 80, "Slight rain showers 🌦️" },
            { 81, "Moderate rain showers 🌧️☔" },
            { 82, "Violent rain showers 🌧️🌪️" },
            { 85, "Slight snow showers 🌨️" },
            { 86, "Heavy snow showers 🌨️❄️" },
            { 95, "Thunderstorm ⛈️" },
            { 96, "Thunderstorm with slight hail ⛈️🧊" },
            { 99, "Thunderstorm with heavy hail ⛈️🌩️🧊" }
        };

        public class WeatherData
        {
            public float temperature { get; set; }
            public int weather_code { get; set; }
            public float humidity { get; set; }
            public float precipitation { get; set; }
            public float windSpeed { get; set; }
        }
        private void txtCity_Enter(object sender, EventArgs e)
        {
            if (txtCity.Text == "Enter a city name..." && txtCity.ForeColor == Color.DimGray)
            {
                txtCity.Text = "";
                txtCity.ForeColor = Color.Black;
            }
        }

        private void txtCity_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCity.Text) && txtCity.ForeColor == Color.White)
            {
                txtCity.Text = "Enter a city name...";
                txtCity.ForeColor = Color.DimGray;
            }
        }
        private void btnGet_Click(object sender, EventArgs e)
        {
            getWeather();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            copyData();
        }

        private void txtCity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Prevent the ding sound
                getWeather();
            }
        }

        private void toggleFarenheit_CheckedChanged(object sender, EventArgs e)
        {
            if (lastWeatherData != null)
            {
                updateDisplay(lastWeatherData);
            }
        }

        private float cToF(float celcius)
        {
            return (celcius * 9 / 5) + 32;
        }

        private void updateDisplay(WeatherData data)
        {
            float temp = data.temperature;
            string unit = "°C";

            if (toggleFarenheit.Checked)
            {
                temp = cToF(temp);
                unit = "°F";
            }

            lblTemperature.Text = $"Temperature: {temp:F1}{unit}";
            lblHumidity.Text = $"💧 Humidity: {data.humidity}%";
            lblPrecipitation.Text = $"🌧️ Precipitation: {data.precipitation} mm";
            lblWindSpeed.Text = $"💨 Wind Speed: {data.windSpeed} km/h";

            weatherDescriptions.TryGetValue(data.weather_code, out string condition);
            lblCondition.Text = $"🌤️ Condition: {condition ?? "Unknown"}";
        }

        private void copyData()
        {
            if (lastWeatherData == null) return;

            string city = txtCity.Text;
            string text = $"Weather in {city}:\n" +
                           $"{lblTemperature.Text}\n" +
                           $"{lblHumidity.Text}\n" +
                           $"{lblPrecipitation.Text}\n" +
                           $"{lblWindSpeed.Text}\n" +
                           $"{lblCondition.Text}";
            Clipboard.SetText(text);
            btnCopy.Text = "Copied!";
            Task.Delay(3000).ContinueWith(t =>
            {
                Invoke(new Action(() => btnCopy.Text = "Copy"));
            });
        }

        private WeatherData lastWeatherData = null;
        private async void getWeather()
        {
            string city = txtCity.Text;
            if (city == "Enter a city name..." || string.IsNullOrWhiteSpace(city))
            {
                MessageBox.Show("Please enter a valid city name.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string url = $"http://localhost:8080/weather?city={Uri.EscapeDataString(city)}";
                    string response = await client.GetStringAsync(url);
                    WeatherData data = JsonSerializer.Deserialize<WeatherData>(response);
                    lastWeatherData = data;
                    updateDisplay(data);

                    if (string.IsNullOrEmpty(response))
                    {
                        MessageBox.Show("No data received from the weather service.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                catch (HttpRequestException)
                {
                    MessageBox.Show("City not found. Please try another.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while fetching the weather data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
