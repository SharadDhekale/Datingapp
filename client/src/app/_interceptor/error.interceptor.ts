import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse,
  HttpResponse
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { catchError } from 'rxjs/operators';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private router: Router, private toastrService: ToastrService) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError<any, any>(error => {
        if (error) {
          switch (error.status) {
            case 400:
              if (error.error.errors) {
                console.log( error.error.errors);
                var ErrorList = [];
                for (var key in error.error.errors) {
                  if (error.error.errors[key]) {
                    ErrorList.push(error.error.errors[key]);
                  }
                }
                throw ErrorList.flat();
              } else {
                console.log('coming in else')
                this.toastrService.error(error.statusText, error.status);
              }
              break;
            case 401:
              this.toastrService.error(error.statusText, error.status);
              break;
            case 404:
              this.router.navigateByUrl('/not-found');
              break;
            case 500:
              const navigateExtras: any = [{ status: error.error }];
              this.router.navigateByUrl('/server-error', navigateExtras);
              break;
            default:
              this.toastrService.error('Something went wrong!!!');
              console.log(error.error);
              break;
          }
        }
        return throwError(error);
      })
    )
  }
}
