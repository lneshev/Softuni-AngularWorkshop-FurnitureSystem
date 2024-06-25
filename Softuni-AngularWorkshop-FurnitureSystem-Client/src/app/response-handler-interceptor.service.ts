import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable, catchError, tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ResponseHandlerInterceptorService implements HttpInterceptor {
  constructor(private toastr: ToastrService, private router: Router) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(tap((success) => {
      if (success instanceof HttpResponse) {
        if (success.url?.endsWith('/login')) {
          this.toastr.success('You successfully logged in!', 'Success');
        }
      }
    }), catchError((error) => {
      if (error.status === 401) {
        this.toastr.error('You are not authorized. Please login again.', 'Error');
        this.router.navigate(['/signin']);
      }
      else {
        this.toastr.error(error.error?.message, 'Error');
      }
      throw error;
    }));
  }
}
