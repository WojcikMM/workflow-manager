import { createReducer, on, Action } from '@ngrx/store';
import { EntityState, EntityAdapter, createEntityAdapter } from '@ngrx/entity';

import * as ProcessesActions from './processes.actions';
import { ProcessesEntity } from './processes.models';

export const PROCESSES_FEATURE_KEY = 'processes';

export interface State extends EntityState<ProcessesEntity> {
  error?: string | null; // last known error (if any)
}

export interface ProcessesPartialState {
  readonly [PROCESSES_FEATURE_KEY]: State;
}

export const processesAdapter: EntityAdapter<ProcessesEntity> = createEntityAdapter<ProcessesEntity>();

export const initialState: State = processesAdapter.getInitialState({});

const processesReducer = createReducer(
  initialState,
  // Init loading / action
  on(ProcessesActions.loadProcesses,
    ProcessesActions.createProcess,
    ProcessesActions.updateProcess,
    (state) => ({
      ...state,
      error: null,
    })),
  // Failures
  on(ProcessesActions.loadProcessesFailure,
    ProcessesActions.createProcessFailure,
    ProcessesActions.updateProcessFailure,
    (state, {error}) => ({
      ...state,
      error,
    })),

  // Successes
  on(ProcessesActions.loadProcessesSuccess, (state, {processes}) =>
    processesAdapter.setAll(processes, {...state})),

  // Request Accepted
  on(ProcessesActions.createProcessAccepted, (state) => ({...state})),
  on(ProcessesActions.updateProcessAccepted, (state) => ({...state}))
);

export function reducer(state: State | undefined, action: Action) {
  return processesReducer(state, action);
}
