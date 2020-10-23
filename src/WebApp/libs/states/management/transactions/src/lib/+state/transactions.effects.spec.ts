import { TestBed } from '@angular/core/testing';

import { Observable } from 'rxjs';

import { provideMockActions } from '@ngrx/effects/testing';
import { provideMockStore } from '@ngrx/store/testing';

import { NxModule, DataPersistence } from '@nrwl/angular';
import { hot } from '@nrwl/angular/testing';

import { TransactionsEffects } from './transactions.effects';
import * as TransactionsActions from './transactions.actions';

describe('TransactionsEffects', () => {
  let actions: Observable<any>;
  let effects: TransactionsEffects;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [NxModule.forRoot()],
      providers: [
        TransactionsEffects,
        DataPersistence,
        provideMockActions(() => actions),
        provideMockStore(),
      ],
    });

    effects = TestBed.get(TransactionsEffects);
  });

  describe('loadTransactions$', () => {
    it('should work', () => {
      actions = hot('-a-|', { a: TransactionsActions.loadTransactions() });

      const expected = hot('-a-|', {
        a: TransactionsActions.loadTransactionsSuccess({ transactions: [] }),
      });

      expect(effects.loadTransactions$).toBeObservable(expected);
    });
  });
});
