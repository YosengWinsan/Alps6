import { Injectable, ComponentRef } from '@angular/core';
import { AlpsLoadingBarComponent } from './alps-loading-bar/alps-loading-bar.component';
import { Overlay, OverlayRef } from '@angular/cdk/overlay';
import { ComponentPortal } from '@angular/cdk/portal';

@Injectable({
  providedIn: 'root'
})
export class AlpsLoadingBarService {
  openCount = 0;
  overlayRef: OverlayRef;
  componentRef: ComponentRef<AlpsLoadingBarComponent>;
  constructor(private overlay: Overlay) { }
  open() {
    this.openCount++;
    if (!this.overlayRef) {
      var ps = this.overlay.position().global().centerHorizontally().centerVertically();
      this.overlayRef = this.overlay.create({ hasBackdrop: true, positionStrategy: ps });
      var loadingBarportal = new ComponentPortal(AlpsLoadingBarComponent);
      this.componentRef = this.overlayRef.attach(loadingBarportal);
    }
  }
  error() {
    if (!this.componentRef) {
      this.open();      
    }
    this.componentRef.instance.toggleError();
    
    this.overlayRef.backdropClick().subscribe(()=>this.close());
  }
  close() {
    this.openCount--;
    if (this.openCount == 0 && this.overlayRef) {
      this.componentRef.destroy();
      this.componentRef=null;
      this.overlayRef.dispose();
      this.overlayRef = null;

    }
  }

}
