import { createFeatureSelector, createSelector } from '@ngrx/store';
import {
  PROCESSES_FEATURE_KEY,
  State,
  ProcessesPartialState,
  processesAdapter,
} from './processes.reducer';
import * as fromRouter from '@ngrx/router-store';
import { ProcessesEntity } from './processes.models';
import { GLOBAL_CONST } from '@workflow-manager-frontend/shared';

// Lookup the 'Processes' feature state managed by NgRx
export const getProcessesState = createFeatureSelector<ProcessesPartialState, State>(PROCESSES_FEATURE_KEY);
const {selectAll, selectEntities} = processesAdapter.getSelectors();
// Router selectors
const routerStateSelectors = fromRouter.getSelectors((state) => state[GLOBAL_CONST.FEATURE_STATE_NAMES.ROUTER]);
export const selectRouteProcessId = routerStateSelectors.selectRouteParam('processId');


export const getProcessesError = createSelector(
  getProcessesState,
  (state: State) => state.error
);

export const getAllProcesses = createSelector(
  getProcessesState,
  (state: State) => selectAll(state)
);

export const getProcessesEntities = createSelector(
  getProcessesState,
  (state: State) => selectEntities(state)
);

export const getProcessByPathId = createSelector(
  getProcessesEntities,
  selectRouteProcessId,
  (processesEntities, processId) => processId && processesEntities[processId] as ProcessesEntity
);


