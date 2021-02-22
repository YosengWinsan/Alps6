import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { LoanVoucherSummaryComponent } from './loan-voucher-summary.component';

describe('LoanVoucherSummaryComponent', () => {
  let component: LoanVoucherSummaryComponent;
  let fixture: ComponentFixture<LoanVoucherSummaryComponent>;

  beforeEach(waitForAsync(() => {
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
