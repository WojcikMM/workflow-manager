import { StatusesEntity } from './statuses.models';
import * as StatusesActions from './statuses.actions';
import { State, initialState, reducer } from './statuses.reducer';

describe('Statuses Reducer', () => {
  const createStatusesEntity = (id: string, name = '') =>
    ({
      id,
      name: name || `name-${id}`,
    } as StatusesEntity);

  beforeEach(() => {});

  describe('valid Statuses actions', () => {
    it('loadStatusesSuccess should return set the list of known Statuses', () => {
      const statuses = [
        createStatusesEntity('PRODUCT-AAA'),
        createStatusesEntity('PRODUCT-zzz'),
      ];
      const action = StatusesActions.loadStatusesSuccess({ statuses });

      const result: State = reducer(initialState, action);

      expect(result.loaded).toBe(true);
      expect(result.ids.length).toBe(2);
    });
  });

  describe('unknown action', () => {
    it('should return the previous state', () => {
      const action = {} as any;

      const result = reducer(initialState, action);

      expect(result).toBe(initialState);
    });
  });
});
