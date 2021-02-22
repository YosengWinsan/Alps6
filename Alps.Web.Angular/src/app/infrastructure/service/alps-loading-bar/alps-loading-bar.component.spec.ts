import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { AlpsLoadingBarComponent } from './alps-loading-bar.component';

describe('AlpsLoadingBarComponent', () => {
  let component: AlpsLoadingBarComponent;
  let fixture: ComponentFixture<AlpsLoadingBarComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ AlpsLoadingBarComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AlpsLoadingBarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
