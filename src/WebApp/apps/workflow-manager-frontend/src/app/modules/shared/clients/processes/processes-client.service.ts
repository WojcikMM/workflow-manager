import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../../../../environments/environment';
import {Observable, throwError} from 'rxjs';
import {ProcessDto} from './process.dto';
import {CreateProcessDto} from './create-process.dto';
import {UpdateProcessDto} from './update-process.dto';
import {catchError} from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ProcessesClientService {

  private readonly baseUrl = environment.services.processes;

  constructor(private readonly _httpClient: HttpClient) {
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
