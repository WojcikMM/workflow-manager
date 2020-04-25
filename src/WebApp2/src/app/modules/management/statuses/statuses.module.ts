import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {StatusesListComponent} from './statuses-list/statuses-list.component';
import {RouterModule, Routes} from '@angular/router';
import {SharedModule} from '../../shared';
import {StatusEditFormComponent} from './status-edit-form/status-edit-form.component';
import {ReactiveFormsModule} from '@angular/forms';
import {StatusesService} from './statuses.service';
import {MatCardModule} from '@angular/material/card';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import {MatButtonModule} from '@angular/material/button';
import {MatIconModule} from '@angular/material/icon';
import {MatSelectModule} from '@angular/material/select';
import {MatSnackBarModule} from '@angular/material/snack-bar';
import {MatTooltipModule} from '@angular/material/tooltip';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    component: StatusesListComponent
  }
];


@NgModule({
  declarations: [StatusesListComponent, StatusEditFormComponent],
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
    MatTooltipModule
  ],
  providers: [
    StatusesService
  ]
})
export class StatusesModule {
}
