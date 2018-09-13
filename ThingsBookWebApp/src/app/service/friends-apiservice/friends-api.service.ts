import { Injectable } from '@angular/core';
import { AuthenticationService } from '../authentication/authentication.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { FilteredLends } from '../../models/filtered-lends';
import { Friend } from '../../models/friend';

@Injectable({
  providedIn: 'root'
})
export class FriendsApiService {
  constructor(private authService: AuthenticationService, private http: HttpClient) {
    this.headers = new HttpHeaders({
      'Authorization': 'Bearer ' + this.authService.accessToken,
      'ContentType': 'application/json'
    });
  }

  private readonly baseUrl = 'http://localhost/ThingsBook.WebApi/';

  private headers: HttpHeaders;

  public getFriends(): Observable<Friend[]> {
    const url = this.baseUrl + 'friends';
    return this.http.get<Friend[]>(url, { headers: this.headers })
      .pipe(
        catchError(this.handleError('getFriends', []))
      );
  }

  public addFriend(friend: Friend): Observable<Friend> {
    const url = this.baseUrl + 'friend';
    return this.http.post<Friend>(url, friend, { headers: this.headers })
    .pipe(
      catchError(this.handleError<Friend>('addFriend'))
    );
  }

  public updateFriend(Id: string, friend: Friend): Observable<Friend> {
    const url = this.baseUrl + 'friend/' + Id;
    return this.http.put<Friend>(url, friend, { headers: this.headers })
    .pipe(
      catchError(this.handleError<Friend>('updateFriend'))
    );
  }

  public deleteFriend(Id: string): Observable<any> {
    const url = this.baseUrl + 'friend/' + Id;
    return this.http.delete(url, { headers: this.headers })
    .pipe(
      catchError(this.handleError<any>('deleteFriend'))
    );
  }

  public getLends(Id: string): Observable<FilteredLends> {
    const url = this.baseUrl + 'friend/' + Id + '/lends';
    return this.http.get<FilteredLends>(url, { headers: this.headers })
      .pipe(
        catchError(this.handleError<any>('getThingsForFriend', []))
      );
  }

  private handleError<T> (operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);
      return of(result as T);
    };
  }
}
