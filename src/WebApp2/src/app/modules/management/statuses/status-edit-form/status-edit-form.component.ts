import {Component, EventEmitter, Input, Output} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {StatusViewModel} from '../../../shared';
import {ProcessDto} from '@workflow-manager/shared';

@Component({
  selector: 'app-status-edit-form',
  templateUrl: './status-edit-form.component.html',
  styleUrls: ['./status-edit-form.component.scss']
})
export class StatusEditFormComponent {

  formTitle: string;
  reactForm: FormGroup;

  @Input()
  set model(value: StatusViewModel) {
    this._setupForm(value);
  }
  @Input() processes: ProcessDto[];

  @Output() submitFormEvent: EventEmitter<StatusViewModel> = new EventEmitter<StatusViewModel>();
  @Output() closeFormEvent: EventEmitter<void> = new EventEmitter<void>();

  constructor(private _fb: FormBuilder) {
  }


  private _setupForm(model: StatusViewModel): void {
    this.formTitle = model.$key ? 'Update Status' : 'Add Status';
    this.reactForm = this._fb.group({
      $key: this._fb.control(model.$key),
      name: this._fb.control(model.name, Validators.required),
      description: this._fb.control(model.description),
      processKey: this._fb.control(model.processKey, Validators.required),
      processName: this._fb.control(model.processName)
    });
  }

  onSubmit() {
    const {$key, name, description, processKey} = this.reactForm.value;
    const processName = this.processes.find(process => process.id === processKey).name;
    this.submitFormEvent.emit({
      $key,
      name,
      description,
      processKey,
      processName
    });
  }
}
