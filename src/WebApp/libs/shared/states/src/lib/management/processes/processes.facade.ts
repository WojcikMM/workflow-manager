import { Injectable } from '@angular/core';
import { Select, Store } from '@ngxs/store';
import { Observable } from 'rxjs';
import { ProcessEntity } from './processes.models';
import * as fromActions from './processes.actions';
import { ProcessesSelectors } from './processes.selectors';
import { Navigate } from '@ngxs/router-plugin';

@Injectable()
export class ProcessesFacade {

  constructor(private readonly store: Store) {
  }

  @Select(ProcessesSelectors.allProcesses)
  readonly allProcesses$: Observable<ProcessEntity[]>;

  @Select(ProcessesSelectors.selectedProcessByRouter)
  readonly selectedProcess$: Observable<(paramName: string) => ProcessEntity>;

  @Select(ProcessesSelectors.lastError)
  readonly lastError$: Observable<Error>;


  loadProcesses() {
    this.store.dispatch(new fromActions.LoadProcessesAction());
  }

  createProcess(name: string) {
    this.store.dispatch(new fromActions.CreateProcessAction(name));
  }

  updateProcess(id: string, newName: string) {
    this.store.dispatch(new fromActions.UpdateProcessAction(id, newName));
  }

  navigateTo(url: string) {
    this.store.dispatch(new Navigate([url]));
  }
}
