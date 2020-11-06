import { Injectable } from '@angular/core';
import { NgxsOnInit, State, StateContext } from '@ngxs/store';
import { GLOBAL_CONST } from '@workflow-manager-frontend/shared/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { TransactionsStateModel } from './transactions.models';

@Injectable()
@State<TransactionsStateModel>({
  name: GLOBAL_CONST.FEATURE_STATE_NAMES.STATUSES,
  defaults: {
    entities: {},
    pendingOperations: []
  }
})
export class TransactionsState implements NgxsOnInit {

  constructor(private readonly _matSnackbar: MatSnackBar) {
  }

  ngxsOnInit(ctx?: StateContext<any>): any {
  }


}
