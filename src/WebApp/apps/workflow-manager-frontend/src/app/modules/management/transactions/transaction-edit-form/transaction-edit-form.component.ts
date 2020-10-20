import {Component, EventEmitter, Input, Output} from '@angular/core';
import {FormBuilder, FormGroup} from '@angular/forms';
import {BehaviorSubject, Observable} from 'rxjs';
import {ProcessesService} from '../../processes/processes.service';
import {ProcessDto} from '@workflow-manager-frontend/shared';

class TransactionViewModel {
  $key: string;
}

class StatusViewModel {
}

@Component({
  selector: 'app-transaction-edit-form',
  templateUrl: './transaction-edit-form.component.html',
  styleUrls: ['./transaction-edit-form.component.scss']
})
export class TransactionEditFormComponent {

  formTitle: string;
  reactForm: FormGroup;

  @Input()
  set model(value: TransactionViewModel) {
    this._setupForm(value);
  }

  processes$: Observable<ProcessDto[]>;
  statuses$: Observable<StatusViewModel[]>;
  @Output() submitFormEvent: EventEmitter<TransactionViewModel> = new EventEmitter<TransactionViewModel>();
  @Output() closeFormEvent: EventEmitter<void> = new EventEmitter<void>();

  constructor(private _fb: FormBuilder,
              processesService: ProcessesService) {
    this.processes$ = processesService.processes$;
    // this.statuses$ = this.processSubject$.pipe(switchMap(processKey =>
    //   _firestore.collection<StatusViewModel>('statuses', query =>
    //     query.where('processKey', '==', processKey))
    //     .valueChanges({idField: '$key'})
    // ));
  }


  private _setupForm(model: TransactionViewModel): void {
    // this.formTitle = model.$key ? 'Update Transaction' : 'Add Transaction';
    // this.reactForm = this._fb.group({
    //   $key: this._fb.control(model.$key),
    //   isInitial: this._fb.control(model.isInitial || false),
    //   name: this._fb.control(model.name, Validators.required),
    //   processKey: this._fb.control(model.name, Validators.required),
    //   incomingStatusKey: this._fb.control(model.incomingStatusKey, Validators.required),
    //   outgoingStatusKey: this._fb.control(model.outgoingStatusKey, Validators.required),
    // });
    //
    // const incomingStatusKey = this.reactForm.get('incomingStatusKey');
    // if (model.isInitial) {
    //   incomingStatusKey.disable();
    // } else {
    //   incomingStatusKey.enable();
    // }
  }

  onSubmit() {
    // this.statuses$.subscribe(statuses => {
    //   const {$key, name, isInitial, processKey, incomingStatusKey, outgoingStatusKey} = this.reactForm.value;
    //   const incomingStatusName = statuses.find(status => status.$key === incomingStatusKey)?.name;
    //   const outgoingStatusName = statuses.find(status => status.$key === outgoingStatusKey)?.name;
    //   this.submitFormEvent.emit({
    //     $key,
    //     name,
    //     isInitial,
    //     processKey,
    //     incomingStatusKey: isInitial ? null : incomingStatusKey,
    //     incomingStatusName: isInitial ? '-' : incomingStatusName,
    //     outgoingStatusKey,
    //     outgoingStatusName
    //   });
    // });
  }

  onCheckboxChange(checked: boolean) {
    const incomingStatusKey = this.reactForm.get('incomingStatusKey');
    if (checked) {
      incomingStatusKey.reset();
      incomingStatusKey.disable();
    } else {
      incomingStatusKey.enable();
    }
  }
}
