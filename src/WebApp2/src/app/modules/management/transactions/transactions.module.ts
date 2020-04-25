import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {TransactionsListComponent} from './transactions-list/transactions-list.component';
import {TransactionEditFormComponent} from './transaction-edit-form/transaction-edit-form.component';
import {MatButtonModule} from '@angular/material/button';
import {MatIconModule} from '@angular/material/icon';
import {MatCardModule} from '@angular/material/card';
import {MatFormFieldModule} from '@angular/material/form-field';
import {ReactiveFormsModule} from '@angular/forms';
import {MatSelectModule} from '@angular/material/select';
import {RouterModule, Routes} from '@angular/router';
import {TransactionsService} from './transactions.service';
import {SharedModule} from '../../shared';
import {MatSnackBarModule} from '@angular/material/snack-bar';
import {MatInputModule} from '@angular/material/input';
import {MatTooltipModule} from '@angular/material/tooltip';
import {MatCheckboxModule} from '@angular/material/checkbox';
import {StatusesService} from '../statuses/statuses.service';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    component: TransactionsListComponent
  }
];

@NgModule({
  declarations: [TransactionsListComponent, TransactionEditFormComponent],
    imports: [
        CommonModule,
        RouterModule.forChild(routes),
        SharedModule,
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
    ],
  providers: [
    TransactionsService,
    StatusesService
  ]
})
export class TransactionsModule { }
