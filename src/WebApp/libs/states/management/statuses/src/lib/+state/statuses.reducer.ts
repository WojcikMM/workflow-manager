import { createReducer, on, Action } from '@ngrx/store';
import { EntityState, EntityAdapter, createEntityAdapter } from '@ngrx/entity';

import * as StatusesActions from './statuses.actions';
import { StatusesEntity } from './statuses.models';

export const STATUSES_FEATURE_KEY = 'statuses';

export interface State extends EntityState<StatusesEntity> {
  selectedId?: string | number; // which Statuses record has been selected
  loaded: boolean; // has the Statuses list been loaded
  error?: string | null; // last known error (if any)
}

export interface StatusesPartialState {
  readonly [STATUSES_FEATURE_KEY]: State;
}

export const statusesAdapter: EntityAdapter<StatusesEntity> = createEntityAdapter<
  StatusesEntity
>();

export const initialState: State = statusesAdapter.getInitialState({
  // set initial required properties
  loaded: false,
});

const statusesReducer = createReducer(
  initialState,
  on(StatusesActions.loadStatuses, (state) => ({
    ...state,
    loaded: false,
    error: null,
  })),
  on(StatusesActions.loadStatusesSuccess, (state, { statuses }) =>
    statusesAdapter.addAll(statuses, { ...state, loaded: true })
  ),
  on(StatusesActions.loadStatusesFailure, (state, { error }) => ({
    ...state,
    error,
  }))
);

export function reducer(state: State | undefined, action: Action) {
  return statusesReducer(state, action);
}
