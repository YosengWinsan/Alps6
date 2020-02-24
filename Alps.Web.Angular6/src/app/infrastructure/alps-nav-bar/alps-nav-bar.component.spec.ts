import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AlpsNavBarComponent } from './alps-nav-bar.component';

describe('AlpsNavBarComponent', () => {
  let component: AlpsNavBarComponent;
  let fixture: ComponentFixture<AlpsNavBarComponent>;

  beforeEach(async(() => {
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
