import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AlpsActionToolbarComponent } from './alps-action-toolbar.component';

describe('AlpsActionToolbarComponent', () => {
  let component: AlpsActionToolbarComponent;
  let fixture: ComponentFixture<AlpsActionToolbarComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AlpsActionToolbarComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AlpsActionToolbarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
