import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { DeleteProductComponent } from './delete-product.component';

describe('DeleteProductComponent', () => {
  let component: DeleteProductComponent;
  let fixture: ComponentFixture<DeleteProductComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [DeleteProductComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DeleteProductComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
