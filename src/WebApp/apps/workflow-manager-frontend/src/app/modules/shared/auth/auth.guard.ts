import { Injectable } from '@angular/core';
import { CanActivate } from '@angular/router';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private readonly _oauthService: AuthService) {
  }

  canActivate(): Observable<boolean> {
    return this._oauthService.isLoggedIn$.pipe(
      tap(result => {
        if (!result) {
          this._oauthService.login();
        }
      })
    );
  }

}
