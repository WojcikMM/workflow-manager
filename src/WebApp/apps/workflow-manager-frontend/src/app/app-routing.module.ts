import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {LayoutComponent} from './layout/layout.component';
import {AuthGuard, UnauthorizedComponent} from '@workflow-manager-frontend/shared';


const routes: Routes = [
  {
    path: 'unauthorized',
    pathMatch: 'full',
    component: UnauthorizedComponent
  },
  {
    path: '',
    component: LayoutComponent,
    canActivate: [AuthGuard],
    children: [
      {
        path: 'management',
        loadChildren: () => import('@workflow-manager-frontend/management/core').then(mod => mod.ManagementCoreModule)
      },
      {
        path: 'applications',
        loadChildren: () => import('./modules/applications/applications.module').then(mod => mod.ApplicationsModule)
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
