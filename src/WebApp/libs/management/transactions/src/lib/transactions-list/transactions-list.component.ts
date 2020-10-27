import {Component} from '@angular/core';
import {Observable} from 'rxjs';
import {MatDialog} from '@angular/material/dialog';
import {AbilitiesService, SimpleDialogComponent, SimpleDialogData, ProcessDto, ProcessesClientService} from '@workflow-manager-frontend/shared';

class TransactionViewModel {
  $key: string;
}

class StatusViewModel {
}

@Component({
  selector: 'management-transactions-list',
  templateUrl: './transactions-list.component.html',
  styleUrls: ['./transactions-list.component.scss']
})
export class TransactionsListComponent {

  transactionToEdit: TransactionViewModel;
  transactions$: Observable<TransactionViewModel[]>;
  isHandset$: Observable<boolean>;
  statuses$: Observable<StatusViewModel[]>;
  processes$: Observable<ProcessDto[]>;

  constructor(private readonly _processesService: ProcessesClientService,
              private readonly _statusesService: any,
             // private readonly _transactionsService: TransactionsService,
              private readonly _dialog: MatDialog,
              abilitiesService: AbilitiesService) {
    this.isHandset$ = abilitiesService.isHandset$;
   // this.transactions$ = _transactionsService.transactions$;
    this.statuses$ = _statusesService.statuses$;
    this.processes$ = _processesService.getProcesses();
  }


  onAddStatusClicked() {
    const newProcess = {} as TransactionViewModel;
    if (this.transactionToEdit) {
      this._closeEditFormConfirmation(newProcess);
    } else {
      this.transactionToEdit = newProcess;
    }
  }

  onRemoveButtonClicked(status: TransactionViewModel) {
    if (this.transactionToEdit) {
      this._closeEditFormConfirmation(null);
    } else {
      this._dialog.open(SimpleDialogComponent, {
        width: '30em',
        data: {
          title: 'Remove confirmation',
          bodyRows: ['Are you sure to remove this status?'],
          isConfirm: true
        } as SimpleDialogData
      }).afterClosed()
        .subscribe(isConfirmed => {
          if (isConfirmed) {
           // this._transactionsService.remove(status.$key);
          }
        });
    }
  }


  onFormSubmitted(status: TransactionViewModel) {
    // this._transactionsService.addOrUpdate(status)
    //   .then(() => {
    //     this.transactionToEdit = null;
    //   });
  }

  onFormClosed() {
    this.transactionToEdit = null;
  }

  onEditClicked(status: TransactionViewModel) {
    if (this.transactionToEdit) {
      this._closeEditFormConfirmation(status);
    } else {
      this.transactionToEdit = status;
    }
  }

  private _closeEditFormConfirmation(newEditFormValue: TransactionViewModel) {
    this._dialog.open(SimpleDialogComponent, {
      width: '30em',
      data: {
        title: 'Close edit form confirmation',
        bodyRows: [
          'You are actually edit some transaction.',
          ' Are you sure to close edit form without saving changes?'
        ],
        isConfirm: true
      } as SimpleDialogData
    }).afterClosed()
      .subscribe(isConfirmed => {
        if (isConfirmed) {
          this.transactionToEdit = newEditFormValue;
        }
      });
  }

  onProcessSelectionChange(processKey: string) {
    this._statusesService.setValue(processKey);
  }

  onStatusSelectionChange(statusKey: string) {
  //  this._transactionsService.setStatusKey(statusKey);
  }
}
