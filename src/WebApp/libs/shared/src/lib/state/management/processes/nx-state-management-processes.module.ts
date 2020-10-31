import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgxsModule } from '@ngxs/store';
import { ProcessesState } from './processes.state';
import { NxProcessesFacade } from './nx-processes-facade';


@NgModule({
  imports: [
    CommonModule,
    NgxsModule.forFeature([ProcessesState])
  ],
  providers: [
    NxProcessesFacade
  ]
})
export class NxStateManagementProcessesModule {
}
