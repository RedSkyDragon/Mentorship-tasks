import { Injectable } from '@angular/core';
import { AuthenticationService } from '../authentication/authentication.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { History } from '../../models/history';

@Injectable({
  providedIn: 'root'
})
export class HistoryApiService {
  constructor(private authService: AuthenticationService, private http: HttpClient) { }

  public readonly baseUrl = 'http://localhost/ThingsBook.WebApi/';

  public getHistory(): Observable<History[]> {
    const url = this.baseUrl + 'history';
    return this.http.get<History[]>(url, { headers: this.createHeaders() });
  }

  public deleteHistory(Id: string): Observable<any> {
    const url = this.baseUrl + 'history/' + Id;
    return this.http.delete(url, { headers: this.createHeaders() });
  }

  private createHeaders(): HttpHeaders {
    return new HttpHeaders({
      'Authorization': 'Bearer ' + this.authService.accessToken,
      'ContentType': 'application/json'
    });
  }
}
