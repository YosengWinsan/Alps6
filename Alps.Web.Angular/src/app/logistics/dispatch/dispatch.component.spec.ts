import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { DispatchComponent } from './dispatch.component';

describe('DispatchComponent', () => {
  let component: DispatchComponent;
  let fixture: ComponentFixture<DispatchComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ DispatchComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DispatchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
