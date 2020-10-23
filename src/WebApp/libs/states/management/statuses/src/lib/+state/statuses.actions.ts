import { createAction, props } from '@ngrx/store';
import { StatusesEntity } from './statuses.models';

export const loadStatuses = createAction('[Statuses] Load Statuses');

export const loadStatusesSuccess = createAction(
  '[Statuses] Load Statuses Success',
  props<{ statuses: StatusesEntity[] }>()
);

export const loadStatusesFailure = createAction(
  '[Statuses] Load Statuses Failure',
  props<{ error: any }>()
);
