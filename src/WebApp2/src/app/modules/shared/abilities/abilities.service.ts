import {Injectable} from '@angular/core';
import {BreakpointObserver, Breakpoints} from '@angular/cdk/layout';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';

@Injectable()
export class AbilitiesService {

  public isHandset$: Observable<boolean>;

  constructor(breakpointObserver: BreakpointObserver) {
    this.isHandset$ = breakpointObserver.observe(Breakpoints.Handset)
      .pipe(map(result => result.matches));
  }
}
