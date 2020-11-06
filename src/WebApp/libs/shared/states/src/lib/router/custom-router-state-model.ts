import { Params } from '@angular/router';

export interface CustomRouterStateModel {
    url: string;
    params: Params;
    queryParams: Params;
    data: any;
}
