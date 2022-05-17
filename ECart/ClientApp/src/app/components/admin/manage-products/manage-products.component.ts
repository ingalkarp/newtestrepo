import { Component, OnInit, ViewChild, OnDestroy } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Product } from 'src/app/models/product';
import { ProductService } from 'src/app/services/product.service';
import { SnackbarService } from 'src/app/services/snackbar.service';
import { DeleteProductComponent } from '../delete-product/delete-product.component';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { Order } from '../../../models/order';
import { MyordersService } from '../../../services/myorders.service';

@Component({
  selector: 'app-manage-products',
  templateUrl: './manage-products.component.html',
  styleUrls: ['./manage-products.component.scss']
})
export class ManageProductsComponent implements OnInit, OnDestroy {

  displayedColumns: string[] = ['id', 'title', 'seller', 'category', 'price', 'operation'];
  //displayedOrderList: Order[]
  //isLoading: boolean

  dataSource = new MatTableDataSource<Product>();
  //dataSourceOrder = new MatTableDataSource<Order>();

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;

  private unsubscribe$ = new Subject<void>();
  constructor(
    private productService: ProductService,
    //private orderService: MyordersService,
    public dialog: MatDialog,
    private snackBarService: SnackbarService) {
  }

  ngOnInit() {
    this.getAllProductData();
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
    //this.getAllOrdersData();
  }

  getAllProductData() {
    this.productService.getAllProducts()
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe((data: Product[]) => {
        this.dataSource.data = Object.values(data);
      }, error => {
        console.log('Error ocurred while fetching product details : ', error);
      });
  }

  //getAllOrdersData() {
  //this.orderService.getAllOrders()
  //  //  .pipe(takeUntil(this.unsubscribe$))
  //  //  .subscribe((data: Order[]) => {
  //  //    this.dataSourceOrder.data = Object.values(data);
  //  //  }, error => {
  //  //    console.log('Error ocurred while fetching product details : ', error);
  //  //  });

  //  this.orderService.getAllOrders()
  //    .pipe(takeUntil(this.unsubscribe$))
  //    .subscribe((result: Order[]) => {
  //      if (result != null) {
  //        this.dataSourceOrder.data = Object.values(result);
  //        this.isLoading = false;
  //      }
  //    }, error => {
  //      console.log('Error ocurred while fetching my order details : ', error);
  //    });
  //}

  //getMyOrderDetails() {
    
  //}

  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  deleteConfirm(id: number): void {
    const dialogRef = this.dialog.open(DeleteProductComponent, {
      data: id
    });

    dialogRef.afterClosed()
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe(result => {
        if (result === 1) {
          this.getAllProductData();
          this.snackBarService.showSnackBar('Data deleted successfully');
        } else {
          this.snackBarService.showSnackBar('Error occurred!! Try again');
        }
      });
  }

  ngOnDestroy() {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }
}
