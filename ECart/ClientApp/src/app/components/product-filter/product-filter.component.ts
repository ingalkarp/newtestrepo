import { Component, OnInit, Input } from '@angular/core';
import { ProductService } from 'src/app/services/product.service';
import { catchError } from 'rxjs/operators';
import { EMPTY, Observable } from 'rxjs';
import { Categories } from 'src/app/models/categories';

@Component({
  selector: 'app-product-filter',
  templateUrl: './product-filter.component.html',
  styleUrls: ['./product-filter.component.scss']
})
export class ProductFilterComponent implements OnInit {

  @Input()
  category: string;

  categories$: Observable<Categories[]>;

  constructor(private productService: ProductService) { }

  ngOnInit() {
    this.fetchCategories();
  }

  fetchCategories() {
    this.categories$ = this.productService.categories$
      .pipe(
        catchError(error => {
          console.log('Error ocurred while fetching category List : ', error);
          return EMPTY;
        }));
  }
}
