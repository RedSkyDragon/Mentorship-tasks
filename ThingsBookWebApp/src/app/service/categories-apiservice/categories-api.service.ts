import { Injectable } from '@angular/core';
import { AuthenticationService } from '../authentication/authentication.service';
import { Observable,  of } from 'rxjs';
import { Category } from '../../models/category';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { ThingWithLend } from '../../models/thing-with-lend';

@Injectable({
  providedIn: 'root'
})
export class CategoriesApiService {
  constructor(private authService: AuthenticationService, private http: HttpClient) { }

  private readonly baseUrl = 'http://localhost/ThingsBook.WebApi/';

  public getCategories(): Observable<Category[]> {
    const url = this.baseUrl + 'categories';
    return this.http.get<Category[]>(url, { headers: this.createHeaders() })
      .pipe(
        catchError(this.handleError('getCategories', []))
      );
  }

  public addCategory(category: Category): Observable<Category> {
    const url = this.baseUrl + 'category';
    return this.http.post<Category>(url, category, { headers: this.createHeaders() })
    .pipe(
      catchError(this.handleError<Category>('addCategory'))
    );
  }

  public updateCategory(Id: string, category: Category): Observable<Category> {
    const url = this.baseUrl + 'category/' + Id;
    return this.http.put<Category>(url, category, { headers: this.createHeaders() })
    .pipe(
      catchError(this.handleError<Category>('updateCategory'))
    );
  }

  public deleteCategory(Id: string): Observable<any> {
    const url = this.baseUrl + 'category/' + Id;
    return this.http.delete(url, { headers: this.createHeaders() })
    .pipe(
      catchError(this.handleError<any>('deleteCategory'))
    );
  }

  public deleteAndReplaceCategory(Id: string, replace: string): Observable<any> {
    const url = this.baseUrl + 'category/' + Id + '/replace?replacementId=' + replace;
    return this.http.delete(url, { headers: this.createHeaders() })
    .pipe(
      catchError(this.handleError<any>('deleteAndReplaceCategory'))
    );
  }

  public getThings(Id: string): Observable<ThingWithLend[]> {
    const url = this.baseUrl + 'category/' + Id + '/things';
    return this.http.get<ThingWithLend[]>(url, { headers: this.createHeaders() })
      .pipe(
        catchError(this.handleError<ThingWithLend[]>('getThingsForCategory', []))
      );
  }

  private handleError<T> (operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);
      return of(result as T);
    };
  }

  private createHeaders(): HttpHeaders {
    return new HttpHeaders({
      'Authorization': 'Bearer ' + this.authService.accessToken,
      'ContentType': 'application/json'
    });
  }
}
