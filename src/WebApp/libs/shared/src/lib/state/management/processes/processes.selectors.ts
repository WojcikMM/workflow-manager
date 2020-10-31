import { Selector } from '@ngxs/store';
import { RouterState, RouterStateModel } from '@ngxs/router-plugin';
import { ProcessesState, ProcessesStateModel } from './processes.state';
import { CustomRouterStateModel } from '../../router';

export class ProcessesSelectors {

  @Selector([ProcessesState])
  static allProcesses({loadedProcesses}: ProcessesStateModel) {
    return Object.values(loadedProcesses);
  }

  @Selector([ProcessesState])
  static lastError({error}: ProcessesStateModel) {
    return error;
  }

  @Selector([ProcessesState, RouterState])
  static selectedProcessByRouter({loadedProcesses}: ProcessesStateModel, {state}: RouterStateModel<CustomRouterStateModel>) {
    return (paramName: string) => {
      const paramValue = paramName && state.params[paramName];
      return paramValue && loadedProcesses[paramValue] || null;
    };
  }
}
