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

  public addCategory(category: Category): Observable<Category> {
    const url = this.baseUrl + 'category';
    return this.http.post<Category>(url, category, { headers: this.headers })
    .pipe(
      catchError(this.handleError<Category>('addCategory'))
    );
  }

  public updateCategory(Id: string, category: Category): Observable<Category> {
    const url = this.baseUrl + 'category/' + Id;
    return this.http.put<Category>(url, category, { headers: this.headers })
    .pipe(
      catchError(this.handleError<Category>('updateCategory'))
    );
  }

  public deleteCategory(Id: string): Observable<any> {
    const url = this.baseUrl + 'category/' + Id;
    return this.http.delete(url, { headers: this.headers })
    .pipe(
      catchError(this.handleError<any>('deleteCategory'))
    );
  }

  public deleteAndReplaceCategory(Id: string, replace: string): Observable<any> {
    const url = this.baseUrl + 'category/' + Id + '/replace?replacementId=' + replace;
    return this.http.delete(url, { headers: this.headers })
    .pipe(
      catchError(this.handleError<any>('deleteAndReplaceCategory'))
    );
  }

  public getThings(Id: string): Observable<ThingWithLend[]> {
    const url = this.baseUrl + 'category/' + Id + '/things';
    return this.http.get<ThingWithLend[]>(url, { headers: this.headers })
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
}
