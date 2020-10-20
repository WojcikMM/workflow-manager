import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ServiceVersionDto } from './service-version.dto';

export abstract class BaseClientService {

  protected readonly baseUrl: string;
  protected readonly versionUrl: string;

  protected constructor(protected _httpClient: HttpClient,
                        serviceUrl: string,
                        basePostfix: string) {
    this.baseUrl = `${serviceUrl}/${basePostfix}`;
    this.versionUrl = `${serviceUrl}/version`;
  }

  getVersion$(): Observable<ServiceVersionDto> {
    return this._httpClient.get<ServiceVersionDto>(this.versionUrl);
  }

}

