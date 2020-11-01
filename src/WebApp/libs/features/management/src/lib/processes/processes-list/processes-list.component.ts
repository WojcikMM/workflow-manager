import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { ProcessEntity, ProcessesFacade } from '@workflow-manager-frontend/shared/states';


@Component({
  selector: 'management-processes-list',
  templateUrl: './processes-list.component.html',
  styleUrls: ['./processes-list.component.scss']
})
export class ProcessesListComponent {
  readonly processes$: Observable<ProcessEntity[]>;

  constructor(processesFacade: ProcessesFacade) {
    this.processes$ = processesFacade.allProcesses$;
  }
}
