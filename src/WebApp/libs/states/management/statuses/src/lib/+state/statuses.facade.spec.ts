import { NgModule } from '@angular/core';
import { TestBed } from '@angular/core/testing';
import { readFirst } from '@nrwl/angular/testing';

import { EffectsModule } from '@ngrx/effects';
import { StoreModule, Store } from '@ngrx/store';

import { NxModule } from '@nrwl/angular';

import { StatusesEntity } from './statuses.models';
import { StatusesEffects } from './statuses.effects';
import { StatusesFacade } from './statuses.facade';

import * as StatusesActions from './statuses.actions';
import {
  STATUSES_FEATURE_KEY,
  State,
  reducer,
} from './statuses.reducer';

interface TestSchema {
  statuses: State;
}

describe('StatusesFacade', () => {
  let facade: StatusesFacade;
  let store: Store<TestSchema>;
  const createStatusesEntity = (id: string, name = '') =>
    ({
      id,
      name: name || `name-${id}`,
    } as StatusesEntity);

  beforeEach(() => {});

  describe('used in NgModule', () => {
    beforeEach(() => {
      @NgModule({
        imports: [
          StoreModule.forFeature(STATUSES_FEATURE_KEY, reducer),
          EffectsModule.forFeature([StatusesEffects]),
        ],
        providers: [StatusesFacade],
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
      facade = TestBed.get(StatusesFacade);
    });

    /**
     * The initially generated facade::loadAll() returns empty array
     */
    it('loadAll() should return empty list with loaded == true', async (done) => {
      try {
        let list = await readFirst(facade.allStatuses$);
        let isLoaded = await readFirst(facade.loaded$);

        expect(list.length).toBe(0);
        expect(isLoaded).toBe(false);

        facade.dispatch(StatusesActions.loadStatuses());

        list = await readFirst(facade.allStatuses$);
        isLoaded = await readFirst(facade.loaded$);

        expect(list.length).toBe(0);
        expect(isLoaded).toBe(true);

        done();
      } catch (err) {
        done.fail(err);
      }
    });

    /**
     * Use `loadStatusesSuccess` to manually update list
     */
    it('allStatuses$ should return the loaded list; and loaded flag == true', async (done) => {
      try {
        let list = await readFirst(facade.allStatuses$);
        let isLoaded = await readFirst(facade.loaded$);

        expect(list.length).toBe(0);
        expect(isLoaded).toBe(false);

        facade.dispatch(
          StatusesActions.loadStatusesSuccess({
            statuses: [
              createStatusesEntity('AAA'),
              createStatusesEntity('BBB'),
            ],
          })
        );

        list = await readFirst(facade.allStatuses$);
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
