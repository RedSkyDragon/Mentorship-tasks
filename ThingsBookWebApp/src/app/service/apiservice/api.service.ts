import { Injectable } from '@angular/core';
import { AuthenticationService } from '../authentication/authentication.service';
import { Observable,  of } from 'rxjs';
import { Category } from '../../models/category';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  constructor(private authService: AuthenticationService, private http: HttpClient) {
    this.headers = new HttpHeaders({
      'Authorization': 'Bearer ' + this.authService.accessToken,
      'ContentType': 'application/json'
    });
  }

  private readonly baseUrl = 'http://localhost/ThingsBook.WebApi/';

  private headers: HttpHeaders;

  public getCategories(): Observable<Category[]> {
    const url = this.baseUrl + 'categories';
    return this.http.get<Category[]>(url, { headers: this.headers })
      .pipe(
        catchError(this.handleError('getCategories', []))
      );
  }

  private handleError<T> (operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);
      return of(result as T);
    };
  }
}
