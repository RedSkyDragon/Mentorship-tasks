import { Injectable, ErrorHandler, Injector, NgZone } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';

@Injectable()
export class ErrorsHandler implements ErrorHandler {

    constructor(private injector: Injector) { }

    handleError(error: Error | HttpErrorResponse) {
        const router = this.injector.get(Router);
        if (error instanceof HttpErrorResponse) {
            if (!navigator.onLine) {
                router.navigate(['/error'], { queryParams: {error: 'No internet connection'} });
            } else {
                router.navigate(['/error'], { queryParams: {error: error.status + ' ' + error.statusText} });
            }
        } else {
            router.navigate(['/error'], { queryParams: {error: error} });
        }
        console.error('Error happens: ', error);
    }
}
