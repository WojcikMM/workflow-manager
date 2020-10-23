import { TransactionsEntity } from './transactions.models';
import {
  transactionsAdapter,
  initialState,
} from './transactions.reducer';
import * as TransactionsSelectors from './transactions.selectors';

describe('Transactions Selectors', () => {
  const ERROR_MSG = 'No Error Available';
  const getTransactionsId = (it) => it.id;
  const createTransactionsEntity = (id: string, name = '') =>
    ({
      id,
      name: name || `name-${id}`,
    } as TransactionsEntity);

  let state;

  beforeEach(() => {
    state = {
      transactions: transactionsAdapter.addAll(
        [
          createTransactionsEntity('PRODUCT-AAA'),
          createTransactionsEntity('PRODUCT-BBB'),
          createTransactionsEntity('PRODUCT-CCC'),
        ],
        {
          ...initialState,
          selectedId: 'PRODUCT-BBB',
          error: ERROR_MSG,
          loaded: true,
        }
      ),
    };
  });

  describe('Transactions Selectors', () => {
    it('getAllTransactions() should return the list of Transactions', () => {
      const results = TransactionsSelectors.getAllTransactions(state);
      const selId = getTransactionsId(results[1]);

      expect(results.length).toBe(3);
      expect(selId).toBe('PRODUCT-BBB');
    });

    it('getSelected() should return the selected Entity', () => {
      const result = TransactionsSelectors.getSelected(state);
      const selId = getTransactionsId(result);

      expect(selId).toBe('PRODUCT-BBB');
    });

    it('getTransactionsLoaded() should return the current \'loaded\' status', () => {
      const result = TransactionsSelectors.getTransactionsLoaded(state);

      expect(result).toBe(true);
    });

    it('getTransactionsError() should return the current \'error\' state', () => {
      const result = TransactionsSelectors.getTransactionsError(state);

      expect(result).toBe(ERROR_MSG);
    });
  });
});
