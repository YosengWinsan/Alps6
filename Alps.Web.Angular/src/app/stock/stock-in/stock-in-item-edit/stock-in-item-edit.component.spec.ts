import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { StockInItemEditComponent } from './stock-in-item-edit.component';

describe('StockInItemEditComponent', () => {
  let component: StockInItemEditComponent;
  let fixture: ComponentFixture<StockInItemEditComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ StockInItemEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StockInItemEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
