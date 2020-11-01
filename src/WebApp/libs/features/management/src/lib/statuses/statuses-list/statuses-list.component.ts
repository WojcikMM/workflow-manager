import { Component } from '@angular/core';
import { StatusEntity, StatusesFacade } from '@workflow-manager-frontend/shared/states';
import { Observable } from 'rxjs';

@Component({
  selector: 'management-statuses-list',
  templateUrl: './statuses-list.component.html',
  styleUrls: ['./statuses-list.component.scss']
})
export class StatusesListComponent {
  statuses$: Observable<StatusEntity[]>;


  constructor(statusesFacade: StatusesFacade) {
    this.statuses$ = statusesFacade.allStatuses$;
  }
}
