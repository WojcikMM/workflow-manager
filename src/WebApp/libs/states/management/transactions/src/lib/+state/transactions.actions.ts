import { createAction, props } from '@ngrx/store';
import { TransactionsEntity } from './transactions.models';

export const loadTransactions = createAction(
  '[Transactions] Load Transactions'
);

export const loadTransactionsSuccess = createAction(
  '[Transactions] Load Transactions Success',
  props<{ transactions: TransactionsEntity[] }>()
);

export const loadTransactionsFailure = createAction(
  '[Transactions] Load Transactions Failure',
  props<{ error: any }>()
);
