import {Component, Input} from '@angular/core';
import {Observable} from 'rxjs';
import {FormControl} from '@angular/forms';
import { ProcessDto, ProcessesClientService } from '@workflow-manager-frontend/shared/core';

@Component({
  selector: 'app-process-info-step',
  templateUrl: './process-info-step.component.html',
  styleUrls: ['./process-info-step.component.scss']
})
export class ProcessInfoStepComponent {
  @Input() processControl: FormControl;
  processes$: Observable<ProcessDto[]>;

  constructor(processesService: ProcessesClientService) {
    this.processes$ = processesService.getProcesses();
  }

}
