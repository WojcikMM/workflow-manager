import { Injectable } from '@angular/core';

import { select, Store, Action } from '@ngrx/store';

import * as fromStatuses from './statuses.reducer';
import * as StatusesSelectors from './statuses.selectors';

@Injectable()
export class StatusesFacade {
  loaded$ = this.store.pipe(select(StatusesSelectors.getStatusesLoaded));
  allStatuses$ = this.store.pipe(select(StatusesSelectors.getAllStatuses));
  selectedStatuses$ = this.store.pipe(select(StatusesSelectors.getSelected));

  constructor(private store: Store<fromStatuses.StatusesPartialState>) {}

  dispatch(action: Action) {
    this.store.dispatch(action);
  }
}
