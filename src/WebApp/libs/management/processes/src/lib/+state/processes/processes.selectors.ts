import { createFeatureSelector, createSelector } from '@ngrx/store';
import {
  PROCESSES_FEATURE_KEY,
  State,
  ProcessesPartialState,
  processesAdapter,
} from './processes.reducer';

// Lookup the 'Processes' feature state managed by NgRx
export const getProcessesState = createFeatureSelector<
  ProcessesPartialState,
  State
>(PROCESSES_FEATURE_KEY);

const { selectAll, selectEntities } = processesAdapter.getSelectors();

export const getProcessesLoaded = createSelector(
  getProcessesState,
  (state: State) => state.loaded
);

export const getProcessesError = createSelector(
  getProcessesState,
  (state: State) => state.error
);

export const getAllProcesses = createSelector(getProcessesState, (state: State) =>
  selectAll(state)
);

export const getProcessesEntities = createSelector(
  getProcessesState,
  (state: State) => selectEntities(state)
);

export const getSelectedId = createSelector(
  getProcessesState,
  (state: State) => state.selectedId
);

export const getSelected = createSelector(
  getProcessesEntities,
  getSelectedId,
  (entities, selectedId) => selectedId && entities[selectedId]
);
