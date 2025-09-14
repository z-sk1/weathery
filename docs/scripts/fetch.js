document.getElementById("cityInput").addEventListener("keydown", function (event) {
    if (event.key === "Enter") {
        getWeather();
    }
});

function getWeather() {
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