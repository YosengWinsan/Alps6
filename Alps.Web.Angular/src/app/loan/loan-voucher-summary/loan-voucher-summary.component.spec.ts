import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LoanVoucherSummaryComponent } from './loan-voucher-summary.component';

describe('LoanVoucherSummaryComponent', () => {
  let component: LoanVoucherSummaryComponent;
  let fixture: ComponentFixture<LoanVoucherSummaryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LoanVoucherSummaryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LoanVoucherSummaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
