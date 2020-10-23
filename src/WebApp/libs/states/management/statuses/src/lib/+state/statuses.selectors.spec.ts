import { StatusesEntity } from './statuses.models';
import { statusesAdapter, initialState } from './statuses.reducer';
import * as StatusesSelectors from './statuses.selectors';

describe('Statuses Selectors', () => {
  const ERROR_MSG = 'No Error Available';
  const getStatusesId = (it) => it.id;
  const createStatusesEntity = (id: string, name = '') =>
    ({
      id,
      name: name || `name-${id}`,
    } as StatusesEntity);

  let state;

  beforeEach(() => {
    state = {
      statuses: statusesAdapter.addAll(
        [
          createStatusesEntity('PRODUCT-AAA'),
          createStatusesEntity('PRODUCT-BBB'),
          createStatusesEntity('PRODUCT-CCC'),
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

  describe('Statuses Selectors', () => {
    it('getAllStatuses() should return the list of Statuses', () => {
      const results = StatusesSelectors.getAllStatuses(state);
      const selId = getStatusesId(results[1]);

      expect(results.length).toBe(3);
      expect(selId).toBe('PRODUCT-BBB');
    });

    it('getSelected() should return the selected Entity', () => {
      const result = StatusesSelectors.getSelected(state);
      const selId = getStatusesId(result);

      expect(selId).toBe('PRODUCT-BBB');
    });

    it('getStatusesLoaded() should return the current \'loaded\' status', () => {
      const result = StatusesSelectors.getStatusesLoaded(state);

      expect(result).toBe(true);
    });

    it('getStatusesError() should return the current \'error\' state', () => {
      const result = StatusesSelectors.getStatusesError(state);

      expect(result).toBe(ERROR_MSG);
    });
  });
});
