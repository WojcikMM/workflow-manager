import {Injectable} from '@angular/core';
import {Observable} from 'rxjs';
import {MatSnackBar} from '@angular/material/snack-bar';

class StatusViewModel {
  $key: any;
  name: string;
}

@Injectable()
export class StatusesService {

  readonly statuses$: Observable<StatusViewModel[]>;

  constructor(private readonly _snackbarService: MatSnackBar) {
  }

  setValue(processKey: string) {
    // this._statusesCollectionService.setProcessKey(processKey);
  }

  addOrUpdate(process: StatusViewModel): Promise<void> {

    const message = process.$key ?
      `Process "${process.name}" successfully updated.` :
      `Process "${process.name}" successfully added.`;
    const errorMessage = process.$key ?
      `Could not not update "${process.name}" process. Please try again later.` :
      `Could not not add "${process.name}" process. Please try again later.`;

    // return this._statusesCollectionService.addOrUpdate(process).then(() => {
    //   this._showToast(message);
    // }).catch(() => {
    //   this._showToast(errorMessage);
    // });
    return new Promise<void>(() => {
    });
  }

  remove($key: string) {
    // this._statusesCollectionService.remove($key)
    //   .then(_ => {
    //     this._showToast('Process successfully removed');
    //   }).catch(() => {
    //   this._showToast('Cannot remove this Process. Please try again later.');
    // });
    return new Promise<void>(() => {
    });
  }

  private _showToast(summary: string) {
    this._snackbarService.open(summary, null, {
      horizontalPosition: 'right',
      verticalPosition: 'top',
      duration: 3000
    });
  }
}
