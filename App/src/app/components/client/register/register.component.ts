import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../../service/auth.service';
import { ToastrService } from 'ngx-toastr';
import { CommonModule } from '@angular/common';
import { auth } from '../../../model/auth';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [RouterLink, FormsModule, ReactiveFormsModule, CommonModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent implements OnInit {
  registerForm!: FormGroup
  confirmPasswordControl!: FormControl
  
  constructor(
    private auth: AuthService,
    private router: Router,
    private formBuilder: FormBuilder,
    private message: ToastrService
  ){}

  ngOnInit(): void {
      this.registerForm = this.formBuilder.group({
        fullName: ['', [Validators.required, Validators.maxLength(100)]],
        userName: ['', [Validators.required, Validators.maxLength(50)]],
        password: ['', [Validators.required, Validators.minLength(8)]],
        email: ['',[Validators.required, Validators.email]]
      })
      //Khởi tạo FormControl cho confirm-password
      this.confirmPasswordControl = new FormControl('', Validators.required)
      //Thêm validator so khớp password
      this.confirmPasswordControl.addValidators(this.mathPassword.bind(this))
      //Khi password thay đổi, cập nhật lại validation của confirmPassword
      this.registerForm
      .get('password')
      ?.valueChanges
      .subscribe(()=> this.confirmPasswordControl.updateValueAndValidity())
  }

  //Hàm validator so sánh password
  private mathPassword(
    control: AbstractControl
    ) : { [key: string]: boolean} | null{
      const password = this.registerForm?.get('password')?.value
      if(password && control.value != password){
        return {mathPassword : true}        
      }
      return null;
    }

  onSubmit(){
    if(this.registerForm.invalid) return

    this.auth.register(this.registerForm.value)
    .subscribe({
      next: (res: auth) =>{
        if(res.status){
          this.message.success("Đăng ký thành công !", "Thông báo !")
          const user = this.auth.getUserFromToken()
          console.log('user', user)
          this.router.navigate(['/'])
        }else{
          this.message.warning("Email đã tồn tại !", "Thông báo !")
        }
      },
      error: err => this.message.error("Lỗi đăng ký !", "Cảnh báo") || err
    })
  }

}
