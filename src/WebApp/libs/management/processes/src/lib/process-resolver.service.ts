import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { ProcessesFacade } from '@workflow-manager-frontend/states/management/processes';

@Injectable({
  providedIn: 'root'
})
export class ProcessResolverService implements Resolve<void> {

  constructor(private readonly _processesFacade: ProcessesFacade) {
  }

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): void {
    this._processesFacade.loadProcesses();
  }
}
