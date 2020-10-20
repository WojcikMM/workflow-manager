import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {ProcessesListComponent} from './processes-list/processes-list.component';
import {RouterModule, Routes} from '@angular/router';
import {ProcessEditFormComponent} from './process-edit-form/process-edit-form.component';
import {ReactiveFormsModule} from '@angular/forms';
import {ProcessesService} from './processes.service';
import {SharedModule} from '@workflow-manager-frontend/shared';
import {MatCardModule} from '@angular/material/card';
import {MatButtonModule} from '@angular/material/button';
import {MatSnackBarModule} from '@angular/material/snack-bar';
import {MatGridListModule} from '@angular/material/grid-list';
import {MatTooltipModule} from '@angular/material/tooltip';
import {MatIconModule} from '@angular/material/icon';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import {MatDialogModule} from '@angular/material/dialog';
import {PROCESS_EDIT_FORM_CONST} from './process-edit-form/process-edit-form.const';
import {ProcessResolverService} from './process-resolver.service';
import {MatStepperModule} from '@angular/material/stepper';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    component: ProcessesListComponent
  },
  {
    path: 'preview/:id',
    component: ProcessEditFormComponent,
    resolve: {
      [PROCESS_EDIT_FORM_CONST.RESOLVER_PROP_NAME]: ProcessResolverService
    }
  },
  {
    path: 'create',
    component: ProcessEditFormComponent
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
    MatStepperModule
  ],
  providers: [
    ProcessesService,
    ProcessResolverService
  ]
})
export class ProcessesModule {
}
