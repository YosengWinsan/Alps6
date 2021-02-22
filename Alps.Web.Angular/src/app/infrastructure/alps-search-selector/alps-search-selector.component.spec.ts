import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { AlpsSearchSelectorComponent } from './alps-search-selector.component';

describe('AlpsSearchSelectorComponent', () => {
  let component: AlpsSearchSelectorComponent;
  let fixture: ComponentFixture<AlpsSearchSelectorComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ AlpsSearchSelectorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AlpsSearchSelectorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
