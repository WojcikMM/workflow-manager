import {Component} from '@angular/core';
import {Observable} from 'rxjs';
import {ProcessesService} from '../processes.service';
import {MatDialog} from '@angular/material/dialog';
import {AbilitiesService, ProcessDto, SimpleDialogComponent, SimpleDialogData} from '@workflow-manager-frontend/shared';


@Component({
  selector: 'management-processes-list',
  templateUrl: './processes-list.component.html',
  styleUrls: ['./processes-list.component.scss']
})
export class ProcessesListComponent {
  processToEdit: ProcessDto;
  readonly isHandset$: Observable<boolean>;
  readonly processes$: Observable<ProcessDto[]>;

  constructor(private _processesService: ProcessesService,
              private readonly _dialog: MatDialog,
              abilitiesService: AbilitiesService) {
    this.isHandset$ = abilitiesService.isHandset$;
    this.processes$ = _processesService.processes$;
  }

  onAddProcessClicked() {
    const newProcess = {} as ProcessDto;
    if (this.processToEdit) {
      this._closeEditFormConfirmation(newProcess);
    } else {
      this.processToEdit = newProcess;
    }
  }

  onEditClicked(process: ProcessDto) {
    if (this.processToEdit) {
      this._closeEditFormConfirmation(process);
    } else {
      this.processToEdit = process;
    }
  }

  onFormClosed() {
    this.processToEdit = null;
  }

  onFormSubmitted(processViewModel: ProcessDto) {
    this._processesService.addOrUpdate(processViewModel)
      .subscribe(() => {
        this.processToEdit = null;
      });
  }

  onRemoveButtonClicked(process: ProcessDto) {

    if (this.processToEdit) {
      this._closeEditFormConfirmation(null);
    } else {
      this._dialog.open(SimpleDialogComponent, {
        width: '30em',
        data: {
          title: 'Remove confirmation',
          bodyRows: [
            'Are you sure to remove this process?',
            'This operation trigger remove operation on linked statuses.'
          ],
          isConfirm: true
        } as SimpleDialogData
      }).afterClosed().subscribe(isConfirmed => {
        if (isConfirmed) {
          // this._processesService.remove(process.id);
        }
      });
    }
  }

  private _closeEditFormConfirmation(newEditFormValue: ProcessDto) {
    this._dialog.open(SimpleDialogComponent, {
      width: '30em',
      data: {
        title: 'Close edit form confirmation',
        bodyRows: [
          'You are actually edit some process.',
          ' Are you sure to close edit form without saving changes?'
        ],
        isConfirm: true
      } as SimpleDialogData
    }).afterClosed()
      .subscribe(isConfirmed => {
        if (isConfirmed) {
          this.processToEdit = newEditFormValue;
        }
      });
  }
}
