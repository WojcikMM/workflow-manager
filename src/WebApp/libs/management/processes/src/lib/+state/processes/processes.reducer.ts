import { createReducer, on, Action } from '@ngrx/store';
import { EntityState, EntityAdapter, createEntityAdapter } from '@ngrx/entity';

import * as ProcessesActions from './processes.actions';
import { ProcessesEntity } from './processes.models';

export const PROCESSES_FEATURE_KEY = 'processes';

export interface State extends EntityState<ProcessesEntity> {
  selectedId?: string | number; // which Processes record has been selected
  loaded: boolean; // has the Processes list been loaded
  error?: string | null; // last known error (if any)
}

export interface ProcessesPartialState {
  readonly [PROCESSES_FEATURE_KEY]: State;
}

export const processesAdapter: EntityAdapter<ProcessesEntity> = createEntityAdapter<ProcessesEntity>();

export const initialState: State = processesAdapter.getInitialState({
  // set initial required properties
  loaded: false,
});

const processesReducer = createReducer(
  initialState,
  on(ProcessesActions.loadProcesses, (state) => ({
    ...state,
    loaded: false,
    error: null,
  })),
  on(ProcessesActions.loadProcessesSuccess, (state, { processes }) =>
    processesAdapter.setAll(processes, { ...state, loaded: true })
  ),
  on(ProcessesActions.loadProcessesFailure, (state, { error }) => ({
    ...state,
    error,
  }))
);

export function reducer(state: State | undefined, action: Action) {
  return processesReducer(state, action);
}
