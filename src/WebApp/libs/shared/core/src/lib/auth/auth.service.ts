import { Injectable } from '@angular/core';
import { User, UserManager } from 'oidc-client';
import { Observable } from 'rxjs';
import { fromPromise } from 'rxjs/internal-compatibility';
import { map } from 'rxjs/operators';
import { Router } from '@angular/router';
import { GLOBAL_CONFIG } from '../consts';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  manager: UserManager = new UserManager(GLOBAL_CONFIG.AUTH_CONFIG);

  constructor(private readonly _router: Router) {
    this.manager.events.addAccessTokenExpiring(() => {
      this.renewToken();
      console.log(`WARNING! ACCESS TOKEN WILL EXPIRE IN ${this.manager.settings.accessTokenExpiringNotificationTime} SECONDS.`);
    });

    this.manager.events.addSilentRenewError(x => {
      console.log('SILENT RENEW ERROR', x);
    });

    this.manager.events.addAccessTokenExpired(() => {
      this.renewToken();
      console.log('WARNING ACCESS TOKEN HAS EXPIRED');
    });
  }

  get isLoggedIn$(): Observable<boolean> {
    return this.getUserInfo$.pipe(
      map(user => !!user)
    );
  }

  get getToken$(): Observable<string> {
    return this.getUserInfo$.pipe(
      map(user => user?.access_token)
    );
  }

  get getUserInfo$(): Observable<User> {
    return fromPromise(this.manager.getUser());
  }

  login(): void {
    this.manager.signinRedirect().catch(e => {
      this._router.navigateByUrl('/unauthorized').catch(console.log);
      console.error('Cannot login redirect. Cause:', e);
    });
  }

  logOut(): void {
    this.manager.signoutRedirect().catch(e => {
      console.log('Cannot logout redirect. Cause:', e);
    });
  }

  renewToken(): void {
    this.manager.signinSilent().catch(err => this._logError('CANNOT RENEW TOKEN. CAUSE:', err));
  }

  private _logError(message: string, e: unknown) {
    console.log(message, e);
  }
}
