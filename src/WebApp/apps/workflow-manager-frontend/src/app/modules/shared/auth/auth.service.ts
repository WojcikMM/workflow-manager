import { Injectable } from '@angular/core';
import { User, UserManager } from 'oidc-client';
import { environment } from '../../../../environments/environment';
import { Observable, Subject } from 'rxjs';
import { fromPromise } from 'rxjs/internal-compatibility';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  manager: UserManager = new UserManager(environment.authentication);
  userLoadedEvent: Subject<User> = new Subject<User>();

  get isLoggedIn$(): Observable<boolean> {
    return this.getUserInfo$.pipe(
      map(user => !!user)
    );
  }

  get getUserInfo$(): Observable<User> {
    return fromPromise(this.manager.getUser());
  }


  login() {
    return this.manager.signinRedirect();
  }

  logOut() {
    this.manager.signoutRedirect().catch(e => {
      console.log(e);
    });
  }
}
