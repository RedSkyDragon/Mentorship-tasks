import { Injectable } from '@angular/core';
import { AuthenticationService } from '../authentication/authentication.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { Thing } from '../../models/thing';
import { catchError } from 'rxjs/operators';
import { ThingWithLend } from '../../models/thing-with-lend';
import { FilteredLends } from '../../models/filtered-lends';

@Injectable({
  providedIn: 'root'
})
export class ThingsApiService {
  constructor(private authService: AuthenticationService, private http: HttpClient) { }

  private readonly baseUrl = 'http://localhost/ThingsBook.WebApi/';

  public getThings(): Observable<ThingWithLend[]> {
    const url = this.baseUrl + 'things';
    return this.http.get<ThingWithLend[]>(url, { headers: this.createHeaders() })
    .pipe(
        catchError(this.handleError('getThings', []))
      );
  }

  public addThing(thing: Thing): Observable<ThingWithLend> {
    const url = this.baseUrl + 'thing';
    return this.http.post<ThingWithLend>(url, thing, { headers: this.createHeaders() })
    .pipe(
      catchError(this.handleError<ThingWithLend>('addThing'))
    );
  }

  public updateThing(Id: string, thing: Thing): Observable<ThingWithLend> {
    const url = this.baseUrl + 'thing/' + Id;
    return this.http.put<ThingWithLend>(url, thing, { headers: this.createHeaders() })
    .pipe(
      catchError(this.handleError<ThingWithLend>('updateThing'))
    );
  }

  public deleteThing(Id: string): Observable<any> {
    const url = this.baseUrl + 'thing/' + Id;
    return this.http.delete(url, { headers: this.createHeaders() })
    .pipe(
      catchError(this.handleError<any>('deleteThing'))
    );
  }

  public getThingLends(Id: string): Observable<FilteredLends> {
    const url = this.baseUrl + 'thing/' + Id + '/lends';
    return this.http.get(url, { headers: this.createHeaders() })
    .pipe(
      catchError(this.handleError<any>('getThingLends'))
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
