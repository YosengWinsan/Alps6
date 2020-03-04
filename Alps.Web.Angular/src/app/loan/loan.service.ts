import { Injectable, Injector } from '@angular/core';
import { RepositoryService } from '../infrastructure/infrastructure.module';

@Injectable({
  providedIn: 'root'
})
export class LoanService extends RepositoryService {

  constructor(injector: Injector) {
    super(injector);
  }
  private setLender() {
    this.setBaseUrl("api/lenders");
  }
  private setLoanVoucher() {
    this.setBaseUrl("api/loanvoucher2s");
  }
  getLenders() {
    this.setLender();
    return this.getall();
  }
  getLender(id) {
    this.setLender();
    return this.get(id);
  }
  saveLender(lender) {
    this.setLender();
    return this.createAndUpdate(lender);
  }
  getLoanVouchersByHashCode(hashCode) {
    this.setLoanVoucher();
    return this.query("getByHashCode/" + hashCode);
  }
  getLoanVoucherInfo(id) {
    this.setLoanVoucher();
    return this.query("getloanvoucherinfo/" + id);
  }
  deposit(d) {
    this.setLoanVoucher();
    return this.action("deposit", d);
  }
  withdraw(v) {
    this.setLoanVoucher();
    return this.action("withdraw", v);
  }
  settleInterest(id) {
    this.setLoanVoucher();
    return this.action("settleInterest/" + id);
  }
  getWaterBills() {
    this.setLoanVoucher();
    return this.query("getWaterBills");
  }
  getPrintInfo(type, id) {
    this.setLoanVoucher();
    return this.action("getPrintInfo", { type: type, id: id });
  }
  invalidateLender(id: string) {
    this.setLender();
    return this.action("invalidate/" + id);
  }
  importLender(importstr:string){
    this.setLender();
    return this.action("importlender",importstr);
  }
  invalidvoucher(id:string){
    this.setLoanVoucher();
    return this.action("invalidvoucher/"+id);
  }

}
