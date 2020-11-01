import { Action, NgxsOnInit, State, StateContext } from '@ngxs/store';
import { GLOBAL_CONST, StatusesClientService } from '@workflow-manager-frontend/shared/core';
import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import * as fromActions from './statuses.actions';
import { catchError, map, tap } from 'rxjs/operators';
import { createEntityAdapter } from '@ngrx/entity';
import { BackendOperationStatus } from '@workflow-manager-frontend/shared/states';
import { throwError } from 'rxjs';
import { StatusEntity, StatusesStateModel } from './statuses.models';

export const statusesEntityAdapter = createEntityAdapter<StatusEntity>();

@Injectable()
@State<StatusesStateModel>({
  name: GLOBAL_CONST.FEATURE_STATE_NAMES.STATUSES,
  defaults: statusesEntityAdapter.getInitialState({
    pendingOperations: []
  })
})
export class StatusesState implements NgxsOnInit {

  constructor(private readonly _statusesClientService: StatusesClientService,
              private readonly _matSnackbar: MatSnackBar) {
  }

  ngxsOnInit({dispatch}: StateContext<StatusesStateModel>): any {
    dispatch(new fromActions.LoadStatusesAction());
  }

  @Action(fromActions.LoadStatusesAction)
  loadStatuses({patchState, getState}: StateContext<StatusesStateModel>) {
    return this._statusesClientService.getStatuses()
      .pipe(
        map((statuses) => statuses.map(status => ({
          id: status.id,
          name: status.name,
          processId: status.processId,
          createdAt: new Date(status.createdAt),
          updatedAt: new Date(status.updatedAt),
          version: status.version
        } as StatusEntity))),
        tap((statusesEntities) => {
          patchState(statusesEntityAdapter.addMany(statusesEntities, {
            ...getState(),
            error: null
          }));
        }),
        catchError(err => {
          patchState({
            error: err
          });
          return throwError(err);
        })
      );
  }

  @Action(fromActions.CreateStatusAction)
  createStatus({patchState, getState}: StateContext<StatusesStateModel>, {name, processId}: fromActions.CreateStatusAction) {
    return this._statusesClientService.createStatus({name, processId})
      .pipe(
        tap(acceptedResponse => {
          patchState(statusesEntityAdapter.addOne({
            id: acceptedResponse.aggregateId,
            name,
            processId,
            createdAt: new Date(new Date().getUTCDate()),
            updatedAt: new Date(new Date().getUTCDate()),
            version: GLOBAL_CONST.AGGREGATE_INITIAL_VERSION
          }, {
            ...getState(),
            error: null,
            pendingOperations: [
              ...getState().pendingOperations,
              {
                aggregateId: acceptedResponse.aggregateId,
                correlationId: acceptedResponse.correlationId,
                status: BackendOperationStatus.PENDING
              }
            ]
          }));

          this._matSnackbar.open('Status creation accepted.');
        }),
        catchError(err => {
          this._matSnackbar.open('Status creation failure.');
          patchState({
            error: err
          });
          return throwError(err);
        })
      );
  }

  @Action(fromActions.UpdateStatusAction)
  updateStatus({patchState, getState}: StateContext<StatusesStateModel>, {id, name, processId, version}: fromActions.UpdateStatusAction) {
    return this._statusesClientService.updateStatus(id, {name, processId, version})
      .pipe(
        tap(acceptedResponse => {

          const currentStatus = getState().entities[id];

          patchState(statusesEntityAdapter.setOne({
            ...currentStatus,
            name,
            processId,
            version: currentStatus.version + 1
          }, {
            ...getState(),
            error: null,
            pendingOperations: [
              ...getState().pendingOperations,
              {
                aggregateId: acceptedResponse.aggregateId,
                correlationId: acceptedResponse.correlationId,
                status: BackendOperationStatus.PENDING
              }
            ]
          }));

          this._matSnackbar.open('Status update accepted.');
        }),
        catchError(err => {
          this._matSnackbar.open('Status update failure.');
          patchState({
            error: err
          });
          return throwError(err);
        })
      );
  }

}
