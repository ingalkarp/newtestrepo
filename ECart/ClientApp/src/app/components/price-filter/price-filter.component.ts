import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { ProductService } from 'src/app/services/product.service';
import { Product } from 'src/app/models/product';

@Component({
  selector: 'app-price-filter',
  templateUrl: './price-filter.component.html',
  styleUrls: ['./price-filter.component.scss']
})
export class PriceFilterComponent implements OnInit {

  @Output()
  priceValue = new EventEmitter<number>(true);

  max: number;
  min: number;
  value: number;
  step = 100;
  thumbLabel = true;

  constructor(private productService: ProductService) { }

  ngOnInit(): void {
    this.setPriceFilterProperties();
  }

  setPriceFilterProperties() {
    this.productService.products$.pipe().subscribe(
      (data: Product[]) => {
        this.setMinValue(data);
        this.setMaxValue(data);
      }
    );
  }

  formatLabel(value: number) {
    if (value >= 1000) {
      return Math.round(value / 1000) + 'k';
    }
    return value;
  }

  onChange(event) {
    this.priceValue.emit(event.value);
  }

  setMinValue(product: Product[]) {
    this.min = product.reduce((prev, curr) => {
      return prev.price < curr.price ? prev : curr;
    }).price;
  }

  setMaxValue(product: Product[]) {
    this.value = this.max = product.reduce((prev, curr) => {
      return prev.price > curr.price ? prev : curr;
    }).price;
  }
}
