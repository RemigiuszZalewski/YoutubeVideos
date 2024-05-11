import {HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import {AuthService} from "../services/auth.service";
import { Observable } from "rxjs";
import { Injectable } from "@angular/core";


@Injectable()
export class JwtInterceptor implements HttpInterceptor {

  constructor(private authService: AuthService) { }

  intercept(request: HttpRequest<any>, next: HttpHandler) : Observable<HttpEvent<any>> {
    const isLoggedIn = this.authService.isLoggedIn();

    if (isLoggedIn) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${localStorage.getItem('accessToken')}`
        }
      })
    }

    return next.handle(request);
  }
}
