import { Component, Input, OnChanges } from '@angular/core';
import { WishlistService } from 'src/app/services/wishlist.service';
import { SubscriptionService } from 'src/app/services/subscription.service';
import { SnackbarService } from 'src/app/services/snackbar.service';
import { Product } from 'src/app/models/product';

@Component({
  selector: 'app-addtowishlist',
  templateUrl: './addtowishlist.component.html',
  styleUrls: ['./addtowishlist.component.scss']
})
export class AddtowishlistComponent implements OnChanges {

  @Input()
  itemId: number;

  @Input()
  showButton = false;

  userId;
  toggle: boolean;
  buttonText: string;
  public wishlistItems: Product[];

  constructor(
    private wishlistService: WishlistService,
    private subscriptionService: SubscriptionService,
    private snackBarService: SnackbarService) {
    this.userId = localStorage.getItem('userId');
  }

  ngOnChanges() {

    this.subscriptionService.wishlistItem$.pipe().subscribe(
      (productData: Product[]) => {
        this.setFavourite(productData);
        this.setButtonText();

      });
  }

  setFavourite(productData: Product[]) {
    const favProduct = productData.find(f => f.itemId == this.itemId);

    if (favProduct) {
      this.toggle = true;
    } else {
      this.toggle = false;
    }
  }

  setButtonText() {
    if (this.toggle) {
      this.buttonText = 'Remove from Wishlist';
    } else {
      this.buttonText = 'Add to Wishlist';
    }
  }

  toggleValue() {
    this.toggle = !this.toggle;
    this.setButtonText();

    this.wishlistService.toggleWishlistItem(this.userId, this.itemId).subscribe(
      () => {
        if (this.toggle) {
          this.snackBarService.showSnackBar('Item added to your Wishlist');
        } else {
          this.snackBarService.showSnackBar('Item removed from your Wishlist');
        }
      }, error => {
        console.log('Error ocurred while adding to wishlist : ', error);
      });
  }
}
