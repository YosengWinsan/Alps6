import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LoanSettingComponent } from './loan-setting.component';

describe('LoanSettingComponent', () => {
  let component: LoanSettingComponent;
  let fixture: ComponentFixture<LoanSettingComponent>;

  beforeEach(async(() => {
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
