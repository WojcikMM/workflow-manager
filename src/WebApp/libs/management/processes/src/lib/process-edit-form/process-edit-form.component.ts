import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { PROCESS_EDIT_FORM_CONST } from './process-edit-form.const';
import { MatVerticalStepper } from '@angular/material/stepper';
import { Observable } from 'rxjs';
import { ProcessesEntity } from '../+state/processes';

@Component({
  selector: 'management-processes-edit-form',
  templateUrl: './process-edit-form.component.html',
  styleUrls: ['./process-edit-form.component.scss']
})
export class ProcessEditFormComponent implements AfterViewInit {
  public reactForm: FormGroup;
  public formTitle: string;
  public processName$: Observable<string>;
  public processViewModel: ProcessesEntity;
  public PROCESS_EDIT_FORM_CONST = PROCESS_EDIT_FORM_CONST;

  @ViewChild(MatVerticalStepper)
  private readonly _matStepperComponent: MatVerticalStepper;

  constructor(private readonly _formBuilder: FormBuilder,
              route: ActivatedRoute) {
    this.processViewModel = route.snapshot.data[PROCESS_EDIT_FORM_CONST.RESOLVER_PROP_NAME] || {} as ProcessesEntity;
    this._setupForm(this.processViewModel);
  }

  private _setupForm(model: ProcessesEntity) {
    this.formTitle = !!model.id ? 'Edit Process' : 'Add Process';

    this.reactForm = this._formBuilder.group({
      [PROCESS_EDIT_FORM_CONST.REACT_FORM_NAMES.NAME]: this._formBuilder.control(model.name, Validators.required)
    });
    this.processName$ = this.reactForm.get(PROCESS_EDIT_FORM_CONST.REACT_FORM_NAMES.NAME).valueChanges;
  }

  ngAfterViewInit(): void {
    if (!!this.processViewModel.id) {
      this._matStepperComponent.next();
    }
  }
}
