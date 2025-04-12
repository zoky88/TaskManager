import { bootstrapApplication } from '@angular/platform-browser';
import { AppComponent } from './app/app.component';
import { provideHttpClient } from '@angular/common/http';
import { appConfig } from './app/app.config';
import { ApplicationConfig, mergeApplicationConfig } from '@angular/core';

const config: ApplicationConfig = mergeApplicationConfig(appConfig, {
  providers: [provideHttpClient()]
});

bootstrapApplication(AppComponent, config)
  .then(() => console.log('Application started successfully'))
  .catch(err => console.error(err));

