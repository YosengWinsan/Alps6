import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { AlpsShopcartComponent } from './alps-shopcart.component';

describe('AlpsShopcartComponent', () => {
  let component: AlpsShopcartComponent;
  let fixture: ComponentFixture<AlpsShopcartComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ AlpsShopcartComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AlpsShopcartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
