import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgxsModule } from '@ngxs/store';
import { TransactionsState } from './transactions.state';
import { TransactionsFacade } from './transactions.facade';


@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    NgxsModule.forFeature([TransactionsState])
  ],
  providers: [
    TransactionsFacade
  ]
})
export class StateManagementStatusesModule {
}
