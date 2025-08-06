let lastWeatherData = null;

const weatherDescriptions = {
  0: "Clear sky ☀️",
  1: "Mainly clear 🌤️",
  2: "Partly cloudy ⛅",
  3: "Overcast ☁️",
  45: "Fog 🌫️",
  48: "Depositing rime fog 🌫️❄️",
  51: "Light drizzle 🌦️",
  53: "Moderate drizzle 🌧️",
  55: "Dense drizzle 🌧️💧",
  56: "Freezing light drizzle 🌧️❄️",
  57: "Freezing dense drizzle 🌧️🧊",
  61: "Slight rain 🌦️",
  63: "Moderate rain 🌧️",
  65: "Heavy rain 🌧️💦",
  66: "Freezing light rain 🌧️❄️",
  67: "Freezing heavy rain 🌧️🧊",
  71: "Slight snow fall 🌨️",
  73: "Moderate snow fall 🌨️❄️",
  75: "Heavy snow fall ❄️🌨️",
  77: "Snow grains ❄️🌾",
  80: "Slight rain showers 🌦️",
  81: "Moderate rain showers 🌧️☔",
  82: "Violent rain showers 🌧️🌪️",
  85: "Slight snow showers 🌨️",
  86: "Heavy snow showers 🌨️❄️",
  95: "Thunderstorm ⛈️",
  96: "Thunderstorm with slight hail ⛈️🧊",
  99: "Thunderstorm with heavy hail ⛈️🌩️🧊"
};

document.getElementById("cityInput").addEventListener("keydown", function (event) {
    if (event.key === "Enter") {
        getWeather();
    }
});

document.getElementById("unitToggle").addEventListener("change", () => {
  if (lastWeatherData) {
    updateDisplay(lastWeatherData);
  }
});


function cToF(celsius) {
    return (celsius * 9) / 5 + 32;
}

function updateDisplay(data) {
    const resultDiv = document.getElementById("result");
    const unitToggle = document.getElementById("unitToggle");

    const description = weatherDescriptions[data.weather_code] || "Unknown weather"; 

    let temp = data.temperature;
    let unit = "°C";

    if (unitToggle.checked) {
        temp = cToF(temp);
        unit = "°F";
    }

    // round temp to 1 decimal place
    temp = temp.toFixed(1);

    resultDiv.innerHTML = `
    <p>Temperature: ${temp}${unit}</p>
    <p>💧 Humidity: ${data.humidity}%</p>
    <p>🌧️ Precipitation: ${data.precipitation} mm</p>
    <p>💨 Wind Speed: ${data.windSpeed} km/h</p>
    <p>🌤️ Condition: ${description || "Unknown"}</p>
    <button onclick = "copyData()" id = "copyBtn">Copy</button>`;
}

function copyData() {
    const resultDiv = document.getElementById("result");
    const copyBtn = document.getElementById("copyBtn");
    const city = document.getElementById("cityInput").value;
    let textToCopy = "";

    textToCopy += city;

    for (let node of resultDiv.childNodes) {
        if (node.nodeType === Node.ELEMENT_NODE && node.tagName === "BUTTON") {
            continue;
        }

        if (node.nodeType === Node.TEXT_NODE || node.nodeType === Node.ELEMENT_NODE) {
            textToCopy += node.textContent;
        }
    }

    navigator.clipboard.writeText(textToCopy.trim())
        .then(() => {
            copyBtn.innerText = "Copied!";
            setTimeout(() => {copyBtn.innerText = "Copy"}, 3000);
        })
        .catch(err => {
            alert("Failed to copy! Error: ", err);
        });
}

async function getWeather() {
    const city = document.getElementById("cityInput").value;
    const resultDiv = document.getElementById("result");

    if (!city) {
        resultDiv.textContent = "Please enter a city name.";
        return;
    }

    fetch(`https://weathery-service.onrender.com/weather?city=${encodeURIComponent(city)}`)
        .then(response => {
            if (!response.ok) {
                return response.text().then(errorText => {
                    throw new Error(errorText || "Network connection was not ok")
                });
                
            }
            return response.json();
        })
        .then(data => {
            lastWeatherData = data;
            updateDisplay(data);
        })
        .catch(error => {
            if (error.message.includes("city not found")) {
                resultDiv.textContent = "City not found. Please try another.";
            } else {
                resultDiv.textContent = "Error" + error.message;
                console.error("Fetch error:", error);
            }
        });
} 