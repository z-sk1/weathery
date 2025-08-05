function getWeather() {
    const city = document.getElementById("cityInput").value;
    const resultDiv = document.getElementById("result")

    if (!city) {
        resultDiv.textContent = "Please enter a city name.";
        return;
    }

    fetch(`http://localhost:8080/weather?city=${encodeURIComponent(city)}`)
        .then(response => {
            if (!response.ok) {
                throw new Error("Network connection was not ok");
            }
            return response.json();
        })
        .then(data => {
            resultDiv.textContent = `Temperature: ${data.temperature}\u00B0C, Code: ${data.weather_code}`;
        })
        .catch(error => {
            resultDiv.textContent = "Error" + error.message;
            console.error("Fetch error:", error);
        });
} 