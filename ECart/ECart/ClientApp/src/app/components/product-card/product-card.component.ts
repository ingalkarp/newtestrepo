import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Product } from 'src/app/models/product';
import { User } from 'src/app/models/user';
import { SubscriptionService } from 'src/app/services/subscription.service';

@Component({
  selector: 'app-product-card',
  templateUrl: './product-card.component.html',
  styleUrls: ['./product-card.component.scss']
})
export class ProductCardComponent implements OnInit {

  @Input()
  product: Product;

  isActive = false;
  userData$: Observable<User>;

  constructor(private router: Router, private subscriptionService: SubscriptionService) { }

  ngOnInit() {
    this.userData$ = this.subscriptionService.userData;
  }

  goToPage(id: number) {
    this.router.navigate(['/products/details/', id]);
  }
}
