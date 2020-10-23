import { Injectable } from '@angular/core';

import { select, Store, Action } from '@ngrx/store';

import * as fromProcesses from './processes.reducer';
import * as ProcessesSelectors from './processes.selectors';

@Injectable()
export class ProcessesFacade {
  loaded$ = this.store.pipe(select(ProcessesSelectors.getProcessesLoaded));
  allProcesses$ = this.store.pipe(select(ProcessesSelectors.getAllProcesses));
  selectedProcesses$ = this.store.pipe(select(ProcessesSelectors.getSelected));

  constructor(private store: Store<fromProcesses.ProcessesPartialState>) {}

  dispatch(action: Action) {
    this.store.dispatch(action);
  }
}
