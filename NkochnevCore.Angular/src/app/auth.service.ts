import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { authResult } from './authresult';

import * as moment from "moment";

import 'rxjs/add/operator/map';
import { environment } from '../environments/environment';

@Injectable()
export class AuthService {

  private tokenLocalStorageKeyName = 'access_token';
  private expiresInLocalStorageKeyName = 'expires_in';
  private refreshLocalStorageKeyName = 'refresh_token';
  constructor(private http: HttpClient) { }

  authUrl: string = '/api/auth';
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  validatePass(pass: string) {
    var result = this.http.post<authResult>(this.authUrl, { pass }, this.httpOptions).map(res => {
      this.setSession(res);
    });
    return result;
  }

  setSession(authResult: authResult) {
    localStorage.setItem(this.tokenLocalStorageKeyName, authResult.accessToken);
    localStorage.setItem(this.expiresInLocalStorageKeyName, JSON.stringify(authResult.expiresIn));
    localStorage.setItem(this.refreshLocalStorageKeyName, authResult.refreshToken);
  }

  logout() {
    localStorage.removeItem(this.tokenLocalStorageKeyName);
    localStorage.removeItem(this.expiresInLocalStorageKeyName);
    localStorage.removeItem(this.refreshLocalStorageKeyName);
  }

  public isLoggedIn() {
    return moment().isBefore(this.getExpiration());
  }

  isLoggedOut() {
    return !this.isLoggedIn();
  }

  getExpiration() {
    const expiration = localStorage.getItem(this.expiresInLocalStorageKeyName);
    const expiresAt = JSON.parse(expiration);
    return moment(expiresAt);
  }

  refreshToken(): Observable<authResult> {
    const refreshTokenUrl = this.authUrl + '/refresh';
    let refreshToken = this.getRefreshToken();
    var result = this.http.post<authResult>(refreshTokenUrl, { refreshToken }, this.httpOptions);
    return result;
  }

  getToken(): string {
    return localStorage.getItem(this.tokenLocalStorageKeyName);
  }

  getRefreshToken() {
    return localStorage.getItem(this.refreshLocalStorageKeyName);
  }
}