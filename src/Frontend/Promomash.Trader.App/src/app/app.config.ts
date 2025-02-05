import {ApplicationConfig, importProvidersFrom} from '@angular/core';
import {provideRouter} from '@angular/router';
import {routes} from './app.routes';
import {provideAnimationsAsync} from '@angular/platform-browser/animations/async';
import {HttpClientModule} from '@angular/common/http';
import {provideEnvironmentNgxMask} from 'ngx-mask';
import {environment} from '../environments/environment';

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    provideAnimationsAsync(),
    importProvidersFrom(HttpClientModule),
    provideEnvironmentNgxMask(),
    {provide: 'BASE_API_URL', useValue: environment.apiUrl},
  ]
};
