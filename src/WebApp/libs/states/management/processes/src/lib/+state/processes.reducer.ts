import { createReducer, on, Action } from '@ngrx/store';
import { EntityState, EntityAdapter, createEntityAdapter } from '@ngrx/entity';

import * as ProcessesActions from './processes.actions';
import { ProcessesEntity } from './processes.models';

export const PROCESSES_FEATURE_KEY = 'processes';

export interface State extends EntityState<ProcessesEntity> {
  pendingCommands: string[];
  error?: string | null; // last known error (if any)
}

export interface ProcessesPartialState {
  readonly [PROCESSES_FEATURE_KEY]: State;
}

export const processesAdapter: EntityAdapter<ProcessesEntity> = createEntityAdapter<ProcessesEntity>();

export const initialState: State = processesAdapter.getInitialState({
  pendingCommands: []
});

const processesReducer = createReducer(
  initialState,
  // Init http action
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
  on(ProcessesActions.createProcessAccepted, (state, {acceptedResponse, processName}) =>
    processesAdapter.addOne({
      id: acceptedResponse.aggregateId,
      name: processName,
      createdAt: new Date(),
      updatedAt: new Date(),
      version: 1
    }, {
      ...state,
      pendingCommands: [...state.pendingCommands, acceptedResponse.correlationId]
    })),
  on(ProcessesActions.updateProcessAccepted, (state, {acceptedResponse, processName, version}) =>
    processesAdapter.setOne({
      ...state.entities[acceptedResponse.aggregateId],
      name: processName,
      version: version++,
    }, {
      ...state,
      pendingCommands: [...state.pendingCommands, acceptedResponse.correlationId]
    }))
);


export function reducer(state: State | undefined, action: Action) {
  return processesReducer(state, action);
}
