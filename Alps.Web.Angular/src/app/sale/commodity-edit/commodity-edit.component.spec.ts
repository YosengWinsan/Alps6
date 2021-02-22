import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { CommodityEditComponent } from './commodity-edit.component';

describe('CommodityEditComponent', () => {
  let component: CommodityEditComponent;
  let fixture: ComponentFixture<CommodityEditComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ CommodityEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CommodityEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
