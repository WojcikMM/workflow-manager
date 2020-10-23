import { Injectable } from '@angular/core';
import { createEffect, Actions, ofType } from '@ngrx/effects';
import { fetch } from '@nrwl/angular';

import * as StatusesActions from './statuses.actions';

@Injectable()
export class StatusesEffects {
  loadStatuses$ = createEffect(() =>
    this.actions$.pipe(
      ofType(StatusesActions.loadStatuses),
      fetch({
        run: () => {
          // Your custom service 'load' logic goes here. For now just return a success action...
          return StatusesActions.loadStatusesSuccess({ statuses: [] });
        },

        onError: (action, error) => {
          console.error('Error', error);
          return StatusesActions.loadStatusesFailure({ error });
        },
      })
    )
  );

  constructor(private actions$: Actions) {}
}
