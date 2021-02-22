import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { AlpsNavBarComponent } from './alps-nav-bar.component';

describe('AlpsNavBarComponent', () => {
  let component: AlpsNavBarComponent;
  let fixture: ComponentFixture<AlpsNavBarComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ AlpsNavBarComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AlpsNavBarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
