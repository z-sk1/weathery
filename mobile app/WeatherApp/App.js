import * as Clipboard from 'expo-clipboard';
import React, { useState } from 'react';
import { View, StyleSheet, Text, TextInput, Button, TouchableOpacity, Switch, Alert } from 'react-native';

export default function App() {
  const [text, setText] = useState('');
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);
  const [isFahrenheit, setIsFahrenheit] = useState(false);
  const [lastWeatherData, setLastWeatherData] = useState(null);
  const [displayData, setDisplayData] = useState(null);
  const [isFocused, setFocused] = useState(false);
  const [copyIsPressed, copySetPressed] = useState(false);
  const [isPressed, setPressed] = useState(false);
  const [copyBtnText, setCopyBtnText] = useState("Copy");

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

  return (
    <View style = {styles.container}>
      <View>
        <Text style = {styles.header}>weathery</Text>
      </View>

      <View style = {styles.inputGroup}>
        <TextInput
          style = {[styles.textInput, isFocused && styles.textInputFocused]}
          onFocus={(() => setFocused(true))}
          onBlur = {(() => setFocused(false))}
          placeholder = "Name a city..."
          value = {text}
          onChangeText = {setText}
        />
        
        <View style = {styles.buttonToggleRow}>
          <TouchableOpacity
          style = {[styles.button, isPressed && styles.buttonPressed]}
          onPressIn = {(() => setPressed(true))}
          onPressOut = {(() => setPressed(false))}
          onPress = {fetchWeather}>
            <Text style = {styles.buttonText}>Get Weather</Text>
          </TouchableOpacity>

          <View style = {styles.toggleContainer}>
            <Switch
              value = {isFahrenheit}
              onValueChange = {(val) => {
                setIsFahrenheit(val);
                updateDisplay(lastWeatherData, val)
              }}
            />
            <Text style = {styles.toggleLabel}>°F</Text>
          </View>
        </View>
      </View>

      <View style = {styles.result}>
        {error ? (
          <Text style = {[styles.resultText, {color: 'red'}]}>{error}</Text>
        ) : displayData ? (
          <>
            <Text style = {styles.resultText}>Temperature: {displayData.temperature}{displayData.unit}</Text>
            <Text style = {styles.resultText}>Humidity: {displayData.humidity}%</Text>
            <Text style = {styles.resultText}>Precipitation: {displayData.precipitation} mm</Text>
            <Text style = {styles.resultText}>Wind Speed: {displayData.windSpeed} km/h</Text>
            <Text style = {styles.resultText}>Condition: {weatherDescriptions[displayData.weather_code]}</Text>

            <TouchableOpacity
              style = {[styles.copyButton, copyIsPressed && styles.buttonPressed]}
              onPressIn = {(() => copySetPressed(true))}
              onPressOut = {(() => copySetPressed(false))}
              onPress = {copyData}>
              <Text style = {styles.buttonText}>{copyBtnText}</Text>
            </TouchableOpacity>
          </>
        ) : (
          <Text style = {styles.resultText}>Result Here</Text>
        )}
      </View>

    </View>
  );

  function cToF(celsius) {
    return (celsius * 9) / 5 + 32;
  }

  function updateDisplay(data, toFarenheit) {
    if(!data) {
      setDisplayData(null);
      return;
    }

    let temperature = data.temperature;
    let unit = "°C";

    if (toFarenheit) {
      temperature = cToF(temperature);
      unit = "°F";
    }

    const newDisplayData = {
      ...data,
      temperature: temperature.toFixed(1),
      unit,

    };

    setDisplayData(newDisplayData);
  }

  async function copyData() {
    if (!text.trim() || !displayData) {
      Alert.alert("Nothing to copy", "Please fetch weather data first.");
      return;
    }

    const weatherText = `
    City: ${text}
    Temperature: ${displayData.temperature}${displayData.unit}
    Humidity: ${displayData.humidity}%
    Precipitation: ${displayData.precipitation} mm
    Wind Speed: ${displayData.windSpeed} km/h
    Condition: ${weatherDescriptions[displayData.weather_code]}
      `.trim();  
      
    try {
      await Clipboard.setStringAsync(weatherText);
      setCopyBtnText("Copied!")
      setTimeout(() => setCopyBtnText("Copy"), 3000);
    } catch (err) {
      Alert.alert("Copy failure:", err.message || "Unknown message");
    }
  }

  async function fetchWeather() {
    if (!text.trim()) {
      setError("Please enter a city name.");
      setLastWeatherData(null);
      setDisplayData(null);
      return;
    }

    setLoading(true);
    setError(null);

    try {
      const response = await fetch(`https://weathery-service.onrender.com/weather?city=${encodeURIComponent(text)}`)

      if (!response.ok) {
        throw new Error("City not found or network error.");
      }
      const data = await response.json();
      setLastWeatherData(data);
      updateDisplay(data, isFahrenheit);
    } catch (err) {
      setError(err.message);
      setLastWeatherData(null);
      setDisplayData(null);
    } finally {
      setLoading(false);
    }
  }
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    flexDirection: 'column',
    alignItems: 'center',
    justifyContent: 'flex-start',
    paddingVertical: 80,
    paddingHorizontal: 20,
    backgroundColor: '#2b3e50', // fallback solid color if no gradient
  },

  header: {
    fontSize: 48,
    marginBottom: 40,
    color: 'rgba(255, 255, 255, 0.47)', // #ffffff78 alpha
    textShadowColor: 'rgba(0, 0, 0, 0.3)',
    textShadowOffset: { width: 1, height: 1 },
    textShadowRadius: 3,
  },

  inputGroup: {
    flexDirection: 'column',
    flexWrap: 'wrap',
    justifyContent: 'center',
    marginBottom: 20,
  },

  textInput: {
    paddingVertical: 12,
    paddingHorizontal: 16,
    marginBottom: 25,
    borderRadius: 10,
    fontSize: 16,
    width: 260,
    maxWidth: '100%',
    backgroundColor: '#fff',

    //shadow
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.2,
    shadowRadius: 6,
    elevation: 3,
  },

  textInputFocused: {
    borderWidth: 1,
    borderColor: '#4CAF50',
    shadowColor: '#4CAF50',
    shadowOpacity: 0.9,
    shadowRadius: 10,
    elevation: 10,
  },

  button: {
    backgroundColor: '#667eb6ff',
    minWidth: 140,
    paddingVertical: 15,
    paddingHorizontal: 20, // increase horizontal padding
    borderRadius: 10,
    alignItems: 'center',
    justifyContent: 'center',  // add vertical centering
    alignSelf: 'center',
    flexGrow: 1,

    shadowColor: '#000',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.2,
    shadowRadius: 6,
    elevation: 3,
  },

  copyButton: {
    backgroundColor: '#667eb6ff',
    minWidth: 140,
    marginTop: 15,
    paddingVertical: 15,
    paddingHorizontal: 20, // increase horizontal padding
    borderRadius: 10,
    alignItems: 'center',
    justifyContent: 'center',  // add vertical centering
    alignSelf: 'center',
    flexGrow: 0,

    shadowColor: '#000',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.2,
    shadowRadius: 6,
    elevation: 3,
  },

  buttonPressed: {
    borderWidth: 1,
    borderColor: 'yellow',
    shadowColor: 'yellow',
    shadowOpacity: 0.9,
    shadowRadius: 10,
    elevation: 10,
  },

  buttonText: {
    color: '#fff',
    fontSize: 16,
    fontWeight: 'bold',
  },

  buttonToggleRow: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'center',
    marginTop: 10,
  },

  toggleContainer: {
    flexDirection: 'row',
    alignItems: 'center',
    marginLeft: 10, // or whatever spacing you want
  },

  toggleLabel: {
    fontSize: 16,
    color: '#fff',
    marginLeft: 8,
  },

  result: {
    backgroundColor: 'rgba(255, 255, 255, 0.1)',
    borderRadius: 12,
    paddingVertical: 20,
    paddingHorizontal: 30,
    marginTop: 40,

    // shadows:
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 4 },
    shadowOpacity: 0.3,
    shadowRadius: 12,
    elevation: 5,
  },

  resultText: {
    color: 'rgba(255, 255, 255, 0.87)',
    textAlign: 'center',
    fontSize: 19,
  }
});