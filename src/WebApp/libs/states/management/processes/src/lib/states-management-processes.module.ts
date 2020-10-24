import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';
import * as fromProcesses from './+state/processes.reducer';
import { ProcessesEffects } from './+state/processes.effects';
import { ProcessesFacade } from './+state/processes.facade';
import { SharedModule } from '@workflow-manager-frontend/shared';

@NgModule({
  imports: [
    CommonModule,
    StoreModule.forFeature(
      fromProcesses.PROCESSES_FEATURE_KEY,
      fromProcesses.reducer
    ),
    EffectsModule.forFeature([ProcessesEffects]),
  ],
  providers: [ProcessesFacade],
  exports: [
    SharedModule
  ]
})
export class StatesManagementProcessesModule {}
