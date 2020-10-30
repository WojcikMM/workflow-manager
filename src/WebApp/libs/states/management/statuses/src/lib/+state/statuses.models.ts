/**
 * Interface for the 'Statuses' data
 */
export interface StatusesEntity {
  id: string;
  name: string;
  createdAt: Date;
  updatedAt: Date;
  version: number;
  processId: string;
  processName: string;
}
