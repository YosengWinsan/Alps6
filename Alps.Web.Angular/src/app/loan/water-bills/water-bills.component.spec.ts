import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { WaterBillsComponent } from './water-bills.component';

describe('WaterBillsComponent', () => {
  let component: WaterBillsComponent;
  let fixture: ComponentFixture<WaterBillsComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ WaterBillsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WaterBillsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
