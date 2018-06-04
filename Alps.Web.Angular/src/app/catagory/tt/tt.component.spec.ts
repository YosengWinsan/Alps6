
import { fakeAsync, ComponentFixture, TestBed } from '@angular/core/testing';

import { TtComponent } from './tt.component';

describe('TtComponent', () => {
  let component: TtComponent;
  let fixture: ComponentFixture<TtComponent>;

  beforeEach(fakeAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ TtComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TtComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should compile', () => {
    expect(component).toBeTruthy();
  });
});
