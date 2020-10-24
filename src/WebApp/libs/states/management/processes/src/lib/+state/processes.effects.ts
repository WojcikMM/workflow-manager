import { Injectable } from '@angular/core';
import { createEffect, Actions, ofType } from '@ngrx/effects';
import { fetch } from '@nrwl/angular';

import * as ProcessesActions from './processes.actions';
import { map, tap } from 'rxjs/operators';
import { ProcessesClientService } from '@workflow-manager-frontend/shared';
import { ProcessesEntity } from './processes.models';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable()
export class ProcessesEffects {
  loadProcesses$ = createEffect(() =>
    this._actions$.pipe(
      ofType(ProcessesActions.loadProcesses),
      fetch({
        run: () => {
        return this._processesClientService.getProcesses()
            .pipe(
              map(processesDtoArray => processesDtoArray.map(processDto => ({
                  id: processDto.id,
                  name: processDto.name,
                  createdAt: processDto.createdAt,
                  updatedAt: processDto.updatedAt,
                  version: processDto.version
                } as ProcessesEntity))
              ),
              map(processes => ProcessesActions.loadProcessesSuccess({processes}))
            );
        },

        onError: (action, error) => {
          return ProcessesActions.loadProcessesFailure({error});
        },
      })
    )
  );

  createProcess$ = createEffect(() =>
    this._actions$.pipe(
      ofType(ProcessesActions.createProcess),
      fetch({
        run: (action) => {
         return this._processesClientService.createProcess({name: action.processName})
            .pipe(
              tap(() => {
                this._matSnackBar.open('Process creation accepted.');
              }),
              map(() => ProcessesActions.createProcessAccepted())
            );
        },
        onError: (action, error) => {
          return ProcessesActions.createProcessFailure({error});
        }
      })
    )
  );


  updateProcess$ = createEffect(() =>
    this._actions$.pipe(
      ofType(ProcessesActions.updateProcess),
      fetch({
        run: (action) => {
         return this._processesClientService.updateProcess(action.processId, {name: action.processName, version: action.version})
            .pipe(
              tap(() => {
                this._matSnackBar.open('Process updating accepted.', null, {
                  horizontalPosition: 'end',
                  verticalPosition: 'bottom',
                  duration: 2500,
                  announcementMessage: 'Dupa',
                  panelClass: 'snack-bar'
                });
              }),
              map(() => ProcessesActions.updateProcessAccepted())
            );
        },
        onError: (action, error) => {
          return ProcessesActions.createProcessFailure({error});
        }
      })
    )
  );

  constructor(private readonly _actions$: Actions,
              private readonly _processesClientService: ProcessesClientService,
              private readonly _matSnackBar: MatSnackBar) {
  }
}
