import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { AlpsInfoChipComponent } from './alps-info-chip.component';

describe('AlpsInfoChipComponent', () => {
  let component: AlpsInfoChipComponent;
  let fixture: ComponentFixture<AlpsInfoChipComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ AlpsInfoChipComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AlpsInfoChipComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
