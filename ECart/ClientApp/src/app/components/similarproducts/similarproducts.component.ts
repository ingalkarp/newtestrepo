import { Component, OnInit, Input } from '@angular/core';
import { Product } from 'src/app/models/product';
import { ProductService } from 'src/app/services/product.service';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-similarproducts',
  templateUrl: './similarproducts.component.html',
  styleUrls: ['./similarproducts.component.scss']
})
export class SimilarproductsComponent implements OnInit {

  @Input()
  itemId: number;

  SimilarProduct$: Observable<Product[]>;

  constructor(
    private productService: ProductService,
    private route: ActivatedRoute) {
  }

  ngOnInit() {
    this.route.params.subscribe(
      params => {
        this.itemId = +params.id;
        this.getSimilarProductData();
      }
    );
  }

  getSimilarProductData() {
    this.SimilarProduct$ = this.productService.getsimilarProducts(this.itemId);
  }
}
