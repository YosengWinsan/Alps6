import { Injectable } from '@angular/core';
import { Observable, BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ShopcartService {

  private _shopcartID:string="";
  private _shopcart: IShopcartItem[] = [];
  //shopcart:Observable<any>;
  private shopcartSubject: BehaviorSubject<any>;
  constructor() {
    this._shopcart = this.getShopcart();
    this.shopcartSubject = new BehaviorSubject(this._shopcart);
  }
  get shopcart() {
    return this.shopcartSubject;
  }
  // notifyChangesTimer;
  notifyChanges() {
    // clearTimeout(this.notifyChangesTimer);
    //this.notifyChangesTimer = setTimeout(() => {
    this.saveShopcart();
    this.shopcart.next(this._shopcart);
    //}, 500);
  }
  add(item: Icommodity) {
    let cartItem = this._shopcart.find(p => p.productSkuID == item.id);
    let cartItemQuantity = cartItem ? cartItem.quantity : 0;
    if (item.sellableQuantity < item.quantityRate + cartItemQuantity) {
      alert("此商品库存不足");
      return;
    }
    if (!cartItem) {

      cartItem = { productSkuID: item.id, auxiliaryQuantity: 0, quantity: 0, price: item.listPrice, commodityName: item.commodityName, quantityRate: item.quantityRate, amount: 0 };
      this._shopcart.push(cartItem);
    }
    cartItem.auxiliaryQuantity++;
    cartItem.quantity += item.quantityRate;
    cartItem.amount = cartItem.quantity * cartItem.price;
    this.notifyChanges();
  }
  remove(item: IShopcartItem) {
    let cartItem = this._shopcart.find(p => p.productSkuID == item.productSkuID);
    if (cartItem) {
      cartItem.auxiliaryQuantity--;
      cartItem.quantity -= cartItem.quantityRate;
      cartItem.amount = cartItem.quantity * cartItem.price;
      if (cartItem.quantity == 0) {
        this._shopcart.splice(this._shopcart.indexOf(cartItem), 1);
      }
      this.notifyChanges();
    }

  }
  clear() {
    this._shopcart.splice(0, this._shopcart.length);
    this.notifyChanges();
  }
  private saveShopcart() {
    localStorage.setItem("_ALPS_SHOP_CART", JSON.stringify(this._shopcart));
  }
  private getShopcart() {
    let items = JSON.parse(localStorage.getItem("_ALPS_SHOP_CART"));
    return items ? items : [];
  }
  // private loadShopcart() {
  //   let items;
  //   if (this._shopcartID != "")
  //     items = JSON.parse(sessionStorage.getItem("_ALPS_SHOP_CART_" + this._shopcartID));
  //   else
  //     items = JSON.parse(sessionStorage.getItem("_ALPS_SHOP_CART"));
  //   return items ? items : [];
  // }
}
export interface Icommodity {
  id: string;
  commodityName: string;
  auxiliaryQuantity: number;
  quantity: number;
  listPrice: number;
  quantityRate: number;
  sellableQuantity: number;
  sellableAuxiliaryQuantity: number;
}
export interface IShopcartItem {
  productSkuID: string;
  commodityName: string;
  auxiliaryQuantity: number;
  quantity: number;
  price: number;
  quantityRate: number;
  amount: number;
}