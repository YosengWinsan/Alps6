import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { PositionEditComponent } from './position-edit.component';

describe('PositionEditComponent', () => {
  let component: PositionEditComponent;
  let fixture: ComponentFixture<PositionEditComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ PositionEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PositionEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
