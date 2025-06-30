import { Component } from '@angular/core';
import { NavigationService } from '../../model/navigation/navigation.service';

@Component({
  selector: 'app-about',
  standalone: true,
  imports: [],
  templateUrl: './about.component.html',
  styleUrl: './about.component.css'
})
export class AboutComponent {
  constructor(private navigationService: NavigationService){}

  navigateTo(route: string){
    this.navigationService.navigateTo(route);
  }
}
