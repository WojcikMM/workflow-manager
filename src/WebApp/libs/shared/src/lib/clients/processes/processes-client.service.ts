import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../../../apps/workflow-manager-frontend/src/environments/environment';
import { Observable } from 'rxjs';
import { ProcessDto } from './process.dto';
import { CreateProcessDto } from './create-process.dto';
import { UpdateProcessDto } from './update-process.dto';
import { BaseClientService } from '../base';

@Injectable({
  providedIn: 'root'
})
export class ProcessesClientService extends BaseClientService {

  constructor(httpClient: HttpClient) {
    super(httpClient, environment.services.configurationService, 'processes');
  }

  getProcesses(): Observable<ProcessDto[]> {
    return this._httpClient.get<any>(this.baseUrl);
  }

  getProcessById(id: string) {
    return this._httpClient.get<ProcessDto>(`${this.baseUrl}/${id}`);
  }

  createProcess(body: CreateProcessDto): Observable<void> {
    return this._httpClient.post<void>(this.baseUrl, body);
  }

  updateProcess(id: string, body: UpdateProcessDto): Observable<void> {
    return this._httpClient.patch<void>(`${this.baseUrl}/${id}`, body);
  }
}
