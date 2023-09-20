import { Component, OnInit } from '@angular/core';
import { LoginApiService } from '../services/login-api.service';
import { LoginDTO } from '../models/login-dto.model';
import { tap } from 'rxjs';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  username: string = '';
  password: string = '';

  constructor(private _api : LoginApiService, private router: Router, private authService: AuthService) { }

  ngOnInit(): void {
  }

  login(){

    const loginDTO: LoginDTO = {
      username: this.username,
      password: this.password
    };

    this._api.loginUser(loginDTO).pipe(
      tap({
       next: (response) => {
          console.log('Login successful: ', response);
          this.authService.setAuthenticated(true);
          this.router.navigate(['/success']);
        },
        error: (error) => {
          console.error('Login error: ', error);
        }
      })
    ).subscribe();
  }

}
