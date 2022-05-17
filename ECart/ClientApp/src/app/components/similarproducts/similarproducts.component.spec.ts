import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { SimilarproductsComponent } from './similarproducts.component';

describe('SimilarproductsComponent', () => {
  let component: SimilarproductsComponent;
  let fixture: ComponentFixture<SimilarproductsComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [SimilarproductsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SimilarproductsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
