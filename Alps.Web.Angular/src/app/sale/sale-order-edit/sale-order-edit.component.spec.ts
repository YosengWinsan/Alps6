import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { SaleOrderEditComponent } from './sale-order-edit.component';

describe('SaleOrderEditComponent', () => {
  let component: SaleOrderEditComponent;
  let fixture: ComponentFixture<SaleOrderEditComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ SaleOrderEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SaleOrderEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
