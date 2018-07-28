import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CatagoryListComponent } from './catagory-list.component';

describe('CatagoryListComponent', () => {
  let component: CatagoryListComponent;
  let fixture: ComponentFixture<CatagoryListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CatagoryListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CatagoryListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
