import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AlpsCommodityCatagorySelectorComponent } from './alps-commodity-catagory-selector.component';

describe('AlpsCommodityCatagorySelectorComponent', () => {
  let component: AlpsCommodityCatagorySelectorComponent;
  let fixture: ComponentFixture<AlpsCommodityCatagorySelectorComponent>;

  beforeEach(async(() => {
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
