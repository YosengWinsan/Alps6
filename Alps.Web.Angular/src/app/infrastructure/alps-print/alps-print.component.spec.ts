import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { AlpsPrintComponent } from './alps-print.component';

describe('AlpsPrintComponent', () => {
  let component: AlpsPrintComponent;
  let fixture: ComponentFixture<AlpsPrintComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ AlpsPrintComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AlpsPrintComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
