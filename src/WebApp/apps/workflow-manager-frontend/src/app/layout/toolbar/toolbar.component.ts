import {Component, EventEmitter, Input, Output} from '@angular/core';
import {Observable, of} from 'rxjs';
import {OAuthService} from 'angular-oauth2-oidc';

@Component({
  selector: 'app-toolbar',
  templateUrl: './toolbar.component.html',
  styleUrls: ['./toolbar.component.scss']
})
export class ToolbarComponent {

  @Output() toggleSidebarEvent: EventEmitter<void> = new EventEmitter<void>();

  isAuthorized$ = of(true);
  @Input() isHandset$: Observable<boolean>;

  constructor(private readonly _oauthService: OAuthService) {
  }

  signOut() {
    this._oauthService.logOut();
  }
}
