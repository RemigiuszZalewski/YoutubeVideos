import { Component, OnInit } from '@angular/core';
import {AuthService} from "../../services/auth.service";
import {LoginRequest} from "../../models/login-request";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent implements OnInit {

  constructor(private authService: AuthService) { }

  ngOnInit(): void {
    this.isLoggedIn();
  }

  credentials: LoginRequest = {
    email: '',
    password: ''
  };

  isLoggedIn() {
    return this.authService.isLoggedIn();
  }

  login() {
    this.authService.login(this.credentials)
      .subscribe(() => {
        console.log('Login successful');
      })
  }
}
