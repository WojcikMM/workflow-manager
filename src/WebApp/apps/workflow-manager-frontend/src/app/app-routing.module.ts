import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {LayoutComponent} from './layout/layout.component';
import {AuthGuard, UnauthorizedComponent} from '@workflow-manager-frontend/shared/core';


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
        loadChildren: () => import('@workflow-manager-frontend/features/management').then(mod => mod.FeaturesManagementModule)
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
