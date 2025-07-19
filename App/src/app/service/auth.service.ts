import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { catchError, Observable, of, tap, throwError } from 'rxjs';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { auth } from '../model/auth';
import { login } from '../model/login';
import { register } from '../model/register';
import { user } from '../model/user';
import { jwtDecode } from 'jwt-decode';
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
      catchError((err: HttpErrorResponse)=>{
        if(err.status === 401){
          return of({
            token: null,
            status: false,
            message: err.error?.message || 'Unauthorized'
          })
        }
        return throwError(() => err)
      }),
      tap(res =>{

        if(res.status && res.token){
          localStorage.setItem('access_token', res.token)        
        }        
      })
    )
  }

  register(register: register): Observable<auth>{
    return this.http.post<auth>(`${this.ApiUrl}/register`, register)
    .pipe(
      catchError((err: HttpErrorResponse)=>{
        if(err.status === 401){
          return of({
            token: null,
            status: false,
            message: err.error?.message || 'Unauthorized'
          })
        }
        return throwError(() => err)
      }),
      tap(res =>{
        if(res.status && res.token){
          localStorage.setItem('access_token', res.token)        
        }
      })
    )
  }
  
  logout(): void {
    localStorage.removeItem('access_token');    
  }
  getToken(): string | null {
    return localStorage.getItem('access_token');
  }

  isLoggedIn(): boolean {
    return !!this.getToken();
  }

  getUserFromToken(): user | null{
    const token = this.getToken();
    if(!token) return null;
    try{
      return jwtDecode<user>(token);
    }catch{
      return null;
    }
  }
}
