import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { StockOutComponent } from './stock-out.component';

describe('StockOutComponent', () => {
  let component: StockOutComponent;
  let fixture: ComponentFixture<StockOutComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ StockOutComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StockOutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
