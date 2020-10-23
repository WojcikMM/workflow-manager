import { Injectable } from '@angular/core';

import { select, Store, Action } from '@ngrx/store';

import * as fromTransactions from './transactions.reducer';
import * as TransactionsSelectors from './transactions.selectors';

@Injectable()
export class TransactionsFacade {
  loaded$ = this.store.pipe(
    select(TransactionsSelectors.getTransactionsLoaded)
  );
  allTransactions$ = this.store.pipe(
    select(TransactionsSelectors.getAllTransactions)
  );
  selectedTransactions$ = this.store.pipe(
    select(TransactionsSelectors.getSelected)
  );

  constructor(
    private store: Store<fromTransactions.TransactionsPartialState>
  ) {}

  dispatch(action: Action) {
    this.store.dispatch(action);
  }
}
