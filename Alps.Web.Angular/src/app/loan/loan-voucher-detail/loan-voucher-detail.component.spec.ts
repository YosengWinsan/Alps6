import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { LoanVoucherDetailComponent } from './loan-voucher-detail.component';

describe('LoanVoucherDetailComponent', () => {
  let component: LoanVoucherDetailComponent;
  let fixture: ComponentFixture<LoanVoucherDetailComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ LoanVoucherDetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LoanVoucherDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
