import { Injectable } from '@angular/core';
import { createEffect, Actions, ofType } from '@ngrx/effects';
import { fetch } from '@nrwl/angular';

import * as StatusesActions from './statuses.actions';
import { map, tap } from 'rxjs/operators';
import { StatusesEntity } from '@workflow-manager-frontend/states/management/statuses';
import { StatusesClientService } from '@workflow-manager-frontend/shared';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Action } from '@ngrx/store';

@Injectable()
export class StatusesEffects {
  // TODO: Handle UTC format conversion ( with local offset apply )
  loadStatuses$ = createEffect(() =>
    this._actions$.pipe(
      ofType(StatusesActions.loadStatuses),
      fetch({
        run: () => {
          return this._statusesClientService.getStatuses()
            .pipe(
              map(statusesDtoArray => statusesDtoArray.map(statusDto => ({
                  id: statusDto.id,
                  name: statusDto.name,
                  createdAt: new Date(statusDto.createdAt),
                  updatedAt: new Date(statusDto.updatedAt),
                  version: statusDto.version
                } as StatusesEntity))
              ),
              map(statuses => StatusesActions.loadStatusesSuccess({statuses}))
            );
        },

        onError: (action, error) => {
          return StatusesActions.loadStatusesFailure({error});
        },
      })
    )
  );

  createStatus$ = createEffect(() =>
    this._actions$.pipe(
      ofType(StatusesActions.createStatus),
      fetch({
        run: ({name, processId}) => {
          return this._statusesClientService.createStatus({name, processId})
            .pipe(
              tap(() => {
                this._matSnackBar.open('Status creation accepted.');
              }),
              map(acceptedResponse => StatusesActions.createStatusAccepted({
                acceptedResponse,
                name,
                processId,
                processName: 'Sample process Name'
              }))
            );
        },
        onError: (action, error) => {
          return StatusesActions.createStatusFailure({error});
        }
      })
    )
  );


  updateStatus$ = createEffect(() =>
    this._actions$.pipe(
      ofType(StatusesActions.updateStatus),
      fetch({
        run: ({id, name, processId, version}) => {
          return this._statusesClientService.updateStatus(id, {name, processId, version})
            .pipe(
              tap(() => {
                this._matSnackBar.open('Status updating accepted.', null, {
                  horizontalPosition: 'end',
                  verticalPosition: 'bottom',
                  duration: 2500,
                  panelClass: 'snack-bar'
                });
              }),
              map((acceptedResponse) =>
                StatusesActions.updateStatusAccepted({acceptedResponse, name, processId, version})
              )
            );
        },
        onError: (action, error) => {
          return StatusesActions.createStatusFailure({error});
        }
      })
    )
  );

  constructor(private readonly _actions$: Actions,
              private readonly _statusesClientService: StatusesClientService,
              private readonly _matSnackBar: MatSnackBar) {
  }

  ngrxOnInitEffects(): Action {
    return StatusesActions.loadStatuses();
  }
}
