import { createFeatureSelector, createSelector } from '@ngrx/store';
import {
  TRANSACTIONS_FEATURE_KEY,
  State,
  TransactionsPartialState,
  transactionsAdapter,
} from './transactions.reducer';

// Lookup the 'Transactions' feature state managed by NgRx
export const getTransactionsState = createFeatureSelector<
  TransactionsPartialState,
  State
>(TRANSACTIONS_FEATURE_KEY);

const { selectAll, selectEntities } = transactionsAdapter.getSelectors();

export const getTransactionsLoaded = createSelector(
  getTransactionsState,
  (state: State) => state.loaded
);

export const getTransactionsError = createSelector(
  getTransactionsState,
  (state: State) => state.error
);

export const getAllTransactions = createSelector(
  getTransactionsState,
  (state: State) => selectAll(state)
);

export const getTransactionsEntities = createSelector(
  getTransactionsState,
  (state: State) => selectEntities(state)
);

export const getSelectedId = createSelector(
  getTransactionsState,
  (state: State) => state.selectedId
);

export const getSelected = createSelector(
  getTransactionsEntities,
  getSelectedId,
  (entities, selectedId) => selectedId && entities[selectedId]
);
