import { createFeatureSelector, createSelector } from '@ngrx/store';
import {
  STATUSES_FEATURE_KEY,
  State,
  StatusesPartialState,
  statusesAdapter,
} from './statuses.reducer';
import * as fromRouter from '@ngrx/router-store';
import { GLOBAL_CONST } from '@workflow-manager-frontend/shared';
import { StatusesEntity } from './statuses.models';

// Lookup the 'Statuses' feature state managed by NgRx
export const getStatusesState = createFeatureSelector<StatusesPartialState, State>(STATUSES_FEATURE_KEY);
const {selectAll, selectEntities} = statusesAdapter.getSelectors();
// Router selectors
const routerStateSelectors = fromRouter.getSelectors((state) => state[GLOBAL_CONST.FEATURE_STATE_NAMES.ROUTER]);
export const selectRouteStatusId = routerStateSelectors.selectRouteParam(GLOBAL_CONST.ROUTER_PARAM_NAMES.STATUS_ID);


export const getStatusesError = createSelector(
  getStatusesState,
  (state: State) => state.error
);

export const getAllStatuses = createSelector(
  getStatusesState,
  (state: State) => selectAll(state)
);

export const getStatusesEntities = createSelector(
  getStatusesState,
  (state: State) => selectEntities(state)
);

export const getStatusByPathId = createSelector(
  getStatusesEntities,
  selectRouteStatusId,
  (statusesEntities, statusId) => statusId && statusesEntities[statusId] as StatusesEntity
);
