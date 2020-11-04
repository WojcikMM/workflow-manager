import { Injectable } from '@angular/core';
import { Action, NgxsOnInit, State, StateContext } from '@ngxs/store';
import { MatSnackBar } from '@angular/material/snack-bar';
import { throwError } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import * as fromActions from './processes.actions';
import { ProcessEntity, ProcessesStateModel } from './processes.models';
import { GLOBAL_CONST, ProcessesClientService } from '@workflow-manager-frontend/shared/core';
import { BackendOperationStatus } from '../../core';

@State<ProcessesStateModel>({
  name: GLOBAL_CONST.FEATURE_STATE_NAMES.PROCESSES,
  defaults: {
    loadedProcesses: {},
    pendingOperations: [],
    error: null
  }
})
@Injectable()
export class ProcessesState implements NgxsOnInit {

  constructor(private readonly _processesClientService: ProcessesClientService,
              private readonly _matSnackBar: MatSnackBar) {
  }

  ngxsOnInit(ctx?: StateContext<ProcessesStateModel>): any {
    ctx.dispatch(new fromActions.LoadProcessesAction());
  }

  @Action(fromActions.LoadProcessesAction)
  loadProcesses({patchState}: StateContext<ProcessesStateModel>) {
    return this._processesClientService.getProcesses()
      .pipe(
        map(processes => Object.assign({},
          ...processes.map(process => ({
            [process.id]: {
              id: process.id,
              name: process.name,
              createdAt: new Date(process.createdAt),
              updatedAt: new Date(process.updatedAt),
              version: process.version
            } as ProcessEntity
          })))),
        tap(processes => {
          patchState({
            loadedProcesses: processes,
            error: null
          });
        })
      );
  }

  @Action(fromActions.CreateProcessAction)
  createProcess({getState, patchState}: StateContext<ProcessesStateModel>, action: fromActions.CreateProcessAction) {
    return this._processesClientService.createProcess({name: action.name})
      .pipe(
        tap(result => {
          const {loadedProcesses, pendingOperations} = getState();
          patchState({
            error: null,
            loadedProcesses: {
              ...loadedProcesses,
              [result.aggregateId]: {
                id: result.aggregateId,
                name: action.name,
                createdAt: new Date(),
                updatedAt: new Date(),
                version: GLOBAL_CONST.AGGREGATE_INITIAL_VERSION
              }
            },
            pendingOperations: [
              ...pendingOperations,
              {
                aggregateId: result.aggregateId,
                correlationId: result.correlationId,
                status: BackendOperationStatus.PENDING
              }
            ],
          });

          this._matSnackBar.open('Process creation accepted.');
        }),
        catchError(err => {
          patchState({
            error: err
          });
          this._matSnackBar.open('Process creation failed.');
          return throwError(err);
        })
      );
  }

  @Action(fromActions.UpdateProcessAction)
  updateProcess({getState, patchState}: StateContext<ProcessesStateModel>, action: fromActions.UpdateProcessAction) {
    const {version} = getState().loadedProcesses[action.id];
    return this._processesClientService.updateProcess(action.id, {name: action.newName, version})
      .pipe(
        tap(result => {
          const {loadedProcesses, pendingOperations} = getState();
          const currentProcess = loadedProcesses[action.id];
          patchState({
            error: null,
            pendingOperations: [
              ...pendingOperations,
              {
                aggregateId: result.aggregateId,
                correlationId: result.correlationId,
                status: BackendOperationStatus.PENDING
              }
            ],
            loadedProcesses: {
              ...loadedProcesses,
              [action.id]: {
                ...currentProcess,
                name: action.newName,
                updatedAt: new Date(new Date().getUTCDate()),
                version: currentProcess.version + 1
              }
            }
          });

          this._matSnackBar.open('Process update accepted.');
        }),
        catchError(err => {
          patchState({
            error: err
          });
          this._matSnackBar.open('Process update failed.');
          return throwError(err);
        })
      );
  }

}
