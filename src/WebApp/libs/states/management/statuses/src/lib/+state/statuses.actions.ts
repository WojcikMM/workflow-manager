import { createAction, props } from '@ngrx/store';
import { StatusesEntity } from './statuses.models';
import { BaseAcceptedResponseDto } from '@workflow-manager-frontend/shared';

// LOAD STATUSES
export const loadStatuses = createAction(
  '[Statuses] Load Statuses');
export const loadStatusesSuccess = createAction(
  '[Statuses] Load Statuses Success', props<{ statuses: StatusesEntity[] }>());
export const loadStatusesFailure = createAction(
  '[Statuses] Load Statuses Failure', props<{ error: any }>());

// CREATE STATUS

export const createStatus = createAction(
  '[Statuses] Create Status', props<{ name: string, processId: string }>());
export const createStatusAccepted = createAction(
  '[Statuses] Create Status Accepted',
  props<{ acceptedResponse: BaseAcceptedResponseDto, name: string, processId: string, processName: string }>());
export const createStatusFailure = createAction(
  '[Statuses] Create Status Failure', props<{ error: any }>());

// UPDATE STATUSES
export const updateStatus = createAction(
  '[Statuses] Update Status', props<{ id: string, name: string, processId: string, version: number }>());
export const updateStatusAccepted = createAction(
  '[Statuses] Update Status Accepted',
  props<{ acceptedResponse: BaseAcceptedResponseDto, name: string, processId: string, version: number }>());
export const updateStatusFailure = createAction(
  '[Statuses] Update Status Failure', props<{ error: any }>());
