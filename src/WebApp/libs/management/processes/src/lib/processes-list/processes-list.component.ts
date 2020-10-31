import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { NxProcessesFacade, ProcessEntity } from '@workflow-manager-frontend/shared';


@Component({
  selector: 'management-processes-list',
  templateUrl: './processes-list.component.html',
  styleUrls: ['./processes-list.component.scss']
})
export class ProcessesListComponent {
  readonly processes$: Observable<ProcessEntity[]>;

  constructor(processesFacade: NxProcessesFacade) {
    this.processes$ = processesFacade.allProcesses$;
  }
}
