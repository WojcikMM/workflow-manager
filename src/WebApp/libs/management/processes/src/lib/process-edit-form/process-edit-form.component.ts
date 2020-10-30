import { Component, ViewChild } from '@angular/core';
import { MatVerticalStepper } from '@angular/material/stepper';
import { ProcessesEntity, ProcessesFacade } from '@workflow-manager-frontend/states/management/processes';
import { FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'management-processes-edit-form',
  templateUrl: './process-edit-form.component.html',
  styleUrls: ['./process-edit-form.component.scss']
})
export class ProcessEditFormComponent {
  public readonly nameFormControl: FormControl;
  public selectedProcess: ProcessesEntity;

  @ViewChild(MatVerticalStepper, {static: true})
  private readonly _matStepperComponent: MatVerticalStepper;

  constructor(private readonly processesFacade: ProcessesFacade,
              private readonly router: Router) {
    this.nameFormControl = new FormControl('', Validators.required);
    processesFacade.processById$.subscribe((process) => {
      this.selectedProcess = process;
      if (process) {
        this.nameFormControl.setValue(process.name);
        setTimeout(() => {
          this._matStepperComponent.next();
        }, 1);
      }
    });
  }

  onUpdateProcess(id: string, processName: string, version: number) {
    this.processesFacade.updateProcess(id, processName, version);
    this.redirectToList();
  }

  onCreateProcess(processName: string) {
    this.processesFacade.createProcess(processName);
    this.redirectToList();
  }

  redirectToList() {
    this.router.navigateByUrl('/management/processes')
      .catch(console.log);
  }
}
