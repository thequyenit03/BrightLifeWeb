import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Observable, tap } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { auth } from '../model/auth';
import { login } from '../model/login';
import { register } from '../model/register';
@Injectable({
  providedIn: 'root'
})
export class AuthService {
  public ApiUrl = environment.hostApi + '/Auth'

  constructor(    
    private http: HttpClient
  ) {}

  login(login: login): Observable<auth>{
    return this.http.post<auth>(`${this.ApiUrl}/login`, login)
    .pipe(
      tap(res =>{
        localStorage.setItem('access_token', res.token)
        localStorage.setItem('user', JSON.stringify(res.user))
      })
    )
  }

  register(register: register): Observable<auth>{
    return this.http.post<auth>(`${this.ApiUrl}/register`, register)
    .pipe(
      tap(res =>{
        localStorage.setItem('access_token', res.token)
        localStorage.setItem('user', JSON.stringify(res.user))
      })
    )
  }
  
  logout(): void {
    localStorage.removeItem('access_token');
    localStorage.removeItem('user');
  }
  getToken(): string | null {
    return localStorage.getItem('access_token');
  }

  isLoggedIn(): boolean {
    return !!this.getToken();
  }
  getUser(): auth | null {
    const user = localStorage.getItem('user');
    return user ? JSON.parse(user) : null;
  }
}
