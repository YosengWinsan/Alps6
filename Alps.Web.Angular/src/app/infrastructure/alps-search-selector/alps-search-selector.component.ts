import { Component, OnInit, OnDestroy, Input } from '@angular/core';
import { FormControl, ControlValueAccessor } from '@angular/forms';
import { Observable, timer, EMPTY, combineLatest, Subject, BehaviorSubject, of, Subscription } from 'rxjs';
import { AlpsSelectorOption } from '../alps-selector/alps-selector-dialog/alps-selector-dialog.component';
import { startWith, distinctUntilChanged, debounce, filter, switchMap, map, catchError, publishReplay, refCount, withLatestFrom, take } from 'rxjs/operators';
import { AlpsSearchSelectorDataSource, AlpsSearchSelectorOption } from './alps-search-selector-type';
import { MatFormFieldControl } from '@angular/material';

interface SearchResult {
  search: string;
  list?: AlpsSearchSelectorOption[];
  errorMessage?: string;
}

@Component({
  selector: 'alps-search-selector',
  templateUrl: './alps-search-selector.component.html',
  styleUrls: ['./alps-search-selector.component.css']
})
export class AlpsSearchSelectorComponent implements ControlValueAccessor, OnDestroy {
  @Input() placeholder: string;
  @Input() debounceTime = 100;
  @Input() width = '';
  @Input() emptyText = '';
  @Input() autoActiveFirstOption = false;
  @Input() set dataSource(ds: AlpsSearchSelectorDataSource) { this.incomingDataSources.next(ds); }
  searchControl = new FormControl();
  private incomingValues = new Subject<any>();
  private incomingDataSources = new BehaviorSubject<AlpsSearchSelectorDataSource>({
    displayValue: of,
    search: () => of([])
  });
  loading: Observable<boolean>;
  list: Observable<AlpsSearchSelectorOption[] | undefined>;
  empty: Observable<boolean>;
  errorMessage: Observable<string | undefined>;
  private incomingDataSourcesSub: Subscription;
  private outsideValue: any;
  private selectedValueSub: Subscription;
  private selectedValue: Observable<any>;
  constructor() {
    const searches: Observable<AlpsSearchSelectorOption | string | null> =
      this.searchControl.valueChanges.pipe(
        startWith(this.searchControl.value),
        distinctUntilChanged(),
        debounce(srch => {
          // Typing into input sends strings.
          if (typeof srch === 'string') {
            return timer(this.debounceTime);
          }
          return EMPTY; // immediate - no debounce for choosing from the list
        })
      );
    const options: Observable<SearchResult> = combineLatest(
      searches,
      this.incomingDataSources.pipe(filter(ds => !!ds)),
    ).pipe(
      switchMap(([srch, ds]) => {
        // Initial value is sometimes null.
        if (srch === null) {
          srch = '';
        }
        // Typing into input sends strings.
        if (typeof srch === 'string') {
          const search: string = srch;
          return ds.search(srch).pipe(
            map(list => ({ search, list })),
            catchError(errorMessage => of({ search, errorMessage })),
            startWith({ search })
          );
        }

        // Selecting from Material Option List sends an object, so there is
        // no need to call function to search for it.
        const entry = srch as AlpsSearchSelectorOption;
        return of<SearchResult>({
          search: srch.displayValue,
          list: [{ ...entry }]
        });
      }),
      publishReplay(1),
      refCount()
    );

    function matcher(search: string, entry: AlpsSearchSelectorOption) {
      return entry.displayValue === search;
    }

    this.selectedValue = options.pipe(
      filter(result => !!result.list),
      withLatestFrom(this.incomingDataSources),
      map(([result, ds]) => {
        const list = result.list || []; // appease TS
        const matchFn = ds.match || matcher;
        const entry = list.find(option => matchFn(result.search, option));
        return entry && entry.value || null;
      }),
      distinctUntilChanged()
    );

    this.loading = options.pipe(map(o => !o.list && !o.errorMessage));
    this.list = options.pipe(map(o => o.list));
    this.empty = options.pipe(map(o => o.list ? o.list.length === 0 : false));
    this.errorMessage = options.pipe(map(o => o.errorMessage));

    // a value was provided by the form; request the full entry
    this.incomingDataSourcesSub = this.incomingValues.pipe(
      withLatestFrom(this.incomingDataSources),
      switchMap<[any, AlpsSearchSelectorDataSource], AlpsSearchSelectorOption>(([value, ds]) => ds.displayValue(value))
    )
      .subscribe(value => this.searchControl.setValue(value));
  }


  focus() {
    // While focused, user selection will be propagated to the form.
    this.selectedValueSub = this.selectedValue.subscribe(this.checkAndPropagate.bind(this));
  }

  blur() {
    this.onTouched();
    // Now that we've lost focus, stop propagating changes.
    if (this.selectedValueSub) {
      this.selectedValueSub.unsubscribe();
    }
    // However, it's possible the user has just typed some text that will be
    // confirmed (by the application provided function) as a match,
    // asynchronously. We can't force the system to wait for that to happen, we
    // are losing focus right now. But we can subscribe to pick up that one last
    // change and propagate it if/when it arrives.
    this.selectedValue
      .pipe(take(1))
      .subscribe(this.checkAndPropagate.bind(this));
    // However, this code raises an important question about valid behavior of a
    // Angular form control. Is it acceptable for a form control to
    // asynchronously provide a new value when it no longer has focus?
  }
  private checkAndPropagate(value: any) {
    // Only send a change if there really is one.
    if (value !== this.outsideValue) {
      this.outsideValue = value;
      this.onChange(value);
    }
  }
  displayWith(value: AlpsSearchSelectorOption): string {
    return value ? value.displayValue : '';
  }


  writeValue(obj: any): void {
    // Angular sometimes writes a value that didn't really change.
    if (obj !== this.outsideValue) {
      this.outsideValue = obj;
      this.incomingValues.next(obj);
    }
  }

  registerOnChange(fn: any): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }

  setDisabledState(isDisabled: boolean): void {
    if (isDisabled) {
      this.searchControl.disable();
    } else {
      this.searchControl.enable();
    }
  }

  ngOnDestroy(): void {
    if (this.incomingDataSourcesSub) {
      this.incomingDataSourcesSub.unsubscribe();
    }
  }

  private onChange = (_: any) => { };
  private onTouched = () => { };

}
