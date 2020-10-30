import { Injectable } from '@angular/core';

import { select, Store } from '@ngrx/store';

import * as ProcessesActions from './processes.actions';
import * as fromProcesses from './processes.reducer';
import * as ProcessesSelectors from './processes.selectors';

@Injectable({
  providedIn: 'root'
})
export class ProcessesFacade {
  allProcesses$ = this.store.pipe(select(ProcessesSelectors.getAllProcesses));
  processById$ = this.store.pipe(select(ProcessesSelectors.getProcessByPathId));

  constructor(private store: Store<fromProcesses.ProcessesPartialState>) {
  }

  loadProcesses() {
    this.store.dispatch(ProcessesActions.loadProcesses());
  }

  createProcess(processName: string) {
    this.store.dispatch(ProcessesActions.createProcess({processName}));
  }

  updateProcess(processId: string, processName: string, version: number) {
    this.store.dispatch(ProcessesActions.updateProcess({processName, processId, version}));
  }
}
