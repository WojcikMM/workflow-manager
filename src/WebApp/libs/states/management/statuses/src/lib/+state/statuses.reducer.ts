import { createReducer, on, Action } from '@ngrx/store';
import { EntityState, EntityAdapter, createEntityAdapter } from '@ngrx/entity';

import * as StatusesActions from './statuses.actions';
import { StatusesEntity } from './statuses.models';
import { GLOBAL_CONST } from '@workflow-manager-frontend/shared';

export const STATUSES_FEATURE_KEY = 'statuses';

export interface State extends EntityState<StatusesEntity> {
  pendingCommands: string[];
  error?: string | null; // last known error (if any)
}

export interface StatusesPartialState {
  readonly [STATUSES_FEATURE_KEY]: State;
}

export const statusesAdapter: EntityAdapter<StatusesEntity> = createEntityAdapter<StatusesEntity>();

export const initialState: State = statusesAdapter.getInitialState({
  // set initial required properties
  pendingCommands: []
});

const statusesReducer = createReducer(
  initialState,
  // Init http action
  on(StatusesActions.loadStatuses,
    StatusesActions.createStatus,
    StatusesActions.updateStatus,
    (state) => ({
      ...state,
      error: null,
    })),
  // Failures
  on(StatusesActions.loadStatusesFailure,
    StatusesActions.createStatusFailure,
    StatusesActions.updateStatusFailure,
    (state, {error}) => ({
      ...state,
      error,
    })),

  // Successes
  on(StatusesActions.loadStatusesSuccess, (state, {statuses}) =>
    statusesAdapter.setAll(statuses, {...state})),

  // Request Accepted
  on(StatusesActions.createStatusAccepted, (state, {acceptedResponse, name, processId, processName}) =>
    statusesAdapter.addOne({
      id: acceptedResponse.aggregateId,
      name,
      processId,
      processName,
      createdAt: new Date(),
      updatedAt: new Date(),
      version: GLOBAL_CONST.AGGREGATE_INITIAL_VERSION
    }, {
      ...state,
      pendingCommands: [...state.pendingCommands, acceptedResponse.correlationId]
    })),
  on(StatusesActions.updateStatusAccepted, (state, {acceptedResponse, name, version}) =>
    statusesAdapter.setOne({
      ...state.entities[acceptedResponse.aggregateId],
      name,
      version: version++,
    }, {
      ...state,
      pendingCommands: [...state.pendingCommands, acceptedResponse.correlationId]
    }))
);

export function reducer(state: State | undefined, action: Action) {
  return statusesReducer(state, action);
}
