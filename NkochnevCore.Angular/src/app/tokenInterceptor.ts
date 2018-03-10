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

    request = this.addToken(request, this._authService.getToken());

    return next.handle(request).catch(error => this.catchError(error, request, next));
  }

  private catchError(error: any, request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    console.log(error.message);

    if (error instanceof HttpErrorResponse) {
      switch ((<HttpErrorResponse>error).status) {
        case 401:
          return this.handle401Error(request, next);
      }
    } else {
      return Observable.throw(error);
    }
  }

  handle401Error(req: HttpRequest<any>, next: HttpHandler) {
    if (!this.isRefreshingToken) {
      this.isRefreshingToken = true;

      // Reset here so that the following requests wait until the token
      // comes back from the refreshToken call.
      this.tokenSubject.next(null);

      return this._authService.refreshToken()
        .switchMap((newToken: authResult) => {
          if (newToken) {
            this.tokenSubject.next(newToken);
            this._authService.setSession(newToken);
            return next.handle(this.addToken(req, newToken.accessToken));
          }

          // If we don't get a new token, we are in trouble so logout.
          return this.logoutUser();
        })
        .catch(error => {
          // If there is an exception calling 'refreshToken', bad news so logout.
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