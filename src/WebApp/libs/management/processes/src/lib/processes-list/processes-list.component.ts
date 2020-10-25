import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { ProcessesEntity, ProcessesFacade } from '@workflow-manager-frontend/states/management/processes';


@Component({
  selector: 'management-processes-list',
  templateUrl: './processes-list.component.html',
  styleUrls: ['./processes-list.component.scss']
})
export class ProcessesListComponent {
  readonly processes$: Observable<ProcessesEntity[]>;

  constructor(processesFacade: ProcessesFacade) {
    this.processes$ = processesFacade.allProcesses$;
  }
}
