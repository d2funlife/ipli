import {Inject, Injectable, PipeTransform} from '@angular/core';

import {BehaviorSubject, from, Observable, of, Subject} from 'rxjs';

import {Source} from './source';
import {DecimalPipe} from '@angular/common';
import {debounceTime, delay, switchMap, tap} from 'rxjs/operators';
import {SortDirection} from '../sortable.directive';
import {HttpClient, HttpResponse} from '@angular/common/http';

interface SearchResult {
  sources: Source[];
  total: number;
}



interface State {
  page: number;
  pageSize: number;
  searchTerm: string;
  sortColumn: string;
  sortDirection: SortDirection;
}

function compare(v1, v2) {
  return v1 < v2 ? -1 : v1 > v2 ? 1 : 0;
}

function sort(sources: Source[], column: string, direction: string): Source[] {
  if (direction === '') {
    return sources;
  } else {
    return [...sources].sort((a, b) => {
      const res = compare(a[column], b[column]);
      return direction === 'asc' ? res : -res;
    });
  }
}

function matches(source: Source, term: string, pipe: PipeTransform) {
  return source.title.toLowerCase().includes(term);
}

@Injectable({providedIn: 'root'})
export class SourceService {
  private _loading$ = new BehaviorSubject<boolean>(true);
  private _search$ = new Subject<void>();
  private _sources$ = new BehaviorSubject<Source[]>([]);
  private _total$ = new BehaviorSubject<number>(0);

  private _state: State = {
    page: 1,
    pageSize: 4,
    searchTerm: '',
    sortColumn: '',
    sortDirection: ''
  };
  private baseUrl: string;

  constructor(private pipe: DecimalPipe, private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
    this._search$.pipe(
      tap(() => this._loading$.next(true)),
      debounceTime(200),
      switchMap(() => this._search()),
      delay(200),
      tap(() => this._loading$.next(false))
    ).subscribe(result => {
      this._sources$.next(result.sources);
      this._total$.next(result.total);
    });

    this._search$.next();
  }

  get sources$() { return this._sources$.asObservable(); }
  get total$() { return this._total$.asObservable(); }
  get loading$() { return this._loading$.asObservable(); }
  get page() { return this._state.page; }
  get pageSize() { return this._state.pageSize; }
  get searchTerm() { return this._state.searchTerm; }

  set page(page: number) { this._set({page}); }
  set pageSize(pageSize: number) { this._set({pageSize}); }
  set searchTerm(searchTerm: string) { this._set({searchTerm}); }
  set sortColumn(sortColumn: string) { this._set({sortColumn}); }
  set sortDirection(sortDirection: SortDirection) { this._set({sortDirection}); }

  private _set(patch: Partial<State>) {
    Object.assign(this._state, patch);
    this._search$.next();
  }

  private _search(): Observable<SearchResult> {
    const {sortColumn, sortDirection, pageSize, page, searchTerm} = this._state;

    var offset = (this.page - 1) * this.pageSize;
    var limit = this.pageSize;
    var resps =  this.http.get<HttpResponse<Source[]>>(this.baseUrl + 'api/sources?search=' + this.searchTerm + '&offset=' + offset + '&limit=' + limit).toPromise().then(resp => {
      return{sources : resp, total : 1000};
    });

    return from<SearchResult>(from(resps));
  }
}
