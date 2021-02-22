import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { StockInComponent } from './stock-in.component';

describe('StockInComponent', () => {
  let component: StockInComponent;
  let fixture: ComponentFixture<StockInComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ StockInComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StockInComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
