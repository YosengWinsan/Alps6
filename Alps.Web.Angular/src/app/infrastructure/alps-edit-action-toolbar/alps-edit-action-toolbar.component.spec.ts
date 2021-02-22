import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { AlpsEditActionToolbarComponent } from './alps-edit-action-toolbar.component';

describe('AlpsEditActionToolbarComponent', () => {
  let component: AlpsEditActionToolbarComponent;
  let fixture: ComponentFixture<AlpsEditActionToolbarComponent>;

  beforeEach(waitForAsync(() => {
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
