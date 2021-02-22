import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { SupplierClassEditComponent } from './supplier-class-edit.component';

describe('SupplierClassEditComponent', () => {
  let component: SupplierClassEditComponent;
  let fixture: ComponentFixture<SupplierClassEditComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ SupplierClassEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SupplierClassEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
