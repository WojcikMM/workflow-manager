import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';


const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    redirectTo: 'processes'
  },
  {
    path: 'processes',
    loadChildren: () => import('./processes/management-processes.module').then(mod => mod.ManagementProcessesModule)
  },
  {
    path: 'statuses',
    loadChildren: () => import('./statuses/management-statuses.module').then(mod => mod.ManagementStatusesModule)
  },
  {
    path: 'transactions',
    loadChildren: () => import('./transactions/management-transactions.module').then(mod => mod.ManagementTransactionsModule)
  }
];

@NgModule({
  imports: [CommonModule, RouterModule.forChild(routes)],
})
export class FeaturesManagementModule {
}
