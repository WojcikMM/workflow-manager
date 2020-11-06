import { BackendOperation } from '../../core';

export interface StatusEntity {
  id: string;
  name: string;
  createdAt: Date;
  updatedAt: Date;
  version: number;
  processId: string;
}

export interface StatusesStateModel {
  entities: { [p: string]: StatusEntity };
  error?: Error;
  pendingOperations: BackendOperation[];
}
