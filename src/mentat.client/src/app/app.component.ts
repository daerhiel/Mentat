import { Component, inject } from '@angular/core';
import { AsyncPipe } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { RouterOutlet } from '@angular/router';

interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}

@Component({
  selector: 'app-root',
  imports: [AsyncPipe, RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  readonly #http = inject(HttpClient);

  title = 'mentat.client';

  readonly forecasts = this.#http.get<WeatherForecast[]>('/weatherforecast');
}
