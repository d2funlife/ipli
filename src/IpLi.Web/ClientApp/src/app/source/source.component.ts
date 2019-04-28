import {DecimalPipe} from '@angular/common';
import {Component, QueryList, ViewChildren} from '@angular/core';
import {Observable} from 'rxjs';

import {Source} from './source';
import {SourceService} from './source.service';
import {NgbdSortableHeader, SortEvent} from '../sortable.directive';


@Component(
  {selector: 'ngbd-table-complete', templateUrl: './source.component.html', providers: [SourceService, DecimalPipe]})
export class SourceComponent {
  sources$: Observable<Source[]>;
  total$: Observable<number>;

  @ViewChildren(NgbdSortableHeader) headers: QueryList<NgbdSortableHeader>;

  constructor(public service: SourceService) {
    this.sources$ = service.sources$;
    this.total$ = service.total$;
  }

  onSort({column, direction}: SortEvent) {
    // resetting other headers
    this.headers.forEach(header => {
      if (header.sortable !== column) {
        header.direction = '';
      }
    });

    this.service.sortColumn = column;
    this.service.sortDirection = direction;
  }
}

