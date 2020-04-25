import {Component, EventEmitter, Output} from '@angular/core';
import {SidebarViewModel} from './sidebar.view-model';
import {environment} from '../../../environments/environment';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
})
export class SidebarComponent {
  @Output() linkClicked: EventEmitter<void> = new EventEmitter<void>();
  sidebarItems: SidebarViewModel[] = environment.sidebarMenu;
}
