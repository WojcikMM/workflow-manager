import {Injectable} from '@angular/core';
import {Observable, throwError} from 'rxjs';
import {MatSnackBar} from '@angular/material/snack-bar';
import {ProcessDto, ProcessesClientService} from '@workflow-manager-frontend/shared';
import {catchError, tap} from 'rxjs/operators';

@Injectable()
export class ProcessesService {

  readonly processes$: Observable<ProcessDto[]>;

  constructor(private readonly _processesService: ProcessesClientService,
              private readonly _snackbarService: MatSnackBar) {
    this.processes$ = _processesService.getProcesses();

  }

  addOrUpdate(process: ProcessDto): Observable<void> {

    if (!!process.id) {
      return this._updateProcess(process);
    } else {
      return this._addProcess(process);
    }
  }

  private _addProcess(process: ProcessDto) {
    return this._processesService.createProcess({
      name: process.name
    }).pipe(
      tap(() => {
          this._showToast(`Process "${process.name}" successfully added.`);
        },
        catchError(err => {
          this._showToast(`Could not not add "${process.name}" process. Please try again later.`);
          return throwError(err);
        }))
    );
  }

  private _updateProcess(process: ProcessDto) {
    return this._processesService.updateProcess(process.id, {
      name: process.name,
      version: process.version
    }).pipe(
      tap(() => {
        this._showToast(`Process "${process.name}" successfully updated.`);
      }),
      catchError(err => {
        this._showToast(`Could not not update "${process.name}" process. Please try again later.`);
        return throwError(err);
      })
    );
  }

  private _showToast(summary: string) {

    this._snackbarService.open(summary, null, {
      horizontalPosition: 'right',
      verticalPosition: 'top',
      duration: 3000
    });
  }
}
