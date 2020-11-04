import { BackendOperation } from '../../core';

export interface TransactionsEntity {
  id: string;
  name: string;
  createdAt: Date;
  updatedAt: Date;
  version: number;
  processId: string;
}

export interface TransactionsStateModel {
  entities: { [p: string]: TransactionsEntity };
  error?: Error;
  pendingOperations: BackendOperation[];
}
