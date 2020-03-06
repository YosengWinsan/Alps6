import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SettlableInterestSummaryComponent } from './settlable-interest-summary.component';

describe('SettlableInterestSummaryComponent', () => {
  let component: SettlableInterestSummaryComponent;
  let fixture: ComponentFixture<SettlableInterestSummaryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SettlableInterestSummaryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SettlableInterestSummaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
