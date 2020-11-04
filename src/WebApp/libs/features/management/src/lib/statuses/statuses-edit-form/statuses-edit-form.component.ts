import { Component, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ProcessEntity, ProcessesFacade, StatusEntity, StatusesFacade } from '@workflow-manager-frontend/shared/states';
import { Observable } from 'rxjs';
import { map, mergeMap, tap } from 'rxjs/operators';
import { MatVerticalStepper } from '@angular/material/stepper';

@Component({
  selector: 'management-statuses-edit-form',
  templateUrl: './statuses-edit-form.component.html',
  styleUrls: ['./statuses-edit-form.component.scss']
})
export class StatusesEditFormComponent {
  readonly reactForm: FormGroup;
  readonly allProcesses$: Observable<ProcessEntity[]>;
  readonly selectedStatus$: Observable<StatusEntity>;
  readonly selectedProcessName$: Observable<string>;

  @ViewChild(MatVerticalStepper, {static: true})
  private readonly _matStepperComponent: MatVerticalStepper;

  constructor(processesFacade: ProcessesFacade,
              private readonly _statusesFacade: StatusesFacade) {

    this.reactForm = this._setupForm();

    this.allProcesses$ = processesFacade.allProcesses$;
    this.selectedStatus$ = _statusesFacade.selectedStatus$
      .pipe(
        map(paramFun => paramFun('statusId')),
        tap(status => {
          if (status) {
            this.reactForm.patchValue({
              name: status.name,
              processId: status.processId
            });
            if (!this._matStepperComponent?.selectedIndex) {
              setTimeout(() => {
                this._matStepperComponent.next();
              }, 1);
            }
          }
        })
      );

    this.selectedProcessName$ = this.reactForm.get('processId').valueChanges
      .pipe(
        mergeMap(processId => processesFacade.getProcessById$.pipe(
          map(processIdFun => processIdFun(processId)?.name)
        ))
      );
  }

  onUpdateStatus(statusId: string, statusVersion: number): void {
    const {name, processId} = this.reactForm.value;
    this._statusesFacade.updateStatus(statusId, name, processId, statusVersion);
    this.redirectToList();
  }

  onCreateStatus(): void {
    const {name, processId} = this.reactForm.value;
    this._statusesFacade.createStatus(name, processId);
    this.redirectToList();
  }

  redirectToList(): void {
    this._statusesFacade.navigateTo('/management/statuses');
  }

  private _setupForm(): FormGroup {
    return new FormGroup({
      name: new FormControl('', Validators.required),
      processId: new FormControl('', Validators.required)
    });
  }
}
