import { Component } from '@angular/core';
import { Router } from '@angular/router';
@Component({
  selector: 'app-blogs',
  standalone: true,
  imports: [],
  templateUrl: './blogs.component.html',
  styleUrl: './blogs.component.css'
})
export class BlogsComponent {
  constructor(private router: Router){}
  navigateTo(route: string){
    this.router.navigate([route]).then(() => {          
      window.scrollTo(0,0);
    });
  }
}
