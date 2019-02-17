import {throwError as observableThrowError,  Observable ,  BehaviorSubject } from 'rxjs';
import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { AuthService } from './services/auth.service';
import { Router } from "@angular/router";
import { catchError, switchMap,finalize,take, filter } from 'rxjs/operators';
import { of } from 'rxjs';
import { authResult } from './models/authresult';

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
    return next.handle(request).pipe(catchError(err => this.handleError(err, request, next)));
  }

  private isRefreshRequest(request: HttpRequest<any>): boolean {
    return request.url.indexOf('auth/refresh') > -1;
  }

  private handleError(error: any, request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (error instanceof HttpErrorResponse) {
      switch ((<HttpErrorResponse>error).status) {
        case 401:
          if (this.isRefreshRequest(request)) {
            this.router.navigate(['enter']);
            return of(null);
          }
          return this.refreshToken(request, next);
        case 500:
          return this.handle500Error(error);
        case 0:
          return this.handle0Error(error);
      }
    } else {
      return observableThrowError(error);
    }
  }

  handle500Error(error: HttpErrorResponse) {
    alert('На сервере проблема, сообщите моему хозяину');
    return observableThrowError(error);
  }

  handle0Error(error: HttpErrorResponse) {
    alert('Сервер недоступен, сообщите моему хозяину');
    return observableThrowError(error);
  }

  refreshToken(req: HttpRequest<any>, next: HttpHandler) {
    if (!this.isRefreshingToken) {
      this.isRefreshingToken = true;

      this.tokenSubject.next(null);

      return this._authService.refreshToken().pipe(switchMap((newToken: authResult) => {
          if (newToken) {
            this.tokenSubject.next(newToken);
            this._authService.setSession(newToken);
            return next.handle(this.addToken(req, newToken.accessToken));
          }

          return this.logoutUser();
        }),
        catchError(error => {
          return this.logoutUser();
        }),
        finalize(() => {
          this.isRefreshingToken = false;
        }))
    } else {
      return this.tokenSubject.pipe(
        filter(token => token != null),
        take(1),
        switchMap(token => {
          return next.handle(this.addToken(req, token.accessToken));
        }));
    }
  }

  private logoutUser() {
    this._authService.logout();
    this.router.navigate(['/'])
    return of(null);
  }

  private addToken(req: HttpRequest<any>, token: string): HttpRequest<any> {
    return req.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`
      }
    });
  }
}