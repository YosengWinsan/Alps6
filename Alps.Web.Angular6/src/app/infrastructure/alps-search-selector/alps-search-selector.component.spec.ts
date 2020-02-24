import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AlpsSearchSelectorComponent } from './alps-search-selector.component';

describe('AlpsSearchSelectorComponent', () => {
  let component: AlpsSearchSelectorComponent;
  let fixture: ComponentFixture<AlpsSearchSelectorComponent>;

  beforeEach(async(() => {
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
