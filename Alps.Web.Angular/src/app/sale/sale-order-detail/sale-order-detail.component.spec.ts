import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { SaleOrderDetailComponent } from './sale-order-detail.component';

describe('SaleOrderDetailComponent', () => {
  let component: SaleOrderDetailComponent;
  let fixture: ComponentFixture<SaleOrderDetailComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ SaleOrderDetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SaleOrderDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
