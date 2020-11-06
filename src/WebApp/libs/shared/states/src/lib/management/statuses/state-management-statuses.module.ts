import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgxsModule } from '@ngxs/store';
import { StatusesState } from './statuses.state';
import { StatusesFacade } from './statuses.facade';


@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    NgxsModule.forFeature([StatusesState])
  ],
  providers: [
    StatusesFacade
  ]
})
export class StateManagementStatusesModule {
}
