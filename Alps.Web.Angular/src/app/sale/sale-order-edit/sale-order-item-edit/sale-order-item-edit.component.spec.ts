import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { SaleOrderItemEditComponent } from './sale-order-item-edit.component';

describe('SaleOrderItemEditComponent', () => {
  let component: SaleOrderItemEditComponent;
  let fixture: ComponentFixture<SaleOrderItemEditComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ SaleOrderItemEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SaleOrderItemEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
