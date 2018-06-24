import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SaleOrderItemEditComponent } from './sale-order-item-edit.component';

describe('SaleOrderItemEditComponent', () => {
  let component: SaleOrderItemEditComponent;
  let fixture: ComponentFixture<SaleOrderItemEditComponent>;

  beforeEach(async(() => {
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
