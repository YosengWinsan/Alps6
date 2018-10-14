import { Component, OnInit, OnDestroy, Input, forwardRef } from '@angular/core';
import { FormControl, ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';
import { Observable, timer, EMPTY, combineLatest, Subject, BehaviorSubject, of, Subscription } from 'rxjs';
import { startWith, distinctUntilChanged, debounce, filter, switchMap, map, catchError, publishReplay, refCount, withLatestFrom, take, delayWhen, tap } from 'rxjs/operators';
import { AlpsSearchSelectorOption } from './alps-search-selector-type';
import { PinYinHelper } from '../../extends/PinYinHelper';


@Component({
  selector: 'alps-search-selector',
  templateUrl: './alps-search-selector.component.html',
  styleUrls: ['./alps-search-selector.component.css'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => AlpsSearchSelectorComponent),
      multi: true
    }]

})
export class AlpsSearchSelectorComponent implements ControlValueAccessor, OnDestroy {
  @Input() placeholder: string;
  @Input() debounceTime = 1000;
  @Input() width = '';
  @Input() emptyText = '';
  @Input() autoActiveFirstOption = false;
  @Input() set dataSource(ds: AlpsSearchSelectorOption[]) {
    if (ds instanceof Array) {
      for (const option of ds) {
        if (!(option.pinyin && option.pinyin !== "")) {
          option.pinyin = PinYinHelper.ConvertPinyin(option.displayValue).toLowerCase();
        }
      }
      this.incomingDataSources.next(ds);
    }
  }

  searchControl = new FormControl();
  private incomingValues = new Subject<any>();
  private incomingDataSources: BehaviorSubject<AlpsSearchSelectorOption[]> = new BehaviorSubject<AlpsSearchSelectorOption[]>([]);

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
        tap((v) => console.info(v)),
        startWith(this.searchControl.value),
        distinctUntilChanged(),
        debounce(srch => {
          console.info('debounce'+srch);
          // Typing into input sends strings.
          if (typeof srch === 'string') {
            return timer(this.debounceTime);
          }
          console.info('EMPTY'+srch);
          return EMPTY; // immediate - no debounce for choosing from the list
        }),
        tap((v) => console.info(v))
      );
    const options: Observable<AlpsSearchSelectorOption[] | any> = combineLatest(
      searches,
      this.incomingDataSources.pipe(filter(ds => !!ds))
    ).pipe(
      tap((v) => console.info(v)),
      switchMap(([srch, ds]) => {
        // Initial value is sometimes null.
        if (srch === null) {
          srch = '';
        }
        // Typing into input sends strings.
        if(srch instanceof AlpsSearchSelectorOption)
        {
          return of([srch]);
        }
        if (typeof srch === 'string') {
          return of(ds.filter(option => option.displayValue.toLowerCase().indexOf(<string>srch) >= 0 || option.pinyin.indexOf(<string>srch) >= 0))
            .pipe(
              delayWhen(event => timer(Math.random() * 300 + 100))
            )
        }
        return of(null);
      }),
      publishReplay(1),
      refCount()
    );
this.selectedValue=//of(1);
searches.pipe(
  tap((v) => console.info(v)),
  filter(s=>!!s && !!(<AlpsSearchSelectorOption>s).value),
  map(s=>{return (<AlpsSearchSelectorOption>s).value}),
  distinctUntilChanged(),
  tap((v) => console.info(v))
);
    // this.selectedValue = options.pipe(
    //   filter(result => !!result),
    //   withLatestFrom(searches),

    //   map(([options, search]) => {
    //     const option = options.find(option => {
    //       if (option.pinyin == search || option.displayValue.toLowerCase() == search)
    //         return true;
    //     });
    //     return option && option.value || null;
    //   }),
    //   distinctUntilChanged()
    // );

    // this.loading = options.pipe(map(o => !o.list && !o.errorMessage));
    this.list = options;
    // this.empty = options.pipe(map(o => o.list ? o.list.length === 0 : false));
    // this.errorMessage = options.pipe(map(o => o.errorMessage));

    // a value was provided by the form; request the full entry
    this.incomingDataSourcesSub = this.incomingValues.pipe(
      withLatestFrom(this.incomingDataSources),
      switchMap(([value, options]) => {
        let finded = false;
        let displayValue = null;
        for (const option of options) {
          if (option.value == value) {
            displayValue = option.displayValue;
            break;
          }

        }
        if (displayValue)
          return of(displayValue);
        else
          return of(null);
      }
      )
    )
      .subscribe(value => {this.searchControl.setValue(value);console.info('setValue');});
      
  }


  focus() {
    // While focused, user selection will be propagated to the form.
    //this.selectedValueSub = this.selectedValue.subscribe(this.checkAndPropagate.bind(this));
    //this.selectedValueSub = this.selectedValue.subscribe(()=>console.info('XX'));
    // let o= this.searchControl.valueChanges.pipe(startWith(this.searchControl.value),map((v)=>{
    //   console.info('PIPE'+v);
    //   return v;}));
    //   o.subscribe((v)=>console.info('1:'+v));
    //   o.subscribe((v)=>console.info('2:'+v));
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
    console.info('Blur');
    this.selectedValue
      .pipe(take(1))
      .subscribe(this.checkAndPropagate.bind(this));
    // However, this code raises an important question about valid behavior of a
    // Angular form control. Is it acceptable for a form control to
    // asynchronously provide a new value when it no longer has focus?
  }
  private checkAndPropagate(value: any) {
    console.info(value);
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
      console.info('WriteValue');
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
