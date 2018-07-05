import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { StockOutItemEditComponent } from './stock-out-item-edit.component';

describe('StockOutItemEditComponent', () => {
  let component: StockOutItemEditComponent;
  let fixture: ComponentFixture<StockOutItemEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ StockOutItemEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StockOutItemEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
