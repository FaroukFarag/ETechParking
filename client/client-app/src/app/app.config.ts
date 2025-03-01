import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { provideRouter } from '@angular/router';
import { routes } from './app.routes';

import { HTTP_INTERCEPTORS, provideHttpClient, withInterceptors, withInterceptorsFromDi } from '@angular/common/http';
import { MyInterceptor } from './my-interceptor.interceptor';

export const appConfig: ApplicationConfig = {
  providers: [
    provideAnimationsAsync(),
   
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
      provideHttpClient(
        withInterceptorsFromDi(),
      ),
      {provide: HTTP_INTERCEPTORS, useClass: MyInterceptor, multi: true},
  ]
};
