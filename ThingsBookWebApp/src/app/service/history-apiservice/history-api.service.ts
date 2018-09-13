import { Injectable } from '@angular/core';
import { AuthenticationService } from '../authentication/authentication.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { History } from '../../models/history';

@Injectable({
  providedIn: 'root'
})
export class HistoryApiService {
  constructor(private authService: AuthenticationService, private http: HttpClient) {
    this.headers = new HttpHeaders({
      'Authorization': 'Bearer ' + this.authService.accessToken,
      'ContentType': 'application/json'
    });
  }

  private readonly baseUrl = 'http://localhost/ThingsBook.WebApi/';

  private headers: HttpHeaders;

  public getHistory(): Observable<History[]> {
    const url = this.baseUrl + 'history';
    return this.http.get<History[]>(url, { headers: this.headers })
      .pipe(
        catchError(this.handleError('getHistory', []))
      );
  }

  public deleteHistory(Id: string): Observable<any> {
    const url = this.baseUrl + 'history/' + Id;
    return this.http.delete(url, { headers: this.headers })
      .pipe(
        catchError(this.handleError<any>('deleteHistory', []))
      );
  }

  private handleError<T> (operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);
      return of(result as T);
    };
  }
}
