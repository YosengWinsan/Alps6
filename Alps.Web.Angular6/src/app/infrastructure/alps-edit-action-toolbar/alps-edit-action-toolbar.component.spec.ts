import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AlpsEditActionToolbarComponent } from './alps-edit-action-toolbar.component';

describe('AlpsEditActionToolbarComponent', () => {
  let component: AlpsEditActionToolbarComponent;
  let fixture: ComponentFixture<AlpsEditActionToolbarComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AlpsEditActionToolbarComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AlpsEditActionToolbarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
