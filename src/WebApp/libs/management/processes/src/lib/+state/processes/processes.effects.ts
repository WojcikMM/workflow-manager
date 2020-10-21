import { Injectable } from '@angular/core';
import { createEffect, Actions, ofType } from '@ngrx/effects';
import { fetch } from '@nrwl/angular';

import * as ProcessesActions from './processes.actions';
import { ProcessesClientService } from '@workflow-manager-frontend/shared';
import { map } from 'rxjs/operators';
import { ProcessesEntity } from '@workflow-manager-frontend/management/processes';

@Injectable()
export class ProcessesEffects {
  loadProcesses$ = createEffect(() =>
    this._actions$.pipe(
      ofType(ProcessesActions.loadProcesses),
      fetch({
        run: () => {
          this._processesClientService.getProcesses()
            .pipe(
              map(processesDtoArray => processesDtoArray.map(processDto => ({
                  id: processDto.id,
                  name: processDto.name,
                  createdAt: processDto.createdAt,
                  updatedAt: processDto.updatedAt,
                  version: processDto.version
                } as ProcessesEntity))
              ),
              map(processes => ProcessesActions.loadProcessesSuccess({processes}))
            );
        },

        onError: (action, error) => {
          console.error('Error', error);
          return ProcessesActions.loadProcessesFailure({error});
        },
      })
    )
  );

  constructor(private readonly _actions$: Actions,
              private readonly _processesClientService: ProcessesClientService) {
  }
}
