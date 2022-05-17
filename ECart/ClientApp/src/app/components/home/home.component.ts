import { Component, OnInit, OnDestroy } from '@angular/core';
import { Product } from 'src/app/models/product';
import { ActivatedRoute } from '@angular/router';
import { ProductService } from 'src/app/services/product.service';
import { switchMap } from 'rxjs/operators';
import { SubscriptionService } from 'src/app/services/subscription.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit, OnDestroy {

  public products: Product[];
  public filteredProducts: Product[];
  category: string;
  priceRange = Number.MAX_SAFE_INTEGER;
  isLoading: boolean;
  searchItem: string;

  constructor(
    private route: ActivatedRoute,
    private peoductService: ProductService,
    private subscriptionService: SubscriptionService) {
  }

  ngOnInit() {
    this.isLoading = true;
    this.getAllProductData();
  }

  getAllProductData() {
    this.peoductService.products$.pipe(switchMap(
      (data: Product[]) => {
        this.filteredProducts = data;
        return this.route.queryParams;
      }
    )).subscribe(params => {
      this.category = params.category;
      this.searchItem = params.item;
      this.subscriptionService.searchItemValue$.next(this.searchItem);
      this.filterProductData();
    });
  }

  filterPrice(value: number) {
    this.priceRange = value;
    this.filterProductData();
  }

  filterProductData() {
    const filteredData = this.filteredProducts.filter(b => b.price <= this.priceRange).slice();

    if (this.category) {
      this.products = filteredData.filter(b => b.category.toLowerCase() === this.category.toLowerCase());
    } else if (this.searchItem) {
      this.products = filteredData.filter(b => b.title.toLowerCase().indexOf(this.searchItem) !== -1
        || b.seller.toLowerCase().indexOf(this.searchItem) !== -1);
    } else {
      this.products = filteredData;
    }
    this.isLoading = false;
  }

  ngOnDestroy() {
    this.subscriptionService.searchItemValue$.next('');
  }
}
