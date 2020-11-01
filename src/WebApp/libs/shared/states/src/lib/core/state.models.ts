export enum BackendOperationStatus {
  PENDING = 'PENDING',
  COMPLETE = 'COMPLETE',
  FAILED = 'FAILED'
}

export interface BackendOperation {
  aggregateId: string;
  correlationId: string;
  status: BackendOperationStatus;
}
