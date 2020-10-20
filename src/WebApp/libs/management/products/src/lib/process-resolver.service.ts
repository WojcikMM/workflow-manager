import {Injectable} from '@angular/core';
import {ActivatedRouteSnapshot, Resolve, RouterStateSnapshot} from '@angular/router';
import {ProcessViewModel} from './process.view-model';
import {Observable} from 'rxjs';
import {ProcessesClientService} from '@workflow-manager-frontend/shared';
import {map} from 'rxjs/operators';

@Injectable()
export class ProcessResolverService implements Resolve<ProcessViewModel> {

  constructor(private readonly _processesClientService: ProcessesClientService) {
  }

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<ProcessViewModel> {
    const processId = route.paramMap.get('id');
    return this._processesClientService.getProcessById(processId)
      .pipe(map(processDto => {
        return {
          id: processId,
          name: processDto.name,
          createdAt: new Date(processDto.createdAt).toLocaleDateString(),
          updatedAt: new Date(processDto.updatedAt).toLocaleDateString(),
          version: processDto.version
        } as ProcessViewModel;
      }));
  }
}
