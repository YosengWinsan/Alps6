import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LenderEditComponent } from './lender-edit.component';

describe('LenderEditComponent', () => {
  let component: LenderEditComponent;
  let fixture: ComponentFixture<LenderEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LenderEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LenderEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
