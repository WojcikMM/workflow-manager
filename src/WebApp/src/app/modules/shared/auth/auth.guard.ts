import {Injectable} from '@angular/core';
import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree} from '@angular/router';
import {Observable} from 'rxjs';
import {OAuthService} from 'angular-oauth2-oidc';
import {JwksValidationHandler} from 'angular-oauth2-oidc-jwks';
import {environment} from '../../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private readonly _oauthService: OAuthService,
              private readonly _router: Router) {
    this._oauthService.configure(environment.authentication);
    this._oauthService.tokenValidationHandler = new JwksValidationHandler();
  }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    return this._oauthService.loadDiscoveryDocumentAndLogin()
      .then(result => {
        return result || this._redirectToServiceUnavailable();
      }).catch(() => {
        return this._redirectToServiceUnavailable();
      });
  }

  private _redirectToServiceUnavailable() {
    return this._router.createUrlTree(['/unauthorized']);
  }

}
