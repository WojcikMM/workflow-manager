import {Injectable} from '@angular/core';
import {ActivatedRouteSnapshot, Resolve, RouterStateSnapshot} from '@angular/router';
import {Observable} from 'rxjs';
import {ProcessesClientService} from '@workflow-manager-frontend/shared';
import {map} from 'rxjs/operators';
import { ProcessesEntity } from '@workflow-manager-frontend/management/processes';

@Injectable()
export class ProcessResolverService implements Resolve<ProcessesEntity> {

  constructor(private readonly _processesClientService: ProcessesClientService) {
  }

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<ProcessesEntity> {
    const processId = route.paramMap.get('id');
    return this._processesClientService.getProcessById(processId)
      .pipe(map(processDto => {
        return {
          id: processId,
          name: processDto.name,
          createdAt: new Date(processDto.createdAt).toLocaleDateString(),
          updatedAt: new Date(processDto.updatedAt).toLocaleDateString(),
          version: processDto.version
        } as ProcessesEntity;
      }));
  }
}
