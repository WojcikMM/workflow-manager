import {Component, EventEmitter, Input, Output} from '@angular/core';
import {Observable, of} from 'rxjs';
import { AuthService } from '../../modules/shared/auth/auth.service';

@Component({
  selector: 'app-toolbar',
  templateUrl: './toolbar.component.html',
  styleUrls: ['./toolbar.component.scss']
})
export class ToolbarComponent {

  @Output() toggleSidebarEvent: EventEmitter<void> = new EventEmitter<void>();

  isAuthorized$: Observable<boolean>;
  @Input() isHandset$: Observable<boolean>;

  constructor(private readonly _authService: AuthService) {
    this.isAuthorized$ = _authService.isLoggedIn$;
  }

   signOut() {
     this._authService.logOut();
   }
}
