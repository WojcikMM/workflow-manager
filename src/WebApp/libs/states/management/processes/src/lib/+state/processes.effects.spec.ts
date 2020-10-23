import { TestBed } from '@angular/core/testing';

import { Observable } from 'rxjs';

import { provideMockActions } from '@ngrx/effects/testing';
import { provideMockStore } from '@ngrx/store/testing';

import { NxModule, DataPersistence } from '@nrwl/angular';
import { hot } from '@nrwl/angular/testing';

import { ProcessesEffects } from './processes.effects';
import * as ProcessesActions from './processes.actions';

describe('ProcessesEffects', () => {
  let actions: Observable<any>;
  let effects: ProcessesEffects;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [NxModule.forRoot()],
      providers: [
        ProcessesEffects,
        DataPersistence,
        provideMockActions(() => actions),
        provideMockStore(),
      ],
    });

    effects = TestBed.get(ProcessesEffects);
  });

  describe('loadProcesses$', () => {
    it('should work', () => {
      actions = hot('-a-|', {a: ProcessesActions.loadProcesses()});

      const expected = hot('-a-|', {
        a: ProcessesActions.loadProcessesSuccess({processes: []}),
      });

      expect(effects.loadProcesses$).toBeObservable(expected);
    });
  });
});
