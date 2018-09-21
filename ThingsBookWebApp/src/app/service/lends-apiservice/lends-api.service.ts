import { Injectable } from '@angular/core';
import { AuthenticationService } from '../authentication/authentication.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Lend } from '../../models/lend';
import { Observable, of } from 'rxjs';
import { ThingWithLend } from '../../models/thing-with-lend';
import { ActiveLend } from '../../models/active-lend';

@Injectable({
  providedIn: 'root'
})
export class LendsApiService {
  constructor(private authService: AuthenticationService, private http: HttpClient) { }

  private readonly baseUrl = 'http://localhost/ThingsBook.WebApi/';

  public addLend(thingId: string, lend: Lend): Observable<ThingWithLend> {
    const url = this.baseUrl + 'lend/' + thingId;
    return this.http.post<ThingWithLend>(url, lend, { headers: this.createHeaders() });
  }

  public updateLend(thingId: string, lend: Lend): Observable<ThingWithLend> {
    const url = this.baseUrl + 'lend/' + thingId;
    return this.http.put<ThingWithLend>(url, lend, { headers: this.createHeaders() });
  }

  public deleteLend(Id: string, returnDate: Date): Observable<any> {
    const url = this.baseUrl + 'lend/' + Id + '?returnDate=' + returnDate.toUTCString();
    return this.http.delete(url, { headers: this.createHeaders() });
  }

  public getLends(): Observable<ActiveLend[]> {
    const url = this.baseUrl + 'lends';
    return this.http.get<ActiveLend[]>(url, { headers: this.createHeaders() });
  }

  private createHeaders(): HttpHeaders {
    return new HttpHeaders({
      'Authorization': 'Bearer ' + this.authService.accessToken,
      'ContentType': 'application/json'
    });
  }
}
