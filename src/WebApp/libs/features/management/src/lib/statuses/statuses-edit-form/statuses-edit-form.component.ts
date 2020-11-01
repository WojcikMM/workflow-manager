import { Component } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'management-statuses-edit-form',
  templateUrl: './statuses-edit-form.component.html',
  styleUrls: ['./statuses-edit-form.component.scss']
})
export class StatusesEditFormComponent {
  // TODO: Add edit ability ( router select )

  // reactForm: FormGroup;
  // selectedStatus: StatusesEntity;
  // allProcesses$: Observable<ProcessesEntity[]>;
  // selectedProcessName$: Observable<string>;

  // constructor(private readonly _fb: FormBuilder,
  //             private readonly _router: Router,
  //             private readonly _statusesFacade: StatusesFacade,
  //             processesFacade: ProcessesFacade) {
  //   this.reactForm = this._setupForm();
  //   this.allProcesses$ = processesFacade.allProcesses$;
  //   this.selectedProcessName$ = this._createSelectedProcess$();
  // }


  private _createSelectedProcess$() {
    // return combineLatest([this.allProcesses$, this.reactForm.get('processId').valueChanges])
    //   .pipe(
    //     filter(([_, processId]) => !!processId),
    //     map(([processes, processId]) => {
    //       const result = processes.filter(process => process.id === processId).map(process => process.name);
    //       return result.length ? result[0] : null;
    //     })
    //   );
  }

  private _setupForm(): FormGroup {
    // return this._fb.group({
    //   name: this._fb.control('', Validators.required),
    //   processId: this._fb.control('', Validators.required)
    // });
    return new FormGroup({});
  }

  onUpdateStatus() {
    // const {name, processId} = this.reactForm.value;
    // const {id, version} = this.selectedStatus;
    // this._statusesFacade.updateStatus(id, name, processId, version);
    // this.redirectToList();
  }

  onCreateStatus() {
    // const {name, processId} = this.reactForm.value;
    // this._statusesFacade.createStatus(name, processId);
    // this.redirectToList();
  }

  redirectToList() {
    // this._router.navigateByUrl('/management/statuses');
  }
}
