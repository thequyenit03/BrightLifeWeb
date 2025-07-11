import { Component, NgModule, OnInit } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { Form, FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../../service/auth.service';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ RouterLink, FormsModule, ReactiveFormsModule, CommonModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent implements OnInit{

  loginForm!: FormGroup;
  errorMessage = '';
  
  constructor(
    public auth: AuthService,
    private router: Router,
    private formBuilder: FormBuilder
  ){ }

  ngOnInit(): void {
      this.loginForm = this.formBuilder.group({
        email: ['', [Validators.required, Validators.email]],
        password: ['', Validators.required]
      })
  }

  get email() {
    return this.loginForm.get('email');
  }
  get password() {
    return this.loginForm.get('password');
  }

  onSubmit(){
    if (this.loginForm.invalid){      
      return
    };
    this.auth.login(this.loginForm.value)
    .subscribe({
      next: res =>{
        console.log(res);
        this.router.navigate(['/']);        
      },
      error: err => this.errorMessage = err.error|| 'Đăng nhập thất bại',
    })
  }
}
