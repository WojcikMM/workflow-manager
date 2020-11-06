import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TransactionsListComponent } from './transactions-list/transactions-list.component';
import { TransactionsEditFormComponent } from './transaction-edit-form/transactions-edit-form.component';
import { RouterModule, Routes } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { ReactiveFormsModule } from '@angular/forms';
import { MatSelectModule } from '@angular/material/select';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatInputModule } from '@angular/material/input';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { SharedCoreModule } from '@workflow-manager-frontend/shared/core';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    component: TransactionsListComponent
  },
  {
    path: 'create',
    component: TransactionsEditFormComponent,
  },
  {
    path: 'preview/:statusId',
    component: TransactionsEditFormComponent
  }
];

@NgModule({
  declarations: [
    TransactionsListComponent,
    TransactionsEditFormComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    SharedCoreModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    MatSelectModule,
    MatSnackBarModule,
    MatInputModule,
    MatTooltipModule,
    MatCheckboxModule
  ]
})
export class ManagementTransactionsModule {
}
