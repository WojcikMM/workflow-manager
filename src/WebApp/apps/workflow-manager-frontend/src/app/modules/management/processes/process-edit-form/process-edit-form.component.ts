import {AfterViewInit, Component, EventEmitter, Input, OnInit, Output, ViewChild} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {ProcessDto} from '@workflow-manager/shared';
import {ActivatedRoute} from '@angular/router';
import {PROCESS_EDIT_FORM_CONST} from './process-edit-form.const';
import {ProcessViewModel} from '../process.view-model';
import {MatVerticalStepper} from '@angular/material/stepper';

@Component({
  selector: 'app-process-edit-form',
  templateUrl: './process-edit-form.component.html',
  styleUrls: ['./process-edit-form.component.scss']
})
export class ProcessEditFormComponent implements OnInit, AfterViewInit {

  @Output() submitFormEvent: EventEmitter<ProcessDto> = new EventEmitter<ProcessDto>();
  @Output() closeFormEvent: EventEmitter<void> = new EventEmitter<void>();
  @ViewChild(MatVerticalStepper) private readonly _matStepperComponent: MatVerticalStepper;
  reactForm: FormGroup;
  formTitle: string;
  public processViewModel: ProcessViewModel;
  public PROCESS_EDIT_FORM_CONST = PROCESS_EDIT_FORM_CONST;
  public startingStepIndex: number;

  constructor(private readonly _formBuilder: FormBuilder,
              route: ActivatedRoute) {
    this.processViewModel = route.snapshot.data[PROCESS_EDIT_FORM_CONST.RESOLVER_PROP_NAME] || {} as ProcessViewModel;
    this._setupForm(this.processViewModel);
  }

  ngOnInit(): void {

    // this.startingStepIndex = !!this.processViewModel.id ?
    //   PROCESS_EDIT_FORM_CONST.STEP_INDEXES.PREVIEW :
    //   PROCESS_EDIT_FORM_CONST.STEP_INDEXES.DEFINITION;
  }

  private _setupForm(model: ProcessViewModel) {
    this.formTitle = !!model.id ? 'Edit Process' : 'Add Process';

    this.reactForm = this._formBuilder.group({
      [PROCESS_EDIT_FORM_CONST.REACT_FORM_NAMES.ID]: this._formBuilder.control(model.id),
      [PROCESS_EDIT_FORM_CONST.REACT_FORM_NAMES.NAME]: this._formBuilder.control(model.name, Validators.required)
    });
  }

  ngAfterViewInit(): void {
    if (!!this.processViewModel.id) {
      this._matStepperComponent.next();
    }
    // this._matStepperComponent.selectedIndex = !!this.processViewModel.id ?
    //   PROCESS_EDIT_FORM_CONST.STEP_INDEXES.PREVIEW :
    //   PROCESS_EDIT_FORM_CONST.STEP_INDEXES.DEFINITION;

  }
}
