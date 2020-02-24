import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CommoditySelectorComponent } from './commodity-selector.component';

describe('CommoditySelectorComponent', () => {
  let component: CommoditySelectorComponent;
  let fixture: ComponentFixture<CommoditySelectorComponent>;

  beforeEach(async(() => {
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
