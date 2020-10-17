import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {LayoutComponent} from './layout/layout.component';
import {AuthGuard } from '@workflow-manager/shared';
import {UnauthorizedComponent} from './unauthorized/unauthorized.component';


const routes: Routes = [
  {
    path: 'unauthorized',
    pathMatch: 'full',
    component: UnauthorizedComponent,
  },
  {
    path: '',
    component: LayoutComponent,
    canActivate: [AuthGuard],
    children: [
      {
        path: 'management',
        loadChildren: () => import('./modules/management/management.module').then(mod => mod.ManagementModule)
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
