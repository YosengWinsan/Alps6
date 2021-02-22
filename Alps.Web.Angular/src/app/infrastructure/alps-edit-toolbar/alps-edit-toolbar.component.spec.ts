import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { AlpsEditToolbarComponent } from './alps-edit-toolbar.component';

describe('AlpsEditToolbarComponent', () => {
  let component: AlpsEditToolbarComponent;
  let fixture: ComponentFixture<AlpsEditToolbarComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ AlpsEditToolbarComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AlpsEditToolbarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
