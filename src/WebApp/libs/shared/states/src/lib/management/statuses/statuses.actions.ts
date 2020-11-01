// STATUSES API ACTIONS
export class LoadStatusesAction {
  static readonly type = '[Statuses API] Load Statuses';
}

export class CreateStatusAction {
  static readonly type = '[Statuses API] Create Status';

  constructor(public readonly name: string,
              public readonly processId: string) {
  }
}

export class UpdateStatusAction {
  static readonly type = '[Statuses API] Update Status';

  constructor(public readonly id: string,
              public readonly name: string,
              public readonly processId: string,
              public readonly version: number) {
  }
}
