import { useEffect, useState } from 'react';

const storageKey = 'wpfw-weather-theme-mode';
const location = { name: 'Amsterdam', latitude: 52.3676, longitude: 4.9041 };
const weatherDescriptions = {
  0: 'Clear sky', 1: 'Mainly clear', 2: 'Partly cloudy', 3: 'Overcast',
  45: 'Fog', 48: 'Rime fog',
  51: 'Light drizzle', 53: 'Drizzle', 55: 'Heavy drizzle',
  56: 'Freezing drizzle', 57: 'Heavy freezing drizzle',
  61: 'Light rain', 63: 'Rain', 65: 'Heavy rain',
  66: 'Freezing rain', 67: 'Heavy freezing rain',
  71: 'Light snow', 73: 'Snow', 75: 'Heavy snow', 77: 'Snow grains',
  80: 'Light rain showers', 81: 'Rain showers', 82: 'Heavy rain showers',
  85: 'Snow showers', 86: 'Heavy snow showers',
  95: 'Thunderstorm', 96: 'Thunderstorm with hail', 99: 'Heavy thunderstorm'
};

const modes = [
  { mode: 'auto', label: 'Auto' },
  { mode: 'light', label: 'Light theme', icon: '☀️' },
  { mode: 'dark', label: 'Dark theme', icon: '🌙' }
];

function readSavedMode() {
  try {
    const saved = localStorage.getItem(storageKey);
    return ['auto', 'light', 'dark'].includes(saved) ? saved : 'auto';
  } catch {
    return 'auto';
  }
}

/** Theme switcher: "auto" turns dark mode on when the current Amsterdam weather code is 3 or higher. */
export default function WeatherTheme() {
  const [mode, setMode] = useState(readSavedMode);
  const [weather, setWeather] = useState({ text: 'Loading weather...', theme: 'light' });

  useEffect(() => {
    const controller = new AbortController();
    const url = new URL('https://api.open-meteo.com/v1/forecast');
    url.search = new URLSearchParams({
      latitude: String(location.latitude),
      longitude: String(location.longitude),
      current: 'weather_code',
      timezone: 'Europe/Amsterdam'
    });

    fetch(url, { signal: controller.signal })
      .then((response) => {
        if (!response.ok) throw new Error('Weather request failed');
        return response.json();
      })
      .then((data) => {
        const code = data.current?.weather_code;
        if (typeof code !== 'number') throw new Error('Weather data is unavailable');
        setWeather({
          text: weatherDescriptions[code] ?? 'Current conditions',
          theme: code >= 3 ? 'dark' : 'light'
        });
      })
      .catch((error) => {
        if (error.name !== 'AbortError') setWeather({ text: 'Weather unavailable', theme: 'light' });
      });

    return () => controller.abort();
  }, []);

  const isDark = mode === 'dark' || (mode === 'auto' && weather.theme === 'dark');
  useEffect(() => {
    document.body.classList.toggle('theme-dark', isDark);
  }, [isDark]);

  const selectMode = (newMode) => {
    setMode(newMode);
    try {
      localStorage.setItem(storageKey, newMode);
    } catch {
      // The selection still applies until the page is closed.
    }
  };

  return (
    <div className="weather-theme">
      <p className="weather-theme__status" role="status" aria-live="polite">
        {location.name} | {weather.text}
      </p>
      <div className="weather-theme__controls" role="group" aria-label="Theme mode">
        {modes.map(({ mode: value, label, icon }) => (
          <button
            key={value}
            type="button"
            className={`weather-theme__button${icon ? ' weather-theme__icon-button' : ''}`}
            aria-pressed={mode === value}
            aria-label={icon ? label : undefined}
            title={icon ? label : undefined}
            onClick={() => selectMode(value)}
          >
            {icon ? <span aria-hidden="true">{icon}</span> : label}
          </button>
        ))}
      </div>
    </div>
  );
}
