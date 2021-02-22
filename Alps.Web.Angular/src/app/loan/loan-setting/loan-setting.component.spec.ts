import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { LoanSettingComponent } from './loan-setting.component';

describe('LoanSettingComponent', () => {
  let component: LoanSettingComponent;
  let fixture: ComponentFixture<LoanSettingComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ LoanSettingComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LoanSettingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
