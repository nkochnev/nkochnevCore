import { Injectable } from '@angular/core';
import { Router, NavigationEnd, RouterStateSnapshot, ActivatedRouteSnapshot } from '@angular/router';
import { CanActivate } from '@angular/router';
import { AuthService } from './auth.service';
import { HttpParams } from '@angular/common/http';

@Injectable()
export class AuthGuard implements CanActivate {

  constructor(private _authService: AuthService, private router: Router) { }

  canActivate(route: ActivatedRouteSnapshot) {
    if (this._authService.isLoggedIn()) {
      return true;
    } else {
      const url = route.url.join('/');
      this.router.navigate(['enter'], { queryParams: { backUrl: url } });
      return false;
    }
  }
}