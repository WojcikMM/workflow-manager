import { Injectable } from '@angular/core';
import { Select, Store } from '@ngxs/store';
import { Observable } from 'rxjs';
import { StatusEntity } from './statuses.models';
import * as fromActions from './statuses.actions';
import { StatusesSelectors } from './statuses.selectors';
import { Navigate } from '@ngxs/router-plugin';

@Injectable()
export class StatusesFacade {

  constructor(private readonly store: Store) {
  }

  @Select(StatusesSelectors.allStatuses)
  readonly allStatuses$: Observable<StatusEntity[]>;

  @Select(StatusesSelectors.selectedStatusByRouter)
  readonly selectedStatus$: Observable<(paramName: string) => StatusEntity>;

  @Select(StatusesSelectors.lastError)
  readonly lastError$: Observable<Error>;


  loadProcesses() {
    this.store.dispatch(new fromActions.LoadStatusesAction());
  }

  createProcess(name: string, processId: string) {
    this.store.dispatch(new fromActions.CreateStatusAction(name, processId));
  }

  updateProcess(id: string, newName: string, newProcessId: string, version: number) {
    this.store.dispatch(new fromActions.UpdateStatusAction(id, newName, newProcessId, version));
  }

  navigateTo(url: string) {
    this.store.dispatch(new Navigate([url]));
  }
}
