import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatTooltipModule } from '@angular/material/tooltip';
import { StatusesListComponent } from './statuses-list/statuses-list.component';
import { StatusesEditFormComponent } from './statuses-edit-form/statuses-edit-form.component';
import { MatStepperModule } from '@angular/material/stepper';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatDialogModule } from '@angular/material/dialog';
import { MatSelectModule } from '@angular/material/select';
import { StateManagementProcessesModule, StateManagementStatusesModule } from '@workflow-manager-frontend/shared/states';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    component: StatusesListComponent
  },
  {
    path: 'create',
    component: StatusesEditFormComponent,
  },
  {
    path: 'preview/:statusId',
    component: StatusesEditFormComponent
  }
];

@NgModule({
  declarations: [StatusesListComponent, StatusesEditFormComponent],
  imports: [
    CommonModule,
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
    StateManagementProcessesModule,
    StateManagementStatusesModule,
    MatSelectModule
  ]
})
export class ManagementStatusesModule {
}
