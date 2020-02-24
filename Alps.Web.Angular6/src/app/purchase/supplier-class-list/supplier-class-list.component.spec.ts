import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SupplierClassListComponent } from './supplier-class-list.component';

describe('SupplierClassListComponent', () => {
  let component: SupplierClassListComponent;
  let fixture: ComponentFixture<SupplierClassListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SupplierClassListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SupplierClassListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
