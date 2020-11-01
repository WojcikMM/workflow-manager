import { BackendOperation } from '../../core';

export interface ProcessEntity {
  id: string;
  name: string;
  createdAt: Date;
  updatedAt: Date;
  version: number;
}

export interface ProcessesStateModel {
  loadedProcesses: { [p: string]: ProcessEntity };
  error?: Error;
  pendingOperations: BackendOperation[];
}
