import { Injectable } from '@angular/core';
import { AuthenticationService } from '../authentication/authentication.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { Thing } from '../../models/thing';
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
    return this.http.get<ThingWithLend[]>(url, { headers: this.createHeaders() });
  }

  public addThing(thing: Thing): Observable<ThingWithLend> {
    const url = this.baseUrl + 'thing';
    return this.http.post<ThingWithLend>(url, thing, { headers: this.createHeaders() });
  }

  public updateThing(Id: string, thing: Thing): Observable<ThingWithLend> {
    const url = this.baseUrl + 'thing/' + Id;
    return this.http.put<ThingWithLend>(url, thing, { headers: this.createHeaders() });
  }

  public deleteThing(Id: string): Observable<any> {
    const url = this.baseUrl + 'thing/' + Id;
    return this.http.delete(url, { headers: this.createHeaders() });
  }

  public getThingLends(Id: string): Observable<FilteredLends> {
    const url = this.baseUrl + 'thing/' + Id + '/lends';
    return this.http.get<FilteredLends>(url, { headers: this.createHeaders() });
  }

  private createHeaders(): HttpHeaders {
    return new HttpHeaders({
      'Authorization': 'Bearer ' + this.authService.accessToken,
      'ContentType': 'application/json'
    });
  }
}
