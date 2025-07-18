import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';

import { NavigationService } from '../../../model/navigation/navigation.service';
import { Messages } from '../../../model/lang/MessagesVN';
import { getMessages } from '../../../model/lang/Messages';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../../service/auth.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [RouterLink, RouterLinkActive, CommonModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent {


  constructor(
    private navigationService: NavigationService,
    private auth: AuthService,
    private message: ToastrService
    ){}

    get userName():string{
      const user = this.auth.getUserFromToken();
      return user && user.userName ? user.userName: ''
    }

    logout(){
      this.auth.logout()
      this.message.warning("Đăng xuất thành công !", "Thông báo !")
    }
    isLogin() : boolean{
      return this.auth.isLoggedIn()
    }
  navigateTo(route : string){
    this.navigationService.navigateTo(route);
  }
  
}
