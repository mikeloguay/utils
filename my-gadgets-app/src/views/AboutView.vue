<script setup lang="ts">
import { onMounted, ref } from 'vue'

interface WeatherForecast {
  date: string;
  temperatureC: number;
}

const forecasts = ref<WeatherForecast[]>([]);

const BASE_URL = `http://localhost:5116`

const fetchWeatherForecast = async () => {
  const response = await fetch(`${BASE_URL}/weatherforecast`);
  forecasts.value = await response.json();
};

onMounted(fetchWeatherForecast);

</script>

<template>
  <div class="about">
    <ul v-if="forecasts.length">
      <li v-for="forecast in forecasts">
        <p>date: {{ forecast.date }}</p>
        <p>temperatureC: {{ forecast.temperatureC }}</p>
      </li>
    </ul>
    <p v-else>Loading...</p>
  </div>
</template>

<style>
@media (min-width: 1024px) {
  .about {
    min-height: 100vh;
    display: flex;
    align-items: center;
  }
}
</style>