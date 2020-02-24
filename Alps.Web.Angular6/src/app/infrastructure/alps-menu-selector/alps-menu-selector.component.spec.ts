import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AlpsMenuSelectorComponent } from './alps-menu-selector.component';

describe('AlpsMenuSelectorComponent', () => {
  let component: AlpsMenuSelectorComponent;
  let fixture: ComponentFixture<AlpsMenuSelectorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AlpsMenuSelectorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AlpsMenuSelectorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
