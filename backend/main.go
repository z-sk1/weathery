package main

import (
	"encoding/json"
	"fmt"
	"io"
	"log"
	"net/http"
	"net/url"
)

type GeocodeResponse struct {
	Results []struct {
		Latitude  float64 `json:"latitude"`
		Longitude float64 `json:"longitude"`
	} `json:"results"`
}

type WeatherResponse struct {
	Current struct {
		Temperature   float64 `json:"temperature_2m"`
		WeatherCode   int     `json:"weathercode"`
		Humidity      float64 `json:"relative_humidity_2m"`
		Precipitation float64 `json:"precipitation"`
		WindSpeed     float64 `json:"windspeed_10m"`
	} `json:"current"`
}

func main() {
	http.Handle("/weather", enableCORS(http.HandlerFunc(weatherHandler)))
	fmt.Println("Server running on localhost:8080")
	log.Fatal(http.ListenAndServe(":8080", nil))
}

func getCoordinates(city string) (float64, float64, error) {
	safeCity := url.QueryEscape(city)
	url := fmt.Sprintf("https://geocoding-api.open-meteo.com/v1/search?name=%s", safeCity)
	log.Println("Geocoding API URL:", url)

	resp, err := http.Get(url)
	if err != nil {
		return 0, 0, err
	}
	defer resp.Body.Close()

	if resp.StatusCode != http.StatusOK {
		bodyBytes, _ := io.ReadAll(resp.Body)
		return 0, 0, fmt.Errorf("geocoding API error: status %d, body: %s", resp.StatusCode, string(bodyBytes))
	}

	var geo GeocodeResponse

	if err := json.NewDecoder(resp.Body).Decode(&geo); err != nil {
		return 0, 0, err
	}

	if len(geo.Results) == 0 {
		return 0, 0, fmt.Errorf("city not found")
	}

	return geo.Results[0].Latitude, geo.Results[0].Longitude, nil
}

func getWeather(lat, lon float64) (float64, int, float64, float64, float64, error) {
	url := fmt.Sprintf("https://api.open-meteo.com/v1/forecast?latitude=%f&longitude=%f&current=temperature_2m,weather_code,relative_humidity_2m,precipitation,windspeed_10m", lat, lon)

	resp, err := http.Get(url)
	if err != nil {
		return 0, 0, 0, 0, 0, err
	}

	defer resp.Body.Close()

	var weather WeatherResponse
	if err := json.NewDecoder(resp.Body).Decode(&weather); err != nil {
		return 0, 0, 0, 0, 0, err
	}

	return weather.Current.Temperature, weather.Current.WeatherCode, weather.Current.Humidity, weather.Current.Precipitation, weather.Current.WindSpeed, nil
}

func weatherHandler(w http.ResponseWriter, r *http.Request) {
	city := r.URL.Query().Get("city")
	if city == "" {
		http.Error(w, "Missing 'city' parameter", http.StatusBadRequest)
		return
	}

	lat, lon, err := getCoordinates(city)
	if err != nil {
		http.Error(w, err.Error(), http.StatusInternalServerError)
		return
	}

	temp, code, humidity, prec, windS, err := getWeather(lat, lon)
	if err != nil {
		http.Error(w, err.Error(), http.StatusInternalServerError)
		return
	}

	result := map[string]interface{}{
		"temperature":   temp,
		"weather_code":  code,
		"humidity":      humidity,
		"precipitation": prec,
		"windSpeed":     windS,
	}

	w.Header().Set("Content-Type", "application/json")
	json.NewEncoder(w).Encode(result)
}

func enableCORS(next http.Handler) http.Handler {
	return http.HandlerFunc(func(w http.ResponseWriter, r *http.Request) {

		// Allow any origin, for devs only
		w.Header().Set("Access-Control-Allow-Origin", "*")
		w.Header().Set("Access-Control-Allow-Methods", "GET, OPTIONS")
		w.Header().Set("Access-Control-Allow-Headers", "Content-Type")

		// Handle "OPTIONS" request
		if r.Method == "OPTIONS" {
			w.WriteHeader(http.StatusOK)
			return
		}

		// Call the next handler
		next.ServeHTTP(w, r)
	})
}
