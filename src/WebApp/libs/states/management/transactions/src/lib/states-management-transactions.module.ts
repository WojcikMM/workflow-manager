import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';
import * as fromTransactions from './+state/transactions.reducer';
import { TransactionsEffects } from './+state/transactions.effects';
import { TransactionsFacade } from './+state/transactions.facade';
import { SharedModule } from '@workflow-manager-frontend/shared';

@NgModule({
  imports: [
    CommonModule,
    StoreModule.forFeature(
      fromTransactions.TRANSACTIONS_FEATURE_KEY,
      fromTransactions.reducer
    ),
    EffectsModule.forFeature([TransactionsEffects]),
  ],
  providers: [TransactionsFacade],
  exports: [
    SharedModule
  ]
})
export class StatesManagementTransactionsModule {}
