import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BaseAcceptedResponseDto, BaseClientService } from '../base';
import { GLOBAL_CONFIG } from '../../consts';
import { StatusDto } from './status.dto';

@Injectable({
  providedIn: 'root'
})
export class StatusesClientService extends BaseClientService {

  constructor(httpClient: HttpClient) {
    super(httpClient, GLOBAL_CONFIG.SERVICES.CONFIGURATION_SERVICE, 'statuses');
  }


  getStatuses() {
    return this._httpClient.get<StatusDto[]>(this.baseUrl);
  }

  createStatus(param: { name: string, processId: string }) {
    return this._httpClient.post<BaseAcceptedResponseDto>(this.baseUrl, param);
  }

  updateStatus(statusId: string, param: { name: string, processId: string, version: number }) {
    return this._httpClient.patch<BaseAcceptedResponseDto>(`${this.baseUrl}/${statusId}`, param);
  }
}

