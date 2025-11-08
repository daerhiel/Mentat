import { ApplicationConfig, provideBrowserGlobalErrorListeners, provideZonelessChangeDetection } from '@angular/core';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { provideRouter } from '@angular/router';

import { provideThemes } from '@core/theme';
import { routes } from './app.routes';

/**
 * List of available application themes.
 */
export const themes = [
  { id: 'azure-blue', name: 'Azure & Blue' },
  { id: 'magenta-violet', name: 'Magenta & Violet' }
];

export const appConfig: ApplicationConfig = {
  providers: [
    provideAnimationsAsync(),
    provideBrowserGlobalErrorListeners(),
    provideZonelessChangeDetection(),
    provideHttpClient(withInterceptors([])),
    provideRouter(routes),
    provideThemes(themes)
  ]
};
