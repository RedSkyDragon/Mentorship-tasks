import { Injectable } from '@angular/core';
import { AuthenticationService } from '../authentication/authentication.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { FilteredLends } from '../../models/filtered-lends';
import { Friend } from '../../models/friend';

@Injectable({
  providedIn: 'root'
})
export class FriendsApiService {
  constructor(private authService: AuthenticationService, private http: HttpClient) { }

  private readonly baseUrl = 'http://localhost/ThingsBook.WebApi/';

  public getFriends(): Observable<Friend[]> {
    const url = this.baseUrl + 'friends';
    return this.http.get<Friend[]>(url, { headers: this.createHeaders() });
  }

  public addFriend(friend: Friend): Observable<Friend> {
    const url = this.baseUrl + 'friend';
    return this.http.post<Friend>(url, friend, { headers: this.createHeaders() });
  }

  public updateFriend(Id: string, friend: Friend): Observable<Friend> {
    const url = this.baseUrl + 'friend/' + Id;
    return this.http.put<Friend>(url, friend, { headers: this.createHeaders() });
  }

  public deleteFriend(Id: string): Observable<any> {
    const url = this.baseUrl + 'friend/' + Id;
    return this.http.delete(url, { headers: this.createHeaders() });
  }

  public getLends(Id: string): Observable<FilteredLends> {
    const url = this.baseUrl + 'friend/' + Id + '/lends';
    return this.http.get<FilteredLends>(url, { headers: this.createHeaders() });
  }

  private createHeaders(): HttpHeaders {
    return new HttpHeaders({
      'Authorization': 'Bearer ' + this.authService.accessToken,
      'ContentType': 'application/json'
    });
  }
}
