import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { ProcessDto } from '@workflow-manager-frontend/shared';
import { ProcessesFacade } from '@workflow-manager-frontend/states/management/processes';


@Component({
  selector: 'management-processes-list',
  templateUrl: './processes-list.component.html',
  styleUrls: ['./processes-list.component.scss']
})
export class ProcessesListComponent {
  readonly processes$: Observable<ProcessDto[]>;

  constructor(processesFacade: ProcessesFacade) {
    this.processes$ = processesFacade.allProcesses$;
  }
}
