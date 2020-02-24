import { Injectable } from "@angular/core";
import { HttpInterceptor, HttpRequest, HttpHandler, HttpErrorResponse } from "@angular/common/http";
import { Observable, of } from "rxjs";
import { catchError, tap } from "rxjs/operators";
import { Router } from "@angular/router";
import { AuthService } from "./auth.service";

@Injectable({
    providedIn: 'root'
})
export class AuthHttpInterceptor implements HttpInterceptor {
    constructor(private router: Router, private authService: AuthService) { }
    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<any> {
        if (this.authService.tokenString != "")
            req = req.clone({ headers: req.headers.set("Authorization", "Bearer " + this.authService.tokenString) });
        return next.handle(req);
    }

}
