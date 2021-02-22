import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { AlpsSelectorDialogComponent } from './alps-selector-dialog.component';

describe('AlpsSelectorDialogComponent', () => {
  let component: AlpsSelectorDialogComponent;
  let fixture: ComponentFixture<AlpsSelectorDialogComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ AlpsSelectorDialogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AlpsSelectorDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
