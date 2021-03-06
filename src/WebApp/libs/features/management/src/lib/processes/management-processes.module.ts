import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
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
import { ProcessesListComponent } from './processes-list/processes-list.component';
import { ProcessEditFormComponent } from './process-edit-form/process-edit-form.component';
import { FlexModule } from '@angular/flex-layout';
import { StateManagementProcessesModule } from '@workflow-manager-frontend/shared/states';

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
    path: 'preview/:processId',
    component: ProcessEditFormComponent
  }
];

@NgModule({
  declarations: [ProcessesListComponent, ProcessEditFormComponent],
  imports: [
    CommonModule,
    FlexModule,
    RouterModule.forChild(routes),
    ReactiveFormsModule,
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
    StateManagementProcessesModule
  ]
})
export class ManagementProcessesModule {
}
