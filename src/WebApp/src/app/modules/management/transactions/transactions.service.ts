import {Injectable} from '@angular/core';
import {Observable} from 'rxjs';
import {MatSnackBar} from '@angular/material/snack-bar';

class TransactionViewModel {
}

@Injectable()
export class TransactionsService {

  readonly transactions$: Observable<TransactionViewModel[]>;


  constructor(private readonly _snackbarService: MatSnackBar) {
  }

  setStatusKey(statusKey: string) {
    // this._statusesCollectionService.setStatusKey(statusKey);
  }

  addOrUpdate(transaction: TransactionViewModel): Promise<void> {

    // const message = transaction.$key ?
    //   `Status "${transaction.name}" successfully updated.` :
    //   `Status "${transaction.name}" successfully added.`;
    // const errorMessage = transaction.$key ?
    //   `Could not not update "${transaction.name}" status. Please try again later.` :
    //   `Could not not add "${transaction.name}" status. Please try again later.`;
    //
    // return this._statusesCollectionService.addOrUpdate(transaction).then(() => {
    //   this._showToast(message);
    // }).catch(() => {
    //   this._showToast(errorMessage);
    // });
    return new Promise<void>(() => {});
  }

  remove($key: string) {
    // this._statusesCollectionService.remove($key)
    //   .then(_ => {
    //     this._showToast('Status successfully removed');
    //   }).catch(() => {
    //   this._showToast('Cannot remove this status. Please try again later.');
    // });
  }

  private _showToast(summary: string) {
    this._snackbarService.open(summary, null, {
      horizontalPosition: 'right',
      verticalPosition: 'top',
      duration: 3000
    });
  }
}
