import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { AlpsSelectorComponent } from './alps-selector.component';

describe('AlpsSelectorComponent', () => {
  let component: AlpsSelectorComponent;
  let fixture: ComponentFixture<AlpsSelectorComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ AlpsSelectorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AlpsSelectorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
