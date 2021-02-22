import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { SettleInterestComponent } from './settle-interest.component';

describe('SettleInterestComponent', () => {
  let component: SettleInterestComponent;
  let fixture: ComponentFixture<SettleInterestComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ SettleInterestComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SettleInterestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
