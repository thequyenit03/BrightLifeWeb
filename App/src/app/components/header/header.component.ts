import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { NavigationService } from '../../model/navigation/navigation.service';
import { Messages } from '../../model/lang/MessagesVN';
import { getMessages } from '../../model/lang/Messages';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [RouterLink, RouterLinkActive],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent {

  message:Messages = getMessages('');

  constructor(private navigationService: NavigationService){}

  navigateTo(route : string){
    this.navigationService.navigateTo(route);
  }
  
}
