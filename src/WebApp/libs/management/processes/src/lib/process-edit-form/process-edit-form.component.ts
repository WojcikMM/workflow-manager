import { Component, ViewChild } from '@angular/core';
import { MatVerticalStepper } from '@angular/material/stepper';
import { FormControl, Validators } from '@angular/forms';
import { NxProcessesFacade, ProcessEntity } from '@workflow-manager-frontend/shared';
import { map } from 'rxjs/operators';

@Component({
  selector: 'management-processes-edit-form',
  templateUrl: './process-edit-form.component.html',
  styleUrls: ['./process-edit-form.component.scss']
})
export class ProcessEditFormComponent {
  public readonly nameFormControl: FormControl;
  public selectedProcess: ProcessEntity;

  @ViewChild(MatVerticalStepper, {static: true})
  private readonly _matStepperComponent: MatVerticalStepper;

  constructor(private readonly processesFacade: NxProcessesFacade) {
    this.nameFormControl = new FormControl('', Validators.required);

    processesFacade.selectedProcess$
      .pipe(map(paramFun => paramFun('processId')))
      .subscribe((process) => {
        this.selectedProcess = process;
        if (process) {
          this.nameFormControl.setValue(process.name);
          setTimeout(() => {
            this._matStepperComponent.next();
          }, 1);
        }
      });
  }

  onUpdateProcess(id: string, processName: string) {
    this.processesFacade.updateProcess(id, processName);
    this.redirectToList();
  }

  onCreateProcess(processName: string) {
    this.processesFacade.createProcess(processName);
    this.redirectToList();
  }

  redirectToList() {
    this.processesFacade.navigateTo('/management/processes');
  }
}
