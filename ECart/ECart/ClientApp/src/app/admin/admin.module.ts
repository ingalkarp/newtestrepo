import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

import { AdminRoutingModule } from './admin-routing.module';
import { ProductFormComponent } from '../components/admin/product-form/product-form.component';
import { ManageProductsComponent } from '../components/admin/manage-products/manage-products.component';
import { NgMaterialModule } from '../ng-material/ng-material.module';
import { DeleteProductComponent } from '../components/admin/delete-product/delete-product.component';

@NgModule({
  declarations: [
    ProductFormComponent,
    ManageProductsComponent,
    DeleteProductComponent
  ],
  imports: [
    CommonModule,
    AdminRoutingModule,
    ReactiveFormsModule,
    NgMaterialModule
  ],
})
export class AdminModule { }
