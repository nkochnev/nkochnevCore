import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { AuthService } from './auth.service';
import { Router } from "@angular/router";

import { Observable } from 'rxjs/Observable';
import { BehaviorSubject } from "rxjs/BehaviorSubject";
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';
import 'rxjs/add/observable/of';
import 'rxjs/add/observable/fromPromise';
import 'rxjs/add/operator/switchMap';
import 'rxjs/add/operator/finally';
import 'rxjs/add/operator/filter';
import 'rxjs/add/operator/take';
import { authResult } from './authresult';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {

  isRefreshingToken: boolean;
  tokenSubject: BehaviorSubject<authResult> = new BehaviorSubject<authResult>(null);

  constructor(private _authService: AuthService, private router: Router) { }
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    if (!this.isRefreshRequest(request) && this._authService.isLoggedOut() && this._authService.getRefreshToken()) {
      return this.refreshToken(request, next);
    }

    request = this.addToken(request, this._authService.getToken());

    return next.handle(request).catch(error => this.catchError(error, request, next));
  }

  private isRefreshRequest(request: HttpRequest<any>): boolean {
    return request.url.indexOf('auth/refresh') > -1;
  }

  private catchError(error: any, request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (this.isRefreshRequest(request)) {
      this.router.navigate(['enter']);
      return Observable.of(null);
    }

    if (error instanceof HttpErrorResponse) {
      switch ((<HttpErrorResponse>error).status) {
        case 401:
          return this.refreshToken(request, next);
        case 500:
          return this.handle500Error(error);
        case 0:
          return this.handle0Error(error);
      }
    } else {
      return Observable.throw(error);
    }
  }

  handle500Error(error: HttpErrorResponse) {
    alert('На сервере проблема, сообщите моему хозяину');
    return Observable.throw(error);
  }

  handle0Error(error: HttpErrorResponse) {
    alert('Сервер недоступен, сообщите моему хозяину');
    return Observable.throw(error);
  }

  refreshToken(req: HttpRequest<any>, next: HttpHandler) {
    if (!this.isRefreshingToken) {
      this.isRefreshingToken = true;

      this.tokenSubject.next(null);

      return this._authService.refreshToken()
        .switchMap((newToken: authResult) => {
          if (newToken) {
            this.tokenSubject.next(newToken);
            this._authService.setSession(newToken);
            return next.handle(this.addToken(req, newToken.accessToken));
          }

          return this.logoutUser();
        })
        .catch(error => {
          return this.logoutUser();
        })
        .finally(() => {
          this.isRefreshingToken = false;
        });
    } else {
      return this.tokenSubject
        .filter(token => token != null)
        .take(1)
        .switchMap(token => {
          return next.handle(this.addToken(req, token.accessToken));
        });
    }
  }

  private logoutUser() {
    this._authService.logout();
    this.router.navigate(['/'])
    return Observable.of(null);
  }

  private addToken(req: HttpRequest<any>, token: string): HttpRequest<any> {
    return req.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`
      }
    });
  }
}