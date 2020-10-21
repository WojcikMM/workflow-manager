import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';
import { RouterModule, Routes } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from '@workflow-manager-frontend/shared';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatDialogModule } from '@angular/material/dialog';
import { MatStepperModule } from '@angular/material/stepper';
import { PROCESSES_FEATURE_KEY, reducer, ProcessesEffects } from './+state/processes';
import { ProcessesListComponent } from './processes-list/processes-list.component';
import { ProcessEditFormComponent } from './process-edit-form/process-edit-form.component';
import { PROCESS_EDIT_FORM_CONST } from './process-edit-form/process-edit-form.const';
import { ProcessResolverService } from './process-resolver.service';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    component: ProcessesListComponent
  },
  {
    path: 'create',
    component: ProcessEditFormComponent,
  },
  {
    path: 'preview/:id',
    component: ProcessEditFormComponent,
    resolve: {
      [PROCESS_EDIT_FORM_CONST.RESOLVER_PROP_NAME]: ProcessResolverService
    }
  }
];

@NgModule({
  declarations: [ProcessesListComponent, ProcessEditFormComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    ReactiveFormsModule,
    SharedModule,
    MatCardModule,
    MatButtonModule,
    MatSnackBarModule,
    MatGridListModule,
    MatTooltipModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    MatDialogModule,
    MatStepperModule,
    StoreModule.forFeature(PROCESSES_FEATURE_KEY, reducer),
    EffectsModule.forFeature([ProcessesEffects]),
  ],
})
export class ManagementProcessesModule {
}
