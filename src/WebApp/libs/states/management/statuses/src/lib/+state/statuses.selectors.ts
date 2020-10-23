import { createFeatureSelector, createSelector } from '@ngrx/store';
import {
  STATUSES_FEATURE_KEY,
  State,
  StatusesPartialState,
  statusesAdapter,
} from './statuses.reducer';

// Lookup the 'Statuses' feature state managed by NgRx
export const getStatusesState = createFeatureSelector<
  StatusesPartialState,
  State
>(STATUSES_FEATURE_KEY);

const { selectAll, selectEntities } = statusesAdapter.getSelectors();

export const getStatusesLoaded = createSelector(
  getStatusesState,
  (state: State) => state.loaded
);

export const getStatusesError = createSelector(
  getStatusesState,
  (state: State) => state.error
);

export const getAllStatuses = createSelector(getStatusesState, (state: State) =>
  selectAll(state)
);

export const getStatusesEntities = createSelector(
  getStatusesState,
  (state: State) => selectEntities(state)
);

export const getSelectedId = createSelector(
  getStatusesState,
  (state: State) => state.selectedId
);

export const getSelected = createSelector(
  getStatusesEntities,
  getSelectedId,
  (entities, selectedId) => selectedId && entities[selectedId]
);
