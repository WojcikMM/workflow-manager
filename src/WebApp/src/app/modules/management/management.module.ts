import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {RouterModule, Routes} from '@angular/router';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    redirectTo: 'processes'
  },
  {
    path: 'processes',
    loadChildren: () => import('./processes/processes.module').then(mod => mod.ProcessesModule)
  },
  {
    path: 'statuses',
    loadChildren: () => import('./statuses/statuses.module').then(mod => mod.StatusesModule)
  },
  {
    path: 'transactions',
    loadChildren: () => import('./transactions/transactions.module').then(mod => mod.TransactionsModule)
  }
];


@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ]
})
export class ManagementModule {
}
