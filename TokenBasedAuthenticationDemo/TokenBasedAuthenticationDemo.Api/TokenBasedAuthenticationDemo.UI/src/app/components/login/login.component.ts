import { Component } from '@angular/core';
import {AuthService} from "../../services/auth.service";
import {LoginRequest} from "../../models/login-request";
import {Router} from "@angular/router";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

  constructor(private authService: AuthService,
              private router: Router) { }

  credentials: LoginRequest = {
    email: '',
    password: ''
  };

  login() {
    this.authService.login(this.credentials)
      .subscribe(() => {
        console.log('Login successful');
        this.router.navigateByUrl('/products');
      })
  }
}
