import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AlpsShopcartComponent } from './alps-shopcart.component';

describe('AlpsShopcartComponent', () => {
  let component: AlpsShopcartComponent;
  let fixture: ComponentFixture<AlpsShopcartComponent>;

  beforeEach(async(() => {
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
