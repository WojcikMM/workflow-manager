import {Component} from '@angular/core';
import {Observable} from 'rxjs';
import {StatusesService} from '../statuses.service';
import {AbilitiesService, SimpleDialogComponent, SimpleDialogData, StatusViewModel} from '../../../shared';
import {MatDialog} from '@angular/material/dialog';
import {MatSelectChange} from '@angular/material/select';
import {ProcessDto, ProcessesClientService} from '@workflow-manager/shared';

@Component({
  selector: 'app-statuses-list',
  templateUrl: './statuses-list.component.html',
  styleUrls: ['./statuses-list.component.scss']
})
export class StatusesListComponent {
  statusToEdit: StatusViewModel;
  statuses$: Observable<StatusViewModel[]>;
  isHandset$: Observable<boolean>;
  processes$: Observable<ProcessDto[]>;

  constructor(private readonly _statusesService: StatusesService,
              private readonly _dialog: MatDialog,
              abilitiesService: AbilitiesService,
              processesCollectionService: ProcessesClientService) {
    this.isHandset$ = abilitiesService.isHandset$;
    this.statuses$ = this._statusesService.statuses$;
    this.processes$ = processesCollectionService.getProcesses();
  }


  onAddStatusClicked() {
    const newProcess = {} as StatusViewModel;
    if (this.statusToEdit) {
      this._closeEditFormConfirmation(newProcess);
    } else {
      this.statusToEdit = newProcess;
    }
  }

  onRemoveButtonClicked(status: StatusViewModel) {
    if (this.statusToEdit) {
      this._closeEditFormConfirmation(null);
    } else {
      this._dialog.open(SimpleDialogComponent, {
        width: '30em',
        data: {
          title: 'Remove confirmation',
          bodyRows: [
            'Are you sure to remove this status?',
            'This operation trigger remove operation on all linked transactions.'
          ],
          isConfirm: true
        } as SimpleDialogData
      }).afterClosed().subscribe(isConfirmed => {
        if (isConfirmed) {
          this._statusesService.remove(status.$key);
        }
      });
    }
  }


  onFormSubmitted(status: StatusViewModel) {
    this._statusesService.addOrUpdate(status)
      .then(() => {
        this.statusToEdit = null;
      });
  }

  onFormClosed() {
    this.statusToEdit = null;
  }

  onEditClicked(status: StatusViewModel) {
    if (this.statusToEdit) {
      this._closeEditFormConfirmation(status);
    } else {
      this.statusToEdit = status;
    }
  }

  private _closeEditFormConfirmation(newEditFormValue: StatusViewModel) {
    this._dialog.open(SimpleDialogComponent, {
      width: '30em',
      data: {
        title: 'Close edit form confirmation',
        bodyRows: [
          'You are actually edit some status.',
          ' Are you sure to close edit form without saving changes?'
        ],
        isConfirm: true
      } as SimpleDialogData
    }).afterClosed()
      .subscribe(isConfirmed => {
        if (isConfirmed) {
          this.statusToEdit = newEditFormValue;
        }
      });
  }

  onProcessSelectionChange(selectChange: MatSelectChange) {
    this._statusesService.setValue(selectChange.value);
  }
}
