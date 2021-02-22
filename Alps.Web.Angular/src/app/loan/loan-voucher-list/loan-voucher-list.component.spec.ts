import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { LoanVoucherListComponent } from './loan-voucher-list.component';

describe('LoanVoucherListComponent', () => {
  let component: LoanVoucherListComponent;
  let fixture: ComponentFixture<LoanVoucherListComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ LoanVoucherListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LoanVoucherListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
