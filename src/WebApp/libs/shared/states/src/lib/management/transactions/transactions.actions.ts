// TRANSACTIONS API ACTIONS
export class LoadTransactionsAction {
  static readonly type = '[Transactions API] Load Transactions';
}

export class CreateStatusAction {
  static readonly type = '[Transactions API] Create Status';

  constructor(public readonly name: string,
              public readonly statusId: string) {
  }
}

export class UpdateStatusAction {
  static readonly type = '[Transactions API] Update Status';

  constructor(public readonly id: string,
              public readonly name: string,
              public readonly statusId: string,
              public readonly version: number) {
  }
}
