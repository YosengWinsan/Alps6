import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LenderImportComponent } from './lender-import.component';

describe('LenderImportComponent', () => {
  let component: LenderImportComponent;
  let fixture: ComponentFixture<LenderImportComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LenderImportComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LenderImportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
