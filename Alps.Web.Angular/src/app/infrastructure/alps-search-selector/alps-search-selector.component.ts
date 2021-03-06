import { Component, OnInit, OnDestroy, Input, forwardRef,  ElementRef, HostBinding } from '@angular/core';
import { FormControl, ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';
import { Observable, timer, EMPTY, combineLatest, Subject, BehaviorSubject, of, Subscription } from 'rxjs';
import { startWith, distinctUntilChanged, debounce, filter, switchMap, map, catchError, publishReplay, refCount, withLatestFrom, take, delayWhen, tap } from 'rxjs/operators';
import { AlpsSearchSelectorOption } from './alps-search-selector-type';
import { PinYinHelper } from '../../extends/PinYinHelper';
import { _countGroupLabelsBeforeOption } from '@angular/material/core';


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
  @Input() debounceTime = 200;
  @Input() width = '';
  @Input() emptyText = '无匹配项';
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
  private isNgInvalid:boolean=true;
  constructor(el:ElementRef) {
    
    const searchesOb =
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
    const optionsOb = combineLatest(searchesOb, this.incomingDataSources)
      .pipe(
        switchMap(([srch, ds]) => {
          if (srch == null)
            srch = '';
          if (typeof srch === 'string') {
            let list = ds.filter(option => option.displayValue.toLowerCase().indexOf(<string>srch) >= 0 || option.pinyin.indexOf(<string>srch) >= 0);
            let value: any = srch;
            if (list.length == 1 && list[0].displayValue == srch)
              value = ds[0];
            return of({ value, list })
              .pipe(
                delayWhen(event => timer(Math.random() * 300 + 100))
              );

          }
          return of({ value: srch, list: [srch] });
        }),
        publishReplay(1),
        refCount()
      );

    this.selectedValue =//of(1);
      optionsOb.pipe(
        filter(o => !!o.value),
        map(o => { return o.value.value ? o.value.value : null }),
        distinctUntilChanged()
      );

    // this.loading = options.pipe(map(o => !o.list && !o.errorMessage));
    this.list = optionsOb.pipe(map(o => o.list));
     this.empty = optionsOb.pipe(map(o => !(o.value && o.value.value)));//.list ? o.list.length === 0 : false));
    // this.errorMessage = options.pipe(map(o => o.errorMessage));

    // a value was provided by the form; request the full entry
    this.incomingDataSourcesSub = this.incomingValues.pipe(
      withLatestFrom(this.incomingDataSources),
      switchMap(([value, options]) => {
        let finded = false;
        let controlValue = null;
        for (const option of options) {
          if (option.value == value) {
            controlValue = option;
            break;
          }

        }
        return of(controlValue);
      }
      )
    )
      .subscribe(value => { this.searchControl.setValue(value); });

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
