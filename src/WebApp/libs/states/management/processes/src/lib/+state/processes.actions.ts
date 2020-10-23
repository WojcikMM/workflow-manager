import { createAction, props } from '@ngrx/store';
import { ProcessesEntity } from './processes.models';

export const loadProcesses = createAction('[Processes] Load Processes');

export const loadProcessesSuccess = createAction(
  '[Processes] Load Processes Success',
  props<{ processes: ProcessesEntity[] }>()
);

export const loadProcessesFailure = createAction(
  '[Processes] Load Processes Failure',
  props<{ error: any }>()
);
