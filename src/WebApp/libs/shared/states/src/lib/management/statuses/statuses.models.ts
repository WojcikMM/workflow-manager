import { EntityState } from '@ngrx/entity/src/models';
import { BackendOperation } from '@workflow-manager-frontend/shared/states';

export interface StatusEntity {
  id: string;
  name: string;
  createdAt: Date;
  updatedAt: Date;
  version: number;
  processId: string;
}

export interface StatusesStateModel extends EntityState<StatusEntity> {
  error?: Error;
  pendingOperations: BackendOperation[];
}
