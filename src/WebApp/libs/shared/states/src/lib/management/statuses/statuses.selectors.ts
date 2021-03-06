import { Selector } from '@ngxs/store';
import { RouterState, RouterStateModel } from '@ngxs/router-plugin';
import { CustomRouterStateModel } from '../../router';
import { StatusesState } from './statuses.state';
import { StatusesStateModel } from './statuses.models';

export class StatusesSelectors {

  @Selector([StatusesState])
  static allStatuses(stateModel: StatusesStateModel) {
    return Object.values(stateModel.entities);
  }

  @Selector([StatusesState])
  static lastError({error}: StatusesStateModel) {
    return error;
  }

  @Selector([StatusesState, RouterState])
  static selectedStatusByRouter({entities}: StatusesStateModel, {state}: RouterStateModel<CustomRouterStateModel>) {
    return (paramName: string) => {
      const paramValue = paramName && state.params[paramName];
      return paramValue && entities[paramValue] || null;
    };
  }
}
