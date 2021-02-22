import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { AlpsCommodityCatagorySelectorComponent } from './alps-commodity-catagory-selector.component';

describe('AlpsCommodityCatagorySelectorComponent', () => {
  let component: AlpsCommodityCatagorySelectorComponent;
  let fixture: ComponentFixture<AlpsCommodityCatagorySelectorComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ AlpsCommodityCatagorySelectorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AlpsCommodityCatagorySelectorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
