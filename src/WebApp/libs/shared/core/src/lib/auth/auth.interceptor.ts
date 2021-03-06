import { Injectable } from '@angular/core';
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from './auth.service';
import { map, mergeMap } from 'rxjs/operators';
import { GLOBAL_CONFIG } from '../consts';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(private readonly _oauthService: AuthService) {
  }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {

    if (this._isInternalCall(request.url)) {
      return this._oauthService.getToken$.pipe(
        map(token => token ? this._addAuthorizationHeader(request, token) : request),
        mergeMap(req => {
          return next.handle(req);
        })
      );
    }
  }


  private _isInternalCall(url: string) {
    return Object.values(GLOBAL_CONFIG.SERVICES).some(serviceBaseUrl => url.includes(serviceBaseUrl));
  }


  private _addAuthorizationHeader(request: HttpRequest<unknown>, token: string): HttpRequest<unknown> {
    console.log(`intercepted request to: ${request.url}`);
    return request.clone({
      headers: request.headers.set('Authorization', `Bearer ${token}`)
    });
  }
}
