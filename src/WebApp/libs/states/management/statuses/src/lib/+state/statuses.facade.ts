import { Injectable } from '@angular/core';

import { select, Store } from '@ngrx/store';

import * as fromStatuses from './statuses.reducer';
import * as StatusesActions from './statuses.actions';
import * as StatusesSelectors from './statuses.selectors';

@Injectable()
export class StatusesFacade {
  allStatuses$ = this.store.pipe(select(StatusesSelectors.getAllStatuses));
  statusById$ = this.store.pipe(select(StatusesSelectors.getStatusByPathId));

  constructor(private store: Store<fromStatuses.StatusesPartialState>) {
  }

  loadProcesses() {
    this.store.dispatch(StatusesActions.loadStatuses());
  }

  createStatus(name: string, processId: string) {
    this.store.dispatch(StatusesActions.createStatus({processId, name}));
  }

  updateStatus(id: string, name: string, processId: string, version: number) {
    this.store.dispatch(StatusesActions.updateStatus({id, name, processId, version}));
  }

}
