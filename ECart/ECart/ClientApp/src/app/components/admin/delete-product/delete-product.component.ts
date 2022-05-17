import { Component, OnInit, Inject } from '@angular/core';
import { Product } from 'src/app/models/product';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ProductService } from 'src/app/services/product.service';
import { Observable, EMPTY } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Component({
  selector: 'app-delete-product',
  templateUrl: './delete-product.component.html',
  styleUrls: ['./delete-product.component.scss']
})
export class DeleteProductComponent implements OnInit {

  productData$: Observable<Product>;

  constructor(
    public dialogRef: MatDialogRef<DeleteProductComponent>,
    @Inject(MAT_DIALOG_DATA) public itemid: number,
    private productService: ProductService) {
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  confirmDelete(): void {
    this.productService.deleteProduct(this.itemid).subscribe(
      () => {
      }, error => {
        console.log('Error ocurred while fetching product data : ', error);
      });
  }

  ngOnInit() {
    this.fetchProductData();
  }

  fetchProductData() {
    this.productData$ = this.productService.getProductById(this.itemid)
      .pipe(
        catchError(error => {
          console.log('Error ocurred while fetching product data : ', error);
          return EMPTY;
        }));
  }
}
