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
    this.setBaseUrl("api/loanvouchers");
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
    // return this.httpClient.post("settleInterest/" + id);
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

  invalidvoucher(id: string) {
    this.setLoanVoucher();
    return this.action("invalidvoucher/" + id);
  }
  invalidrecord(id: string) {
    this.setLoanVoucher();
    return this.action("invalidrecord/" + id);
  }
  getloanvoucherdetail(id: string) {
    this.setLoanVoucher();
    return this.get('getloanvoucherdetail/' + id);
  }
  getloansetting() {
    this.setLoanVoucher();
    return this.get('getloansetting');
  }
  saveloansetting(setting) {
    this.setLoanVoucher();
    return this.action('saveloansetting', setting);
  }
  // getloaninterestrates() {
  //   this.setLoanVoucher();
  //   return this.get('getloaninterestrates');
  // }
  // publishnewrate(sedate: Date, rate: Number) {
  //   this.setLoanVoucher();
  //   return this.action("publishnewrate", { startExecutionDate: sedate, rate: rate });
  // }
  getVoucherSummary() {
    this.setLoanVoucher();
    return this.get("getloanvouchersummary");
  }
  getinterestsummary(str) {
    this.setLoanVoucher();
    return this.action("getinterestsummary", { filter: str });
  }
  // getinterestbyName(){
  //   this.setLoanVoucher();
  //   return this.get("getinterestbyName");
  // }
  importLender(dto) {
    this.setLoanVoucher();
    return this.action("importlender", dto);
  }
  importvoucher(dto) {
    this.setLoanVoucher();
    return this.action("importvoucher", dto);
  }
  importdeposit(dto) {
    this.setLoanVoucher();
    return this.action("importdeposit", dto);
  }
  importwithdraw(dto) {
    this.setLoanVoucher();
    return this.action("importwithdraw", dto);
  }
  getinteresetdetal(hashcode) {
    this.setLoanVoucher();
    return this.get("getinteresetdetal/" + hashcode);
  }
  getSettleInteresetPrintInfo(id) {
    this.setLoanVoucher();
    return this.get("getSettleInteresetPrintInfo/" + id);
  }

  testCalculateInterest(dto) {
    this.setLoanVoucher();
    return this.action("testCalculateInterest", dto);
  }

  reviewer(recorderID) {
    this.setLoanVoucher();
    return this.action("reviewer/" + recorderID);
  }
  getNoReviewerRecorder() {
    this.setLoanVoucher();
    return this.get("getNoReviewerRecorder" );
  }
}
