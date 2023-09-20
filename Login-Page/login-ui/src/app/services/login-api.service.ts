import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, } from 'rxjs';
import { LoginDTO } from '../models/login-dto.model';

@Injectable({
  providedIn: 'root'
})
export class LoginApiService {

  private loginURL = "http://localhost:5159"

  constructor(private _http : HttpClient) { }

  loginUser(loginDTO: LoginDTO): Observable<any> {
    const loginUrl = `${this.loginURL}/api/Login/user`;

    return this._http.post<any>(loginUrl, loginDTO, { responseType: 'text' as 'json'
  });
  }

}
