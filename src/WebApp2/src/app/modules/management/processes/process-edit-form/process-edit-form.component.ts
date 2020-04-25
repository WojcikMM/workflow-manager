import {Component, EventEmitter, Input, Output} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {ProcessDto} from '@workflow-manager/shared';

@Component({
  selector: 'app-process-edit-form',
  templateUrl: './process-edit-form.component.html',
  styleUrls: ['./process-edit-form.component.scss']
})
export class ProcessEditFormComponent {

  @Input()
  set model(value: ProcessDto) {
    this._setupForm(value);
  }

  @Output() submitFormEvent: EventEmitter<ProcessDto> = new EventEmitter<ProcessDto>();
  @Output() closeFormEvent: EventEmitter<void> = new EventEmitter<void>();
  reactForm: FormGroup;
  formTitle: string;

  constructor(private readonly _formBuilder: FormBuilder) {
  }

  private _setupForm(model: ProcessDto) {
    this.formTitle = !!model.id ? 'Edit Process' : 'Add Process';

    this.reactForm = this._formBuilder.group({
      $key: this._formBuilder.control(model.id),
      name: this._formBuilder.control(model.name, Validators.required)
    });
  }
}
