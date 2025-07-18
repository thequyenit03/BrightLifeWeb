import { Component, NgModule, OnInit } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { Form, FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../../service/auth.service';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { auth } from '../../../model/auth';
import { ToastrService } from 'ngx-toastr';


@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ RouterLink, FormsModule, ReactiveFormsModule, CommonModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent implements OnInit{

  loginForm!: FormGroup;
  
  constructor(
    public auth: AuthService,
    private router: Router,
    private formBuilder: FormBuilder,
    private message: ToastrService
    
  ){ }

  ngOnInit(): void {
      this.loginForm = this.formBuilder.group({
        email: ['', [Validators.required, Validators.email]],
        password: ['', [Validators.required, Validators.minLength(8)]]
      })
  }

  onSubmit(){
    if (this.loginForm.invalid){      
      return
    };
    this.auth.login(this.loginForm.value)
    .subscribe({
      next: (res : auth) =>{
        if(res.status){
        this.message.success("Đăng nhập thành công !", "Thông báo !")
        const user = this.auth.getUserFromToken()
        console.log(res.message)      
        console.log('User', user)
        this.router.navigate(['/'])       
        }else{
          this.message.warning("Sai tài khoản hoặc mật khẩu !", " Thông báo !")
        }
      },
      error: err => this.message.error("Lỗi đăng nhập !", "Cảnh báo !") || err
      
    })
  }
}
