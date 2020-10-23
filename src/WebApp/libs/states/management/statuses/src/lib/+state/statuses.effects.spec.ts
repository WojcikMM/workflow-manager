import { TestBed } from '@angular/core/testing';

import { Observable } from 'rxjs';

import { provideMockActions } from '@ngrx/effects/testing';
import { provideMockStore } from '@ngrx/store/testing';

import { NxModule, DataPersistence } from '@nrwl/angular';
import { hot } from '@nrwl/angular/testing';

import { StatusesEffects } from './statuses.effects';
import * as StatusesActions from './statuses.actions';

describe('StatusesEffects', () => {
  let actions: Observable<any>;
  let effects: StatusesEffects;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [NxModule.forRoot()],
      providers: [
        StatusesEffects,
        DataPersistence,
        provideMockActions(() => actions),
        provideMockStore(),
      ],
    });

    effects = TestBed.get(StatusesEffects);
  });

  describe('loadStatuses$', () => {
    it('should work', () => {
      actions = hot('-a-|', {a: StatusesActions.loadStatuses()});

      const expected = hot('-a-|', {
        a: StatusesActions.loadStatusesSuccess({statuses: []}),
      });

      expect(effects.loadStatuses$).toBeObservable(expected);
    });
  });
});
