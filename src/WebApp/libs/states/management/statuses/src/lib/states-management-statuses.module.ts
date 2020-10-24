import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';
import * as fromStatuses from './+state/statuses.reducer';
import { StatusesEffects } from './+state/statuses.effects';
import { StatusesFacade } from './+state/statuses.facade';
import { SharedModule } from '@workflow-manager-frontend/shared';

@NgModule({
  imports: [
    CommonModule,
    StoreModule.forFeature(
      fromStatuses.STATUSES_FEATURE_KEY,
      fromStatuses.reducer
    ),
    EffectsModule.forFeature([StatusesEffects]),
  ],
  providers: [StatusesFacade],
  exports: [
    SharedModule
  ]
})
export class StatesManagementStatusesModule {}
