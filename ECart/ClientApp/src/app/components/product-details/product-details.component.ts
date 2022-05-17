import { Component, OnInit } from '@angular/core';
import { Product } from 'src/app/models/product';
import { ProductService } from 'src/app/services/product.service';
import { ActivatedRoute } from '@angular/router';
import { EMPTY, Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { User } from 'src/app/models/user';
import { SubscriptionService } from 'src/app/services/subscription.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {

  itemId;
  ProductDetails$: Observable<Product>;
  userData$: Observable<User>;

  constructor(
    private productService: ProductService,
    private route: ActivatedRoute,
    private subscriptionService: SubscriptionService) {
    this.itemId = this.route.snapshot.paramMap.get('id');
  }

  ngOnInit() {
    this.route.params.subscribe(
      params => {
        this.itemId = +params.id;
        this.getProductDetails();
      }
    );
    this.userData$ = this.subscriptionService.userData;
  }

  getProductDetails() {
    this.ProductDetails$ = this.productService.getProductById(this.itemId)
      .pipe(
        catchError(error => {
          console.log('Error ocurred while fetching product data : ', error);
          return EMPTY;
        }));
  }
}
