import {HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Observable } from "rxjs";
import { Injectable } from "@angular/core";


@Injectable()
export class AccessTokenInterceptor implements HttpInterceptor {

  constructor() { }

  intercept(request: HttpRequest<any>, next: HttpHandler) : Observable<HttpEvent<any>> {
    const tokenAvailable = localStorage.getItem('accessToken') !== null;

    if (tokenAvailable) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${localStorage.getItem('accessToken')}`
        }
      })
    }

    return next.handle(request);
  }
}
