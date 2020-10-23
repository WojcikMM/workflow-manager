import { ProcessesEntity } from './processes.models';
import { processesAdapter, initialState } from './processes.reducer';
import * as ProcessesSelectors from './processes.selectors';

describe('Processes Selectors', () => {
  const ERROR_MSG = 'No Error Available';
  const getProcessesId = (it) => it.id;
  const createProcessesEntity = (id: string, name = '') =>
    ({
      id,
      name: name || `name-${id}`,
    } as ProcessesEntity);

  let state;

  beforeEach(() => {
    state = {
      processes: processesAdapter.addAll(
        [
          createProcessesEntity('PRODUCT-AAA'),
          createProcessesEntity('PRODUCT-BBB'),
          createProcessesEntity('PRODUCT-CCC'),
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

  describe('Processes Selectors', () => {
    it('getAllProcesses() should return the list of Processes', () => {
      const results = ProcessesSelectors.getAllProcesses(state);
      const selId = getProcessesId(results[1]);

      expect(results.length).toBe(3);
      expect(selId).toBe('PRODUCT-BBB');
    });

    it('getSelected() should return the selected Entity', () => {
      const result = ProcessesSelectors.getSelected(state);
      const selId = getProcessesId(result);

      expect(selId).toBe('PRODUCT-BBB');
    });

    it('getProcessesLoaded() should return the current \'loaded\' status', () => {
      const result = ProcessesSelectors.getProcessesLoaded(state);

      expect(result).toBe(true);
    });

    it('getProcessesError() should return the current \'error\' state', () => {
      const result = ProcessesSelectors.getProcessesError(state);

      expect(result).toBe(ERROR_MSG);
    });
  });
});
