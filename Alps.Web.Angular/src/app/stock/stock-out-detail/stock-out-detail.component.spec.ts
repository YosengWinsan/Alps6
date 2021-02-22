import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { StockOutDetailComponent } from './stock-out-detail.component';

describe('StockOutDetailComponent', () => {
  let component: StockOutDetailComponent;
  let fixture: ComponentFixture<StockOutDetailComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ StockOutDetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StockOutDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
