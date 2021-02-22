import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { CommoditySelectorComponent } from './commodity-selector.component';

describe('CommoditySelectorComponent', () => {
  let component: CommoditySelectorComponent;
  let fixture: ComponentFixture<CommoditySelectorComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ CommoditySelectorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CommoditySelectorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
