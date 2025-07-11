// main.ts
import { bootstrapApplication } from '@angular/platform-browser';
import { importProvidersFrom } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app/app.component';
import { appConfig } from './app/app.config';

bootstrapApplication(AppComponent, {
  ...appConfig,
  providers: [
    // Bước quan trọng: đưa HttpClientModule vào provider
    importProvidersFrom(HttpClientModule),

    // Giữ lại các providers khác trong appConfig (nếu có)
    ...(appConfig.providers ?? [])
  ]
})
.catch(err => console.error(err));
