import {Injectable} from '@angular/core';
import {HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from '@angular/common/http';
import {from, Observable} from 'rxjs';
import {OAuthService} from 'angular-oauth2-oidc';
import {switchMap} from 'rxjs/operators';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(private readonly _oauthService: OAuthService) {
  }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    const refreshTokenReq = this._oauthService.hasValidAccessToken() ?
      Promise.resolve(this._oauthService.getAccessToken()) :
      this._oauthService.refreshToken().then(res => res.access_token);

    return from(refreshTokenReq)
      .pipe(switchMap(token => {
        const req = this._addAuthorizationHeader(request, token);
        return next.handle(req);
      }));
  }


  private _addAuthorizationHeader(request: HttpRequest<unknown>, token: string): HttpRequest<unknown> {
    console.log(`intercepted request to: ${request.url}`);
    return request.clone({
      headers: request.headers.set('Authorization', `Bearer ${token}`)
    });
  }
}
