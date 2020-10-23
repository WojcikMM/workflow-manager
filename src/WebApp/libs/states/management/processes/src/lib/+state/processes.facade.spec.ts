import { NgModule } from '@angular/core';
import { TestBed } from '@angular/core/testing';
import { readFirst } from '@nrwl/angular/testing';

import { EffectsModule } from '@ngrx/effects';
import { StoreModule, Store } from '@ngrx/store';

import { NxModule } from '@nrwl/angular';

import { ProcessesEntity } from './processes.models';
import { ProcessesEffects } from './processes.effects';
import { ProcessesFacade } from './processes.facade';

import * as ProcessesActions from './processes.actions';
import {
  PROCESSES_FEATURE_KEY,
  State,
  reducer,
} from './processes.reducer';

interface TestSchema {
  processes: State;
}

describe('ProcessesFacade', () => {
  let facade: ProcessesFacade;
  let store: Store<TestSchema>;
  const createProcessesEntity = (id: string, name = '') =>
    ({
      id,
      name: name || `name-${id}`,
    } as ProcessesEntity);

  beforeEach(() => {});

  describe('used in NgModule', () => {
    beforeEach(() => {
      @NgModule({
        imports: [
          StoreModule.forFeature(PROCESSES_FEATURE_KEY, reducer),
          EffectsModule.forFeature([ProcessesEffects]),
        ],
        providers: [ProcessesFacade],
      })
      class CustomFeatureModule {}

      @NgModule({
        imports: [
          NxModule.forRoot(),
          StoreModule.forRoot({}),
          EffectsModule.forRoot([]),
          CustomFeatureModule,
        ],
      })
      class RootModule {}
      TestBed.configureTestingModule({ imports: [RootModule] });

      store = TestBed.get(Store);
      facade = TestBed.get(ProcessesFacade);
    });

    /**
     * The initially generated facade::loadAll() returns empty array
     */
    it('loadAll() should return empty list with loaded == true', async (done) => {
      try {
        let list = await readFirst(facade.allProcesses$);
        let isLoaded = await readFirst(facade.loaded$);

        expect(list.length).toBe(0);
        expect(isLoaded).toBe(false);

        facade.dispatch(ProcessesActions.loadProcesses());

        list = await readFirst(facade.allProcesses$);
        isLoaded = await readFirst(facade.loaded$);

        expect(list.length).toBe(0);
        expect(isLoaded).toBe(true);

        done();
      } catch (err) {
        done.fail(err);
      }
    });

    /**
     * Use `loadProcessesSuccess` to manually update list
     */
    it('allProcesses$ should return the loaded list; and loaded flag == true', async (done) => {
      try {
        let list = await readFirst(facade.allProcesses$);
        let isLoaded = await readFirst(facade.loaded$);

        expect(list.length).toBe(0);
        expect(isLoaded).toBe(false);

        facade.dispatch(
          ProcessesActions.loadProcessesSuccess({
            processes: [
              createProcessesEntity('AAA'),
              createProcessesEntity('BBB'),
            ],
          })
        );

        list = await readFirst(facade.allProcesses$);
        isLoaded = await readFirst(facade.loaded$);

        expect(list.length).toBe(2);
        expect(isLoaded).toBe(true);

        done();
      } catch (err) {
        done.fail(err);
      }
    });
  });
});
