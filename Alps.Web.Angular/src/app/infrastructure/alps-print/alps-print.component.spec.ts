import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AlpsPrintComponent } from './alps-print.component';

describe('AlpsPrintComponent', () => {
  let component: AlpsPrintComponent;
  let fixture: ComponentFixture<AlpsPrintComponent>;

  beforeEach(async(() => {
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
