import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';

import { HomeComponent } from "./components/client/home/home.component";
import { HeaderComponent } from './components/client/header/header.component';
import { FooterComponent } from './components/client/footer/footer.component';
import { HttpClientModule } from '@angular/common/http';


@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, HeaderComponent, FooterComponent, HttpClientModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'Bright-Life';
}
