import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgxsModule } from '@ngxs/store';
import { ProcessesState } from './processes.state';
import { ProcessesFacade } from './processes.facade';


@NgModule({
  imports: [
    CommonModule,
    NgxsModule.forFeature([ProcessesState])
  ],
  providers: [
    ProcessesFacade
  ]
})
export class StateManagementProcessesModule {
}
