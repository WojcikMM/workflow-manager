import {Component, EventEmitter, Output} from '@angular/core';
import {SidebarViewModel} from './sidebar.view-model';
import { GLOBAL_CONFIG } from '@workflow-manager-frontend/shared/core';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
})
export class SidebarComponent {
  @Output() linkClicked: EventEmitter<void> = new EventEmitter<void>();
  sidebarItems: SidebarViewModel[] = GLOBAL_CONFIG.SIDEBAR_MENU_CONFIG;
}
