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
    loadChildren: () => import('@workflow-manager-frontend/management/processes').then(mod => mod.ManagementProcessesModule)
  },
  {
    path: 'statuses',
    loadChildren: () => import('@workflow-manager-frontend/management/processes').then(mod => mod.ManagementProcessesModule)
    // TODO: fix import
  },
  {
    path: 'transactions',
    loadChildren: () => import('@workflow-manager-frontend/management/processes').then(mod => mod.ManagementProcessesModule)
    // TODO: fix import
  }
];


@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ]
})
export class ManagementCoreModule {
}
