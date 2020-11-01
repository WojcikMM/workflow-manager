import { RouterStateSnapshot } from '@angular/router';
import { RouterStateSerializer } from '@ngxs/router-plugin';
import { CustomRouterStateModel } from './custom-router-state-model';

export class CustomRouterSerializer implements RouterStateSerializer<CustomRouterStateModel> {
  serialize(routerState: RouterStateSnapshot): CustomRouterStateModel {
    const {
      url,
      root: {queryParams}
    } = routerState;

    let {root: route} = routerState;
    while (route.firstChild) {
      route = route.firstChild;
    }

    const {params, data} = route;

    // TODO: MAYBE GET OBJECT.VALUES from GLOBAL_CONST AND PASS IT TO ROUTER DATA?
    // if (route.params.title) {
    //   data.title = route.params.title;
    // }

    return {url, params, queryParams, data};
  }

}

