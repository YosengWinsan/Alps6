import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { ProductSkuEditComponent } from './product-sku-edit.component';

describe('ProductSkuEditComponent', () => {
  let component: ProductSkuEditComponent;
  let fixture: ComponentFixture<ProductSkuEditComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ ProductSkuEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProductSkuEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
