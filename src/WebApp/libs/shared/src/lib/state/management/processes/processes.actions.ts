// PROCESSES API ACTIONS
export class LoadProcessesAction {
  static readonly type = '[Processes API] Load Processes';
}

export class CreateProcessAction {
  static readonly type = '[Processes API] Create Process';

  constructor(public readonly name: string) {
  }
}

export class UpdateProcessAction {
  static readonly type = '[Processes API] Update Process';

  constructor(public readonly id: string,
              public readonly newName: string) {
  }
}

// PROCESSES ACTIONS
export class SelectProcessAction {
  static readonly type = '[Processes] Select Process';

  constructor(public readonly id: string) {
  }
}

export class ClearProcessSelection {
  static readonly type = '[Processes] Clear Process Selection';
}




