import { ProcessesEntity } from './processes.models';
import * as ProcessesActions from './processes.actions';
import { State, initialState, reducer } from './processes.reducer';

describe('Processes Reducer', () => {
  const createProcessesEntity = (id: string, name = '') =>
    ({
      id,
      name: name || `name-${id}`,
    } as ProcessesEntity);

  beforeEach(() => {});

  describe('valid Processes actions', () => {
    it('loadProcessesSuccess should return set the list of known Processes', () => {
      const processes = [
        createProcessesEntity('PRODUCT-AAA'),
        createProcessesEntity('PRODUCT-zzz'),
      ];
      const action = ProcessesActions.loadProcessesSuccess({ processes });

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
