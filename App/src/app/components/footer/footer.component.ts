import { Component } from '@angular/core';
import {  RouterLink } from '@angular/router';
import { NavigationService } from '../../model/navigation/navigation.service';
@Component({
  selector: 'app-footer',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './footer.component.html',
  styleUrl: './footer.component.css'
})
export class FooterComponent {    
  constructor(private navigationService: NavigationService){}
  navigateTo(route: string){
    this.navigationService.navigateTo(route);
  }
}

 
