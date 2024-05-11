import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {LoginRequest} from "../models/login-request";
import {LoginResponse} from "../models/login-response";
import { Observable, map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private httpClient: HttpClient) { }

  login(credentials: LoginRequest) : Observable<LoginResponse> {
    return this.httpClient.post<LoginResponse>("http://localhost:5200/login", credentials)
      .pipe(map(response => {
        localStorage.setItem('accessToken', response.accessToken);
        document.cookie = `refreshToken=${response.refreshToken};`;
        return response;
      }))
  }

  refreshToken() : Observable<LoginResponse> {
    const refreshToken = this.getRefreshTokenFromCookie();

    return this.httpClient.post<LoginResponse>("http://localhost:5200/refresh", { refreshToken})
      .pipe(map(response => {
        localStorage.setItem('accessToken', response.accessToken);
        document.cookie = `refreshToken=${response.refreshToken};`;
        return response;
      }))
  }

  private getRefreshTokenFromCookie(): string | null {
    const cookieString = document.cookie;
    const cookieArray = cookieString.split('; ');

    for (const cookie of cookieArray) {
      const [name, value] = cookie.split('=');

      if (name == 'refreshToken') {
        return value;
      }
    }

    return null;
  }

  logout() {
    localStorage.removeItem('accessToken');
  }

  isLoggedIn() : boolean {
    return localStorage.getItem('accessToken') !== null;
  }
}
