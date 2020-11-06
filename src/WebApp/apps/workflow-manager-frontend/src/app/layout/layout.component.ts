import {Component} from '@angular/core';
import {Observable, of} from 'rxjs';
import { AbilitiesService } from '@workflow-manager-frontend/shared/core';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.scss']
})
export class LayoutComponent {
  isHandset$: Observable<boolean>;
  isAuthorized$ = of(true);

  constructor(abilitiesService: AbilitiesService) {
    this.isHandset$ = abilitiesService.isHandset$;
  }

}
