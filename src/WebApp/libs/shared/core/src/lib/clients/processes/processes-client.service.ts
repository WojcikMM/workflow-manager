import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ProcessDto } from './process.dto';
import { CreateProcessDto } from './create-process.dto';
import { UpdateProcessDto } from './update-process.dto';
import { BaseClientService, BaseAcceptedResponseDto } from '../base';
import { GLOBAL_CONFIG } from '../../consts';

@Injectable({
  providedIn: 'root'
})
export class ProcessesClientService extends BaseClientService {

  constructor(httpClient: HttpClient) {
    super(httpClient, GLOBAL_CONFIG.SERVICES.CONFIGURATION_SERVICE, 'processes');
  }

  getProcesses(): Observable<ProcessDto[]> {
    return this._httpClient.get<any>(this.baseUrl);
  }

  getProcessById(id: string) {
    return this._httpClient.get<ProcessDto>(`${this.baseUrl}/${id}`);
  }

  createProcess(body: CreateProcessDto): Observable<BaseAcceptedResponseDto> {
    return this._httpClient.post<BaseAcceptedResponseDto>(this.baseUrl, body);
  }

  updateProcess(id: string, body: UpdateProcessDto): Observable<BaseAcceptedResponseDto> {
    return this._httpClient.patch<BaseAcceptedResponseDto>(`${this.baseUrl}/${id}`, body);
  }
}
