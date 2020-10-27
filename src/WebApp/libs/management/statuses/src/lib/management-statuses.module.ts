import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { SharedModule } from '@workflow-manager-frontend/shared';
import { ReactiveFormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatSelectModule } from '@angular/material/select';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatTooltipModule } from '@angular/material/tooltip';
import { StatusesListComponent } from './statuses-list/statuses-list.component';
import { StatusesEditFormComponent } from './statuses-edit-form/statuses-edit-form.component';
import { StatesManagementStatusesModule } from '@workflow-manager-frontend/states/management/statuses';

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
  declarations: [
    StatusesListComponent,
    StatusesEditFormComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    SharedModule,
    ReactiveFormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    MatSelectModule,
    MatSnackBarModule,
    MatTooltipModule,
    StatesManagementStatusesModule
  ],
})
export class ManagementStatusesModule {
}
