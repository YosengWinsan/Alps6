import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CatagoryEditComponent } from './catagory-edit.component';

describe('CatagoryEditComponent', () => {
  let component: CatagoryEditComponent;
  let fixture: ComponentFixture<CatagoryEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CatagoryEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CatagoryEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
