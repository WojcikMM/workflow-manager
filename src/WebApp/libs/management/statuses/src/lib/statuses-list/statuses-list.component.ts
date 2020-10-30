import { Component } from '@angular/core';
import { StatusesEntity, StatusesFacade } from '@workflow-manager-frontend/states/management/statuses';
import { Observable } from 'rxjs';

@Component({
  selector: 'management-statuses-list',
  templateUrl: './statuses-list.component.html',
  styleUrls: ['./statuses-list.component.scss']
})
export class StatusesListComponent {
  statuses$: Observable<StatusesEntity[]>;


  constructor(statusesFacade: StatusesFacade) {
    this.statuses$ = statusesFacade.allStatuses$;
  }
}
