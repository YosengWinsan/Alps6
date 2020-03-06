import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PrintSettleInterestComponent } from './print-settle-interest.component';

describe('PrintSettleInterestComponent', () => {
  let component: PrintSettleInterestComponent;
  let fixture: ComponentFixture<PrintSettleInterestComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PrintSettleInterestComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PrintSettleInterestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
