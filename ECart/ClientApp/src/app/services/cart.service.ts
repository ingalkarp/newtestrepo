import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { ShoppingCart } from '../models/shoppingcart';

@Injectable({
  providedIn: 'root'
})
export class CartService {

  cartItemCount = 0;
  baseURL: string;

  constructor(private http: HttpClient) {
    this.baseURL = '/api/shoppingcart/';
  }

  addProductToCart(userId: number, itemId: number) {
    return this.http.post<number>(this.baseURL + `addToCart/${userId}/${itemId}`, {});
  }

  getCartItems(userId: number) {
    return this.http.get(this.baseURL + userId)
      .pipe(map((response: ShoppingCart[]) => {
        this.cartItemCount = response.length;
        return response;
      }));
  }

  removeCartItems(userId: number, itemId: number) {
    return this.http.delete<number>(this.baseURL + `${userId}/${itemId}`, {});
  }

  deleteOneCartItem(userId: number, itemId: number) {
    return this.http.put<number>(this.baseURL + `${userId}/${itemId}`, {});
  }

  clearCart(userId: number) {
    return this.http.delete<number>(this.baseURL + `${userId}`, {});
  }

  setCart(oldUserId: number, newUserId: number) {
    return this.http.get(this.baseURL + `setShoppingCart/${oldUserId}/${newUserId}`, {})
      .pipe(map((response: any) => {
        this.cartItemCount = response;
        return response;
      }));
  }
}
