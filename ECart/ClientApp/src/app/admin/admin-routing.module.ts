import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ProductFormComponent } from '../components/admin/product-form/product-form.component';
import { ManageProductsComponent } from '../components/admin/manage-products/manage-products.component';

const adminRoutes: Routes = [
  {
    path: '',
    children: [
      { path: 'new', component: ProductFormComponent },
      { path: ':id', component: ProductFormComponent },
      { path: '', component: ManageProductsComponent },
    ]
  }
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(adminRoutes)
  ],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
