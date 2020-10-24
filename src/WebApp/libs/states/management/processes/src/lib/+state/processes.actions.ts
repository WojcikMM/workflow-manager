import { createAction, props } from '@ngrx/store';
import { ProcessesEntity } from './processes.models';

// LOAD PROCESSES
export const loadProcesses = createAction('[Processes] Load Processes');
export const loadProcessesSuccess = createAction('[Processes] Load Processes Success', props<{
  processes: ProcessesEntity[]
}>());
export const loadProcessesFailure = createAction('[Processes] Load Processes Failure', props<{ error: any }>());

// CREATE PROCESS

export const createProcess = createAction('[Processes] Create Process', props<{
  processName: string
}>());
export const createProcessAccepted = createAction('[Processes] Create Process Accepted');
export const createProcessFailure = createAction('[Processes] Create Process Failure', props<{ error: any }>());

// UPDATE PROCESSES
export const updateProcess = createAction('[Processes] Update Process', props<{
  processId: string,
  processName: string,
  version: number
}>());
export const updateProcessAccepted = createAction('[Processes] Update Process Accepted');
export const updateProcessFailure = createAction('[Processes] Update Process Failure', props<{ error: any }>());
